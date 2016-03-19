var loginApp = angular.module('loginApp', ['ngRoute',
        'ngResource']);
loginApp.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('pwmatch', v);
                });
            });
        }
    }
}]);
loginApp.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/Login', { templateUrl: '/Views/User/login.html', controller: 'LoginCtrl' });
        $routeProvider.when('/Reset/:token', { templateUrl: '/Views/User/reset.html', controller: 'ResetCtrl' });
        $routeProvider.otherwise({ redirectTo: '/Login' });
    }
]).controller('LoginCtrl', function ($scope, $http, $timeout) {

    $scope.loading = false;
    $scope.formData = {};
    $scope.myRegister = {};
    $scope.resetData = {};
    $scope.resetData.ResetType = 0;
    $scope.getVercodeDisabled = true;
    $scope.LoginPwd = "";

    $scope.processForm = function () {
        $scope.loading = true;
        $scope.formData.MD5LoginPwd = (hex_md5($scope.LoginPwd)).toLocaleUpperCase();
        $http({
            method: 'POST',
            url: '/User/UserLogin',
            data: $.param($scope.formData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            if (data.Status != 1) {
                alert(data.Msg);
                $scope.loading = false;
                return;
            }
            $scope.loading = false;
            location = "/#/";//登录成功后重定向到首页
        }).error(function (data) {
            alert(data.Msg);
        });
    };

    $scope.processRegister = function () {
        $scope.loading = true;
        $http({
            method: 'POST',
            url: '/User/UserRegister',
            data: $.param($scope.registerData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            alert(data.Msg);
            $scope.loading = false;
            if (data.Status == 1) {
                $(".modal").trigger("click");
                $scope.RegisterIM();
            }
        }).error(function (data) {
            alert(data.Msg);
        });
    };

    $scope.RegisterIM = function () {
        var options = {
            username: $scope.registerData.LoginName,
            password: hex_md5($scope.registerData.LoginName), //todo 密码需要重新定义
            nickname: $scope.registerData.LoginName,
            appKey: Easemob.im.config.appkey,
            success: function (result) {
                var conn = new Easemob.im.Connection();
                conn.init({
                    https: Easemob.im.config.https,
                    url: Easemob.im.config.xmppURL,
                    //当连接成功时的回调方法
                    onOpened: function () {
                        if (conn.isOpened()) {
                            conn.heartBeat(conn);
                        }
                        conn.subscribe({
                            to: Easemob.im.config.DefaultDoctor,
                            message: "新用户申请加个好友-" + $scope.getLoacalTimeString()
                        });
                        conn.subscribe({
                            to: Easemob.im.config.DefaultServiceStaff,
                            message: "新用户申请加个好友-" + $scope.getLoacalTimeString()
                        });
                    },
                    //当连接关闭时的回调方法
                    onClosed: function () {

                    },
                    //收到联系人订阅请求的回调方法
                    onPresence: function (message) {
                        if (message.from != undefined && message.from != null && message.type == 'subscribe' && (message.from == Easemob.im.config.DefaultDoctor || message.from == Easemob.im.config.DefaultServiceStaff)) {
                            conn.subscribed({
                                to: message.from,
                                message: "[resp:true]"
                            });
                        }
                    },
                });
                conn.open({
                    apiUrl: Easemob.im.config.apiURL,
                    user: $scope.registerData.LoginName,
                    pwd: hex_md5($scope.registerData.LoginName), //todo 
                    //连接时提供appkey
                    appKey: Easemob.im.config.appkey
                });
            },
            error: function (e) {
            },
            apiUrl: Easemob.im.config.apiURL
        };
        Easemob.im.Helper.registerUser(options);
    };

    $scope.getLoacalTimeString = function getLoacalTimeString() {
        var date = new Date();
        var time = date.getHours() + ":" + date.getMinutes() + ":"
                + date.getSeconds();
        return time;
    }

    $scope.mobilePhoneChange = function () {
        if (!angular.isUndefined($scope.registerData.MobilePhone)) {
            $http({
                method: 'GET',
                url: '/User/CheckUserExists?phone=' + $scope.registerData.MobilePhone,
            }).success(function (data) {
                if (data.Status == 1) {
                    $scope.getVercodeDisabled = false;
                } else {
                    alert(data.Msg);
                    $scope.registerData.MobilePhone = '';
                }
            }).error(function (data) {
                alert(data.Msg);
            });
        } else {
            $scope.getVercodeDisabled = true;
        }
    };

    $scope.sendVercode = function (phone) {
        $http({
            method: 'GET',
            url: '/HPN/sendVerCode?phone=' + phone + "&m=" + Math.random()
        }).success(function (data) {
            if (parseInt(data.retCode) != 1000) {
                alert(data.Msg);
                return;
            }

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
    };

    $scope.processReset = function () {

        if (angular.isUndefined($scope.resetData))
            return;

        if ($scope.resetData.ResetType == 0) {
            if (angular.isUndefined($scope.resetData.Email)) {
                $scope.resetEmailError = 'has-error';
                return;
            }
            if ($scope.resetData.Email == '') {
                $scope.resetEmailError = 'has-error';
                return;
            }
            $scope.resetEmailError = '';
        } else if ($scope.resetData.ResetType == 1) {
            if (angular.isUndefined($scope.resetData.MobilePhone)) {
                $scope.resetMobilePhoneError = 'has-error';
                return;
            }
            if ($scope.resetData.MobilePhone == '') {
                $scope.resetMobilePhoneError = 'has-error';
                return;
            }
            $scope.resetMobilePhoneError = '';
        }

        $scope.loading = true;
        $http({
            method: 'POST',
            url: '/User/UserResetPwd',
            data: $.param($scope.resetData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            alert(data.Msg);
            $scope.loading = false;
            if (data.Status == 1) {
                $(".modal").trigger("click");
            }
        }).error(function (data) {
            alert(data.Msg);
        });
    };
}).controller('ResetCtrl', function ($scope, $routeParams, $http, $location) {
    $scope.loading = false;
    $scope.token = $routeParams.token;

    $scope.processForm = function () {
        $scope.loading = true;
        $scope.resetData.ResetToken = $scope.token;
        $http({
            method: 'POST',
            url: '/User/ResetUserPassword',
            data: $.param($scope.resetData),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            alert(data.Msg);
            $scope.loading = false;
            if (data.Status == 1) {
                $(".modal").trigger("click");
                $location.path('/Login');
            }
        }).error(function (data) {
            alert(data.Msg);
        });
    };
});