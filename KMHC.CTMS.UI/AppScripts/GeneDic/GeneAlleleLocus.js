app.controller('GeneAlleleLocusCtrl', ['$scope', '$http', '$location', '$routeParams', 'GeneServices', 'GeneAlleleServices', 'GeneAlleleLocusServices',
    function ($scope, $http, $location, $routeParams, GeneServices, GeneAlleleServices, GeneAlleleLocusServices) {
        $scope.geneAlleleLocusPageIndex = 1;
        $scope.Search = function () {
            $http.get("/api/geneallelelocus?pageIndex=" + $scope.geneAlleleLocusPageIndex + "&geneAlleleID=" + $scope.GeneAlleleID).success(function (data) {
                $scope.GeneAlleleLocusList = $scope.Clone(data.Data);
                $scope.$apply();
                //$scope.GeneJson = angular.toJson($scope.GeneList);
                var geneAlleleLocusPager = new Pager('geneAlleleLocusPager', $scope.geneAlleleLocusPageIndex, data.PagesCount, function (curPage) {
                    $scope.geneAlleleLocusPageIndex = parseInt(curPage,10);
                    $scope.Search();
                });
            });
        };
        //$scope.Search = function () {
        //    GeneAlleleLocusServices.get({ keyWord: $scope.GeneAlleleID }
        //        , function (data) {
        //            $scope.GeneAlleleLocusList = data.Data;
        //        }, function (errResponse) {
        //            alert("发生错误,获取数据失败!")
        //            console.log("GeneAlleleLocusServices.get:" + errResponse.data.Message);
        //        });
        //};

        $scope.Init = function () {
            $scope.GeneAlleleID = $routeParams.id;
            GeneAlleleServices.get({ ID: $scope.GeneAlleleID }
               , function (data) {
                   $scope.GeneAllele = data.Data;
                   //获取基因信息
                   GeneServices.get({ ID: $scope.GeneAllele.GeneID }
                   , function (data) {
                       $scope.Gene = data.Data;
                   }, function (errResponse) {
                       alert("发生错误,获取数据失败!")
                       console.log("GeneServices.get:" + errResponse.data.Message);
                   });
               }, function (errResponse) {
                   alert("发生错误,获取数据失败!")
                   console.log("GeneAlleleServices.get:" + errResponse.data.Message);
               });
            $scope.Search();
        };

        $scope.Init();

        $scope.AddGeneAlleleLocus = function () {
            $scope.CRUDGeneAlleleLocus = { GeneAlleleID: $scope.GeneAlleleID};
            $("#modalEditGeneAlleleLocus").modal("toggle");
        };

        $scope.SaveGeneAlleleLocus = function () {
            GeneAlleleLocusServices.save({ Data: $scope.CRUDGeneAlleleLocus }
            , function (data) {
                $scope.Search();
                $("#modalEditGeneAlleleLocus").modal("toggle");
            }, function (errResponse) {
                alert("发生错误,保存失败!")
                console.log("GeneAlleleLocusServices.save:" + errResponse.data.Message);
            });
        };

        $scope.EditGeneAlleleLocus = function (g) {
            $scope.CRUDGeneAlleleLocus = $scope.Clone(g);
            $("#modalEditGeneAlleleLocus").modal("toggle");
        };

        $scope.DeleteGeneAlleleLocus = function (g) {
            if (confirm('确定删除？')) {
                GeneAlleleLocusServices.remove({ id: g.ID }
                , function (data) {
                    $scope.Search();
                }, function (errResponse) {
                    alert("发生错误,删除失败!")
                    console.log("GeneAlleleLocusServices.remove:" + errResponse.data.Message);
                });
            }
        };

        $scope.BackToGeneAlleleList = function () {
            $location.url("GeneAllele/" + $scope.Gene.ID);
        };

        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
    }]);