var app = angular.module('UserGene', []);
app.controller("UserGene_Controller", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "UserGeneServices", "UserGeneListServices", "GeneServices", "GeneAlleleServices",
    function ($scope, $http, $timeout, $routeParams, $location, UserGeneServices,UserGeneListServices, GeneServices, GeneAlleleServices) {

       

        //查询数据
        $scope.Search = function () {            
            UserGeneServices.get({ Keyword: $scope.UID }, function (data) {
                $scope.UserGeneList = data.Data;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("UserGeneServices.get:" + errRespose.data.Message);
            }); 
        }
        $scope.Init = function () {
            $scope.UID = $routeParams.uid;
            GeneServices.get({ Keyword: "" }
           , function (data) {
               $scope.GeneList = data.Data;
           }, function (errRespose) {
               alert("发生错误,查询失败!")
               console.log("GeneServices.get:" + errRespose.data.Message);
           });
            $scope.GetFile();
            $scope.Search();
        };
        $scope.GetFile = function () {
            $http.get('/api/upload?ModelCode=UserGene&FormID=' + $scope.UID)
           .success(function (data) {
               $scope.UpFiles = data;
               //console.info(data);
           });
        };
        $scope.Init();
        $scope.GeneChanged=function(){
            GeneAlleleServices.get({ Keyword: $scope.CRUDUserGene.GeneID }
            , function (data) {
                $scope.GeneAlleleList = data.Data;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("GeneAlleleServices.get:" + errRespose.data.Message);
            });
        };
        //新增基因信息
        $scope.AddUserGeneList = function () {
            //$scope.CRUDUserGene = { UserID: $scope.UID };
            $('#modalAddUserGenList').modal('toggle');
        };

        //保存基因信息
        $scope.SaveUserGeneList = function () {
            var userGeneList = [];
            $("[name='userGeneGroup']:checked").each(function () {
                userGeneList.push({ UserID: $scope.UID,GeneID:$(this).val(),CopyNumber1:1,CopyNumber2:1})
            });
            UserGeneListServices.save({ Data: userGeneList },
                function (data) {
                    $scope.Search();
                    $('#modalAddUserGenList').modal('toggle');
                },
                function (errResponse) {
                });
        };

       //新增按钮事件弹出新增页面
        $scope.AddUserGene = function () {
            $scope.CRUDUserGene = { UserID: $scope.UID, CopyNumber1: 1, CopyNumber2: 1 };
            $scope.GeneAlleleList = [];
            $('#modalAddUser').modal('toggle');
        };

        $scope.CheckGeneExist = function () {
            if ($scope.CRUDUserGene.ID != null) return false;
            var isExists = false;
            angular.forEach($scope.UserGeneList, function (item) {
                if (isExists) return;
                if (item.GeneID == $scope.CRUDUserGene.GeneID) {
                    isExists = true;
                }
            });
            return isExists;
        };
        //新增保存实体
        $scope.SaveUserGene = function () {
            if ($scope.CheckGeneExist())
            {
                alert("基因已存在,请选择其它基因!")
                return false;
            }
            UserGeneServices.save({ Data: $scope.CRUDUserGene }, function (response) {
                alert("保存成功!");
                $('#modalAddUser').modal('toggle');
                $scope.Search();
            }, function (errResponse) {
                alert("保存失败!");
                console.log("UserGeneServices.save:"+errResponse.data.Message);
            });
        };
        $scope.EditUserGene = function (u) {
            $scope.CRUDUserGene = $scope.Clone(u);
            $scope.GeneChanged();
            $('#modalAddUser').modal('toggle');
        };
        //删除
        $scope.DeleteUserGene = function (u) {
            if (confirm("确定删除？")) {
                UserGeneServices.delete({ id: u.ID }, function (response) {
                    alert("删除成功!");
                    $scope.Search();
                }, function (errResponse) {
                    alert("删除失败!");
                    console.log("UserGeneServices.delete:" + errResponse.data.Message);
                });
            }
        };
        $scope.back = function () {
            $location.path("CancerRecord");
        };
        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
        //上传
        $scope.Upload = function (modelCode, id) {
            $scope.ModelCode = modelCode;
            $scope.FormId = id;
            $('#modalUploader').modal('toggle');
        }

        var uploader = null;
        $('#modalUploader').on('shown.bs.modal', function () {
            uploader = WebUploader.create({
                // swf文件路径
                swf: '/Scripts/Uploader.swf',
                // 文件接收服务端。
                server: '/api/upload',
                sendAsBinary: false,
                formData: { ModelCode: $scope.ModelCode, FormID: $scope.UID },
                fileNumLimit: 1,
                // 选择文件的按钮。可选。
                // 内部根据当前运行是创建，可能是input元素，也可能是flash.
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
                $scope.GetFile();
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

        $scope.ShowFile = function (path) {
            window.open(path);
        };
        $scope.DeleteFile = function (id) {
            if (confirm("确定删除？")) {
                $http.delete('/api/upload?FileID=' + id)
                .success(function (data) {
                 $scope.GetFile();
                });
            }
        };
    }]);