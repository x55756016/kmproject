var app = angular.module('Products', []);

app.controller("ProductsListCtrl", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "ProductsServices","DictionaryServices",
    function ($scope, $http, $timeout, $routeParams, $location, ProductsServices, DictionaryServices) {
        $scope.loading = false;

        DictionaryServices.get({ Keyword: "ProductType" }, function (obj) {
            if (obj) {
                $scope.ProductType = obj.Data[0].nodes;
            }
        });
        DictionaryServices.get({ Keyword: "ProductUnit" }, function (obj) {
            if (obj) {
                $scope.ProductUnit = obj.Data[0].nodes;
            }
        });
        $scope.convertToInt = function (id) {
            return parseInt(id);
        };
        /*$scope.ProductType = [
            { Text: "产品服务", Value: 1 },
            { Text: "医疗服务", Value: 2 },
            { Text: "检查服务", Value: 3 }
        ];*/
        $scope.IsFree = [
            { Text: "是", Value: 1 },
            { Text: "否", Value: 0 }
        ];
        /*$scope.ProductUnit = [
            { Text: "次", Value: 1 },
            { Text: "年", Value: 2 },
            { Text: "月", Value: 3 },
            { Text: "元/小时", Value: 4 },
            { Text: "4次*元/年", Value: 5 },
            { Text: "2次", Value: 6 },
            { Text: "30剂 *4次", Value: 7 },
            { Text: "元/剂", Value: 8 }
        ];*/

        $scope.Search = function () {
            ProductsServices.get({}, function (data) {
                $scope.Data = data.Data;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("ProductServices.get:" + errRespose.data.Message);
            });
        };
        $scope.Search();

        $scope.setProducts = function (p) {
            var obj = {};
            obj.ProductId = p.ProductId;
            obj.ProductType = p.ProductType;
            obj.ProductTypeText = p.ProductTypeText;
            obj.ProductName = p.ProductName;
            obj.IsFree = p.IsFree;
            obj.IsFreeText = p.IsFreeText;
            obj.ProductPrice = p.ProductPrice;
            obj.ProductUnit = p.ProductUnit;
            obj.ProductUnitText = p.ProductUnitText;
            obj.SalePrice = p.SalePrice;
            obj.ProductRemark = p.ProductRemark;
            return obj;
        };

        $scope.EditProducts = function (p) {
            if (p == undefined)
                $scope.ProductModel = {};
            else
                $scope.ProductModel = $scope.setProducts(p);
        };

        $scope.DeleteProducts = function (p) {
            if (!confirm("确定删除？"))
                return;
            ProductsServices.delete({ Keyword: p.ProductId }, function (response) {
                alert(response.Data);
                $scope.Search();
            }, function (errResponse) {
                alert("发生错误,保存失败!")
                console.log("ProductsServices.delete:" + errRespose.data.Message);
            });
        };
        $scope.BuyProducts = function (p) {
            if (confirm("确定购买？")) {
                $http.get("Api/UserInfo").success(function (data) {
                    var account = data.Data.UserInfo.Account;
                    if (p.SalePrice > account) {
                        alert("余额不足，请充值!")
                    }
                    else
                    {
                        //$http.post("Api/AccountRecord", { Data: { ProductID: p.ProductId, ProductName: p.ProductName, Balance: -1, Account: p.SalePrice, SpendType: 1 } }).success(function (data) {
                        //    alert("购买成功!");
                        //}).error(function (errResponse) {
                        //    alert("购买失败!")
                        //});
                        $http.post("Api/MyProduct?productID="+p.ProductId).success(function (data) {
                            alert("购买成功!");
                        }).error(function (errResponse) {
                            alert("购买失败!")
                        });
                    }
                });
            }
        };
        $scope.processForm = function () {
            $scope.loading = true;
            ProductsServices.save({ Data: $scope.ProductModel }, function (response) {
                alert(response.Data);
                $(".close").trigger("click");
                $scope.Search();
                $scope.loading = false;
            }, function (errRespose) {
                alert("发生错误,保存失败!")
                console.log("ProductsServices.post:" + errRespose.data.Message);
                $scope.loading = false;
            });
        };
    }]);