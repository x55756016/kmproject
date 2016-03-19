var app = angular.module('examCategoryApp.controllers', []);

app.controller('examCategoryCtrl', ['$scope', '$http', '$location','$routeParams', 'examcategoriesServices',
            function ($scope, $http, $location, $routeParams, services) {
                var id = $routeParams.id ? $routeParams.id : '';
                var pid = $routeParams.pid ? $routeParams.pid : '';
                if (id != '') {
                    services.get({ ID: id }, function (obj) {
                        $scope.Data = obj.Data;
                        pid = obj.Data.ParentID;
                    });
                }
                $scope.GoBack = function () {
                    $location.path('ExamCategories/pid/' + pid);
                };
                $scope.Save = function () {
                    $scope.Data.ID = id;
                    $scope.Data.ParentID = pid;
                    services.save({ Data: $scope.Data }, function (response) {
                        $scope.GoBack();
                    });
                }
            }
]);
app.controller('examCategoriesCtrl', ['$scope', '$http', '$location', '$routeParams', 'examcategoriesServices',
        function ($scope, $http, $location, $routeParams, services) {
            var pid = $routeParams.pid ? $routeParams.pid : 0;
            $scope.ListItems = [];
            $scope.CurrentPage = 1;
            $scope.ParentID = pid;
            $scope.Search = function () {
                services.get({ Keyword: $scope.Keyword, CurrentPage: $scope.CurrentPage, ParentID: pid,  }, function (obj) {
                    $scope.ListItems = obj.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function (curPage) {
                        $scope.CurrentPage = curPage;
                        $scope.Search();
                    });
                });
            };
            $scope.Search();
            $scope.Add = function () {
                $location.path('ExamCategory/pid/' + pid);
            };
            $scope.Open = function (id) {
                $location.path('ExamCategory/' + id);
            };
            $scope.Remove = function (id) {
                if(confirm('您确定要删除吗？')) {
                    services.remove({ ID: id }, function (response) {
                        $scope.Search();
                    });
                }
            };
            $scope.SubList = function (pid) {
                $location.path('ExamCategories/pid/' + pid);
            }
            $scope.GoBack = function () {
                if (pid != 0) {
                    services.get({ ID: pid }, function (obj) {
                        pid = obj.Data.ParentId;
                        $location.path('ExamCategories/pid/' + pid);
                    });
                }               
            };
        }
]);