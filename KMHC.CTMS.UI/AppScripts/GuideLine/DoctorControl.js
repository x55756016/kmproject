var app = angular.module("DoctorControlFlow", []).controller("DoctorControlFlowCtrl", [
    "$scope", "$http", "$location", "$routeParams", "DoctorControlServices","GuideLineServices",
    function ($scope, $http, $location, $routeParams, doctorControlServices, guideLineServices) {

        //加载GuideLine选项
        $scope.CurrentPage = 1;
        $scope.SearchCondition = function() {
            $scope.CurrentPage = 1;
            $scope.Search();
        };

        $scope.Search = function() {
            doctorControlServices.get({ CurrentPage: $scope.CurrentPage, PageSize: 10, Keyword: $scope.Keyword },
                function(obj) {
                    $scope.ListItems = obj.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function(curPage) {
                        $scope.CurrentPage = curPage;
                        $scope.Search();
                    });
                });
        };
        $scope.Search();

        $scope.ResetCondition = function() {
            $scope.Keyword = "";
        };

        //加载GuideLine
        $scope.GuideLineList = {};
        $scope.Info = {};
        $scope.Edit = function (model) {
            //console.log(JSON.stringify(model)); 
            $scope.Info = $scope.Clone(model);
            $http.get("/GuideLine/GetGuideLineChild?guidelineCode=" + model.CURRENTNODE).success(
                function (obj) {
                    //console.log(JSON.stringify(obj));

                    $scope.GuideLineList = obj;

                    //console.log(JSON.stringify($scope.GuideLineList));

                }).error(function(errorResponse) {
                alert("发生错误,查询失败!");
                console.log("$http.get.UserManage:" + errorResponse.data.Message);
            });
        }

        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };

    }
]);