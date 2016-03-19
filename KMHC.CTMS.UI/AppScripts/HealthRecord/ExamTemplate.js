var app = angular.module('examTemplateApp.controllers', []);


app.controller('examTemplateCtrl', ['$scope', '$http', '$location', '$routeParams', 'examTemplatesServices', 'examcategoriesServices',
        function ($scope, $http, $location, $routeParams, services, examcategoriesServices) {
            var id = $routeParams.id ? $routeParams.id : '';

            var setting = {
                check: {
                    enable: true
                },
                data: {
                    simpleData: {
                        enable: true,
                        idKey: "id",
                        pIdKey: "pId"
                    }
                }
            };
     

            if (id != '') {
                services.get({ ID: id }, function (obj) {
                    $scope.Data = obj.Data;
                    $scope.Categories = {};
                    examcategoriesServices.get(function (obj) {
                        $scope.Categories = obj.Data;
                        var t = $("#tree");
                        t = $.fn.zTree.init(t, setting, $scope.Categories);
                        t.expandAll(true);
                        setCheckCode($scope.Data.Categories, t);
                    });

                });
            }
            else {
                examcategoriesServices.get(function (obj) {
                    $scope.Categories = obj.Data;
                    var t = $("#tree");
                    t = $.fn.zTree.init(t, setting, $scope.Categories);
                    t.expandAll(true);
                });
            }

            

            function setCheckCode(category, tree) {
                for (var i = 0; i < category.length; i++) {
                    tree.checkNode(tree.getNodeByParam("id", category[i].ID, null), true, false, true);
                    if (category[i].SubCategories != null)
                    {
                        setCheckCode(category[i].SubCategories, tree);
                    }
                }
               
            }


        
            $scope.GoBack = function () {
                $location.path('ExamTemplates');
            };

            $scope.Save = function () {
                var xml = "";
                var t = $("#tree");
                var treeObj = $.fn.zTree.getZTreeObj("tree");
                nodes = treeObj.getCheckedNodes(true);

                var previousLevel = 0;
                xml = xml + (nodes.length > 0 ? "<Categories>" : "");
                for (var i = 0; i < nodes.length; i++) {
                    for (var j = 0; j < previousLevel - nodes[i].level; j++) {
                        xml = xml + "</Category>";
                    }
                    xml = xml + "<Category id=\"" + nodes[i].id + "\" code=\"" + nodes[i].name.substr(0, nodes[i].name.indexOf(")")) + "\" name=\"" + nodes[i].name.substr(nodes[i].name.indexOf(")") + 1, nodes[i].name.length - nodes[i].name.indexOf(")") - 1) + "\">"
                    if (nodes[i].children == null || nodes[i].children.length == null || nodes[i].children.length <= 0) {
                        xml = xml + "</Category>";
                    }
                    if (i == nodes.length-1) {
                        for (var j = 0; j < nodes[i].level ; j++) {
                            xml = xml + "</Category>";
                        }
                    }
                    previousLevel = nodes[i].level;
                }
                xml = xml + (nodes.length > 0 ? "</Categories>" : "");
                $scope.Data.TemplateCategory = xml;
                services.save({ Data: $scope.Data }, function (response) {
                    $scope.GoBack();
                });
            }

        }
]);


app.controller('examTemplatesCtrl', ['$scope', '$http', '$location', '$routeParams', 'examTemplatesServices',
        function ($scope, $http, $location, $routeParams, services) {
            var id = $routeParams.id ? $routeParams.id : 0;
            $scope.ListItems = [];
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                services.get({ Keyword: $scope.Keyword, CurrentPage: $scope.CurrentPage }, function (obj) {
                    $scope.ListItems = obj.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function (curPage) {
                        $scope.CurrentPage = curPage;
                        $scope.Search();
                    });
                });
            };
            $scope.Search();
            $scope.Add = function () {
                $location.path('ExamTemplate');
            };
            $scope.Open = function (id) {
                $location.path('ExamTemplate/id/' + id);
            };
            $scope.GoBack = function () {
                $location.path('ExamTemplates');
            };
            $scope.Remove = function (id) {
                if (confirm('您确定要删除吗？')) {
                    services.remove({ ID: id }, function () {
                        //$location.path('Exams/pid/' + pid);
                        $scope.Search();
                    });
                }
            };
        }
]);



