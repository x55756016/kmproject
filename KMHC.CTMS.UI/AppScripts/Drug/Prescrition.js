var app = angular.module("Prescription", ['Dug', 'cancer']).controller("PrescriptionCtrl", [
    "$scope", "$http", "$location", "$routeParams", "PrescriptionServices",
    function ($scope, $http, $location, $routeParams, PrescriptionServices) {
        $scope.DrugName = "";
        $scope.IsShow = false;
        $scope.DrugUser = {};
        $scope.DISID = ""; //开药历史ID
        $scope.PatientID = "";//患者id
        $scope.ListItem = {};

        $scope.isEdit = false; //是否在编辑状态

        //cancer控制器中取得用户信息
        $scope.UserInfo = {};

        $scope.GeneAllele = "";
        $scope.$on('personSelect', function (e, info) {
            $scope.PatientID = info.USERID;
            $scope.UserInfo = info;

            $http.post('/Prescription/GetUserGeneDesc?id=' + info.USERID)
                .success(function(obj) {
                    $scope.UserGeneAllele = obj.Data;

                    for (var index = 0; index < obj.Data.length; index++) {
                        $scope.GeneAllele = $scope.GeneAllele + obj.Data[index] + "\r\n";
                    }
                });
        });

        $scope.Search = function (disID) {
            $http.get('/Api/Prescription/' + disID).success(function(obj) {
                $scope.ListItem = obj.Data;
                $scope.InterEffect = "";
                $scope.GeneEffect = "";


                for (var index1 = 0; index1 < obj.Data.InterEffect.length ; index1++) {
                    var temp = obj.Data.InterEffect[index1].INTERNAME+"与"+obj.Data.InterEffect[index1].NAME+"会产生"+obj.Data.InterEffect[index1].DRUGINTERACTION;
                    $scope.InterEffect = $scope.InterEffect + temp + "\r\n";
                }

                for (var index2 = 0; index2 < obj.Data.GeneEffect.length; index2++) {
                    var temp = obj.Data.GeneEffect[index2].DRUGNAME + "药品会对个人基因" + obj.Data.GeneEffect[index2].GENENAME + "产生" + obj.Data.GeneEffect[index2].GENEDESC + "的效果";
                    $scope.GeneEffect = $scope.GeneEffect + temp + "\r\n";
                }
            });
        };

        //接收参数
        $scope.$on('summon', function (e, id, name) {
            $scope.DrugUser.DrugId = id;
            $scope.DrugUser.DRUGNAME = name;
            $http.post('/Prescription/GetDrugInteration?diseaseId=' + $scope.DISID + '&drugId=' + id + '&userId=' + $scope.PatientID)
                .success(function (obj) {

                    $scope.DrugUser.InterEffect = obj.Data.InterEffect;


                    $scope.DrugUser.GeneEffect = obj.Data.GeneEffect;

                });
        });


        $scope.AddNewDrug = function (type) {
            $scope.DrugUser = {};
            $scope.isEdit = false;
            if ($scope.PatientID == "") {
                alert("请先选择患者");
                $("#dlgDrugUser").modal("hide");
            } else {
                $("#dlgDrugUser").modal("show");
            }

            if (type == 0) {
                $scope.DrugUser.ISNEWDRUG = 0;
            } else {
                $scope.DrugUser.ISNEWDRUG = 1;
            }
        };

        $scope.SaveDrugUse = function() {
            $scope.DrugUser.USERID = $scope.PatientID;
            $scope.DrugUser.Action = 1;
            $scope.DrugUser.DISEASEID = $scope.DISID;

            if ($scope.isEdit) {
                $scope.DrugUser.Action = 2;
            }

            PrescriptionServices.save({ Data: $scope.DrugUser }, function(obj) {
                if (obj != "") {
                    $scope.DISID = obj.Data;
                    $scope.DrugUser.DISEASEID = $scope.DISID;
                    $scope.Search($scope.DISID);
                    $scope.DrugUser = {};
                }
                $("#dlgDrugUser").modal("toggle");
            }, function(errRespose) {
                alert("发生错误,保存失败!");
                console.log("GeneAlleleServices.save:" + errRespose.data.Message);
            });
        };


        $scope.Remove = function (id) {
            if (confirm('您确定要删除吗？')) {
                PrescriptionServices.remove({ id: id }, function (response) {
                    $scope.Search($scope.DISID);
                });
            }
        };

        $scope.Edit = function(id) {
            $scope.isEdit = true;
            $http.post('/Prescription/GetInfo?disId=' + $scope.DISID + "&id=" + id).success(function (obj) {
                $scope.DrugUser = obj.Data;
                if (obj.Data.OldDrug == "") {
                    $scope.DrugUser = obj.Data.NewDrug[0];
                } else {
                    $scope.DrugUser = obj.Data.OldDrug[0];
                }
                $scope.DrugUser.InterEffect = obj.Data.InterEffect;
                $scope.DrugUser.GeneEffect = obj.Data.GeneEffect;
                $("#dlgDrugUser").modal("show");
            });
        }

        $scope.SelectDrug= function() {
            if (!$scope.isEdit) {
                $("#myModal").modal("show");
            }
        }

        //剂量单位
        $scope.UnitList = [{ name: "毫升/ml", value: "1" }, { name: "克", value: "2" }];

        //生活不良习惯选项
        $scope.Habits = [{ DisplayName: "抽烟", name: "Smoking", value: "1", checked: false },
            { DisplayName: "酗酒", name: "Alcohol", value: "2", checked: false },
            { DisplayName: "不运动", name: "NotExercise", value: "3", checked: false }];

        $scope.selected = [];
        $scope.selectedTags = [];
        var updateSelected = function(action, id, name) {
            if (action == 'add' && $scope.selected.indexOf(id) == -1) {
                $scope.selected.push(id);
                $scope.selectedTags.push(name);
            }
            if (action == 'remove' && $scope.selected.indexOf(id) != -1) {
                var idx = $scope.selected.indexOf(id);
                $scope.selected.splice(idx, 1);
                $scope.selectedTags.splice(idx, 1);
            }
        };

        $scope.updateSelection = function($event, id) {
            var checkbox = $event.target;
            var action = (checkbox.checked ? 'add' : 'remove');
            updateSelected(action, id, checkbox.name);
        };
    }
]);