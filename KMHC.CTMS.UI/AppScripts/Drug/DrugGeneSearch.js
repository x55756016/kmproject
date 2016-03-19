var app = angular.module("DrugGeneSearch", []).controller("DrugGeneSearchCtrl", [
    "$scope", "$http", "$location", "$routeParams", "DrugGeneSearchServices",
    function ($scope, $http, $location, $routeParams, DrugSearchServices) {
        $scope.DbId = "";
        $scope.DrugName = "";
        $scope.IsShow = false;
        $scope.loading = false;

        $scope.Search = function () {
            if ($scope.DbId == "") {
                alert("需要先搜索药品");
                return;
            }
            $scope.loading = true;
            DrugSearchServices.get({ dbId: $scope.DbId }, function (obj) {
                $scope.IsShow = obj.Data == "" ? true : false;
                $scope.ListItem = obj.Data;
                $scope.loading = false;
            });
        };

        $scope.$on('summon', function (e, id, name) {
            $scope.DbId = id;
            $scope.DrugName = name;
            $scope.Search();
        });
    }
]).controller("DrugInterationSearchCtrl", [
    "$scope", "$http", "$location", "$routeParams", 'DrugInterSearchServices',
function ($scope, $http, $location, $routeParams ,DrugInterSearchServices) {

        $scope.DbId = "";
        $scope.DrugName = "";
        $scope.IsShow = false;
        $scope.loading = false;

        $scope.Search = function () {
            if ($scope.DbId == "") {
                alert("需要先搜索药品");
                return;
            }
            $scope.loading = true;

            DrugInterSearchServices.get({ dbId: $scope.DbId }, function (obj) {
                $scope.IsShow = obj.Data == "" ? true : false;
                $scope.ListItem = obj.Data;
                $scope.loading = false;
            });
        };

        $scope.$on('summon', function (e, id, name) {
            $scope.DbId = id;
            $scope.DrugName = name;
            $scope.Search();
        });
    }
]);