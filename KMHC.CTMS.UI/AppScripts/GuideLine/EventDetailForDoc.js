var app = angular.module("EventDetailForDoc", []).controller("EventDetailForDocCtrl", [
    "$scope", "$http", "$location", "$routeParams", "UserApplyServices","doctorResultServices","ExamineReportServices","BaseTemplateResultServices","ExamineServices",
    function ($scope, $http, $location, $routeParams, userApplyServices, doctorResultServices, ExamineReportServices, baseTemplateResultServices, examineServices) {

        $scope.USERID = "";

        //病历所需要的资料信息
        /***Todo 虚拟字典 临时解决方案***/
        $scope.DicCancerType = [{ text: "未知", value: "0", ivalue: 0 }, { text: "疑似肿瘤", value: "1", ivalue: 1 }, { text: "肿瘤", value: "2", ivalue: 2 }];
        $scope.DicStage = [
            { text: "IA", value: "IA" },
             { text: "IB", value: "IB" },
            { text: "IIA", value: "IIA" },
            { text: "IIB", value: "IIB" },
            { text: "IIIB", value: "IIIB" },
            { text: "IV", value: "IV" }
        ];
        $scope.DicCytotype = [
            { text: "未知", value: "未知", DiseaseCode: "CTMS01.001" },
            { text: "燕麦细胞癌", value: "燕麦细胞癌", DiseaseCode: "CTMS01.001" },
            { text: "中间细胞癌", value: "中间细胞癌", DiseaseCode: "CTMS01.001" },
            { text: "混合燕麦细胞癌", value: "混合燕麦细胞癌", DiseaseCode: "CTMS01.001" },
            { text: "未知", value: "未知", DiseaseCode: "CTMS01.002" },
            { text: "鳞癌", value: "鳞癌", DiseaseCode: "CTMS01.002" },
            { text: "腺癌", value: "腺癌", DiseaseCode: "CTMS01.002" },
            { text: "腺鳞癌", value: "腺鳞癌", DiseaseCode: "CTMS01.002" },
            { text: "大细胞癌", value: "大细胞癌", DiseaseCode: "CTMS01.002" }
        ];
        $scope.DicGenotyping = [
            { text: "未知", value: "未知", DiseaseCode: "CTMS01.001" },
            { text: "未知", value: "未知", DiseaseCode: "CTMS01.002" },
            { text: "EGFR+", value: "EGFR+", DiseaseCode: "CTMS01.002" },
            { text: "ALK+", value: "ALK+", DiseaseCode: "CTMS01.002" },
            { text: "EGFR-ALK-", value: "EGFR-ALK-", DiseaseCode: "CTMS01.002" }
        ];
        $scope.DicTransfer = [{ text: "无", value: "无" }, { text: "脑转移", value: "脑转移" }, { text: "骨转移", value: "骨转移" }, { text: "其他器官", value: "其他器官" }];

        /***End***/

        $scope.DiseaseCodeChanged = function () {
            $scope.CytotypeList = $.grep($scope.DicCytotype, function (item) {
                return item.DiseaseCode == $scope.Model.ICDList[0].ICDCODE;
            });

            $scope.GenotypingList = $.grep($scope.DicGenotyping, function (item) {
                return item.DiseaseCode == $scope.Model.ICDList[0].ICDCODE;
            });
        }


        //实验室检查结果 查看   his传递  就诊史  id
        $scope.EditDoctorResult = function (obj) {
            //$location.url('/DoctorResultInfo?id=' + obj.LabresultId + '&uid=' + $routeParams.uid + '&hid=' + $routeParams.hid);
            $location.url('/DoctorResultInfo?id=' + obj.LabresultId + '&uid=' + $scope.USERID + '&hid=' + obj.HistoryId + '&isFlow=true&flowId=' + $routeParams.id + '&ApplyId=' + $routeParams.ApplyId);
        };

        //影像学检查结果 查看
        $scope.EditExamineReport = function (model) {
            $location.url("/ExamineReport?uid=" + $scope.USERID + "&examineNo=" + model.Id + "&hid=" + $scope.Hid + '&isFlow=true&flowId=' + $routeParams.id + '&ApplyId=' + $routeParams.ApplyId);
            //$location.url("/ExamineReport/" + $scope.USERID + "/" + model.Id + "/" + model.Hid + "/" + $routeParams.id + "/" + $routeParams.ApplyId);

        };


        //加载依据模板数据 & 答案
        $scope.LoadTemplateData = function (id) {
            examineServices.get({ templateCode: id }, function (data) {
                $scope.TemplateInfo = data.Data;
            });
            $.each($scope.BaseTemplateResultList, function (name, value) {
                if (value.EXAMINETEMPLATESID == id) {
                    if (value.RESULT != "") {
                        $scope.ReportDetail = JSON.parse(value.RESULT);
                    }
                }
            });
        };

        //对疾病ICD的选择操作
        var isMainICD = false;//是否主诉疾病
        $scope.InitICD10 = function (flag) {
            $scope.DiseaseCode = [];//选择的疾病编码
            $scope.DiseaseName = [];//选择的疾病名称
            isMainICD = flag;
            $('#modalAddHistory').modal('toggle');
        };

        $scope.RemoveICD = function (item) {
            for (var i = 0; i < $scope.Model.ICDList.length; i++) {
                var idx = $scope.Model.ICDList[i];

                if (idx.ICDCODE == item) {
                    $scope.Model.ICDList.splice(i, 1);
                    $scope.ChangeLCFQ();
                    break;
                }
            }
        }

        $scope.SaveICD = function () {
            if ($scope.DiseaseCode != null) {
                if (isMainICD) {
                    //判断添加的是否主诉疾病
                    for (var j = 0; j < $scope.Model.ICDList.length; j++) {
                        if ($scope.Model.ICDList[j].ISMAIN == true) {
                            alert("主诉疾病只能添加一种疾病");
                            return;
                        }
                    }
                    //alert("只能选择一种疾病!");
                    //return false;
                }
                var flag = true;
                for (var i = 0; i < $scope.Model.ICDList.length; i++) {
                    for (var j = 0; j < $scope.DiseaseCode.length; j++) {
                        if ($scope.Model.ICDList[i].ICDCODE == $scope.DiseaseCode[j]) {
                            flag = false;
                            alert("该项：" + $scope.Model.ICDList[i].ICDNAME + " 已添加");
                            return;
                        }
                    }
                }
            }
            for (var i = 0; i < $scope.DiseaseCode.length; i++) {
                $scope.Model.ICDList.push({
                    ICDCODE: $scope.DiseaseCode[0],
                    ICDNAME: $scope.DiseaseName[0],
                    DETAILS: "",
                    ISMAIN: isMainICD
                });
            };
            $scope.DiseaseCodeChanged();


            //保存到后台判断流程用
            $scope.SaveHistory();



            $('#ICD10Modal').modal('hide');

            //$scope.GetGuideLineSelect($scope.Model);
        };

        //$scope.NodeList = {};
        var guidLineNodeList = [];
        //当页面加载的时候，判断是否是 非小细胞肺癌 以及 第几期
        //$scope.GetGuideLineSelect = function (model) {
            //1.判断主诉疾病是否是 非小细胞肺癌
            //var flagDisease = false;
            //for (var i = 0; i < model.ICDList.length; i++) {
            //    if (model.ICDList[i].ISMAIN == true) {
            //        if (model.ICDList[i].ICDCODE.trim() == "CTMS01.002") {
            //            flagDisease = true;
            //            break;
            //        }
            //    }
            //}

            //if (flagDisease) {
            //    var data = {};
            //    $scope.NodeList = [];
            //    if (model.STAGE == "I") { //分期
            //        data = {
            //            "ID": "e1a506a0-66b7-44a5-a49d-221a649e22d0",
            //            "Code": "NSCLC手术治疗",
            //            "Name": "NSCLC手术治疗"
            //        }
            //        $scope.NodeList.push(data);
            //    } else if (model.STAGE == "IV") { //第四期
            //        data = {
            //            "ID": "6550679e-a8aa-43e0-8e49-7ab470afac07",
            //            "Code": "NSCLC化疗",
            //            "Name": "NSCLC化疗"
            //        }
            //        $scope.NodeList.push(data);
            //    } else {
            //        if (guidLineNodeList.length > 0) {
            //            $scope.NodeList = guidLineNodeList;
            //            return;
            //        }
            //    }
            //} else {
            //    if (guidLineNodeList.length>0) {
            //        $scope.NodeList = guidLineNodeList;
            //        return;
            //    }
            //}
        //};

        //临床分期选项
        $scope.ChangeLCFQ = function () {
            //$scope.GetGuideLineSelect($scope.Model);
        }


        $scope.TestFlag = false;
        $scope.TestFlagss = function (i) {
            if (i==0) {
                return false;
            }
            return true;
        }

        $scope.LoadBaseTemplateResult = []; //用于显示所有依据模板

        //加载就诊史信息
        $scope.LoadRecord = function (model) {

            if (model != null) {

                //测试用
                $scope.GetTemplateList = function() {
                    baseTemplateResultServices.get({ ID: model.DiseaseInfoId }, function(obj) {
                        if (obj) {
                            $scope.BaseTemplateResultList = obj.Data;
                            //console.log(JSON.stringify(obj.Data));

                            $.each($scope.BaseTemplateResultList, function(name, value) {
                                
                                var id = value.EXAMINETEMPLATESID;

                                var templateInfo = {};

                                examineServices.get({ templateCode: id }, function(data) {
                                    $scope.TemplateInfo = data.Data;
                                    templateInfo = data.Data;
                                    $scope.ReportDetail = JSON.parse(obj.Data[0].RESULT);

                                    //console.log(JSON.stringify(templateInfo));
                                    //$.each($scope.BaseTemplateResultList, function (name, value) {
                                    //    if (value.EXAMINETEMPLATESID == id) {
                                    //        if (value.RESULT != "") {
                                    //            $scope.ReportDetail = JSON.parse(value.RESULT);
                                    //            console.log($scope.ReportDetail);
                                    //        }
                                    //    }
                                    var item = {
                                        Name: value.Name,
                                        TemplateInfo: templateInfo,
                                        ReportDetail: JSON.parse(value.RESULT)
                                    };
                                    //console.log(item.TemplateInfo.length);
                                    $scope.LoadBaseTemplateResult.push(item);
                                });
                            });

                            //console.log(JSON.stringify($scope.LoadBaseTemplateResult));
                            //$scope.LoadTemplateData(obj.Data[0].EXAMINETEMPLATESID);
                            //console.log(JSON.stringify($scope.TemplateInfo));
                            //});
                        }
                    });
                };
                $scope.GetTemplateList();

                //加载依据模板列表
                //$scope.GetTemplateList = function () {
                //    baseTemplateResultServices.get({ ID: model.DiseaseInfoId }, function (obj) {
                //        if (obj) {
                //            $scope.BaseTemplateResultList = obj.Data;

                //            console.log(JSON.stringify(obj.Data));

                //            $scope.LoadTemplateData(obj.Data[0].EXAMINETEMPLATESID);
                //        }
                //    });
                //};
                //$scope.GetTemplateList();


                //加载基本信息
                $http.get('/api/EMRInfo?historyId=' + model.DiseaseInfoId).success(function(obj) {
                        if (obj != null) {
                            $scope.Model = obj.Data;
                            //$scope.GetGuideLineSelect(obj.Data);

                            if ($scope.Model.ICDList != null && $scope.Model.ICDList.length > 0) {
                                $scope.DiseaseCodeChanged();
                            }
                        }
                    }
                );

                doctorResultServices.get({ CurrentPage: "1", Hid: model.DiseaseInfoId }, function(data) {
                    $scope.DoctorResultList = data.Data;
                    //console.log(JSON.stringify(data.Data));
                });

                //影像学检查结果
                ExamineReportServices.get({ examineNo: '', userId: model.USERID }, function(data) {
                    $scope.ExamineReportList = data.Data;
                });

                $http.get('/api/Upload?ModelCode=SeeDoctorHistory&FormID=' + model.DiseaseInfoId).success(function(obj) {
                    if (obj != null) {
                        $scope.Files = obj;
                    }
                });
            }
        };

        $scope.ApplyId = $routeParams.ApplyId;
        $scope.Id = $routeParams.id;
        //临时用
        $scope.Item = {};
        userApplyServices.get({ ID: $scope.ApplyId }, function (obj) {
            if (obj) {
                $scope.Item = obj.Data;

                if ($scope.Item.STATUS == "3") { //已完成状态
                    $("#btnSave").hide();
                    $("#DOCTORSUGGEST").attr("readonly", "readonly");
                    $("#GuideLineFlow").attr("readonly", "readonly");
                    $("#NextGuideLine").hide();
                    $("#btnClosFlow").hide();
                } else {
                    ///当为医生编辑的时候，上一次的医生建议需要清除 --20160126
                    $scope.Item.DOCTORSUGGEST = ""; 

                    if ($scope.Item.NextCurrentNode == null) {
                        $scope.Item.NextCurrentNode = "";
                    }
                    $scope.USERID = obj.Data.USERID;
                    obj.Data.CURRENTNODE = obj.Data.CURRENTNODE == null ? "" : obj.Data.CURRENTNODE;
                }

                $http.get("/GuideLine/GetGuideLineChild?guidelineCode=" + obj.Data.CURRENTNODE).success(
                    function(obj) {
                        guidLineNodeList = obj;
                        $scope.NodeList = guidLineNodeList;
                    }).error(function(errorResponse) {
                    alert("发生错误,查询失败!");
                    console.log("$http.get.UserManage:" + errorResponse.data.Message);
                });
                $scope.LoadRecord(obj.Data);
            }
        });

        //临时跳转去详情页面
        //$scope.JumpTo = function() {
        //    window.location = "/#/LungCancerDiag";
        //};

        $scope.SaveItem = function () {
            //if ($scope.Item.NextCurrentNode == "") {
            //    alert("需要选择下一节点");
            //    return;
            //}
            $scope.SaveHistory();

            //尝试保存信息  医生更新信息
            $http.post('/Api/EMRInfo', { Data: $scope.Model }).success(function (obj) {
                if (obj.Data) {

                    $scope.Item.EventID = $scope.Id;
                    var node = $scope.Item.NextCurrentNode;
                    $scope.Item.NextCurrentNode = node.split(",")[0];
                    $scope.Item.NextCurrentNodeName = node.split(",")[1];   

                    userApplyServices.save({ Data: $scope.Item }, function(obj) {
                        window.location = "/#/DoTreatment";
                    });
                } else
                    alert('网络异常');
            });

        };

        $scope.GoBack = function() {
            window.location = "/#/DoTreatment";
        };

        $scope.CloseEvent = function() {
            if (confirm("是否结束该流程？")) {
                $http.get("/GuideLine/CloseEvent?eventId=" + $scope.Id + "&applyId=" + $scope.ApplyId).success(
                    function(obj) {
                        window.location = "/#/DoTreatment";
                    }).error(function(errorResponse) {
                    alert("发生错误,查询失败!");
                    console.log("$http.get.UserManage:" + errorResponse.data.Message);
                });
            }
        };

        $scope.SaveHistory = function () {

            //console.log($scope.Model.T);


            //$scope.fenqi = "dfa";


            //$http.post('/Api/EMRInfo', { Data: $scope.Model }).success(function (obj) {
            //    if (obj.Data) {
            //        if (obj.Data == true) { //执行成功
            //            //需要请求后台的guideline数据
            //        }
            //    } else
            //        alert('网络异常');
            //});
        };
    }
]);