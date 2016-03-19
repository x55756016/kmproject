var app = angular.module("Authorization", []);

/*app.directive("authCheck", [
    function () {
        return {
            restrict: 'A', //默认值
            replace: false, //不取代当前元素
            controller: function ($scope, $element) {
                $scope.authList = {};
                $scope.getAuthList = function() {
                    $.ajax({
                        type: 'get',
                        cache: false,
                        async: false, //同步这个问题待解决
                        url: '/Api/UserAuth?funId=' + $($element).find("input[id='funId']").val(),
                        success: function(obj) {
                            if (obj) {
                                $scope.authList = obj.Data;
                            }
                        },
                        error: function() {
                        }
                    });
                };
                $scope.getAuthList();
            },
            link: function ($scope, element, attrs) {
                element.find("[rktype]").each(function() {
                    $(this).hide();
                    var rkTypeModel = $(this);
                    $.each($scope.authList, function (name, value) {
                        if (value.PERMISSIONCODE.toString() == rkTypeModel.attr("rktype")) {
                            rkTypeModel.show();
                        }
                    });
                });
            }
        };
    }
]);

app.directive('onFinishRenderFilters', function () {
    return {
        restrict: 'A',
        replace: false,//不取代当前元素
        require: "^authCheck",
        link: function (scope, element, attrs, pCtrl) {
            if (scope.$last === true) {
                $(element).parents("table").find("[rktype]").each(function () {
                    $(this).hide();
                    var rkTypeModel = $(this);
                    $.each(scope.authList, function (name, value) {
                        if (value.PERMISSIONCODE.toString() == rkTypeModel.attr("rktype")) {
                            rkTypeModel.show();
                        }
                    });
                });
            }
        }
    };
});*/