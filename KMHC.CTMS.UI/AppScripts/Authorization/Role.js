var app = angular.module('Role', []);

app.controller('RoleCtrl', ['$scope', '$http', '$location', '$routeParams', 'RoleServices',
        function ($scope, $http, $location, $routeParams, RoleServices) {

            //搜索、分页列表
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                $scope.loading = true;
                RoleServices.get({ RoleName: $scope.RoleName, CurrentPage: $scope.CurrentPage }, function (data) {
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

            //删除
            $scope.Remove = function (obj) {
                KMConfirm({
                    msg: '是否删除当前记录?',
                    btnMsg: '删除'
                }, function (e) {
                    $http.delete('/Api/Role?roleId=' + obj.RoleID).success(function (data) {
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
                $scope.RoleName = null;
            };

            $scope.Search();
        }
]);

app.controller('AddRoleCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $scope.Info = {};
            $scope.Info.SystemCategory = '0';
            $http.get('/Api/Dictionary?Keyword=SystemCategory').success(function (data) {
                $scope.CateData = data.Data;
            }).error(function (response) {
                $scope.CateData = [];
            });

            $http.get('/Api/Role').success(function (data) {
                for (var i = 0; i < data.ExtFuns.length; i++) {
                    var _fun = data.ExtFuns[i];
                    for (var k = 0; k < _fun.Permissions.length; k++) {
                        data.ExtFuns[i]['Permissions'][k]['DataRange'] = [];
                        var xmlDoc = $.parseXML(_fun.Permissions[k].Remark);
                        var $xml = $(xmlDoc);
                        $xml.find("item").each(function (o) {
                            var _item = { relationship: $(this).children("relationship").text(), nameID: $(this).children("nameID").text(), name: $(this).children("name").text(), operation: $(this).children("operation").text(), value: $(this).children("value").text() };
                            data.ExtFuns[i]['Permissions'][k]['DataRange'].push(_item);
                        });

                        if (data.ExtFuns[i]['Permissions'][k]['DataRange'].length > 0)
                            data.ExtFuns[i]['Permissions'][k].Tool = true;
                    }
                }
                $scope.ExtFuns = data.ExtFuns;
            }).error(function (response) {
                $scope.ExtFuns = [];
            });

            //返回
            $scope.GoBack = function () {
                $location.url('/Role');
            };

            //保存
            $scope.Save = function () {
                var RoleFunc = [];
                var ExtFuns = $scope.ExtFuns;
                for (var i = 0; i < ExtFuns.length; i++) {
                    var _permiss = ExtFuns[i].Permissions;
                    for (var j = 0; j < _permiss.length; j++) {
                        var _fun = {};
                        _fun.FunctionID = ExtFuns[i].FunctionID;
                        _fun.RoleID = '';
                        _fun.DataRange = '';
                        var _txt = '';
                        for (var k = 0; k < _permiss[j].DataRange.length; k++) {
                            var _range = _permiss[j].DataRange[k];
                            _txt += _range.name + '#' + _range.nameID + '#' + _range.operation + '#' + _range.relationship + '#' + _range.value + ',';
                        }

                        if (_permiss[j].IsDeleted) {
                            _fun.DataRange = _txt;
                            _fun.PermissionValue = _permiss[j].PermissionValue;
                            RoleFunc.push(_fun);
                        }
                    }
                }
                $scope.Info.RoleFuns = RoleFunc;

                $http.post('/Api/Role', { Data: $scope.Info }).success(function (data) {
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

            //MetaDataPicker
            $scope.SelectedMetaDataID = 0;
            $scope.SelectedMetaDataName = "";
            $scope.SetMetaData = function (text, value) {
                $scope.SelectedMetaDataID = value;
                $scope.SelectedMetaDataName = text;
            };

            $scope.SaveMetaDataPicker = function () {
                var _item = { relationship: 'and', nameID: $scope.SelectedMetaDataID, name: $scope.SelectedMetaDataName, operation: '>', value: '' };
                $scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange'].push(_item);
                $("#modalMetaDataPicker").modal("hide");
            };

            $scope.CheckItem = function (pid, id) {
                $scope.TPid = pid;
                $scope.Tid = id;
                $('#modalMetaDataPicker').modal('toggle');
            };

            //移除
            $scope.Remove = function (p, k, i) {
                $scope.ExtFuns[p]['Permissions'][k]['DataRange'].splice(k, 1);
            };

        }
]);

app.controller('RoleViewCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $scope.Info = {};
            $http.get('/Api/Role?roleId=' + $routeParams.roleId).success(function (data) {
                for (var i = 0; i < data.ExtFuns.length; i++) {
                    var _fun = data.ExtFuns[i];
                    for (var k = 0; k < _fun.Permissions.length; k++) {
                        data.ExtFuns[i]['Permissions'][k]['DataRange'] = [];
                        var xmlDoc = $.parseXML(_fun.Permissions[k].Remark);
                        var $xml = $(xmlDoc);
                        $xml.find("item").each(function (o) {
                            var _item = { relationship: $(this).children("relationship").text(), nameID: $(this).children("nameID").text(), name: $(this).children("name").text(), operation: $(this).children("operation").text(), value: $(this).children("value").text() };
                            data.ExtFuns[i]['Permissions'][k]['DataRange'].push(_item);
                        });

                        if (data.ExtFuns[i]['Permissions'][k]['DataRange'].length > 0)
                            data.ExtFuns[i]['Permissions'][k].Tool = true;
                    }
                }
                $scope.Info.RoleID = data.RoleID;
                $scope.Info.RoleName = data.RoleName;
                $scope.Info.SystemCategory = data.SystemCategory + '';
                $scope.Info.Remark = data.Remark;
                $scope.ExtFuns = data.ExtFuns;
            }).error(function (response) {
                $scope.ExtFuns = [];
            });

            $http.get('/Api/Dictionary?Keyword=SystemCategory').success(function (data) {
                $scope.CateData = data.Data;
            }).error(function (response) {
                $scope.CateData = [];
            });

            //返回
            $scope.GoBack = function () {
                //$location.url('/Role');
                history.go(-1)
            };
        }
]);

app.controller('EditRoleCtrl', ['$scope', '$http', '$location', '$routeParams',
        function ($scope, $http, $location, $routeParams) {
            $scope.Info = {};
            $http.get('/Api/Role?roleId=' + $routeParams.roleId).success(function (data) {
                for (var i = 0; i < data.ExtFuns.length; i++) {
                    var _fun = data.ExtFuns[i];
                    for (var k = 0; k < _fun.Permissions.length; k++) {
                        data.ExtFuns[i]['Permissions'][k]['DataRange'] = [];
                        var xmlDoc = $.parseXML(_fun.Permissions[k].Remark);
                        var $xml = $(xmlDoc);
                        $xml.find("item").each(function (o) {
                            var _item = { relationship: $(this).children("relationship").text(), nameID: $(this).children("nameID").text(), name: $(this).children("name").text(), operation: $(this).children("operation").text(), value: $(this).children("value").text() };
                            data.ExtFuns[i]['Permissions'][k]['DataRange'].push(_item);
                        });

                        if (data.ExtFuns[i]['Permissions'][k]['DataRange'].length > 0)
                            data.ExtFuns[i]['Permissions'][k].Tool = true;
                    }
                }
                $scope.Info.RoleID = data.RoleID;
                $scope.Info.RoleName = data.RoleName;
                $scope.Info.SystemCategory = data.SystemCategory + '';
                $scope.Info.Remark = data.Remark;
                $scope.ExtFuns = data.ExtFuns;
            }).error(function (response) {
                $scope.ExtFuns = [];
            });

            $http.get('/Api/Dictionary?Keyword=SystemCategory').success(function (data) {
                $scope.CateData = data.Data;
            }).error(function (response) {
                $scope.CateData = [];
            });

            //返回
            $scope.GoBack = function () {
                $location.url('/Role');
            };

            //保存
            $scope.Save = function () {
                var RoleFunc = [];
                var ExtFuns = $scope.ExtFuns;
                for (var i = 0; i < ExtFuns.length; i++) {

                    var _permiss = ExtFuns[i].Permissions;
                    for (var j = 0; j < _permiss.length; j++) {
                        var _fun = {};
                        _fun.FunctionID = ExtFuns[i].FunctionID;
                        _fun.RoleID = '';
                        _fun.DataRange = '';
                        var _txt = '';
                        for (var k = 0; k < _permiss[j].DataRange.length; k++) {
                            var _range = _permiss[j].DataRange[k];
                            _txt += _range.name + '#' + _range.nameID + '#' + _range.operation + '#' + _range.relationship + '#' + _range.value + ',';
                        }

                        if (_permiss[j].IsDeleted) {
                            _fun.DataRange = _txt;
                            _fun.PermissionValue = _permiss[j].PermissionValue;
                            RoleFunc.push(_fun);
                        }  
                    }
                }
                $scope.Info.RoleFuns = RoleFunc;

                $http.post('/Api/Role', { Data: $scope.Info }).success(function (data) {
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

            //移除
            $scope.Remove = function (p,k,i) {
                $scope.ExtFuns[p]['Permissions'][k]['DataRange'].splice(k,1);
            };

            //MetaDataPicker
            $scope.SelectedMetaDataID = 0;
            $scope.SelectedMetaDataName = "";
            $scope.SetMetaData = function (text, value) {
                $scope.SelectedMetaDataID = value;
                $scope.SelectedMetaDataName = text;
            };

            $scope.SaveMetaDataPicker = function () {
                var _item = { relationship: 'and', nameID: $scope.SelectedMetaDataID, name: $scope.SelectedMetaDataName, operation: '>', value: '' };
                $scope.ExtFuns[$scope.TPid]['Permissions'][$scope.Tid]['DataRange'].push(_item);
                $("#modalMetaDataPicker").modal("hide");
            };

            $scope.CheckItem = function (pid, id) {
                $scope.TPid = pid;
                $scope.Tid = id;
                $('#modalMetaDataPicker').modal('toggle');
            };
        }
]);