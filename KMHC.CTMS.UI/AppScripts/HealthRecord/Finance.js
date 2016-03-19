
var app = angular.module('finance', []);

app.controller('financeCtrl', ['$scope', '$http', '$location', '$routeParams', 'financeServices',
    function ($scope, $http, $location, $routeParams, financeServices) {
        $scope.AnComeList = [
            { Text: "5万以下", Value: "5万以下" },
            { Text: "5万-10万", Value: "5万-10万" },
            { Text: "10万-20万", Value: "10万-20万" },
            { Text: "20万-30万", Value: "20万-30万" },
            { Text: "30万以上", Value: "30万以上" }
        ];
        $scope.Data = {};
        $http.get('/api/finance?Id='+$routeParams.uid ).success(function (obj) {
            if(obj!=null){
                $scope.Data = obj;
            }
            else {
                $scope.Data.PERSONID = $routeParams.uid;
            }
        });
        $scope.Save = function () {
            financeServices.save({ Data: $scope.Data }, function (response) {
                alert("保存成功！");
            });
        }
    }
]);
