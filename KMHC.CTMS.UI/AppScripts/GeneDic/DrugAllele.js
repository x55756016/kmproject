app.controller('DrugAlleleCtrl', ['$scope', '$http', '$location', '$routeParams','DrugAlleleServices', 'GeneServices', 'GeneAlleleServices', 'DrugGeneSearchServices',
function ($scope, $http, $location, $routeParams,DrugAlleleServices, GeneServices, GeneAlleleServices, DrugGeneSearchServices) {
    $scope.Search = function (drugBankID) {
        if (drugBankID == null || drugBankID == '') drugBankID = $scope.sDrugBankID;
        DrugAlleleServices.get({ Params: [{Key:"DrugBankID",Value:drugBankID},{Key:"GeneID",Value:$scope.sGeneID}] }
            , function (data) {
                $scope.DrugAllele = data.Data;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("DrugAlleleServices.get:" + errRespose.data.Message);
            });
        };
        
        $scope.Init = function () {
            DrugGeneSearchServices.get({ dbId: $routeParams.drugbankID, drugName: $scope.DrugName }, function (obj) {
                var drugList = obj.Data;
                if (drugList != null && drugList.length > 0)
                {
                    $scope.DrugBankID = $routeParams.drugbankID;
                    $scope.Drug = obj.Data[0];
                }
            });
            GeneServices.get({ Keyword: "" }
            , function (data) {
                $scope.GeneList = data.Data;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("GeneServices.get:" + errRespose.data.Message);
            });
            $scope.Search();
        };

        $scope.Init();

        $scope.AddDrugAllele = function () {
            $scope.CRUDDrugAllele = {};
            $("#modalEditDrugAllele").modal("toggle");
        };

        $scope.SaveDrugAllele = function () {
            DrugAlleleServices.save({ Data: $scope.CRUDDrugAllele }
            , function (data) {
                $scope.Search();
                $("#modalEditDrugAllele").modal("toggle");
            }, function (errRespose) {
                alert("发生错误,保存失败!")
                console.log("DrugAlleleServices.save:" + errRespose.data.Message);
            });
        };

        $scope.EditDrugAllele = function (g) {
            $scope.CRUDDrugAllele = $scope.Clone(g);
            $("#modalEditDrugAllele").modal("toggle");
        };

        $scope.DeleteDrugAllele = function (g) {
            if (confirm('确定删除？')) {
                DrugAlleleServices.remove({ id: g.ID }
                , function (data) {
                    $scope.Search();
                }, function (errRespose) {
                    alert("发生错误,删除失败!")
                    console.log("DrugAlleleServices.remove:" + errRespose.data.Message);
                });
            }
        };

        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
    }
]);