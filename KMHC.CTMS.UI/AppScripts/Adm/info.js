var app = angular.module('Adm', []);

app.controller("InfoListCtrl", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "AdmServices","DictionaryServices",
    function ($scope, $http, $timeout, $routeParams, $location, AdmServices, DictionaryServices) {
        $scope.Kwd = '';
        $scope.loading = false;
        $scope.searchData = {}

        $scope.Search = function () {
            $scope.loading = true;
            AdmServices.get({ Keyword: $scope.Kwd }, function (data) {
                $scope.Data = data.Data;
                $scope.loading = false;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("AdmServices.get:" + errRespose.data.Message);
            });
        };

        $scope.Search();

        $scope.InitTree = function () {
            DictionaryServices.get({ Keyword: 'Info_Dictionary' }, function (data) {
                $('#tree').treeview({ data: data.Data });
                $('#tree').on('nodeSelected', function (event, data) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            //点击响应事件
                            AdmServices.get({ Keyword: data.nodeId }, function (data) {
                                $scope.Data = data.Data;
                            }, function (errRespose) {
                                alert("发生错误,查询失败!")
                                console.log("AdmServices.get:" + errRespose.data.Message);
                            });
                        });
                    }, 200);
                });
                $('#tree').on('nodeUnselected', function (event, data) {
                    AdmServices.get({ Keyword: '' }, function (data) {
                        $scope.Data = data.Data;
                    }, function (errRespose) {
                        alert("发生错误,查询失败!")
                        console.log("AdmServices.get:" + errRespose.data.Message);
                    });
                });
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("AdmServices.get:" + errRespose.data.Message);
            });
        };

        $scope.EditInfo = function (id) {
            $location.path('InfoEdit/' + id);
        };
    }]);

app.controller('InfoEditCtrl', [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "AdmServices","DictionaryServices",
    function ($scope, $http, $timeout, $routeParams, $location, AdmServices, DictionaryServices) {
        $scope.loading = false;
        $scope.formData = {};
        $scope.formData.FileId = $routeParams.FileId;
        if ($scope.formData.FileId == undefined || $scope.formData.FileId == 'undefined' || $scope.formData.FileId == '')
            $scope.formData.FileId = new GUID().newGUID();
        else {
            AdmServices.get({ fileId: $scope.formData.FileId }, function (data) {
                $scope.formData = data.Data;
                editor.html($scope.formData.Html);
            }, function (response) {
                alert("发生错误,查询失败!")
                console.log("AdmServices.get:" + errRespose.data.Message);
            });
        }

        $scope.getExt = function (data) {
            var ext = '';
            var arr = new Array();
            for (var i in data) {
                var filename = data[i].FilePath;
                var extIndex = filename.lastIndexOf('.');
                var extStr = filename.substring(extIndex + 1);
                if(arr.indexOf(extStr) < 0)
                    arr.push(filename.substring(extIndex + 1));
            }
            for (var i in arr) {
                ext += "、" + arr[i];
            }
            if (ext != '')
                ext = ext.substring(1);
            return ext;
        };

        $scope.getFileName = function (data) {
            var name = '';
            var arr = new Array();
            for (var i in data) {
                var filename = data[i].FilePath;
                var extIndex = filename.lastIndexOf('\\');
                var extStr = filename.substring(extIndex + 1);
                if (arr.indexOf(extStr) < 0)
                    arr.push(filename.substring(extIndex + 1));
            }
            for (var i in arr) {
                name += "、" + arr[i];
            }
            if (name != '')
                name = name.substring(1);
            return name;
        }

        var uploader = null;
        $('#modalUploader').on('shown.bs.modal', function () {
            uploader = WebUploader.create({
                // swf文件路径
                swf: '/Scripts/Uploader.swf',
                // 文件接收服务端
                server: '/api/upload?identFileName=true',
                formData: { ModelCode: 'Info', FormID: $scope.formData.FileId },
                fileNumLimit: 1,
                // 选择文件的按钮。可选
                // 内部根据当前运行是创建，可能是input元素，也可能是flash
                pick: '#picker',
                // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
                resize: false,
                accept: {
                    title: '文件',
                    extensions: 'doc,docx,xls,xlsx,pdf,ppt,pptx,jpg,png,bmp,jpeg,html,htm,PDF',
                    mimeTypes: 'image/*,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,text/*,application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/vnd.ms-powerpoint,application/vnd.openxmlformats-officedocument.presentationml.presentation'
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
                $http.get('/api/upload?ModelCode=Info&FormID=' + $scope.formData.FileId)
                .success(function (data) {
                    $scope.UpFiles = data;
                    $scope.formData.ArticleType = $scope.getExt(data);
                    $scope.formData.FileNames = $scope.getFileName(data);
                }).error(function (data) {
                    alert(data);
                });
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

        $http.get('/api/upload?ModelCode=Info&FormID=' + $scope.formData.FileId)
            .success(function (data) {
                $scope.UpFiles = data;
                $scope.formData.ArticleType = $scope.getExt(data);
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

        //start----tree
        $scope.Search = function () {
            $scope.loading = true;
            DictionaryServices.get({ Keyword: 'Info_Dictionary' }, function (data) {
                $('#tree').treeview({ data: data.Data });
                $scope.loading = false;
                $('#tree').on('nodeSelected', function (event, data) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.formData.CategoryCode = data.nodeId;
                            $scope.formData.CategoryName = data.text;
                        });
                    }, 50);
                });
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("AdmServices.get:" + errRespose.data.Message);
            });
        };

        $scope.Search();
        //end----tree

        editor = KindEditor.create('#editor_id');

        $scope.processForm = function () {
            $scope.formData.Html = editor.html();
            $scope.loading = true;
            AdmServices.save({ Data: $scope.formData,ID:$scope.formData._id }, function (response) {
                alert("保存成功");
                $location.path('/InfoList');
            }, function (errRespose) {
                alert("发生错误,保存失败!")
                console.log("AdmServices.post:" + errRespose.data.Message);
                $scope.loading = false;
            });
        };

    }]);

app.controller('DictionaryCtrl', [
    '$scope', '$http', '$timeout', '$routeParams', '$location', 'DictionaryServices', function ($scope, $http, $timeout, $routeParams, $location, DictionaryServices) {
        $scope.editData = {};
        $scope.addButtonTitle = '';
        $scope.category = $routeParams.CategoryCode;

        $scope.Search = function () {
            $scope.loading = true;
            DictionaryServices.get({ Keyword: $scope.category }, function (data) {
                $('#tree').treeview({ data: data.Data });
                $('#tree').on('nodeSelected', function (event, data) {
                    $timeout(function () {
                        $scope.$apply(function () {
                            $scope.editData = data;
                            $scope.addButtonTitle = '往当前节点添加子节点';
                            $("#myModal").modal("toggle");
                        });
                    }, 200);
                });
                $scope.loading = false;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("DictionaryServices.get:" + errRespose.data.Message);
            });
        };

        $scope.Search();

        $scope.addRootNode = function () {
            $scope.editData = {};
            $scope.addButtonTitle = '确定';
            $("#myModal").modal("toggle");
        }

        $scope.updateNode = function () {
            $scope.editData.category = $scope.category;
            $scope.editData.nodeId = $scope.editData.tempNodeId;
            DictionaryServices.save({ Data: $scope.editData }, function (response) {
                $scope.Search();
                $("#btnClose").trigger("click");
            }, function (errRespose) {
                alert("发生错误,保存失败!")
                console.log("DictionaryServices.post:" + errRespose.data.Message);
            });
        };

        $scope.addNode = function () {
            $scope.editData.parentId = $scope.editData.tempNodeId;
            $scope.editData.nodeId = '';
            $scope.editData.category = $scope.category;
            DictionaryServices.save({ Data: $scope.editData }, function (response) {
                $scope.Search();
                $("#btnClose").trigger("click");
            }, function (errRespose) {
                alert("发生错误,保存失败!")
                console.log("DictionaryServices.post:" + errRespose.data.Message);
            });
        };

        $scope.deleteNode = function () {
            DictionaryServices.delete({ Keyword: $scope.editData.tempNodeId }, function (data) {
                $("#myModal").modal("toggle");
                $scope.Search();
            });
        };
    }
]);

app.controller('InfoSearchCtrl', ['$scope', '$http', '$timeout', '$routeParams', '$location', 'AdmServices', function ($scope, $http, $timeout, $routeParams, $location, AdmServices) {
    /*$("#txtSearch").autocomplete({
        source: "Info/SearchAutoComplete"
    });*/
    $scope.loading = false;
    $scope.SearchType = "Title";
    $scope.searchInfo = function () {
        $scope.loading = true;
        AdmServices.get({ Keyword: $("#txtSearch").val(), ID: 'SearchInfo', UserName: $scope.SearchType }, function (data) {
            $scope.Data = data.Data;
            $scope.loading = false;
        }, function (errRespose) {
            alert("发生错误,查询失败!")
            console.log("AdmServices.get:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };

    $scope.searchInputOk = function (ev) {
        if (ev.keyCode !== 13) return;
        $scope.searchInfo();
    };

    $scope.CurrentPage = 1;
    $scope.SearchList = function (CategoryCode) {
        AdmServices.get({ Keyword: CategoryCode, ID: 'SearchInfo', UserName: "CategoryCode", CurrentPage: $scope.CurrentPage }, function (data) {
            $scope.CategoryData = data.Data;
            var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                $scope.CurrentPage = parseInt(curPage);
                $scope.SearchList(CategoryCode);
            });
        }, function (errRespose) {
            alert("发生错误,查询失败!")
            console.log("AdmServices.get:" + errRespose.data.Message);
        });
    };

    AdmServices.get({ Type: -1 }, function (data) {
        $scope.AllData = data.Data;
    }, function (errRespose) {
        alert("发生错误,查询失败!")
        console.log("AdmServices.get:" + errRespose.data.Message);
    });

}]);








