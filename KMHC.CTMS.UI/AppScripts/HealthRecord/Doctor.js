var app = angular.module('doctor', []);

app.controller('doctorCtrl', ['$scope', '$http', '$filter', '$location', '$routeParams', 'doctorServices',
    function ($scope, $http, $filter, $location, $routeParams, doctorServices) {

        //查询数据
        $scope.Infos = [];
        $scope.CurrentPage = 1;
        $scope.Search = function () {
            doctorServices.get({ Uid: $routeParams.uid, Keyword: $scope.s_keyword, CurrentPage: $scope.CurrentPage }, function (data) {
                $scope.Infos = data.Data;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            });
        };

        //新增按钮事件弹出新增页面
        $scope.AddHistory = function () {
            $scope.Info = {};
            $scope.Info.PERSONID = $routeParams.uid;
            $scope.DiseaseCode = [];//选择的疾病编码
            $scope.DiseaseName = [];//选择的疾病名称
            $('#modalAddHistory').modal('toggle');
        };

        //编辑记录
        $scope.Edit = function (obj) {
            $scope.Info = {};
            $.extend($scope.Info, obj);
            $scope.Info.DIAGNOSISTIME = $filter('date')($scope.Info.DIAGNOSISTIME, 'yyyy-MM-dd');

            $scope.DiseaseCode = obj.ICD10 == '' ? [] : obj.ICD10.split(',');
            $scope.DiseaseName = obj.ICDList == '' ? [] : obj.ICDList.split(',');
            $('#modalAddHistory').modal('toggle');
        };

        //新增保存实体
        $scope.SaveInfo = function () {
            if ($scope.DiseaseCode.length == 0 || $scope.DiseaseName.length == 0) {
                alert('请先选择疾病');
                return;
            }

            var icd10 = '';
            for (var i in $scope.DiseaseCode) {
                icd10 += $scope.DiseaseCode[i] + '#' + $scope.DiseaseName[i] + ',';
            }
            $scope.Info.ICD10 = icd10;
            doctorServices.save({ Data: $scope.Info }, function (response) {
                alert("保存成功!");
                $('#modalAddHistory').modal('toggle');
                $scope.Search();
            });
        };

        //编辑实验室检查结果
        $scope.EditResult = function (u) {
            $location.url("DoctorResult?hid=" + u.HISTORYID + "&uid=" + $routeParams.uid);
        };

        //编辑影像学检查结果
        $scope.EditImageEx = function (u) {
            //$location.url("ImageEx?hid=" + u.HISTORYID + "&uid=" + $routeParams.uid);
            $location.url("/ExamineReportList/" + $routeParams.uid);
        };

        //电子病历信息补充
        $scope.EditEMR = function (u) {
            $location.url("DiseaseInfo?hid=" + u.HISTORYID + "&uid=" + $routeParams.uid);
        };

        //上传按钮事件
        $scope.Upload = function (modelCode, obj) {
            $scope.ModelCode = modelCode;
            $scope.FormID = obj.HISTORYID;
            $scope.HistoryID = obj.HISTORYID;

            $('#modalUploader').modal('toggle');
            $http.get('/api/upload?ModelCode=' + modelCode + '&FormID=' + $scope.FormID)
            .success(function (data) {
                $scope.UpFiles = data;
            });
        }

        var uploader = null;
        $('#modalUploader').on('shown.bs.modal', function () {
            uploader = WebUploader.create({
                // swf文件路径
                swf: '/Scripts/Uploader.swf',
                // 文件接收服务端。
                server: '/api/upload',
                sendAsBinary: false,
                formData: { ModelCode: $scope.ModelCode, FormID: $scope.FormID },
                fileNumLimit: 1,
                // 选择文件的按钮。可选。
                // 内部根据当前运行是创建，可能是input元素，也可能是flash.
                pick: '#picker',
                // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
                resize: false,
                accept: {
                    title: '文件选择',
                    extensions: 'gif,jpg,jpeg,bmp,png,xls,xlsx',
                    mimeTypes: 'image/*,application/*'
                }
            });
            $scope.Files = [];
            $scope.$apply();
            uploader.on('fileQueued', function (file) {
                $scope.Files.push({ Name: file.name, Msg: '等待上传' });
                $scope.$apply();
            }).on('startUpload', function () {
                var index = $scope.Files.length - 1;
                $scope.Files[index].Msg = "正在上传...";
            }).on('uploadSuccess', function (file, response) {
                var index = $scope.Files.length - 1;
                $scope.Files[index].Msg = "上传完成";
                $scope.$apply();
                uploader.reset();
            }).on('uploadError', function (file, reason) {
                alert('非法文件类型');
            }).on("error", function (type) {
                if (type == "Q_TYPE_DENIED") {
                    alert("请选择图片或Excel文件");
                } else if (type == "Q_EXCEED_NUM_LIMIT") {
                    alert("每次只能上传一个文件");
                }
            });;

            $scope.StartUpload = function () {
                uploader.upload();
            };

            $scope.RemoveFile = function (f) {
                $scope.Files.splice(f, 1);
            };
        });
        $('#modalUploader').on('hide.bs.modal', function () {
            uploader.destroy();
        });

        //返回
        $scope.GoBack = function () {
            $location.url('CancerRecord');
        };

        //删除文件
        $scope.DelFile = function (obj, index) {
            if (!confirm('确定删除？')) {
                return;
            }
            $http.delete('/api/upload?FileID=' + obj.FileUploadid).success(function (data) {
                if (data == "ok") {
                    $scope.UpFiles.splice(index, 1);
                }
            });
        };

        $scope.Search();//页面加载完毕时载入数据

        $("#btnOk").click(function () {
            if ($scope.DiseaseCode != null) {
                $("#txtDisease").val($scope.DiseaseName);
            }
            $('#ICD10Modal').modal('hide');
        });
    }
]);

//检验报告controller
app.controller('resultCtrl', ['$scope', '$http', '$filter', '$location', '$routeParams', 'doctorResultServices',
    function ($scope, $http, $filter, $location, $routeParams, doctorResultServices) {

        //搜索、分页列表
        $scope.CurrentPage = 1;
        $scope.Search = function () {
            $scope.loading = true;
            doctorResultServices.get({ CurrentPage: $scope.CurrentPage, Hid: $routeParams.hid }, function (data) {
                $scope.List = data.Data;
                $scope.loading = false;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            },
                function (data) {
                    $scope.List = [];
                    $scope.loading = false;
                    alert(data.data.Message);
                }
            );
        };

        //  新增
        $scope.Add = function () {
            $location.url('/DoctorResultInfo?uid=' + $routeParams.uid + '&hid=' + $routeParams.hid);
        };

        //编辑
        $scope.Edit = function (obj) {
            $location.url('/DoctorResultInfo?id=' + obj.LabresultId + '&uid=' + $routeParams.uid + '&hid=' + $routeParams.hid);
        };

        //删除
        $scope.Remove = function (obj) {
            if (!confirm('确认删除？')) return;

            $http.delete('/Api/doctorResult?id=' + obj.LabresultId).success(function (data) {
                if (data != "ok") {
                    alert('删除失败');
                } else {
                    $scope.Search();
                }
            }).error(function (response) {
                alert('网络异常！');
            });
        }

        //返回
        $scope.GoBack = function () {
            $location.url('DoctorHistory?uid=' + $routeParams.uid);
        };

        $scope.Search();
    }
]);

//检验报告详情页controller
app.controller('resultInfoCtrl', ['$scope', '$http', '$filter', '$location', '$routeParams', 'doctorServices',
    function ($scope, $http, $filter, $location, $routeParams, doctorServices) {
        $scope.Data = {};
        $scope.LibItems = [];

        $http.get('/api/doctorResult?id=' + ($routeParams.id == undefined ? '' : $routeParams.id) + '&hid=' + $routeParams.hid).success(function (data) {
            if (data.LabResult != null) {
                $scope.Data = data.LabResult;
                $scope.Data.DiagnosisTime = $filter('date')($scope.Data.DiagnosisTime, 'yyyy-MM-dd');
                $scope.LibItems = data.LabItems;
            }
            else
                $scope.Data.HISTORYID = $routeParams.hid;
            $scope.Files = data.FileUplpads;
        }).error(function (response) {
            alert(response.Message);
        });

        $scope.AddLib = function () {
            var item = { TestitemId: "", Ordernumber: $scope.LibItems.length + 1, ItemName: "", ItemNameEN: "", ItemValue: "", Reslut: "", Uom: "", NormalValue: "", ReferenceValue: "", ItemId: "", InspectedMeans: "" };
            $scope.LibItems.push(item);
        };

        $scope.SaveLib = function () {
            $http.post('/api/doctorResult', { Data: { LabResult: $scope.Data, LabItems: $scope.LibItems } }).success(function (data) {
                alert("保存成功!");
                //$location.url("DoctorResult?uid=" + $routeParams.uid + '&hid=' + $routeParams.hid);
                $location.url("DiseaseInfo?uid=" + $routeParams.uid + '&hid=' + $routeParams.hid);
            });
        };

        //加载检验目的列表
        $scope.Templates = [];
        $http.get('/api/doctorResult').success(function (data) {
            $scope.Templates = data;
        }).error(function (response) {
            alert(response.Message);
        });

        //检验目的选择
        $scope.CheckTemplate = function (item) {
            $scope.LibItems = [];
            $scope.Data.MedicalhisAttachment = item.TEMPLATENAME;
            $scope.Data.LabmodelId = item.TEMPLATEID;
            var TemplateItems = item.TemplateItems;
            $.each(item.TemplateItems, function (i) {
                var normalValue = "";
                if (TemplateItems[i].MINVALUE != null && TemplateItems[i].MAXVALUE != null)
                    normalValue = TemplateItems[i].MINVALUE + "-" + TemplateItems[i].MAXVALUE;

                var item = { TestitemId: "", Ordernumber: i + 1, ItemName: TemplateItems[i].ITEMNAME, ItemNameEN: TemplateItems[i].NAMEENG, ItemValue: "", Reslut: "正常", Uom: TemplateItems[i].UNIT, NormalValue: normalValue, ReferenceValue: "", ItemId: TemplateItems[i].TEMPLATEID, InspectedMeans: "" };
                $scope.LibItems.push(item);
            });
            $('#modalAtt').modal('toggle');
        };

        //返回
        $scope.GoBack = function () {
            if ($routeParams.isFlow == "true") {

                $location.url('EventDetailForDoc?id=' + $routeParams.flowId + '&ApplyId=' + $routeParams.ApplyId);

            } else {
                $location.url('DiseaseInfo?hid=' + $routeParams.hid + '&uid=' + $routeParams.uid);

            }
        };
    }
]);

//影像学检查controller
app.controller('imageExCtrl', ['$scope', '$http', '$filter', '$location', '$routeParams', 'doctorServices',
    function ($scope, $http, $filter, $location, $routeParams, doctorServices) {
        $http.get('/api/ImageExamination?historyID=' + $routeParams.hid).success(function (data) {
            if (data != null) {
                $scope.Data = data.ImageEx;
                $scope.Data.CheckTime = $filter('date')($scope.Data.CheckTime, 'yyyy-MM-dd');
                $scope.Data.ReportTime = $filter('date')($scope.Data.ReportTime, 'yyyy-MM-dd');
                $scope.Files = data.Files;
            }
            else
                $scope.Data.HISTORYID = $routeParams.hid;
        });

        $scope.GoBack = function () {
            $location.url("DoctorHistory?uid=" + $routeParams.uid);
        };

        $scope.Save = function () {
            $scope.Data.HISTORYID = $routeParams.hid;
            //console.info($scope.Data);
            $http.post('/api/ImageExamination', { Data: $scope.Data }).success(function (data) {
                alert("保存成功!");
                $location.url("DoctorHistory?uid=" + $routeParams.uid);
            });
        };
    }
]);

//公共个人信息controller
app.controller('personCtrl', ['$scope', '$http', '$location', '$routeParams',
    function ($scope, $http, $location, $routeParams) {
        $http.get('/api/doctor?Uid=' + $routeParams.uid).success(function (data) {
            if (data != null)
                $scope.Person = { Name: data.NAME, IDCard: data.IDCARD, Sex: data.SEX = data.SEX, Age: data.AGE };
        });

    }
]);


app.controller('DiseaseInfoCtrl', ['$scope', '$http', '$filter', '$location', '$routeParams', 'doctorResultServices', 'ExamineReportServices', 'ExamineServices', 'BaseTemplateResultServices',
    function ($scope, $http, $filter, $location, $routeParams, doctorResultServices, ExamineReportServices, examineServices, baseTemplateResultServices) {

        $scope.Uid = $routeParams.uid;
        $scope.Hid = $routeParams.hid;
        $scope.Model = { DISEASETYPE: 0, ISORIGNAL: true };
        $scope.Files = {};
        /***Todo 虚拟字典 临时解决方案***/
        $scope.DicCancerType = [{ text: "未知", value: "0", ivalue: 0 }, { text: "疑似肿瘤", value: "1", ivalue: 1 }, { text: "肿瘤", value: "2", ivalue: 2 }];
        $scope.DicStage = [{ text: "IA", value: "IA" }, { text: "IB", value: "IB" }, { text: "IIA", value: "IIA" }, { text: "IIB", value: "IIB" }, { text: "IIIA", value: "IIIA" }, { text: "IIIB", value: "IIIB" }, { text: "IV", value: "IV" }];
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

        $http.get('/api/EMRInfo?historyId=' + $routeParams.hid).success(function (obj) {
            if (obj != null) {
                $scope.Model = obj.Data;

                $scope.Model.DIAGNOSISTIME = $filter('date')($scope.Model.DIAGNOSISTIME, 'yyyy-MM-dd');


                if ($scope.Model.ICDList != null && $.grep($scope.Model.ICDList,function(item){return item.ISMAIN==true;}).length > 0) {
                    $scope.DiseaseCodeChanged();
                }
            }
        }
        );

        /***实验室检查结果***/
        $scope.DoctorResultCurrentPage = 1;

        $scope.DoctorResultSearch = function () {
            $scope.loading = true;
            doctorResultServices.get({ CurrentPage: $scope.DoctorResultCurrentPage, Hid: $scope.Hid }, function (data) {
                $scope.DoctorResultList = data.Data;
                $scope.loading = false;
                var pager = new Pager('doctorResultPager', $scope.DoctorResultCurrentPage, data.PagesCount, function (curPage) {
                    $scope.DoctorResultCurrentPage = parseInt(curPage);
                    $scope.DoctorResultSearch();
                });
            });
        };
        //新增
        $scope.AddDoctorResult = function () {
            $location.url('/DoctorResultInfo?uid=' + $routeParams.uid + '&hid=' + $routeParams.hid);
        };
        //编辑
        $scope.EditDoctorResult = function (obj) {
            $location.url('/DoctorResultInfo?id=' + obj.LabresultId + '&uid=' + $routeParams.uid + '&hid=' + $routeParams.hid);
        };
        //删除
        $scope.RemoveDoctorResult = function (obj) {
            if (!confirm('确认删除？')) return;

            $http.delete('/Api/doctorResult?id=' + obj.LabresultId).success(function (data) {
                if (data != "ok") {
                    alert('删除失败');
                } else {
                    $scope.DoctorResultSearch();
                }
            }).error(function (response) {
                alert('网络异常！');
            });
        }

        $scope.DoctorResultSearch();

        /***End***/

        /***影像学检查结果***/
        $scope.ExamineReportSearch = function () {
            ExamineReportServices.get({ examineNo: '', userId: $scope.Uid }, function (data) {
                $scope.ExamineReportList = data.Data;
            });
        };
        $scope.ExamineReportSearch();
        $scope.AddExamineReport = function () {
            $location.url("/ExamineReport?uid=" + $scope.Uid + "&examineNo=undefined&hid=" + $scope.Hid);
        };
        $scope.EditExamineReport = function (model) {
            $location.url("/ExamineReport?uid=" + model.UserId + "&examineNo=" + model.Id + "&hid=" + $scope.Hid);
            //$location.url("/ExamineReport?uid=" + $scope.USERID + "&examineNo=" + model.Id + "&hid=" + $scope.Hid + '&isFlow=true&flowId=' + $routeParams.id + '&ApplyId=' + $routeParams.ApplyId);
        };
        $scope.RemoveExamineReport = function (model) {
            ExamineReportServices.delete({ Keyword: model.Id }, function (data) {
                $scope.ExamineReportSearch();
            });
        };
        /***End***/

        //返回
        $scope.GoBack = function () {
            $location.url('DoctorHistory?uid=' + $routeParams.uid);
        };

        $http.get('/api/Upload?ModelCode=SeeDoctorHistory&FormID=' + $routeParams.hid).success(function (obj) {
            if (obj != null) {
                $scope.Files = obj;
            }
        });


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
                    var isMain = idx.ISMAIN;
                    $scope.Model.ICDList.splice(i, 1);
                    if (isMain)
                    {
                        $scope.DiseaseCodeChanged();
                    }
                }
            }
        }

        $scope.SaveICD = function () {
            if ($scope.DiseaseCode != null) {
                if ($scope.DiseaseCode.length > 1) {
                    alert("只能选择一种疾病!");
                    return false;
                }
                var flag = true;
                for (var i = 0; i < $scope.Model.ICDList.length; i++) {
                    for (var j = 0; j < $scope.DiseaseCode.length; j++) {
                        if ($scope.Model.ICDList[i].ICDCODE == $.trim($scope.DiseaseCode[j])) {
                            flag = false;
                            alert("该项：" + $scope.Model.ICDList[i].ICDNAME + " 已添加");
                            return;
                        }
                    }
                }
            }

            for (var i = 0; i < $scope.DiseaseCode.length; i++) {
                $scope.Model.ICDList.push({
                    ICDCODE: $.trim($scope.DiseaseCode[0]),
                    ICDNAME: $scope.DiseaseName[0],
                    DETAILS: "",
                    ISMAIN:isMainICD
                });
            };
            $scope.DiseaseCodeChanged();
            $('#ICD10Modal').modal('hide');
        };

        $scope.DiseaseCodeChanged = function () {
            var mainDisease=$.grep($scope.Model.ICDList,function(item){return item.ISMAIN==true;});
            if (mainDisease != null && mainDisease.length > 0) {
                $scope.CytotypeList = $.grep($scope.DicCytotype, function (item) {
                    return item.DiseaseCode == mainDisease[0].ICDCODE;
                });

                $scope.GenotypingList = $.grep($scope.DicGenotyping, function (item) {
                    return item.DiseaseCode == mainDisease[0].ICDCODE;
                });
            }
            else
            {
                $scope.CytotypeList = [];
                $scope.GenotypingList = [];
            }
        }

        $scope.Save = function () {
            $scope.SaveTempdateList();
            $http.post('/Api/EMRInfo', { Data: $scope.Model }).success(function (obj) {
                if (obj.Data) {
                    $location.url('DoctorHistory?uid=' + $routeParams.uid);
                }
                    
                else
                    alert('网络异常');
            });
        };

        //依据模板选项
        $scope.TemplateList = [];
        $scope.TemplateCurrentPage = 1;
        $scope.SearchTemplate = function () {
            $scope.loading = true;


            var listType = "BasedOnTemplate,LungCancer";

            examineServices.get({
                Keyword: "", CurrentPage: $scope.TemplateCurrentPage,
                PageSize: 15, Keyword: "",
                DicType: listType
            }, function (data) {
                $scope.TemplateList = data.Data;
                $scope.loading = false;
                var pager = new Pager('divtemplatePager', $scope.TemplateCurrentPage, data.PagesCount, function (curPage) {
                    $scope.TemplateCurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            }, function (errRespose) {
                alert("发生错误,操作失败!");
                console.log("ExamineServices.get:" + errRespose.data.Message);
                $scope.loading = false;
            });
        };
        $scope.SearchTemplate();

        $scope.BaseTemplateResultList = []; //存放病历的依据模板

        $scope.GetTemplateList = function() {
            baseTemplateResultServices.get({ID: $routeParams.hid }, function(obj) {
                if (obj) {
                    $scope.BaseTemplateResultList = obj.Data;
                }
            });
        };
        $scope.GetTemplateList();

        var addTempModel = {};
        $scope.AddTemplate = function (index, model) {
            addTempModel = model;
            $('#TableTemplate tr').removeClass('success');
            $($('#TableTemplate tr')[index + 1]).addClass('success');
        };

        $scope.SaveTemplate = function() {
            if (addTempModel == {}) {
                alert("请选择依据模板");
                return;
            }

            var flag = true;
            for (var i = 0; i < $scope.BaseTemplateResultList.length; i++) {
                if ($scope.BaseTemplateResultList[i].EXAMINETEMPLATESID==addTempModel.Id) {
                    flag = false;
                    break;
                }
            }

            if (flag) {
                $scope.BaseTemplateResultList.push({
                    HISTORYID: $routeParams.hid,
                    BASETEMPLATEID: new GUID().newGUID(),
                    ID: new GUID().newGUID(),
                    EXAMINETEMPLATESID: addTempModel.Id,
                    RESULT: "",
                    Name: addTempModel.Name
                });
                addTempModel = {};
                $("#TemplateModal").modal("toggle");
            } else {
                alert("存在相同的依据模板，请重新选择");
            }
        };

        $scope.TemplateInfo = {};//存放题目
        $scope.ReportDetail = {}; //存放题目以及答案
        var editTemplateId = "";//当前编辑的id

        $scope.LoadTemplateData = function (id) {
            editTemplateId = id;
            examineServices.get({ templateCode: id }, function(data) {
                $scope.TemplateInfo = data.Data;
            });
            $scope.ReportDetail = {};
            $.each($scope.BaseTemplateResultList,function(name, value) {
                if (value.EXAMINETEMPLATESID == editTemplateId) {
                    $scope.ReportDetail = JSON.parse(value.RESULT);
                }
            });
        };

        $scope.SaveTemplateData = function () {
            console.log(JSON.stringify($scope.ReportDetail));
            $.each($scope.BaseTemplateResultList, function (name, value) {
                if (value.EXAMINETEMPLATESID == editTemplateId) {
                    value.RESULT = JSON.stringify($scope.ReportDetail);
                }
            });
            $("#TemplateDetailModal").modal("toggle");
        };

        $scope.RemoveTemplateData = function(id) {
            $.each($scope.BaseTemplateResultList,function(name, value) {
                if (value.BASETEMPLATEID == id) {
                    $scope.BaseTemplateResultList.pop(value);
                }
            });
        }

        $scope.SaveTempdateList = function () {
            baseTemplateResultServices.save({ Keyword: $routeParams.hid, DataList: $scope.BaseTemplateResultList }, function (obj) {
                if (obj=="true") {
                    return true;
                }
            });
        };
    }
]);

app.controller('PersonHistoryCtrl', ['$scope', '$http', '$location', '$routeParams', 'userUploadServices',
    function ($scope, $http, $location, $routeParams, userUploadServices) {

        //查询数据
        $scope.Infos = [];
        $scope.IsSave = false;
        $scope.CurrentPage = 1;
        $scope.Search = function () {
            userUploadServices.get({ CreateBy: $routeParams.uid, Date: $scope.Date, CurrentPage: $scope.CurrentPage }, function (data) {
                $scope.Infos = data.Data;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            });
        };

        $scope.SearchCondition = function () {
            $scope.CurrentPage = 1;
            $scope.Search();
        };

        //新增按钮事件弹出新增页面
        $scope.UserUpload = function () {
            $('#modalUploader').modal('toggle');
        };
        //新增保存实体
        $scope.Save = function () {
            if ($scope.UpFiles.length == 0) {
                KMAlert({
                    msg: '请先上传文件!'
                });
                return;
            }
            for (var i = 0; i < $scope.UpFiles.length; i++) {
                $scope.UpFiles[i].Remark = $scope.Remark;
                $scope.UpFiles[i].CreatBy = $scope.UpFiles[i].FormId;
            }

            $scope.IsSave = true;
            userUploadServices.save({ Data: $scope.UpFiles }, function (response) {
                $('#modalUploader').modal('toggle');
                $scope.Search();
                $scope.IsSave = false;
            });
        };
        var uploader = null;
        $scope.Files = [];
        $scope.UpFiles = [];
        $('#modalUploader').on('shown.bs.modal', function () {
            uploader = WebUploader.create({
                // swf文件路径
                swf: '/Scripts/Uploader.swf',
                // 文件接收服务端。
                server: '/api/upload',
                sendAsBinary: false,
                formData: { ModeName: "用户上传", ModelCode: "UserUpload", FormID: $routeParams.uid },
                fileNumLimit: 10,
                // 选择文件的按钮。可选。
                // 内部根据当前运行是创建，可能是input元素，也可能是flash.
                pick: '#picker',
                // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
                resize: false,
                // 只允许选择图片文件。
                accept: {
                    title: 'Images',
                    extensions: 'gif,jpg,jpeg,bmp,png',
                    mimeTypes: 'image/*'
                }
            });
            uploader.on('fileQueued', function (file) {
                $scope.Files.push({ FileName: file.name, Msg: '等待上传',Id:file.id });
                $scope.$apply();
            }).on('uploadSuccess', function (file, response) {
                $scope.UpFiles.push(response[0]);
                for (var i = 0; i < $scope.Files.length; i++) {
                    if ($scope.Files[i].Id == file.id) {
                        $scope.Files[i].Msg = '上传完成';
                        break;
                    }
                }
                $scope.$apply();
                //uploader.reset();
            }).on('uploadProgress', function (file, percentage) {
                for (var i = 0; i < $scope.Files.length; i++) {
                    if ($scope.Files[i].Id == file.id) {
                        $scope.Files[i].Msg = '正在上传...';
                        break;
                    }
                }
                $scope.$apply();
            }).on( 'uploadError', function( file ) {
                for (var i = 0; i < $scope.Files.length; i++) {
                    if ($scope.Files[i].Id == file.id) {
                        $scope.Files[i].Msg = '上传失败';
                        break;
                    }
                }
                $scope.$apply();
            }).on("error", function (type) {

                if (type == "Q_TYPE_DENIED") {
                    alert("不允许的文件格式");
                } else if (type == "Q_EXCEED_NUM_LIMIT") {
                    alert("超出最大允许上传文件个数");
                }
            }).on("uploadComplete", function (file) {

            });

            $scope.StartUpload = function () {
                uploader.upload();
            };

            $scope.RemoveFile = function (f) {
                $scope.Files.splice(f, 1);
            };
        });
        $('#modalUploader').on('hide.bs.modal', function () {
            uploader.destroy();
        });

        //页面加载完毕时载入数据
        $scope.Search();

        $scope.ResetSearch = function () {
            $scope.Date = null;
        }
    }
]);

/*
*用户上传资料整理*
*/
app.controller('UserArrangeCtrl', ['$scope', '$http', '$location', '$routeParams', 'UserArrangeServices',
    function ($scope, $http, $location, $routeParams, UserArrangeServices) {
        //初始化
        $scope.Info = { CreateNew: false, Hosiptal: null, HistoryID: null, Remark: null };

        //查询数据
        $scope.FormID = null;

        $scope.Infos = [];
        $scope.IsSave = false;
        $scope.CurrentPage = 1;
        $scope.Search = function () {
            UserArrangeServices.get({ Date: $scope.Date, ID: $routeParams.id, CurrentPage: $scope.CurrentPage }, function (data) {
                $scope.Infos = data.Data;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            });
        };

        $scope.SearchCondition = function () {
            $scope.CurrentPage = 1;
            $scope.Search();
        };

        $scope.Search();

        //重置搜索
        $scope.ResetSearch = function () {
            $scope.Date = null;
        };

        //提交
        $scope.Save = function () {
            var oTr = $('tbody#tbBody').find('tr');
            for (var i = 0; i < $scope.Infos.length; i++) {
                var _o = $scope.Infos[i];
                if (_o.Type == "" || _o.Type == undefined) {
                    oTr.eq(i).find('td').eq(3).addClass('has-error');
                    return;
                } else {
                    oTr.eq(i).find('td').eq(3).removeClass('has-error');
                }
            }

            if ($scope.Info.CreateNew == false && $scope.Info.HistoryID == null) {
                KMAlert({
                    msg: '请勾选新建患者病历或选择患者已有病历！'
                });
                return;
            }

            var item = {
                EventID: $routeParams.id, HistoryID: $scope.Info.HistoryID, CreateNew: $scope.Info.CreateNew,Remark:$scope.Info.Remark, Files: $scope.Infos
            };

            $http.post('/api/UserArrange', { Data: item }).success(function (data) {
                if (data.Result == "ok") {
                    var _id = data.ID;
                    var _uid = data.Uid;
                    KMAlert({
                        returnUrl: '/#/DiseaseInfo?hid=' + _id + '&uid='+_uid
                    });
                } else {
                    KMAlert({
                        msg: '当前记录保存失败！'
                    });
                }
            }).error(function (response) {
                KMAlert({
                    msg: response
                });
            });
        };

        //返回
        $scope.GoBack = function () {
            $location.url('/');
        };

        //选择病历
        $scope.SelectMHC = function () {
            $('#modalMHC').modal('toggle');
        };

        //接收参数
        $scope.$on('MHC', function (e, obj) {
            $scope.Info.Hosiptal = obj.HOSPITAL;
            $scope.Info.HistoryID = obj.HISTORYID;
            $scope.Info.CreateNew = false;
        });

        $scope.CheckCN = function () {
            $scope.Info.Hosiptal = null;
            $scope.Info.HistoryID = null;
        };
    }
]);

/*
*就诊史选择控件*
*/
app.controller('MedicalHistoryControlCtrl', ['$scope', '$http', '$filter', '$location', '$routeParams', 'doctorServices',
    function ($scope, $http, $filter, $location, $routeParams, doctorServices) {

        //查询数据
        $scope.Infos = [];
        $scope.CurrentPage = 1;
        $scope.Search = function () {
            doctorServices.get({ Id: $routeParams.id, CurrentPage: $scope.CurrentPage }, function (data) {
              
                $scope.Infos = data.Data;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            });
        };

        $scope.Search();

        $scope.CheckMH = function (obj) {
            $scope.$emit('MHC', obj);
            $('#modalMHC').modal('toggle');
        };
    }
]);
