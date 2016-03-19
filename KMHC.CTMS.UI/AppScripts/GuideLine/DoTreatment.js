var app = angular.module('DoTreatment', []);

app.controller('DoTreatmentCtrl', ['$scope', '$http', '$location', '$routeParams', 'DoTreatmentServices',
        function ($scope, $http, $location, $routeParams, DoTreatmentServices) {

            $scope.ActionStatus = "1";
            //搜索、分页列表
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                DoTreatmentServices.get({ ActionInfo: $scope.ActionInfo, ActionStatus: $scope.ActionStatus, CurrentPage: $scope.CurrentPage }, function (data) {
                    $scope.List = data.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                        $scope.CurrentPage = parseInt(curPage);
                        $scope.Search();
                    });
                },
                        function (data) {
                            $scope.List = [];
                        }
                    );
            };

            $scope.SearchCondition = function () {
                $scope.CurrentPage = 1;
                $scope.Search();
            };

            //新增
            $scope.Add = function () {
                $location.url('/AddRole');
            };

            //重置
            $scope.Reset = function () {
                $scope.ActionInfo = null;
            };

            //待办状态切换
            $scope.LoadData = function (i) {
                $scope.ActionStatus = i;
                $scope.SearchCondition();
            };

            $scope.Search();

            //var timer = setInterval(function () {
            //    if ($('div#DoTreatment').length) {
            //        $scope.Search();
            //    } else {
            //        clearInterval(timer);
            //    }
            //}, 5000);
        }
]);

app.controller('DoTreatmentViewCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $http.get('/Api/DoTreatment?id=' + $routeParams.id).success(function (data) {
                $scope.Info = data;
            }).error(function (response) {

            });

            uploader = WebUploader.create({
                // swf文件路径
                swf: '/Scripts/Uploader.swf',
                // 文件接收服务端。
                server: '/api/upload',
                sendAsBinary: false,
                formData: { ModeName: "用户上传", ModelCode: "UserUpload", FormID: $routeParams.id },
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
            $scope.UpFiles = [];
            uploader.on('fileQueued', function (file) {
                $scope.Files.push({ Name: file.name, Msg: '等待上传' });
                $scope.$apply();
            }).on('startUpload', function () {
                var index = $scope.Files.length - 1;
                $scope.Files[index].Msg = "正在上传...";
            }).on('uploadSuccess', function (file, response) {
                var index = $scope.Files.length - 1;
                $scope.UpFiles[index] = response[0];
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
                uploader.reset();
            };

            $scope.Save = function () {
                var UserEvent = {};
                UserEvent.EventID = $scope.Info.EventID;
                UserEvent.Remarks = $scope.Info.Remarks;

                if ($scope.UpFiles.length == 0) {
                    KMAlert({
                        msg: '请先上传文件!'
                    });
                    return;
                }
                for (var i = 0; i < $scope.UpFiles.length; i++) {
                    $scope.UpFiles[i].Remark = $scope.Info.Remarks;
                }

                $http.post('/Api/DoTreatment', { Data: { userEvent: UserEvent, files: $scope.UpFiles } }).success(function (data) {
                    if (data == 'ok') {
                        KMAlert({
                            msg: '当前记录保存成功！',
                            btnMsg: '确定',
                            returnUrl:'/'
                        });
                    }
                    else {
                        KMAlert({
                            msg: '网络异常！',
                            btnMsg: '确定'
                        });
                    }
                }).error(function (response) {

                });
            };

            //购买产品
            $scope.BuyProducts = function (i,p) {
                if (confirm("确定购买？")) {
                    $http.get("Api/UserInfo").success(function (data) {
                        var account = data.Data.UserInfo.Account;
                        if (p.ProductPrice > account) {
                            alert("余额不足，请充值!")
                        }
                        else {
                            $http.post("Api/AccountRecord", { Data: { ProductID: p.ProductId, ProductName: p.ProductName, Balance: -1, Account: p.ProductPrice, SpendType: 1 } }).success(function (data) {
                                $scope.BuySuccess(p);
                                $scope.Info.Products[i].IsAlreadyBuy = '1';
                                alert("购买成功!");
                            }).error(function (errResponse) {
                                alert("购买失败!")
                            });
                        }
                    });
                }
            };

            $scope.BuySuccess = function (obj) {
                $http.get('/api/DoTreatment?epid=' + obj.EventProductId + '&pid=' + obj.ProductID);
            }

            //返回
            $scope.GoBack = function () {
                $location.url('/');
            }
        }
]);

app.controller('ViewUploadCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            uploader = WebUploader.create({
                // swf文件路径
                swf: '/Scripts/Uploader.swf',
                // 文件接收服务端。
                server: '/api/upload',
                sendAsBinary: false,
                formData: { ModeName: "用户上传", ModelCode: "UserUpload", FormID: $routeParams.id },
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
            $scope.UpFiles = [];
            uploader.on('fileQueued', function (file) {
                $scope.Files.push({ Name: file.name, Msg: '等待上传' });
                $scope.$apply();
            }).on('startUpload', function () {
                var index = $scope.Files.length - 1;
                $scope.Files[index].Msg = "正在上传...";
            }).on('uploadSuccess', function (file, response) {
                var index = $scope.Files.length - 1;
                $scope.UpFiles[index] = response[0];
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
                uploader.reset();
            };

            $scope.Save = function () {
                if ($scope.UpFiles.length == 0) {
                    KMAlert({
                        msg: '请先上传文件!'
                    });
                    return;
                }
                for (var i = 0; i < $scope.UpFiles.length; i++) {
                    $scope.UpFiles[i].Remark = $scope.Remark;
                }

                $http.post('/Api/ViewUpload', { Data: $scope.UpFiles, ID: $routeParams.id }).success(function (data) {
                    if (data == 'ok') {
                        KMAlert({
                            msg: '当前记录保存成功！',
                            btnMsg: '确定',
                            returnUrl: '/#/'
                        });
                    }
                    else {
                        KMAlert({
                            msg: '网络异常！',
                            btnMsg: '确定'
                        });
                    }
                }).error(function (response) {

                });
            };

            //返回
            $scope.GoBack = function () {
                $location.url('/');
            }
        }
]);

app.controller('ServiceTaskCtrl', ['$scope', '$http', '$location', '$routeParams','ServiceTaskServices',
    function ($scope, $http, $location, $routeParams,ServiceTaskServices) {
        
        $scope.eventId = $routeParams.id;

        $scope.Init = function () {
            ServiceTaskServices.get({eventId:$scope.eventId}, function (data) {
                $scope.UserInfo = data.Data.User;
                $scope.EventInfo = data.Data.Event;
                $scope.Apply = data.Data.Apply;
            });
        };
        $scope.Init();

        $scope.processForm = function () {
            $scope.loading = true;
            $scope.Model.ForEventId = $scope.EventInfo.EventID;
            ServiceTaskServices.save({ Data: $scope.Model }, function (response) {
                alert("操作成功");
                $scope.loading = false;
                window.location = '/';
            });
        };
    }
]);

app.controller('ServiceTraceListCtrl', ['$scope', '$http', '$location', '$routeParams', 'ServiceTaskServices',
    function ($scope, $http, $location, $routeParams, ServiceTaskServices) {

        $scope.TraceType = [
            { text: "电话", value: 0 },
            { text: "即时通讯", value: 1 },
            { text: "其他", value: 2 }
        ];
        $scope.search = {};

        $scope.Init = function () {
            ServiceTaskServices.get({ Data: $scope.search.TraceType }, function (data) {
                $scope.ServiceTraceInfoList = data.Data;
            });
        };
        $scope.Init();

        $scope.ResetSearch = function () {
            $scope.search.TraceType = "";
        };
    }
]);