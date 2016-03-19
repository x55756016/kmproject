var app = angular.module('MedicalArrange', []);

app.controller('MedicalArrangeCtrl', ['$scope', '$http', '$filter', '$location', '$routeParams', 'MedicalArrangeServices',
    function ($scope, $http, $filter, $location, $routeParams, MedicalArrangeServices) {
        //查询数据
        $scope.Infos = [];
        $scope.CurrentPage = 1;
        $scope.Search = function () {
            MedicalArrangeServices.get({CurrentPage: $scope.CurrentPage,Date:$scope.Date,Name:$scope.Name }, function (data) {
                $scope.Infos = data.Data;
                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            });
        };

        //返回
        $scope.GoBack = function () {
            $location.url('CancerRecord');
        };

        //搜索
        $scope.SearchCondition = function () {
            $scope.CurrentPage = 1;
            $scope.Search();
        };

        //重置
        $scope.ResetSearch = function () {
            $scope.Date = null;
            $scope.Name = null;
        };

        //查看编辑
        $scope.Edit = function (obj) {
            $location.url('/MedicalArrange?hid=' + obj.HISTORYID);
        };

        $scope.Search();//页面加载完毕时载入数据
    }
]);

app.controller('ArrangeEditCtrl', ['$scope', '$http', '$filter', '$location', '$routeParams', 'MedicalArrangeServices',
    function ($scope, $http, $filter, $location, $routeParams, MedicalArrangeServices) {
        //查询数据
        $scope.Infos = [];
        $http.get('/api/MedicalArrange?Id='+$routeParams.hid).success(function (data) {
            $scope.Infos = data;
        });

        //返回
        $scope.GoBack = function () {
            $location.url('/MedicalCenterArrangement');
        };

        //保存
        $scope.Save = function () {
            $http.post('/api/MedicalArrange', { Data: $scope.Infos }).success(function (data) {
                if (data == 'ok') {
                    KMAlert({
                        msg: '保存成功！'
                    });
                } else {
                    KMAlert({
                        msg: '保存失败！'
                    });
                }
            });
        }
    }
]);