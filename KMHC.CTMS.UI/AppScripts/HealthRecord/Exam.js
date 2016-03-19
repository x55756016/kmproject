var app = angular.module('examApp.controllers', []);

app.controller('examCtrl', ['$scope', '$http', '$routeParams', "$location", 'examsServices',
    function ($scope, $http, $routeParams, $location, services) {
        var id = $routeParams.id ? $routeParams.id : 0;

        $scope.Data = {};
        $scope.$on("userInfoLoadSuccess", function (event, data) {
            $scope.Data.RecordId = data.Data[0].ID;
            if ($scope.Data.RecordId != 0) {
                services.query({ ID: $scope.Data.RecordId }, function (data) {
                    var list = data;
                    for (var i = 0; i < list.length; i++) {
                        var item = list[i];
                        $('[id="_' + item.ItemId + '"]').each(function () {
                            if ($(this).is('[multipleselect]'))//多选
                            {
                                var reg = new RegExp("<Value>([^<]*)</Value>", "g");
                                var vsReg,ts="";
                                while (vsReg = reg.exec(item.Result)) {
                                    var tEle = $(this).sibling().find(':checkbox[value="' + vsReg[1] + '"]');
                                    tEle[0].checked = true;
                                    tEle.parents("li").addClass("active");
                                     ts+= ","+tEle.parent().text();
                                }
                                $(this).val(ts.substring(1));
                            } else if($(this).is('select')) {
                                $(this).val(item.Result);
                            }
                            else if ($(this).is(':checkbox'))//复选框
                            {
                                item.Result == "T" ? $(this)[0].checked = true : $(this)[0].checked=false;
                            }
                            else {
                                $(this).val(item.Result);
                            }
                        });
                    }
                });
            }
        });

        $scope.Search= function(recordId) {
            if (RecordId > 0) {
                $scope.Data.RecordId = RecordId;
                services.get({ RecordId: RecordId }, function (data) {
                    $scope.Historys = data.Data;
                });
            }
        }

        //提交数据
        $scope.Submit = function () {
            $scope.Data.ExamType = "1";//年检
            $scope.Data.Doctor = "";
            $scope.Data.Results = [];

            $('[itemId][itemCode]').each(function () {

                var itemId = $(this).attr('itemId');
                var itemCoe = $(this).attr('itemCode');
                var result = "";
                if ($(this).is('[multipleselect]'))//多选
                {
                    result = '<Values>';
                    $(this).sibling().find(':checkbox:checked').each(function () {
                        result += '<Value>' + $(this).val() + '</Value>';
                    });
                    result += '</Values>';
                }
                else if ($(this).is('select'))//单选框
                {
                    result = $(this).val();
                }
                else if ($(this).is(':checkbox'))//复选框
                {
                    result = $(this)[0].checked ? 'T' : 'F';
                }
                else {
                   
                        result = $(this).val();
                }
                if (result) {
                    $scope.Data.Results.push({ ItemId: itemId, ItemCode: itemCoe, Result: result });
                }
            });

            var str = JSON.stringify($scope.Data);
            console.log(str);
            services.save({ Data: $scope.Data }, function () {
                alert('保存成功！');
            });
        };
    }
]);
app.controller('examsCtrl', ['$scope', '$http', '$location', '$routeParams', 'examsServices',
        function ($scope, $http, $location, $routeParams, services) {
            var pid = $routeParams.pid ? $routeParams.pid : 0;
            $scope.ListItems = [];
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                services.get({ Keyword: $scope.Keyword, CurrentPage: $scope.CurrentPage }, function (obj) {
                    $scope.ListItems = obj.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function (curPage) {
                        $scope.CurrentPage = curPage;
                        $scope.search();
                    });
                });
            };
            $scope.Search();
            $scope.Add = function () {
                $location.path('ElderAnnualExam/pid/' + pid);
            };
            $scope.Open = function (id) {
                $location.path('ElderAnnualExam/' + id);
            };
            $scope.Remove = function (id) {
                if (confirm('您确定要删除吗？')) {
                    services.remove({ ID: id }, function () {
                        //$location.path('Exams/pid/' + pid);
                        window.location.reload();
                    });
                }
            };
        }
]);