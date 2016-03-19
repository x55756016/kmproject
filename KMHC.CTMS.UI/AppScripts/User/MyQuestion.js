var app = angular.module("MyQuestion", []);
app.controller('MyQuestionCtrl', ['$scope', '$http', '$location', '$routeParams', 'MyHouseKeeperServices', 'MyQuestionServices',
    function ($scope, $http, $location, $routeParams, MyHouseKeeperServices, MyQuestionServices) {
        $scope.Init = function () {
            $scope.modelID = $routeParams.ModelId;
            $scope.eventID = $routeParams.id;
            $scope.Search();
        };
        $scope.Search = function () {
            MyQuestionServices.get({ ID: $scope.modelID }, function (data) {
                $scope.CRUDQuestion = data.Data;
            });
        }
        $scope.Init();
        $scope.SaveQuestion = function (q) {
            $http.post("Api/MyQuestion?eventID=" + $scope.eventID, { Data: $scope.CRUDQuestion }).success(function (data) {
                $scope.Search();
            });
        };


    }
]);