var app = angular.module("AccountRecord", []);

app.controller('AccountRecordCtrl', ['$scope', '$http', '$location', '$routeParams', 'AccountRecordServices',
    function ($scope, $http, $location, $routeParams, AccountRecordServices) {
        $scope.lastThreeMonth = "0";
        $scope.FormateDate = function (date) {
            return date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        };
        $scope.Search = function () {
            var today = new Date();
            var lastThreeMonthDate = new Date(today.getFullYear(), (today.getMonth()) - 3, today.getDate());
            var startDate = lastThreeMonthDate;
            var endDate = today;
            if ($scope.lastThreeMonth == "1") {
                startDate = new Date(1900, 1, 1);
                endDate = lastThreeMonthDate;
            }
            $http.get("Api/AccountRecord?startDate=" + $scope.FormateDate(startDate) + "&endDate=" + $scope.FormateDate(endDate)).success(function (data) {
                $scope.AccountRecordList = data.Data;
            });
        };
        $scope.Search();
       
    }
]);