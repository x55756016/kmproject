var app = angular.module("GuideLineFlow", []).controller("GuidLineFlowCtrl", [
    "$scope", "$http", "$location", "$routeParams", "GuideLineFlowServices",
    function ($scope, $http, $location, $routeParams, guideLineFlowServices) {
        $scope.CurrentPage = 1;


        $scope.SearchCondition = function() {
            $scope.CurrentPage = 1;
            $scope.Search();
        }

        $scope.Search = function() {
            guideLineFlowServices.get({ CurrentPage: $scope.CurrentPage, PageSize: 10, Keyword: $scope.Keyword },
                function(obj) {
                    $scope.ListItems = obj.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function(curPage) {
                        $scope.CurrentPage = curPage;
                        $scope.Search();
                    });
                });
        };

        $scope.Search();

        //$scope.GetTemplateInfo = function(id) {
        //    if (id != "") {
        //        $location.path("TemplateInfo/" + id);
        //    } else {
        //        $location.path("TemplateInfo");
        //    }
        //};

        $scope.ResetCondition = function() {
            $scope.Keyword = "";
        };

        $scope.Add=function() {
            window.location.href = "/Guideline/index#/Index?fid=";
        }

        $scope.Remove = function(id) {
            if (id == "") {
                return "";
            }
            if (confirm("确定删除该流程吗？")) {
                guideLineFlowServices.delete({ id: id }, function() {
                    $scope.Search();
                }, function(errRespose) {
                    alert("发生错误,获取数据失败!");
                    console.log("guideLineFlowServices.get:" + errRespose.data.Message);
                });
            }
        };

        $scope.CheckRow = function (idx) {
            var tr = $('table.table tbody tr');
            tr.removeClass('success');
            $(tr[idx]).addClass('success');
        }
    }
]);