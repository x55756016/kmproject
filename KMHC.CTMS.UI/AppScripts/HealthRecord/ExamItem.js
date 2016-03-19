var app = angular.module('examItemApp.controllers', []);

app.controller('examItemCtrl', ['$scope', '$http', '$location', '$routeParams', 'examitemsServices',
        function ($scope, $http, $location, $routeParams, services) {
            var id = $routeParams.id ? $routeParams.id : '';
            if (id != '') {
                services.get({ ID: id }, function (obj) {
                    $scope.Data = obj.Data;
                });
            }

            $scope.Save = function () {
                $scope.Data.ID = id;
                services.save({ Data: $scope.Data }, function (response) {
                    $scope.GoBack();
                });
            }

            $scope.GoBack = function () {
                $location.path('ExamItems');
            };
        }
]);


app.controller('examItemsCtrl', ['$scope', '$http', '$location', '$routeParams', 'examitemsServices',
          function ($scope, $http, $location, $routeParams, services) {
              $scope.ListItems = [];
              $scope.CurrentPage = 1;
              $scope.Search = function () {
                  services.get({ Keyword: $scope.Keyword, CurrentPage: $scope.CurrentPage }, function (obj) {
                      $scope.ListItems = obj.Data;
                      var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function (curPage) {
                          $scope.CurrentPage = curPage;
                          $scope.Search();
                      });
                  });
              };
              $scope.Search();

              $scope.Add = function () {
                  $location.path('ExamItem');
              };

              $scope.Open = function (id) {
                  $location.path('ExamItem/id/' + id);
              };

              $scope.Remove = function (id) {
                  if (confirm('您确定要删除吗？')) {
                      services.remove({ ID: id }, function (response) {
                          $scope.Search();
                      });
                  }
              };
          }
]);



