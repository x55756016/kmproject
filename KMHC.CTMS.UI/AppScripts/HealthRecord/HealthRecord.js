var app = angular.module("userApp.controllers", []);

app.controller("createRecordCtrl", [
    "$scope", "$http", "$routeParams", "$location", "cancerUserServices", "healthrecordsServices", "optionsServices", "dictionary",
    function ($scope, $http, $routeParams, $location, cancerUserServices, healthrecordsServices, optionsServices, dictionary) {
        function setCode(ids, _callback) {
            optionsServices.query({ id: encodeURI(ids) }, function (data) {
                _callback(data);
            });
        }
        $scope.Dic = {};
        $scope.Data = {};
        dictionary.dictionary(function (dic) {
            $scope.Dic = dic;
            for (var name in $scope.Dic) {
                $scope.Data[name] = $scope.Dic[name][0].Value;
            }
            $scope.Data.IDTypeName = $scope.Dic.IDType[0].Name;
        });
        $scope.Init = function () {
            $http.get('/api/cancerUser?id=' + $routeParams.uid).success(function (data) {
                if (data != null && data.PERSONID != null && data.PERSONID.length > 0) {
                    healthrecordsServices.get({ ID: data.PERSONID }
                    , function (data) {
                        $scope.Data = data;
                        $scope.Data.IDTypeName = "居民身份证";
                    }
                    , function (errResponse) {
                        $scope.HandleError(errResponse);
                        $scope.LoadDic();
                    }); 
                }
                else
                {
                    $scope.LoadDic();
                    if (data == null || data.IDCARD == null || data.IDCARD.length == 0) {
                        $scope.Data.IDNumber = data.IDCARD;
                        $scope.Data.IDTypeName = "居民身份证";
                    }
                    if (data == null || data.Name == null || data.Name.length == 0) {
                        $scope.Data.Name = data.NAME;
                    }
                }
            });
        };
        $scope.Init();
        $scope.LoadDic = function () {
            dictionary.dictionary(function (dic) {
                $scope.Dic = dic;
                for (var name in $scope.Dic) {
                    $scope.Data[name] = $scope.Dic[name][0].Value;
                }
                $scope.Data.IDTypeName = $scope.Dic.IDType[0].Name;
            });
        };
        
        $scope.Dic.ResponsibleOrganization = [{ Value: "2", Name: "康美医院" }, { Value: "1", Name: "普宁市人民医院" }];

        $scope.Data.RHType = "1";
        $scope.Data.ResponsibleOrganizationID = $scope.Dic.ResponsibleOrganization[0].Value;
        $scope.Data.ResponsibleOrganization = $scope.Dic.ResponsibleOrganization[0].Name;

        $scope.goto = function (element) {
            document.body.scrollTop = $("#" + element).position().top;
        };
        $scope.ResponsibleOrganizationChange = function () {
            $scope.Data.ResponsibleOrganization = $(event.target).text();
        };

        $scope.IDTypeChange = function () {
            var target = $(event.target);
            $scope.Data.IDType = target.attr("Value");
            $scope.Data.IDTypeName = target.text();
        };
        $scope.save = function () {
            healthrecordsServices.save({ Data: $scope.Data, Keyword: $routeParams.uid }
                , function (response) {
                    alert("保存成功!");
                    $location.path("CancerRecord");
                }
                , function (errResponse) {
                    $scope.HandleError(errResponse);
                });
        };
        $scope.Cancel = function () {
            $location.path("CancerRecord");
        };
        //处理Api访问时出错
        $scope.HandleError = function (errResponse) {
            if (errResponse != null && errResponse.data != null && errRespose.data.Message != null) {
                alert("发生错误!");
                console.log("createRecordCtrl:" + errRespose.data.Message);
            }
        };
    }
]);

app.controller("historyDiseaseCtr", [
    "$scope", "$http", "$routeParams", "$location", "optionsServices", "historyDiseaseServices","dictionary",
    function ($scope, $http, $routeParams, $location, optionsServices, historyDiseaseServices, dictionary) {
        //$scope.Areas = { ProvinceId: "320000", CityId: "320100", CountryId: "320102", TownId: "32010201",Address:"test" }
        //$scope.Areas = { ProvinceId: "110000", CityId: "110100", CountyId: "110101", TownId: "" }
        //$scope.Areas = { ProvinceId: "140000", CityId: "", CountyId: "", TownId: "" }
        $scope.Data = {};
        $scope.Dic = { Types: [{ Value: 1, Name: "疾病" }, { Value: 2, Name: "手术" }, { Value: 3, Name: "外伤" }, { Value: 4, Name: "输血" }] };
        $scope.Data.RecordId = $routeParams.RecordId;

        dictionary.dictionary(function (dic) {
            $scope.Dic.Disease = dic.Disease;
            $scope.Data.Disease = $scope.Dic.Disease[0].Value;
        });


        $scope.txt_RecordIdChang = function (RecordId) {
            $scope.Search(RecordId);
        }

        $scope.Search = function (RecordId) {
            if (RecordId > 0) {
                $scope.Data.RecordId = RecordId;
                historyDiseaseServices.get({ RecordId: RecordId,Keyword:encodeURI("1,2,3,4") }, function (data) {
                    $scope.Historys = data.Data;
                });
            }
        };

        $scope.$on("userInfoLoadSuccess", function(event, data) {
            if (data.Data.length > 0) {
                $scope.Data.RecordId = data.Data[0].ID;
                $scope.Search($scope.Data.RecordId);
            } else {
                $scope.Historys = [];
            }
        });

        $scope.Remove = function (id) {
            if (confirm("您确定删除该条记录吗？")) {
                historyDiseaseServices.remove({ ID: id }, function () {
                    $scope.Search($scope.Data.RecordId);
                });
            }
        }

        $scope.Save = function (RecordId) {
            historyDiseaseServices.save((function () {
                var Data = $scope.Data;
                var postData = {};
                postData.RecordId = RecordId;
                postData.Type = Data.Type;
                postData.Name = Data.Name;
                postData.Code = "0";
                postData.StartDate = Data.StartDate;
                if (!Data.EndDate) {
                    postData.EndDate = Data.StartDate;
                }
                if (Data.Type == 1) {
                    postData.Code = Data.Value;
                    for (var i = 0; i < $scope.Dic.Disease.length; i++) {
                        if ($scope.Dic.Disease[i].Value == postData.Code) {
                            postData.Name = $scope.Dic.Disease[i].Name;
                            break;
                        }
                    }
                }
                return { Data: postData };
            })(), function (response) {
                alert("添加成功!");
                $scope.Search(RecordId);
                $scope.Data = { RecordId: RecordId, Operation: "", OuterInjuery: "", Disease: 0, BloodTrans: "", StartDate: "" }
                window.location.href = "#/HistoryDisease";
            });
        }
    }
]);


app.controller("basicInfoCtr", [
    "$scope", "$http","$timeout", "healthrecordsServices", function ($scope, $http,$timeout, healthrecordsServices) {
        $scope.Data = {};

        function EventLoad(RecordId) {
            if (RecordId > 0) {
                healthrecordsServices.get({ RecordId: RecordId }, function (data) {
                    if (data.Data.length > 0) {
                        $scope.Data = data.Data[0];
                    } else {
                        $scope.Data.Name = "";
                        $scope.Data.Gender = "";
                        $scope.Data.BirthDate = "";
                        $scope.Data.Phone = "";
                        $scope.Data.IDNumber = "";
                        $scope.Data.MarriageStatus = "";
                    }
                    $scope.$emit("userInfoLoadSuccess", data);
                });
            }
        }

        $scope.timer = {};
        //通过timeout 在输入停止400ms后自动从后台请求数据
        $scope.txt_RecordNoChang = function (RecordId) {
            $timeout.cancel($scope.timer);//此处移除上一次的定时器
            $scope.timer = $timeout(function () {
                EventLoad(RecordId);
            },400);
        };
    }
]);

/* modify by Bruce Zhang 20151014
app.controller("familyDiseaseHistoryCtr", [
    "$scope", "$http", "$routeParams", "$location", "optionsServices", "historyDiseaseServices", "dictionary",
    function($scope, $http, $routeParams, $location, optionsServices, historyDiseaseServices, dictionary) {
        $scope.Data = {};
        $scope.Dic = {};
        $scope.Data.RecordId = $routeParams.RecordId;
        dictionary.dictionary(function (dic) {
            $scope.Dic.Disease = dic.Disease;
            $scope.Data.Disease = $scope.Dic.Disease[0].Value;
        });

        $scope.SaveFamily = function (RecordId) {
            var checkseds = $(":checked[value!='01']");
            var postDataList = [];
            if (checkseds.length > 0) {
                var checksed;
                for (var i = 0; i < checkseds.length; i++) {
                    checksed = checkseds[i];
                    var tempData = {
                        RecordId: RecordId, Type: checksed.name, Code: checksed.value,
                        Name: $(checksed).parent().text(), StartDate: 0, EndDate: 0
                    };
                    postDataList.push(tempData);
                }
                historyDiseaseServices.save({ DataList: postDataList }, function(data) {
                    alert("保存成功");
                });
            }
        }
        $scope.Search = function(RecordId) {
            historyDiseaseServices.get({ RecordId: RecordId, Keyword: "11,12,13,14" }, function (data) {
                $scope.FamilyData = data.Data;
                for (var i = 0; i < $scope.FamilyData.length; i++) {
                    $(":checkbox[name=" + $scope.FamilyData[i].Type + "][value='" + $scope.FamilyData[i].Code + "']")[0].checked = true;
                }
            });
        }

        $scope.$on("userInfoLoadSuccess", function (event, data) {
            $(":checkbox").each(function () { this.checked = false; });
            if (data.Data.length > 0) {
                $scope.Data.RecordId = data.Data[0].ID;
                $scope.Search($scope.Data.RecordId);
            } 
        });

    }
]);
*/
app.filter("BySide", function () {
    return function (family, side) {
        var arr = [];
        for (var i = 0; i < family.length; i++) {
            if (family[i].RelationShip.Side == side) {
                arr.push(family[i]);
            }
        }
        return arr;
    };
});
app.controller("familyDiseaseHistoryCtr", [
    "$scope", "$http", "$routeParams", "$location", "optionsServices", "historyDiseaseServices", "familyRelationServices","familyDiseaseServices","familyDiseaseMemberServices",
    function ($scope, $http, $routeParams, $location, optionsServices, historyDiseaseServices, familyRelationServices,familyDiseaseServices,familyDiseaseMemberServices) {
       
        var dicDis = [
            { Name: "糖尿病", Code: "xx001" },
            { Name: "肺癌", Code: "xx002" },
            { Name: "高血压", Code: "xx003" }
        ];


        var model = {
            FamilyDiseaseHistiorys: [
                { RecordId: "0001", RelationShip: "51", DiseaseCode: "xx001", DiseaseName: "糖尿病", AttackDate: '2000-05-05', IsInfectious: false, IsTwins: false, DiagnosisAge: 46, TreatmentState: 1, Treatment: "111" },
                { RecordId: "0003", RelationShip: "52", DiseaseCode: "xx003", DiseaseName: "高血压", AttackDate: '2008-05-05', IsInfectious: true, IsTwins: false, DiagnosisAge: 42, TreatmentState: 1, Treatment: "" },
                { RecordId: "0002", RelationShip: "51", DiseaseCode: "xx002", DiseaseName: "肺癌", AttackDate: '2008-05-05', IsInfectious: true, IsTwins: false, DiagnosisAge: 46, TreatmentState: 2, Treatment: "", TreatmentStatDate: "2010-03-01" }
            ]
        }
        $scope.BindFamily = function () {
            familyDiseaseMemberServices.get({ Keyword: $scope.Uid }, function (data) {
                $scope.Data.Family = data.Data;
            });
        }

        $scope.Init = function () {
            //获取uid
            $scope.Uid = $routeParams.uid;
            $scope.BindFamily();

            //获取家族关系
            familyRelationServices.get({}, function (data) {
                $scope.Data.DicFamilyRS = data.Data;
                $scope.Data.FamilyInit = $.grep($scope.Data.DicFamilyRS, function (e) { return e.Apply == false; });
            });

            $scope.DiseaseCode = [];
            $scope.DiseaseName = [];
        }
        $scope.Init();
        
        $scope.Data = model;
        $scope.Data.DicDisease = dicDis;
        $scope.GetGUID = function () { return new Date().getTime(); }
        //$scope.Data.FamilyInit = eval("[" +
        //    "{ ID: \"" + $scope.GetGUID() + "\", Text: \"父亲\", Side: \"本人\", Value: \"51\", Name: null, canDelete: false, Sex:\"1\", IsTwins: null,IsIdentical: null,Diseases:[] }," +
        //    "{ ID: \"" + $scope.GetGUID() + "\", Text: \"母亲\", Side: \"本人\", Value: \"52\", Name: null, canDelete: false, Sex:\"2\", IsTwins: null,IsIdentical: null,Diseases:[] }," +
        //    "{ ID: \"" + $scope.GetGUID() + "\", Text: \"祖父\", Side: \"父亲\", Value: \"61\", Name: null, canDelete: false, Sex:\"1\", IsTwins: null,IsIdentical: null,Diseases:[] }," +
        //    "{ ID: \"" + $scope.GetGUID() + "\", Text: \"祖母\", Side: \"父亲\", Value: \"62\", Name: null, canDelete: false, Sex:\"2\", IsTwins: null,IsIdentical: null,Diseases:[] }," +
        //    "{ ID: \"" + $scope.GetGUID() + "\", Text: \"外祖父\", Side: \"母亲\", Value: \"63\", Name: null, canDelete: false, Sex:\"1\",IsTwins: null,IsIdentical: null,Diseases:[] }," +
        //    "{ ID: \"" + $scope.GetGUID() + "\", Text: \"外祖母\", Side: \"母亲\", Value: \"64\", Name: null, canDelete: false, Sex:\"2\",IsTwins: null,IsIdentical: null,Diseases:[] }]");
        $scope.Data.Family = [
        ];

        $scope.AddFamily = function () {
            $('#addFamilyModal').modal('toggle');
        };
        $scope.SaveFamily = function () {
            var FamilyMembers = [];
            Array.prototype.push.apply(FamilyMembers, $scope.Data.FamilyInit);
            angular.forEach($scope.Data.DicFamilyRS, function (f) {
                if (f.Apply) {
                    for (var i = 0; i < parseInt(f.MemberCount, 10) ; i++) {
                        FamilyMembers.push(eval("({ ID:'" + f.ID + "', Text: '" + f.Text + "', Value: '" + f.Value + "',Generate:'" + f.Generate + "', Side:'" + f.Side + "', Requird: true" + ",Apply:" + f.Apply + ",Sort:" + f.Sort + ",Sex:'" + f.Sex + "' })"));
                    }
                }
            });
            //保存家族组成员
            familyDiseaseMemberServices.save({ Keyword: $scope.Uid, Data: FamilyMembers }, function (data)
            {
                $scope.Data.Family = data.Data;
                $('#addFamilyModal').modal('toggle');
            })
           
        };
        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
        $scope.AddMember = function () {
            $scope.CRUDMember = { Diseases: [], IsAlive: true };
            //$("#rs1").combobox();
            //$("#rs2").combobox();
            $('#editMemberModal').modal('toggle');
        };

        $scope.EditMember = function (m) {
            $scope.CRUDMember = $scope.Clone(m);
            if ($scope.CRUDMember.Diseases == null) $scope.CRUDMember.Diseases = [];
            //if ($scope.CRUDMember.Diseases.length <= 0)
            //{
            //    $scope.CRUDMember.Diseases.push({});
            //}
            $('#editMemberModal').modal('toggle')
        };
        $scope.DeleteMember = function (m) {
            if (confirm('确定删除？')){
                familyDiseaseMemberServices.remove({ ID: m.ID }, function (data) {
                    $scope.BindFamily();
                });
            }
        };
        
        $scope.SaveMember = function (m) {
            familyDiseaseServices.save({Keyword:$scope.Uid, Data: $scope.CRUDMember}, function (data) {
                $scope.BindFamily();
                $('#editMemberModal').modal('toggle');
            });
        }

        $scope.AddDisease = function () {
            $scope.CRUDMember.Diseases.push({});
        };

        $scope.ShowDiseasePicker = function (idx)
        {
            $scope.DiseaseCode = [];
            $scope.DiseaseName = [];
            $scope.CurrentDiseaseIndex = idx;
            $("#DiseaseModal").modal("show");
        }
        $scope.SaveDisease = function () {
            if ($scope.DiseaseCode.length > 1) {
                alert("只能选择一种疾病!")
            }
            else
            {
                $scope.CRUDMember.Diseases[$scope.CurrentDiseaseIndex].DiseaseCode = $scope.DiseaseCode[0];
                $scope.CRUDMember.Diseases[$scope.CurrentDiseaseIndex].DiseaseName = $scope.DiseaseName[0];
                $("#DiseaseModal").modal("hide");
            }
        }
        $scope.Save = function () {
            if ($scope.CURDHis != null && $scope.CURDHis.DiseaseCode != null) {
                $scope.CURDHis.DiseaseName = $.grep($scope.Data.DicDisease, function (item, index) { return item.Code == $scope.CURDHis.DiseaseCode })[0].Name;
            }
            if ($scope.CURDHis.RecordId == null) {
                $scope.CURDHis.RecordId = new Date().getTime();;
                $scope.Data.FamilyDiseaseHistiorys.push($scope.CURDHis);
            }
            else {
                //debugger;
                for (var i = 0; i < $scope.Data.FamilyDiseaseHistiorys.length; i++) {
                    if ($scope.Data.FamilyDiseaseHistiorys[i].RecordId == $scope.CURDHis.RecordId) {
                        $scope.Data.FamilyDiseaseHistiorys[i] = $scope.CURDHis;
                        break;
                    }
                }
            }
            $('#myModal').modal('toggle');
        }

        $scope.Delete = function (arr, obj) {
            arr.splice(arr.indexOf(obj), 1);
        };
        $scope.Reback = function () {
            $location.url("CancerRecord");
        };
        $scope.SelectRelation = function () {
            if ($scope.CRUDMember.RelationID != '')
            {
                var arr =  $.grep($scope.Data.DicFamilyRS, function (e) { return e.ID == $scope.CRUDMember.RelationID;});
                if (arr != null && arr.length >= 1)
                {

                        var relation=$scope.Clone(arr[0]);
                        $scope.CRUDMember.Sex = relation.Sex;
                        $scope.CRUDMember.RelationShip = relation;
                }
            }
        }
        
    }
]);

app.controller("healthRecordCtr", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "optionsServices", "healthrecordsServices",
    function ($scope, $http,$timeout, $routeParams, $location, optionsServices, healthrecordsServices) {
        $scope.Data = {};
        $scope.txt_RecordNoChang = function () {
            $timeout.cancel($scope.timer);//此处移除上一次的定时器
            $scope.timer = $timeout(function () {
                if ($scope.Data.IDNumber.length > 0) {
                    //healthrecordsServices.get({ IDNumber: $scope.Data.IDNumber }, function (data) {
                    //    $scope.Data.Result = data;
                    //});
                }
            }, 400);
        };
        $scope.improtClick= function() {
            window.open('/HealthManage/PdFileContentResult/?IDNumber='+$scope.Data.IDNumber);
        }
    }
]);


app.controller("highBloodManageCreateCtr", [
    "$scope", "$http", "$routeParams", "$location", "optionsServices", "historyDiseaseServices", "dictionary","utility",
    function ($scope, $http, $routeParams, $location, optionsServices, historyDiseaseServices, dictionary, utility) {
        var xml = "<Values><Value>1</Value><Value>2</Value></Values>";
        console.dir(utility.XmlStrToJson(xml));
    }
]);