var app = angular.module("GeneDic.Ctrls", []);

app.controller('GeneCtrl', ['$scope', '$http', '$location', '$routeParams', 'GeneServices',
    function ($scope, $http, $location, $routeParams, GeneServices) {
        $scope.genePageIndex = 1;
        $scope.Search = function () {
            $http.get("/api/gene?pageIndex=" + $scope.genePageIndex + "&keyWord=" + ($scope.keyWord == null ? "" : $scope.keyWord)).success(function (data) {
                $scope.GeneList = $scope.Clone(data.Data);
                $scope.$apply();
                //$scope.GeneJson = angular.toJson($scope.GeneList);
                var genePager = new Pager('genePager', $scope.genePageIndex, data.PagesCount, function (curPage) {
                    $scope.genePageIndex = parseInt(curPage,10);
                    $scope.Search();
                });
            });
        };

        $scope.Search();

        $scope.AddGene = function () {
            $scope.CRUDGene = {};
            $("#modalEditGene").modal("toggle");
        };

        $scope.SaveGene = function () {
            //GeneServices.save({ Data: $scope.CRUDGene, id: $scope.CRUDGene.ID }
            //, function (data) {
            //    $scope.Search();
            //    $("#modalEditGene").modal("toggle");
            //});
            $http.post('/Api/Gene' + ($scope.CRUDGene.ID == null ? "" : "?ID=" + $scope.CRUDGene.ID), { Data: $scope.CRUDGene }).success(function (data) {
                $scope.Search();
                $("#modalEditGene").modal("toggle");
            });
        };

        $scope.EditGene = function (g) {
            $scope.CRUDGene = $scope.Clone(g);
            $("#modalEditGene").modal("toggle");
        };

        $scope.DeleteGene = function (g) {
            if (confirm('确定删除？')) {
                GeneServices.remove({ id: g.ID }
                , function (data) {
                    $scope.Search();
                });
            }
        };

        $scope.EditGeneAllele = function (id) {
            $location.url("GeneAllele/" + id);
        };

        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
    }
]);