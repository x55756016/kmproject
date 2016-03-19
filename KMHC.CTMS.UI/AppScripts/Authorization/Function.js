var app = angular.module('SysFunction', []);

app.controller('SysFunctionCtrl', ['$scope', '$http', '$location', '$routeParams', 'SysFunctionServices',
        function ($scope, $http, $location, $routeParams, SysFunctionServices) {
            $scope.Name = '';
            //搜索、列表
            $scope.Search = function () {
                $http.get('/Api/SysFunction?Name='+$scope.Name).success(function (data) {
                    $scope.List = data;
                });
            };

            $scope.SearchCondition = function () {
                $scope.Search();
            };

            //新增
            $scope.Add = function () {
                $location.url('/AddSysFunction');
            };

            //删除
            $scope.Remove = function (obj) {
                KMConfirm({
                    msg: '是否删除当前记录?',
                    btnMsg: '删除'
                }, function (e) {
                    $http.delete('/Api/SysFunction?ID=' + obj.ID).success(function (data) {
                        if (data != "ok") {
                            KMAlert({
                                msg: '删除失败！',
                                btnMsg: '确定'
                            });
                        } else {
                            $scope.Search();
                        }
                    }).error(function (response) {
                        KMAlert({
                            msg: response,
                            btnMsg: '确定'
                        });
                    });
                });
            };

            //重置
            $scope.Reset = function () {
                $scope.Name = null;
            };

            $scope.Search();
        }
]);

app.controller('AddSysFunctionCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $scope.Info = {};
            $scope.Info.IsExpand = 'false';
            
            $http.get('/Api/SysFunction').success(function (data) {
                $scope.Father = data;
            });

            //返回
            $scope.GoBack = function () {
                $location.url('/SysFunction');
            };

            //保存
            $scope.Save = function () {
                $http.post('/Api/SysFunction', { Data: $scope.Info }).success(function (data) {
                    if (data == 'ok') {
                        KMAlert({
                            msg: '当前记录保存成功！',
                            btnMsg: '确定'
                        });
                    }
                    else {
                        KMAlert({
                            msg: '网络异常！',
                            btnMsg: '确定'
                        });
                    }
                }).error(function (error) {
                    KMAlert({
                        msg: '网络异常！',
                        btnMsg: '确定'
                    });
                });
            };

            //父字典选择
            $scope.CheckFather = function (obj) {
                $scope.Info.FatherName = obj.Name;
                $scope.Info.ParentID = obj.ID;
                $('#modalFather').modal('toggle');
            };

            //移除
            $scope.Clear = function (p, k, i) {
                $scope.Info.FatherName = null;
                $scope.Info.ParentID = null;
            };
        }
]);

app.controller('EditSysFunctionCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $scope.Info = {};
            $http.get('/Api/SysFunction?Act=Edit&ID=' + $routeParams.ID).success(function (data) {
                $scope.Info = data;
                if (data != "") {
                    var _father = $scope.Info.ParentID.split('#');
                    $scope.Info.ParentID = _father[0];
                    $scope.Info.FatherName = _father[1];
                    $scope.Info.IsExpand = data.IsExpand + '';
                }
            }).error(function (response) {
                $scope.Info = {};
            });

            $http.get('/Api/SysFunction').success(function (data) {
                $scope.Father = data;
            });

            //返回
            $scope.GoBack = function () {
                $location.url('/SysFunction');
            };

            //保存
            $scope.Save = function () {
                $http.post('/Api/SysFunction', { Data: $scope.Info }).success(function (data) {
                    if (data == 'ok') {
                        KMAlert({
                            msg: '当前记录保存成功！',
                            btnMsg: '确定'
                        });
                    }
                    else {
                        KMAlert({
                            msg: '网络异常！',
                            btnMsg: '确定'
                        });
                    }
                }).error(function (error) {
                    KMAlert({
                        msg: '网络异常！',
                        btnMsg: '确定'
                    });
                });
            };

            //父字典选择
            $scope.CheckFather = function (obj) {
                $scope.Info.FatherName = obj.Name;
                $scope.Info.ParentID = obj.ID;
                $('#modalFather').modal('toggle');
            };

            //移除
            $scope.Clear = function (p, k, i) {
                $scope.Info.FatherName = null;
                $scope.Info.ParentID = null;
            };
        }
]);