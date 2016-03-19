var app = angular.module("MyHouseKeeper", []);

app.controller('MyHouseKeeperCtrl', ['$scope', '$http', '$location', '$routeParams', 'MyHouseKeeperServices','MyQuestionServices',
    function ($scope, $http, $location, $routeParams, MyHouseKeeperServices, MyQuestionServices) {
        $scope.pageIndex=0;
        $scope.Search = function () {
            $http.get("Api/MyQuestion?keyword=" + escape($scope.keyWord == null || $scope.keyWord == undefined ? "" : $scope.keyWord) + "&pageIndex=" + $scope.pageIndex).success(function (data) {
                $scope.QuestionList = data.Data;
                var pager = new Pager('pager', $scope.pageIndex, data.PagesCount, function (curPage) {
                    $scope.pageIndex = parseInt(curPage, 10);
                    $scope.Search();
                });
            });
        }
        MyHouseKeeperServices.get({}, function (data) {
            $scope.MyHouseKeeperList = data.Data;
        });
        $scope.Search();
        $scope.SendQuestion = function (m) {
            $scope.CRUDQuestion = { ObjectUserID: m.ObjectUserID, ObjectType: m.ObjectType, ObjectLoginName: m.ObjectLoginName,UserID:m.UserID,LoginName:m.LoginName };
            $("#modalQuestion").modal('show');
        };
        $scope.SaveQuestion = function (q) {
            MyQuestionServices.save({ Data: $scope.CRUDQuestion }, function (data) {
                $scope.keyWord = "";
                $scope.Search();
                $("#modalQuestion").modal('hide');
            });
        };
        
       
    }
]);

