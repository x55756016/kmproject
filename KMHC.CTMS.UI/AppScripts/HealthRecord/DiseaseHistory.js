var app = angular.module("diseasehis", []);
app.controller("diseasehisCtrl", [
   "$scope", "$http", "$timeout", "$routeParams", "$location", "diseasehistoryServices", "TreatmentHistoryServices",
    function ($scope, $http, $timeout, $routeParams, $location, diseasehistoryServices, treatmentHistoryServices) {

        $scope.ListItems = {};
        $scope.DiseasehisModel = {};
        $scope.TreatmentList = {};
        $scope.TreatmentHistory = {};

        $scope.CurrentPage = 1;

        $scope.Search = function () {
            diseasehistoryServices.get({ CurrentPage: $scope.CurrentPage, PageSize: 5, UserName: $routeParams.uid, Keyword: $scope.Keyword }, function (obj) {
                $scope.ListItems = obj.Data;
                var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function (curPage) {
                    $scope.CurrentPage = curPage;
                    $scope.Search();
                });
            });
        };
        $scope.Search();

        //查询的时候重置页码
        $scope.SearchCondition = function () {
            $scope.CurrentPage = 1;
            $scope.Search();
        };


        $scope.disId = 0;
        $scope.SetDisId = function (model) {
            //$scope.disId = id;
            //diseasehistoryServices.get({ ID: id, UserName: $routeParams.uid }, function (obj) {
            //    $scope.DiseasehisModel = obj.Data[0];

            //    if ($scope.DiseasehisModel.ISCANCER == "True") {
            //        $("#IsCancer").attr('checked', true);
            //        $scope.tumorMenuState.show = true;
            //    } else {
            //        $scope.tumorMenuState.show = false;
            //        $("#IsCancer").attr('checked', false);
            //    }
            //});
            $scope.disId = model.DISEASEHISTORYID;
            $scope.DiseasehisModel = model;

            if ($scope.DiseasehisModel.ISCANCER == "True") {
                $("#IsCancer").prop('checked', true);
                $scope.tumorMenuState.show = true;
            } else {
                $scope.tumorMenuState.show = false;
                $("#IsCancer").prop('checked', false);
                //$("#IsCancer").attr('checked', 'false');
            }
            $scope.GetTreatmentList();
        }

        $scope.SaveDH = function () {
           $scope.DiseasehisModel.PERSONID = $routeParams.uid;
            diseasehistoryServices.save({ Data: $scope.DiseasehisModel }, function (response) {
                alert("成功");
                $('#myModalInfo').modal('hide');
                $('#myModal').modal('hide');
                $scope.DiseasehisModel={}
                $scope.Search();
            });
        }


        $scope.Remove = function(id) {
            if (confirm('您确定要删除吗？')) {
                diseasehistoryServices.remove({ id: id }, function (response) {
                    $scope.Search();
                });
            }
        };

       
        $scope.tumorMenuState = { show: false };
        $scope.toggleTumorMenu = function () {
            $scope.tumorMenuState.show = !$scope.tumorMenuState.show;
            };

        $scope.back = function () {
            $location.path("CancerRecord");
        };

        
        $scope.GetTreatmentList=function() {
            treatmentHistoryServices.get({ ID: $scope.disId }, function (obj) {
                $scope.TreatmentList = obj.Data;
            });
        }

        $scope.SaveTreatment = function () {
            $scope.TreatmentHistory.DISEASEHISTORYID = $scope.DiseasehisModel.DISEASEHISTORYID;
            treatmentHistoryServices.save({ Data: $scope.TreatmentHistory }, function(response) {
                alert("成功");
                $scope.GetTreatmentList();
            });
        }


        $scope.RemoveTreatment = function (id) {
            if (confirm('您确定要删除吗？')) {
                treatmentHistoryServices.remove({ id: id }, function (response) {
                    $scope.GetTreatmentList();
                });
            }
        };



        //上传按钮事件
        $scope.Upload = function (modelCode, id) {
            $scope.ModelCode = modelCode;
            $scope.HistoryId = id;
            $('#modalUploader').modal('toggle');
            $http.get('/api/upload?ModelCode=' + modelCode + '&FormID=' + id)
            .success(function (data) {
                $scope.UpFiles = data;
                //console.info(data);
            });
        }

        var uploader = null;
        $('#modalUploader').on('shown.bs.modal', function () {
            uploader = WebUploader.create({
                // swf文件路径
                swf: '/Scripts/Uploader.swf',
                // 文件接收服务端
                server: '/api/upload',
                formData: { ModelCode: $scope.ModelCode, FormID: $scope.HistoryId },
                fileNumLimit: 1,
                // 选择文件的按钮。可选
                // 内部根据当前运行是创建，可能是input元素，也可能是flash
                pick: '#picker',
                // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
                resize: false
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
                //console.info(file);
                //console.info(reason);
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

    }
]);