//var app = angular.module("GeneDic.Ctrls", []);

app.controller('GeneAlleleCtrl', ['$scope', '$http', '$location', '$routeParams', 'GeneServices', 'GeneAlleleServices',
    function ($scope, $http, $location, $routeParams, GeneServices, GeneAlleleServices) {
        $scope.geneAllelePageIndex = 1;
        $scope.Search = function () {
            $http.get("/api/geneallele?pageIndex=" + $scope.geneAllelePageIndex + "&geneID=" + $scope.GeneID).success(function (data) {
                $scope.GeneAlleleList = $scope.Clone(data.Data);
                $scope.$apply();
                //$scope.GeneJson = angular.toJson($scope.GeneList);
                var geneAllelePager = new Pager('geneAllelePager', $scope.geneAllelePageIndex, data.PagesCount, function (curPage) {
                    $scope.geneAllelePageIndex = parseInt(curPage,10);
                    $scope.Search();
                });
            });
        };
        //$scope.Search = function () {
        //    GeneAlleleServices.get({ keyWord: $scope.GeneID }
        //        , function (data) {
        //            $scope.GeneAlleleList = data.Data;
        //        }, function (errResponse) {
        //            alert("发生错误,获取数据失败!")
        //            console.log("GeneAlleleServices.get:" + errRespose.data.Message);
        //        });
        //};

        $scope.Init = function () {
            $scope.GeneID = $routeParams.id;
            GeneServices.get({ ID: $scope.GeneID }
                , function (data) {
                    $scope.Gene = data.Data;
                });
            $scope.Search();
        };

        $scope.Init();

        $scope.AddGeneAllele = function () {
            $scope.CRUDGeneAllele = { GeneID: $scope.GeneID };
            if ($scope.CRUDGeneAllele.ID == null) $scope.CRUDGeneAllele.GeneAlleleName = $scope.Gene.GeneName + " ";
            $("#modalEditGeneAllele").modal("toggle");
        };

        $scope.SaveGeneAllele = function () {
            GeneAlleleServices.save({ Data: $scope.CRUDGeneAllele }
            , function (data) {
                $scope.Search();
                $("#modalEditGeneAllele").modal("toggle");
            }, function (errRespose) {
                alert("发生错误,保存失败!")
                console.log("GeneAlleleServices.save:" + errRespose.data.Message);
            });
        };

        $scope.EditGeneAllele = function (g) {
            $scope.CRUDGeneAllele = $scope.Clone(g);
            $("#modalEditGeneAllele").modal("toggle");
        };

        $scope.DeleteGeneAllele = function (g) {
            if (confirm('确定删除？')) {
                GeneAlleleServices.remove({ id: g.ID }
                , function (data) {
                    $scope.Search();
                }, function (errRespose) {
                    alert("发生错误,删除失败!")
                    console.log("GeneAlleleServices.remove:" + errRespose.data.Message);
                });
            }
        };

        $scope.EditGeneAlleleLocus = function (id) {
            $location.url("GeneAlleleLocus/" + id);
        };

        $scope.BackToGeneList = function () {
            $location.url("Gene");
        };

        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
    }]);