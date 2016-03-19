var app = angular.module("UserInfoManager", ["common"]);

app.controller('UserInfoManagerCtrl', [
    '$scope', '$http', '$location', '$routeParams',
    'UserInfoServices', "dictionary","$timeout",
    function ($scope, $http, $location, $routeParams, userInfoServices, dictionary, $timeout) {

        $scope.Areas = {};
        $scope.getVercodeDisabled = false; //是否获取了手机验证码
        $scope.VerCodeVisiable = false; //验证码界面是否隐藏
        $scope.VerCode = "";
        $scope.LoginPwd = "";
        $scope.LoginPwd2 = "";

        $scope.dicLanguage = [
            { Text: "官话", Value: "0" },
            { Text: "湘语", Value: "1" },
            { Text: "粤语", Value: "2" },
            { Text: "闽语", Value: "3" },
            { Text: "客家话", Value: "4" },
            { Text: "赣语", Value: "5" },
            { Text: "吴侬软语", Value: "6" }
        ];


        $scope.InitAreas = function () {
            if ($scope.Model.CnrUser.CITY == undefined) {
                $scope.Areas.ProvinceId = "";
                $scope.Areas.CityId = "";
                $scope.Areas.CountryId = "";
                $scope.Areas.TownId = "";
                $scope.Areas.Address = "";
            } else {
                $scope.Areas = JSON.parse($scope.Model.CnrUser.CITY);
            }
        };

        $scope.MobilePhone = "";
      
        $scope.sendVercode = function(phone) {
            var reg = /^0?1[3|4|5|8][0-9]\d{8}$/;
            if (reg.test(phone)) {
                //1.判断该号码是否被人注册了
                $http({
                    method: 'GET',
                    url: '/User/CheckUserExists?phone=' + phone
                }).success(function(data) {
                    if (data.Status == 1) {
                        $scope.getVercodeDisabled = true;

                        //2.向该号码发送验证码
                        $http({
                            method: 'GET',
                            url: '/HPN/sendVerCode?phone=' + phone + "&m=" + Math.random()
                        }).success(function (data) {
                            if (parseInt(data.retCode) != 1000) {
                                alert(data.Msg);
                                return;
                            }

                            $scope.VerCode = "";
                            $scope.VerCodeVisiable = true;

                            $scope.getVercodeDisabled = true;
                            $scope.time = 60;

                            var timer = setInterval(function () {
                                $scope.time = $scope.time - 1;
                                if ($scope.time > 0) {
                                    $("#btnGetVercode").html($scope.time + "秒后获取");
                                }
                            }, 1000);
                            $timeout(function () {
                                clearInterval(timer);
                                $("#btnGetVercode").html("获取验证码");
                                $scope.getVercodeDisabled = false;
                            }, 60000);
                        }).error(function (data) {
                            alert(data);
                        });
                    } else {
                        alert(data.Msg);
                        //$scope.MobilePhone = '';
                        $scope.getVercodeDisabled = false;
                    }
                });
            } else {
                alert("输入的号码有误，请重新输入");
            };
        };

        $scope.Model = {};
        userInfoServices.get({}, function(obj) {
            $scope.Model = obj.Data;
            $scope.MobilePhone = Number($scope.Model.UserInfo.MobilePhone);

            $scope.InitAreas();
            $scope.$broadcast('AreaLoadSuccess', $scope.Areas);

            $scope.Model.UserInfo.LoginPwd2 = $scope.Model.UserInfo.LoginPwd;
            if ($scope.Model.CnrUser == null) {
                $("#divRecord").hide();
            }
        });

        $scope.Save = function() {

            if ($scope.MobilePhone != "") {
                if ($scope.MobilePhone != $scope.Model.UserInfo.MobilePhone && $scope.VerCode == "") {
                    alert("验证码不能为空");
                    return;
                }
                $scope.Model.UserInfo.MobilePhone = $scope.MobilePhone;
            } else {
                $scope.Model.UserInfo.MobilePhone = $scope.MobilePhone;//当手机号码为空的时候，默认不修改手机号码
            }

            if ($scope.LoginPwd != $scope.LoginPwd2) {
                alert("两次密码不一致，请检查");
                return;
            }
          
            $scope.Model.CnrUser.CITY = JSON.stringify($scope.Areas);
            $scope.Model.VerCode = $scope.VerCode;
            $scope.Model.UserInfo.LoginPwd = (hex_md5($scope.LoginPwd)).toLocaleUpperCase();

            userInfoServices.save({ Data: $scope.Model }, function (obj) {
                if (obj.Data) {
                    if (obj.Data.Succeeded == true) {

                        location = "/#/";

                    } else {
                        alert(obj.Data.Error);
                    }
                }

                console.log(JSON.stringify(obj.Data));
            });
        };

        $scope.DiseaseCode = [];
        $scope.DiseaseName = [];
        $scope.SaveICD = function() {
            var temp = "";
            $.each($scope.DiseaseName, function(name, value) {
                temp = temp + value + ",";
            });
            $scope.Model.CnrUser.DISEASE = temp;
        };

        $scope.AddMoney = function() {
            $http.post("Api/AccountRecord", { Data: { ProductID: "", ProductName: "", Balance: 1, Account: 2000, SpendType: 0 } })
                .success(function(data) {
                    alert("充值成功!");
                    //location.reload();
                }).
                error(function(errResponse) {
                    alert("充值失败!");
                });
        };

        $scope.GoBack=function() {

            location = "/#/";


        }


    }
]);