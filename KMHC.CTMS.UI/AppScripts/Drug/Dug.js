var app = angular.module('Dug', []);

app.controller('CnDrugCtrl', ['$scope', '$http', '$location', '$routeParams', 'CnDrugServices',
        function ($scope, $http, $location, $routeParams, CnDrugServices) {


            $scope.Data = {};
            $scope.Data.progress = '1%';
            var uploader = WebUploader.create({
                // swf文件路径
                swf: '/Scripts/Uploader.swf',
                // 文件接收服务端。
                server: '/api/ImportDrug',
                fileNumLimit: 1,
                // 选择文件的按钮。可选。
                // 内部根据当前运行是创建，可能是input元素，也可能是flash.
                pick: '#picker',
                // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传！
                resize: true,
                auto: true
            }).on('startUpload', function () {
                $('#modalUploader').modal('toggle');
            }).on('uploadSuccess', function (file, response) {
                uploader.reset();
            });

            // 文件上传过程中创建进度条实时显示。
            uploader.on('uploadProgress', function (file, percentage) {
                if (percentage < 1)
                    percentage = percentage / 2;
                var _currentProgress = percentage * 100 + '%';

                $scope.Data.progress = _currentProgress;
                $scope.$apply();
            });

            //搜索、分页列表
            $scope.CurrentPage = 1;
            $scope.Search = function () {
                $scope.loading = true;
                CnDrugServices.get({ Name: $scope.s_Name, PinYin: $scope.s_PinYin, Indication: $scope.s_Indication, CurrentPage: $scope.CurrentPage }, function (data) {
                    $scope.List = data.Data;
                    $scope.loading = false;
                    var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                        $scope.CurrentPage = parseInt(curPage);
                        $scope.Search();
                    });
                },
                    function (data) {
                        $scope.List = [];
                        $scope.loading = false;
                    }
                );
            };

            $scope.SearchCondition = function () {
                $scope.CurrentPage = 1;
                $scope.Search();
            };

            $scope.Add = function () {
                $location.url('/AddCnDrug');
            };

            $scope.Remove = function (obj) {
                KMConfirm({
                    msg: '是否删除当前记录?',
                    btnMsg: '删除'
                }, function (e) {
                    $http.delete('/Api/CnDrug?ID=' + obj.ID).success(function (data) {
                        if (data != "ok") {
                            KMAlert({
                                msg: '删除失败！',
                                btnMsg: '确定'
                            });
                        } else {
                            $scope.Search();
                        }
                    });
                });

            }

            $scope.Search();

            //药名自动补全
            $('#name_search').typeahead({
                items: 15,
                source: function (query, process) {
                    var parameter = { Name: query, Token: new Date().getTime() };
                    $.get('/Api/CnDrug', parameter, function (data) {
                        var results = $.map(data, function (item) {
                            if (item.Name != null && item.Name.indexOf(query) >= 0)
                                return item.Name + "";
                            if (item.CommonName != null && item.CommonName.indexOf(query) >= 0)
                                return item.CommonName + "";
                            if (item.EnName != null && item.EnName.indexOf(query) >= 0)
                                return item.EnName + "";
                        });
                        process(results);
                    });
                }
            });


            $scope.CheckRow = function (idx) {
                var tr = $('table.table tbody tr');
                tr.removeClass('success');
                $(tr[idx]).addClass('success');
            }
        }
]);

app.controller('AddCnDrugCtrl', ['$scope', '$http', '$location', '$routeParams',
    function ($scope, $http, $location, $routeParams) {
        $scope.SelectInfo = [{ name: "是", id: true }, { name: "否", id: false }];
        $scope.selected = [];
        $scope.Types = [
                        {
                            label: "中成药",
                            child: [
                                {
                                    label: "滋补类",
                                    child: [
                                        { label: "人参" },
                                        { label: "鹿茸" }
                                    ]
                                }
                            ]
                        },
                        {
                            label: "西药",
                            child: [
                                {
                                    label: "针剂",
                                    child: [
                                        { label: "激素类" }
                                    ]
                                }, {
                                    label: "片剂",
                                    child: [
                                        { label: "抗菌类" }
                                    ]
                                }
                            ]
                        }
        ];

        $scope.Info = {};
        if ($routeParams.id != undefined) {
            $http.get('/Api/CnDrug?ID=' + $routeParams.id).success(function (data) {
                $scope.Info = data;
                if (data != null) {
                    for (var i in $scope.Types) {
                        if ($scope.Types[i].label == $scope.Info.TypeName) {
                            $scope.selected = $scope.Types[i].child;
                        }
                    }
                }
            });
        } else {
            $scope.Info.IsPrescription = false;
            $scope.Info.IsMedicalInsurance = false;
        }

        $scope.Save = function () {
            $http.post('/Api/CnDrug', { Data: $scope.Info }).success(function (data) {
                if (data == 'ok') {
                    KMAlert({
                        msg: '当前记录保存成功！',
                        btnMsg: '确定'
                    });
                }
                else {
                    KMAlert({
                        msg: '网络异常！',
                        btnMsg: '确定'
                    });
                }
            });
        };

        $scope.GoBack = function () {
            $location.url('Dug');
        }

        $scope.TypeChange = function () {
            for (var i in $scope.Types) {
                if ($scope.Types[i].label == $scope.Info.TypeName) {
                    $scope.selected = $scope.Types[i].child;
                }
            }
        };
    }
]);

app.controller('CnDrugInfoCtrl', ['$scope', '$http', '$location', '$routeParams',
    function ($scope, $http, $location, $routeParams) {
        $scope.SelectInfo = [{ name: "是", id: true }, { name: "否", id: false }];
        $http.get('/Api/CnDrug?ID=' + $routeParams.ID).success(function (data) {
            $scope.Info = data;
        });
        $scope.SelectInfo = [{ name: "是", id: true }, { name: "否", id: false }];
        $scope.GoBack = function () {
            $location.url('Dug');
        }
    }
]);

app.controller('DrugControlCtrl', ['$scope', '$http', '$location', '$routeParams', 'DrugControlServices',
    function ($scope, $http, $location, $routeParams, DrugControlServices) {
        $scope.list1 = [{ Text: "处方药", Value: "true" }, { Text: "非处方药", Value: "false" }];
        $scope.list2 = [{ Text: "医保用药", Value: "true" }, { Text: "非医保用药", Value: "false" }];
        $scope.Info = {};
        $scope.Types = [
                       {
                           label: "中成药",
                           child: [
                               {
                                   label: "滋补类",
                                   child: [
                                       { label: "人参" },
                                       { label: "鹿茸" }
                                   ]
                               }
                           ]
                       },
                       {
                           label: "西药",
                           child: [
                               {
                                   label: "针剂",
                                   child: [
                                       { label: "激素类" }
                                   ]
                               }, {
                                   label: "片剂",
                                   child: [
                                       { label: "抗菌类" }
                                   ]
                               }
                           ]
                       }
        ];

        $scope.TypeChange = function () {
            if ($scope.Keys.TypeName === '') {
                $scope.selected = [];
            }
            else {
                for (var i in $scope.Types) {
                    if ($scope.Types[i].label == $scope.Keys.TypeName) {
                        $scope.selected = $scope.Types[i].child;
                    }
                }
            }
        };

        $('#drugname_search').typeahead({
            items: 15,
            source: function (query, process) {
                var parameter = { Name: query, Token: new Date().getTime() };
                $.get('/Api/CnDrug', parameter, function (data) {
                    $scope.List4 = data;
                    var results = $.map(data, function (item) {
                        if (item.Name != null && item.Name.indexOf(query) >= 0)
                            return item.Name + "";
                        if (item.CommonName != null && item.CommonName.indexOf(query) >= 0)
                            return item.CommonName + "";
                        if (item.EnName != null && item.EnName.indexOf(query) >= 0)
                            return item.EnName + "";
                    });
                    process(results);
                });
            },
            updater: function (item) {
                $scope.List3 = [];
                $.each($scope.List4, function (i, t) {
                    if (t.Name === item) {
                        $scope.List3.push(t);
                    }
                });

                return item;
            }
        });

        $scope.Keys = {};
        //搜索、分页列表
        $scope.CurrentPage = 1;
        $scope.Search = function () {
            DrugControlServices.get({ IsPrescription: $scope.Keys.IsPrescription, IsMedicalInsurance: $scope.Keys.IsMedicalInsurance, TypeName: $scope.Keys.TypeName, KindName: $scope.Keys.KindName, Name: $scope.Keys.Name, PinYin: $scope.Keys.PinYin, Indication: $scope.Keys.Indication, CurrentPage: $scope.CurrentPage }, function (data) {
                $scope.List3 = data.Data;
                var pager = new Pager('controlPager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                    $scope.CurrentPage = parseInt(curPage);
                    $scope.Search();
                });
            });
        };

        $scope.SearchCondition = function () {
            $scope.CurrentPage = 1;
            $scope.Search();
        };

        $scope.Check = function (idx, row) {
            $scope.Info.Indication = row.Indication;
            $scope.Info.Dosage = row.Dosage;
            $('#DrugTable tr').removeClass('success');
            $($('#DrugTable tr')[idx + 1]).addClass('success');
        };

        $scope.Save = function (obj) {
            $scope.$emit('summon', obj.ID, obj.Name);
            $('#myModal').modal('toggle');
        };
    }
]);
