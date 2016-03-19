var app = angular.module("UserManageCtrl", []);
///元数据
app.controller('UserManageCtrl', ['$scope', '$http', '$location', '$routeParams', 'DictionaryServices','UserManageServices','UserTypeServices',
    function ($scope, $http, $location, $routeParams, DictionaryServices, UserManageServices, UserTypeServices) {
        $scope.Init = function () {
            $scope.CurrentPage = 1;
            $scope.s_Name = "";
            $scope.CRUDUserRole = { AllSelected: false };
            $scope.CRUDUser = {};



            //开始用户选择后的回调
            $scope.CurrentUserSearcher = {};//当前选择用户的model，这个名称可随意
            $scope.FinishUserSearch = function (searcher_user) {
                $scope.CRUDUser[$scope.CurrentUserSearcher] = searcher_user;
            };
            $scope.ChoiceDoctor = function () {
                $scope.CurrentUserSearcher = 'MyDoctor';
            };
            $scope.ChoiceService = function () {
                $scope.CurrentUserSearcher = 'MyService';
            }
            //结束用户选择后的回调



            DictionaryServices.get({ keyWord: "UserStatus" }
                , function (data) {
                    $scope.DicUserStatusList = data.Data
                });
            DictionaryServices.get({ keyWord: "UserType" }
              , function (data) {
                  $scope.DicUserTypeList = data.Data
              });

            $http.get("/api/Role?keyWord=&key=").success(
                function (data) {
                    $scope.RoleList = data.Data;

                }).error(function (errorResponse) {
                    alert("发生错误,查询失败!")
                    console.log("$http.get.Role:" + errorResponse.data.Message);
                });
            $scope.Search();

        };
       
        $scope.Search = function () {
            $scope.loading = true;
            UserManageServices.get({ pageIndex: $scope.CurrentPage, keyWord: $scope.s_Name == undefined ? "" : $scope.s_Name }
                , function (data) {
                    $scope.UserList = data.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                        $scope.CurrentPage = parseInt(curPage);
                        $scope.Search();
                    });
                }, function (errorResponse) {
                    alert("发生错误,查询失败!")
                    console.log("UserManageServices.get:" + errorResponse.data.Message);
                });
            $scope.loading = false;
        };
        $scope.Init();
        $scope.Reset = function () {
            $scope.s_Name = "";
        };
        $scope.EditUser = function (u) {
            $scope.CRUDUser = $scope.Clone(u);
            //$("#userType").combobox();
            $("#modalUserEdit").modal("show");

        };
        $scope.SaveUser = function () {
            $scope.loading = true;

            console.log(JSON.stringify($scope.CRUDUser));


            $http.post("/api/UserManage", { Data: $scope.CRUDUser }).success(
                function (data) {
                    $scope.Search();
                    $("#modalUserEdit").modal("hide");
                }).error(function (errorResponse) {
                    alert("发生错误,查询失败!")
                    console.log("$http.get.UserManage:" + errorResponse.data.Message);
                });
            $scope.loading = false;
        };
        $scope.EditUserRole = function (u) {
            $scope.CRUDUserRole.User = $scope.Clone(u);
            $scope.CRUDUserRole.Roles = $scope.Clone($scope.RoleList);
            $scope.CRUDUserRole.AllSelected = false;
            $http.get("/api/UserRole?uid=" + u.UserId).success(
                function (data) {
                   if (data.Data != null && data.Data.length > 0)
                   {
                       angular.forEach(data.Data, function (ur) {
                           angular.forEach($scope.CRUDUserRole.Roles, function (r) {
                               if (r.RoleID == ur.RoleID)
                               {
                                   r.IsSelected = true;
                               }
                           });
                       });
                   }
               }).error(function (errorResponse) {
                   alert("发生错误,查询失败!")
                   console.log("$http.get.UserManage:" + errorResponse.data.Message);
               });
            $("#modalUserRoleEdit").modal("show");
        };
       
        $scope.SaveUserRole = function () {
            $scope.loading = true;
            var UserRoleList=$.grep($scope.CRUDUserRole.Roles, function (r) {
                return r.IsSelected != null && r.IsSelected == true;
            });
            $http.post("/api/UserRole", { Data: UserRoleList, ID: $scope.CRUDUserRole.User.UserId }).success(
                function (data) {
                    $("#modalUserRoleEdit").modal("hide");
                }).error(function (errorResponse) {
                    alert("发生错误,查询失败!")
                    console.log("$http.get.UserRole:" + errorResponse.data.Message);
                });
            $scope.loading = false;
        };
        $scope.ToggleAllRole = function () {
                angular.forEach($scope.CRUDUserRole.Roles, function (r) {
                    r.IsSelected = $scope.CRUDUserRole.AllSelected;
                });
        };

        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
    }
]);

app.controller('UserTypeCtrl', ['$scope', '$http', '$location', '$routeParams', 'UserTypeServices','RoleServices','DictionaryServices',
    function ($scope, $http, $location, $routeParams, UserTypeServices, RoleServices, DictionaryServices) {
        $scope.UserType = [
            { Text: "医生", Value: 1 },
            { Text: "患者", Value: 2 },
            { Text: "医学编辑", Value: 4 },
            { Text: "客服", Value: 5 }
        ];
        DictionaryServices.get({ keyWord: "UserType" }
              , function (data) {
                  $scope.UserType = data.Data
        });
        $scope.loading = false;
        $scope.UserTypeRoleDetail = {};
        $scope.UserTypeRoleDetail.UserType = 2;

        $scope.InitUserRole = function (userType) {
            $scope.loading = true;
            UserTypeServices.get({ Keyword: userType }, function (data) {
                $scope.RoleList = data.Data;
                $scope.loading = false;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("UserTypeServices.get:" + errRespose.data.Message);
                $scope.loading = false;
            });
        };
        $scope.InitUserRole($scope.UserTypeRoleDetail.UserType);

        $scope.Refresh = function () {
            window.location.reload();
        };

        $scope.AddUserRole = function () {
            RoleServices.get({ CurrentPage: 1 }, function (data) {
                $scope.UserRoles = data.Data;
                //取已有数据
                if ($scope.RoleList.length != 0) {
                    $scope.UserTypeRoleDetail.RoleId = $scope.RoleList[0].RoleId;
                    $scope.UserTypeRoleDetail.UserTypeRoleId = $scope.RoleList[0].UserTypeRoleId;
                } else {
                    $scope.UserTypeRoleDetail.RoleId = '';
                    $scope.UserTypeRoleDetail.UserTypeRoleId = '';
                }

            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("RoleServices.get:" + errRespose.data.Message);
                $scope.loading = false;
            });
        };

        $scope.SaveUserTypeRole = function () {
            $scope.loading = true;
            UserTypeServices.save({ Data: $scope.UserTypeRoleDetail }, function (response) {
                $(".close").trigger("click");
                $scope.InitUserRole($scope.UserTypeRoleDetail.UserType);
                $scope.loading = false;
            }, function (errRespose) {
                alert("发生错误,保存失败!")
                console.log("UserTypeServices.post:" + errRespose.data.Message);
                $scope.loading = false;
            });
        };

        $scope.ChoiceRow = function (model) {
            $scope.UserTypeRoleDetail.RoleId = model.RoleID;
        };

        $scope.DeleteUserRole = function () {
            if (!confirm("确定删除？"))
                return;
            UserTypeServices.delete({ Keyword: $scope.UserTypeRoleDetail.UserType }, function (response) {
                $scope.InitUserRole($scope.UserTypeRoleDetail.UserType);
            }, function (errResponse) {
                alert("发生错误,保存失败!")
                console.log("UserTypeServices.delete:" + errRespose.data.Message);
            });
        };

        $scope.UserRoleFunction = function (roleId) {
            $location.url("/RoleView?roleId=" + roleId);
        };
    }]);