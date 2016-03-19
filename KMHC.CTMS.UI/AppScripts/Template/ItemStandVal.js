var app = angular.module("ItemStandVal", []).controller("ItemStandValCtrl", [
    "$scope", "$http", "$location", "$routeParams", "ItemStandValServices","examitemsServices",
    function($scope, $http, $location, $routeParams, itemStandValServices, examitemsServices) {
        $scope.CurrentPage = 1;
        $scope.SearchCondition = function() {
            $scope.CurrentPage = 1;
            $scope.Search();
        };

        $scope.Search = function() {
            itemStandValServices.get({ CurrentPage: $scope.CurrentPage, PageSize: 10, Keyword: $scope.Keyword },
                function(obj) {
                    $scope.ListItems = obj.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function(curPage) {
                        $scope.CurrentPage = curPage;
                        $scope.Search();
                    });
                });
        };
        $scope.Search();

        //窗体数据加载
        $scope.CurrentPageForWindow = 1;
        $scope.SearchConditionForWindow = function() {
            $scope.CurrentPageForWindow = 1;
            $scope.SearchForWindow();
        };

        $scope.SearchForWindow = function() {
            examitemsServices.get({ CurrentPage: $scope.CurrentPage, PageSize: 10, Keyword: $scope.ItemName, ID: $scope.ID },
                function(obj) {
                    $scope.ListItemsForWindows = obj.Data;
                    var pager = new Pager('pager2', $scope.CurrentPage, obj.PagesCount, function(curPage) {
                        $scope.CurrentPageForWindow = curPage;
                        $scope.SearchForWindow();
                    });
                }, function(errRespose) {
                    alert("发生错误,获取数据失败!");
                    console.log("examitemsServices.get:" + errRespose.data.Message);
                });
        };

        $scope.AddItem = {};

        $scope.Check = function(idx, row) {
            $('#examItem tr').removeClass('success');
            $($('#examItem tr')[idx + 1]).addClass('success');
            var model = {
                ITEMNO: row.ITEMNO
            };
            $scope.AddItem = model;
        }

        $scope.AddItemFunction = function() {
            if ($scope.AddItem.ITEMNO == undefined) {
                alert("请选择检查项");
            } else {
                console.log(JSON.stringify($scope.AddItem));
                itemStandValServices.save({ Data: $scope.AddItem },
                    function(obj) {
                        if (obj.Succeeded) {
                            $scope.AddItem = {};
                            $('#examItem tr').removeClass('success');
                            $scope.SearchCondition();
                            alert("添加成功");
                        } else {
                            alert(obj.Error);
                        }
                    }, function(errRespose) {
                        alert("发生错误,获取数据失败!");
                        console.log("examitemsServices.get:" + errRespose.data.Message);
                    });
            }
        }

        $scope.Remove = function(id) {
            if (id == "") {
                return "";
            }
            itemStandValServices.delete({ id: id }, function(obj) {
                if (obj.Succeeded) {
                    $("#addItem").hide();
                    $scope.SearchCondition();
                }
            });
        }


        $scope.EditItem = {};
        $scope.Edit = function(id) {
            $scope.EditItem = {};
            itemStandValServices.get({ id: id },
                function (obj) {
                    if (obj) {
                        //alert(JSON.stringify(obj.Data));
                        $scope.EditItem = obj.Data;
                    }
                }, function(errRespose) {
                    alert("发生错误,获取数据失败!");
                    console.log("examitemsServices.get:" + errRespose.data.Message);
                });
        }

        $scope.EditItemFunction = function () {
            itemStandValServices.save({ Data: $scope.EditItem },
                function(obj) {
                    if (obj.Succeeded) {
                        $scope.SearchCondition();
                    } else {
                        alert(obj.Error);
                    }
                }, function(errRespose) {
                    alert("发生错误,保存数据失败!");
                    console.log("examitemsServices.get:" + errRespose.data.Message);
                });
        }
    }
]);