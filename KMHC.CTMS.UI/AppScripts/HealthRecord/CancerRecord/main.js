var routeApp = angular.module('hpnApp', []);
routeApp.controller('RouteListCtrl', function ($scope, $http) {
    $scope.loading = true;
    $http({
        method: 'GET',
        url: '/HPN/getTestList'
    }).success(function (data) {
        $scope.hpn = data.hpn;
        $scope.mentality = data.mentality;
        $scope.loading = false;
    }).error(function (data) {
        
    });
});

routeApp.controller("ResultCtrl", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "UserGeneServices",
    function ($scope, $http, $timeout, $routeParams, $location, UserGeneServices) {
        $scope.formData = {};
        $scope.IsFinish = true;

        $scope.formData.testno = $routeParams.testno;
        $http({ method: 'GET', url: '/HPN/getResult?testno=' + $scope.formData.testno }).success(function (data) {
            if (!angular.isUndefined(data)) {
                $scope.result = data;
            }
        }).error(function (data) {
            consolo.log(data);
        });

        $scope.processForm = function () {
            var tel = $scope.formData.email;
            var telReg = !!tel.match(/^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/);
            if (!telReg) {
                $scope.errorEmail = "手机格式不正确";
                return;
            } else {
                $scope.errorEmail = "";
            }

            $scope.loading = true;
            $http({
                method: 'POST',
                url: '/HPN/submitUserName',
                data: $.param($scope.formData),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {
                $scope.loading = false;
                window.location = "#/message/" + data.Msg;
            }).error(function (data) {
                alert(data);
                $scope.loading = false;
            });
        };

        $scope.goList = function () {
            $location.url("hpnlist?userid=" + $scope.result.UserId + "&templatetype=" + $scope.result.TemplateType);
        };
    }]);

routeApp.controller('MessageCtrl', function ($scope, $routeParams) {
    $scope.msg = $routeParams.msg;
});

routeApp.controller("TemplateCtrl", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "UserGeneServices",
    function ($scope, $http, $timeout, $routeParams, $location, UserGeneServices) {
        $scope.templatename = $routeParams.templatename;
        $scope.UserId = $routeParams.userid;
        $scope.formData = {};
        $scope.Data = {};
        $scope.loading = true;
        $scope.QuestionIndex = 0;

        $scope.Init = function () {
            $http({
                method: 'GET',
                type: 'json',
                url: "/HPN/getTemplate?templateName=" + $scope.templatename + "&userId=" + $scope.UserId
            }).success(function (data) {
                if (data == '')
                    window.location = '/';

                $scope.Data.Template = data;
                $scope.loading = false;
            }).error(function (data) {
                console.log("error--->" + data);
            });
        };
        
        $scope.Init();

        $scope.getPersent = function () {
            var t = parseInt($scope.QuestionIndex / $scope.Data.Template.Questions.length * 100) + '%';
            return t;
        };

        $scope.getPersentWidth = function () {
            return { width: $scope.getPersent() };
        };

        $scope.IsClicking = false;
        $scope.submitItem = function () {
            if ($scope.IsClicking == false) {
                $scope.IsClicking = true;
                $timeout(function () {
                    if ($scope.QuestionIndex < $scope.Data.Template.Questions.length - 1)
                        $scope.QuestionIndex++;
                    else {
                        $scope.loading = true;
                        $scope.formData.templateName = $scope.Data.Template.NAME;
                        $http({
                            method: 'POST',
                            url: '/HPN/submitTest?m=' + Math.random(),
                            data: $.param($scope.formData),
                            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                        }).success(function (data) {
                            if (data.Status == 1) {
                                $location.path(data.Url);
                            } else {
                                alert(data.Msg);
                            }
                            $scope.loading = false;
                        }).error(function (data) {
                            alert(data.Msg);
                        });
                    }
                    $scope.IsClicking = false;
                }, 300);
            }
        };

        $scope.getSubmitText = function () {
            return $scope.QuestionIndex == $scope.Data.Template.Questions.length - 1 ? "提交测试" : "下一题";
        };

        $scope.getPrevItem = function () {
            if ($scope.QuestionIndex > 0) {
                $scope.QuestionIndex--;
            }
        };

        $scope.canSubmitItem = function () {
            var sum = 0;
            for (var item in $scope.formData) {
                if ($scope.formData[item] != undefined)
                    sum++;
            }
            return sum > $scope.QuestionIndex ? false : true;
        };

        $scope.goNext = function () {
            if (event.keyCode == 13) {
                $scope.submitItem();
            }
        };
    }]);

routeApp.controller("HPNListCtrl", [
    "$scope", "$http", "$timeout", "$routeParams", "$location", "UserGeneServices",
    function ($scope, $http, $timeout, $routeParams, $location, UserGeneServices) {
        $scope.UserId = $routeParams.userid;
        $scope.TemplateType = $routeParams.templatetype;
        $scope.Person = {};
        $scope.History = {};
        $scope.editTemplateName = 'Nrs';

        $scope.loading = true;

        $http({
            method: 'GET',
            url: "/HPN/getTestList?UserId=" + $scope.UserId + "&TemplateType=" + $scope.TemplateType
        }).success(function (data) {
            if (data == '')
                window.location = '/';

            $scope.data = data;
            $scope.Person.Name = data.UserInfo.NAME;
            $scope.Person.IDCard = data.UserInfo.IDCARD;
            $scope.loading = false;
        });

        $scope.goTest = function (templateName, userId) {
            $location.path( "Template/" + templateName + "/" + userId);
        };

        $scope.goBack = function () {
            $location.url("/CancerRecord");
        };

        $scope.randomScalingFactor = function () {
            return Math.round(Math.random() * 100);
        };

        $scope.showHistory = function (templateName) {
            $scope.editTemplateName = templateName;
            $timeout(function () {
                $scope.getHistory();
            }, 300);
        };

        $scope.getHistory = function () {

            $http({
                method: 'GET',
                url: '/HPN/getHistory?userId=' + $scope.UserId + "&templateName=" + $scope.editTemplateName
            }).success(function (data) {
                if (data.Status != 1) {
                    alert(data.Msg);
                    return;
                }

                $scope.History = data.Data;

                if (data.Data.datasets.length > 0) {
                    var ctx = document.getElementById("myChart").getContext("2d");
                    if (angular.isDefined(window.myLine))
                        window.myLine.destroy();
                    window.myLine = new Chart(ctx).Line($scope.History, {
                        responsive: true
                    });
                    //var legend = window.myLine.generateLegend();//这里得到的就是图例的HTML源码，InnerHTML到某个元素里就行了
                }

            }).error(function (data) {
                alert(data);
            });
        };
    }
]);