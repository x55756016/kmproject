var app = angular.module("MyProduct", []);
app.controller('MyProductCtrl', ['$scope', '$http', '$location', '$routeParams', 'MyProductServices','UserInfoServices','MemberServices',
    function ($scope, $http, $location, $routeParams, MyProductServices, UserInfoServices, MemberServices) {
        $scope.Init = function () {
            $scope.SearchUser();
            $scope.SearchProduct();
            $scope.SearchMember();
        };
        $scope.SearchProduct = function () {
            MyProductServices.get({}, function (data) {
                $scope.MyProductList = data.Data;
            });
           
        };
        $scope.SearchUser = function () {
            UserInfoServices.get({}, function (data) {
                $scope.CurrentUser = data.Data;
            });
        };
        $scope.SearchMember = function () {
            $http.get("Api/Member?keyWord=").success(function (data) {
                $scope.MemberList = data.Data;
            });
        };
        $scope.BuyMember = function (m) {
            if (confirm("确定购买" + m.MEMBERNAME + "?")) {
                if ($scope.CurrentUser.UserInfo.Account < m.MEMBERPRICE) {
                    alert("余额不足,请充值!");
                }
                else{
                    MyProductServices.save({ Keyword: "BuyMember", ID: m.MEMBERID }, function (data) {
                        $scope.SearchUser();
                        $scope.SearchProduct();
                        alert("你已经成功升级为" + m.MEMBERNAME + "!");
                    });
                }
            }
        };
        $scope.Init();
        //弹出充值提示框
        $scope.ShowRecharge = function () {
            $scope.CRUDRecharge = { Money: 1000 };
            $("#modalRecharge").modal("show");
        };
        //充值
        $scope.SaveRecharge = function () {
            $http.post("Api/AccountRecord", { Data: { ProductID: "", ProductName: "", Balance: 1, Account: $scope.CRUDRecharge.Money, SpendType: 0 } }).success(function (data) {
                $scope.SearchUser();
                $("#modalRecharge").modal("hide");
                alert("充值成功!");
            }).error(function (errResponse) {
                alert("充值失败!")
            });
        };
    }
]);