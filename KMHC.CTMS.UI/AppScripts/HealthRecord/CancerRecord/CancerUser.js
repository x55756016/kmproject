var app = angular.module('cancer', []);
app.controller("cancer_MainPageCtrl", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "optionsServices","cancerUserServices",
    function ($scope, $http, $timeout, $routeParams, $location, optionsServices, cancerUserServices) {
        $scope.dicLanguage = [
            { Text: "官话", Value: "0" },
            { Text: "湘语", Value: "1" },
            { Text: "粤语", Value: "2" },
            { Text: "闽语", Value: "3" },
            { Text: "客家话", Value: "4" },
            { Text: "赣语", Value: "5" },
            { Text: "吴侬软语", Value: "6" }
        ];

        $scope.UserInfos = {};
        $scope.Search = function () {
            $http.get("Api/cancerUser?IDCard=" + escape($scope.s_IDCard == undefined ? "" : $scope.s_IDCard) + "&UserName=" + escape($scope.s_UserName == undefined ? "" : $scope.s_UserName) + "&Disease=" + escape($scope.s_Disease == undefined ? "" : $scope.s_Disease)).success(function (data) {
                $scope.UserInfos = data.Data;
                //todo：将来需要从档案管理的CancerUserController中返回
                for (var i = 0; i < $scope.UserInfos.length; i++) {
                    if ($scope.UserInfos[i].SEX == "1") {
                        $scope.UserInfos[i].SexName = "男";
                    } else if ($scope.UserInfos[i].SEX == "0") {
                        $scope.UserInfos[i].SexName = "女";
                    } else {
                        $scope.UserInfos[i].SexName = "";
                    }
                }
            });
        }
        $scope.Search();
        $scope.AddUser = function () {
            $scope.CRUDUser = {};
            $('#modalAddUser').modal('toggle');
        };
        $scope.SaveUser = function () {
            $http.post('/Api/CancerUser' + ($scope.CRUDUser.USERID == null ? "" : "?ID=" + $scope.CRUDUser.USERID), { Data: $scope.CRUDUser }).success(function (data) {
                $scope.Search();
                $('#modalAddUser').modal('hide');
            });
        };




        $scope.ApplyId = $routeParams.ApplyId;
        $scope.JumpTo = function() {
            window.location = "/#/EventDetailForDoc?id=" + $routeParams.id + "&ApplyId=" + $routeParams.ApplyId;
        };





        
        $scope.AddItem = function (arr, item) {
            arr.push(item);
        };
        $scope.EditBaseInfo = function (u) {
            $location.path("CreateRecord/" + u.USERID);
        };

        $scope.EditPhysicalCondition = function (u) {
            //体力状况量表
            $location.url("hpnlist?userid=" + u.USERID + "&templatetype=3");
        };
        $scope.EditPsychology = function (u) {
            //心理状况量表
            $location.url("hpnlist?userid=" + u.USERID + "&templatetype=2");
        };
        $scope.EditNutrition = function (u) {
            //营养状况量表
            $location.url("hpnlist?userid=" + u.USERID + "&templatetype=1");
        };
        $scope.EditHistoryDisease = function (u) {

            $location.url("DiseaseHistory?uid=" + u.USERID);

        };
        $scope.EditDiagnosis = function (u) {//就诊史
            $location.url("DoctorHistory?uid=" + u.USERID);
        };
        $scope.EditFamilyMemberDisease = function (u) {
            $location.url("FamilyDiseaseHistory/" + u.USERID);
        };
        
        $scope.EditFinance = function (u) {//财务信息
            $location.url("FinanceManage?uid=" + u.USERID);
        };
        $scope.EditUserGene = function (u) {
            //$location.path("UserGen/uid/"+u.USERID);
            $location.url("UserGene?uid=" + u.USERID);
        };

        $scope.EditPrescription = function (u) {
            //$location.path("UserGen/uid/"+u.USERID);
            $location.url("Prescription?uid=" + u.USERID);
        };
        $scope.EditUser = function (u) {
            $scope.CRUDUser = $scope.Clone(u);
            $('#modalAddUser').modal('toggle');
        };
        $scope.DeleteUser = function (u) {
            if (confirm("确定删除【"+u.NAME+"】的肿瘤档案(删除后不可恢复)?"))
            {
                cancerUserServices.remove({ id: u.USERID }
                    , function (response) {
                        alert("删除成功!");
                        $scope.Search();
                    }
                    , function (errResponse){
                        console.log(errResponse);
                    }
                );
                
            }
        };
        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
    }]);


app.controller("personSelectCtrl", [
    '$scope', '$http', '$location', '$routeParams', 'cancerUserServices',
    function($scope, $http, $location, $routeParams, cancerUserServices) {

        $scope.SelectRow = {};
        $scope.Search = function () {
            cancerUserServices.get({ Keyword: $scope.IDNUMBER, UserName: $scope.NAME }, function (data) {
                $scope.UserInfos = data.Data;
            });
        };

        $scope.Check = function(idx, row) {
            $('#taPerson tr').removeClass('success');
            $($('#taPerson tr')[idx + 1]).addClass('success');
            $scope.SelectRow = row;
        };

        $scope.Save = function () {
            if ($scope.SelectRow.NAME == undefined) {
                alert('请选择患者');
                return;
            }
            $('#personSelect').modal('toggle');

            var info = {
                "USERID": $scope.SelectRow.USERID,
                "NAME": $scope.SelectRow.NAME,
                "SEX": $scope.SelectRow.SEX,
                "AGE": $scope.SelectRow.AGE,
                "IDCARD": $scope.SelectRow.IDCARD,
                "DISEASE": $scope.SelectRow.DISEASE
            };

            $scope.$emit('personSelect', info);
        };

    }
]);






app.controller("cancer_PhysicalConditionCtrl", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "optionsServices", "scoreServices",
    function ($scope, $http, $timeout, $routeParams, $location, optionsServices, scoreServices) {
       
        $scope.init = function () {
            $scope.uid = $location.uid;
            var tempTitleArr = ['KPS评分', 'ECOG评级'];
            angular.forEach(tempTitleArr, function (title) {
                scoreServices.get({ UserID: $scope.uid, TemplateTitle: $scope.s_UserName }, function (data) {
                    $scope.UserInfos = data.Data;
                });
            });     
        }
        $scope.init();
        $scope.AddUser = function () {
            $scope.CRUDUser = {};
            $('#modalAddUser').modal('toggle');
        };
        $scope.SaveUser = function () {
            if (confirm('姓名和身份证号填写后将不能更改，确定保存？'))
            {
                cancerUserServices.save({ Data: $scope.CRUDUser }, function (response) {
                    alert("保存成功!");
                    $('#modalAddUser').modal('toggle');
                });
            }
        };

        $scope.AddItem = function (arr, item) {
            arr.push(item);
        };
        $scope.EditBaseInfo = function (u) {
            $location.path("CreateRecord");
        };

        $scope.EditPhysicalCondition = function (u) {

        };
        $scope.EditPsychology = function (u) {

        };
        $scope.EditNutrition = function (u) {

        };
        $scope.EditHistoryDisease = function (u) {

        };
        $scope.EditDiagnosis = function (u) {

        };
        $scope.EditFamilyMemberDisease = function (u) {

        };

        $scope.EditFinance = function (u) {

        };
        $scope.EditUserGen = function (u) {
            $location.path("UserGen");
        };
    }]);