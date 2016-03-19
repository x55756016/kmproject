var app = angular.module("TemplateMan", []).controller("TemplateManCtrl", [
    "$scope", "$http", "$location", "$routeParams", "TemplateServices",
    function($scope, $http, $location, $routeParams, templateServices) {
        $scope.CurrentPage = 1;

        $scope.SearchCondition = function() {
            $scope.CurrentPage = 1;
            $scope.Search();
        }

        $scope.Search = function() {
            templateServices.get({ CurrentPage: $scope.CurrentPage, PageSize: 10, Keyword: $scope.Keyword },
                function(obj) {
                    $scope.ListItems = obj.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function(curPage) {
                        $scope.CurrentPage = curPage;
                        $scope.Search();
                    });
                });
        };

        $scope.Search();

        $scope.GetTemplateInfo = function(id) {
            if (id != "") {
                $location.path("TemplateInfo/" + id);
            } else {
                $location.path("TemplateInfo");
            }
        };


        $scope.Remove = function(id) {
            if (id == "") {
                return "";
            }
            if (confirm("确定删除该模板吗？")) {
                templateServices.delete({ id: id }, function() {
                    $scope.Search();
                }, function(errRespose) {
                    alert("发生错误,获取数据失败!");
                    console.log("templateServices.get:" + errRespose.data.Message);
                });
            }
        };
    }
]).controller("TemplateInfoCtrl", [
    "$scope", "$http", "$location", "$routeParams", "TemplateServices", "examitemsServices","examcategoriesServices",
    function($scope, $http, $location, $routeParams, templateServices, examitemsServices, examcategoriesServices) {

        $scope.TemplateID = $routeParams.id == undefined ? "" : $routeParams.id;
        $scope.Template = {};
        $scope.Template.TemplateItems = [];


        $scope.Sort = 0; //排序字段


        $scope.Init = function () {
            if ($scope.TemplateID != "") {
                templateServices.get({ ID: $scope.TemplateID }, function (obj) {
                    if (obj.Data) {
                        $scope.Sort = obj.Data.TemplateItems[obj.Data.TemplateItems.length - 1].SORT;
                    }
                    $scope.Template = obj.Data;
                }, function(errRespose) {
                    alert("发生错误,获取数据失败!");
                    console.log("TemplateServices.get:" + errRespose.data.Message);
                });
            }
        };
        $scope.Init();

        $scope.CurrentPage = 1;
        $scope.SearchCondition = function() {
            $scope.CurrentPage = 1;
            $scope.Search();
        }

        $scope.Search = function() {

                examitemsServices.get({ CurrentPage: $scope.CurrentPage, PageSize: 10, Keyword: $scope.ItemName, ID: $scope.ID },
                    function(obj) {
                        $scope.ListItems = obj.Data;
                        var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function(curPage) {
                            $scope.CurrentPage = curPage;
                            $scope.Search();
                        });
                    }, function(errRespose) {
                        alert("发生错误,获取数据失败!");
                        console.log("examitemsServices.get:" + errRespose.data.Message);
                    });
        };

        var i = 1;
        $scope.AddTemplateItem = {}; //保存新增的项
        $scope.Check = function(idx, row) {
            $('#examItem tr').removeClass('success');
            $($('#examItem tr')[idx + 1]).addClass('success');

            $scope.AddTemplateItem = {};
            var model = {
                ITEMID: i++,
                ITEMCODE: row.ITEMNO,
                ITEMNAME: row.ITEMNAME,
                SORT: ++$scope.Sort,
                VALUETYPE: row.VALUETYPE,
                CODEVALUE: row.CODEVALUE
            };
            $scope.AddTemplateItem = model;
        }

        $scope.LoadCatetoryList = function() {
            examcategoriesServices.get({}, function (obj) {
                $scope.CategoryList = obj.Data;
            });
        };
        $scope.LoadCatetoryList();


        $scope.AddItemForm = function () {
            //console.log($scope.CategoryID);
            //if ($scope.CategoryID == undefined) {
            //    alert("请选择所属栏目");
            //} else {
                var flag = true;
                for (var index = 0; index < $scope.Template.TemplateItems.length; index++) {
                    if ($scope.Template.TemplateItems[index].ITEMCODE == $scope.AddTemplateItem.ITEMCODE) {
                        flag = false;
                        $scope.Sort--;
                        break;
                    }
                }

                if (flag) {
                    $scope.AddTemplateItem.CATEGORYID = $scope.CategoryID;
                    $scope.Template.TemplateItems.push($scope.AddTemplateItem);
                    $('#examItem tr').removeClass('success');

                } else {
                    alert("已添加相同的检查项目");
                }
            //}
        };

        $scope.Remove = function(id) {
            if (id == "") {
                return "";
            }
            for (var index = 0; index < $scope.Template.TemplateItems.length; index++) {
                if ($scope.Template.TemplateItems[index].ITEMID == id) {
                    $scope.Template.TemplateItems.splice(index, 1);
                    break;
                }
            }
        };

        $scope.SaveTemplate = function () {
            if ($scope.TemplateID == "") {
                $scope.Template.TEMPLATEID = "";
                $scope.Template.TEMPLATENAME = $scope.Template.TEMPLATENAME;
                $scope.Template.DESCRIPTION = $scope.Template.DESCRIPTION;
            }
            //console.log(JSON.stringify($scope.Template));
            templateServices.save({ Data: $scope.Template }, function () {
                $scope.Back();
            });
        };

        $scope.Back = function() {
            $location.path("TemplateMan");
        };
    }
]);