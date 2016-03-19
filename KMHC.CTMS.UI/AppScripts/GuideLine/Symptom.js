var app = angular.module('Symptom', []);

app.controller('SymptomCtrl', ['$scope', '$http', '$location','$filter', '$routeParams', 'SymptomServices',
        function ($scope, $http, $location, $filter, $routeParams, SymptomServices) {
            //数组去重
            function unique(arr) {
                var result = [], hash = {};
                for (var i = 0, elem; (elem = arr[i]) != null; i++) {
                    if (!hash[elem]) {
                        result.push(elem);
                        hash[elem] = true;
                    }
                }
                return result;
            }

            //获取症状等级名称
            function getLevelName(id) {
                var _name = '';
                switch (id) {
                    case 0: case undefined:
                        _name = '无';
                        break;
                    case 1:
                        _name = '轻微';
                        break;
                    case 2:
                        _name = '中等';
                        break;
                    case 3:
                        _name = '严重';
                        break;
                    default:
                        break;
                }
                return _name;
            }

            $http.get('/api/symptom?id=1').success(function (data) {
                var legendData = [];
                var xData = [];
                var seriesData = [];
                for (var i = 0; i < data.length; i++) {
                    legendData.push(data[i].SymptomName);
                    xData.push($filter('date')(data[i].SymptomDate, 'yyyy-MM-dd'));
                }
                legendData=unique(legendData);

                var temp=[];
                for(var j=0;j<legendData.length;j++){
                    seriesData[j] = legendData[j];
                    var temp=[];
                    for (var i = 0; i < data.length; i++) {
                        if(data[i]['SymptomName']==legendData[j]){
                            temp.push(data[i].SymptomLevel);
                        }
                    }
                    var _name = legendData[j];
                    seriesData[j] = { name: _name, type: 'line', data: temp };
                }

                // 基于准备好的dom，初始化echarts实例
                var myChart = echarts.init(document.getElementById('main'));

                // 指定图表的配置项和数据
                var option = {
                    title: {
                        text: '症状反馈'
                    },
                    tooltip: {
                        show: false,
                        showshowContent:false,
                        trigger: 'axis',
                        //formatter: '{b0}<br /> {c0}<br />{c1}<br />{c2}<br />{c3}'
                        formatter: function (params, ticket) {
                            var _date = params[0].name ;
                            var _content = '';
                            for (var i = 0; i < params.length; i++) {
                                var _txt = getLevelName(parseInt(params[i].value));
                                _content += '<br/>'+ params[i].seriesName + '：' + _txt;
                            }
                            return _date + _content;
                        }
                    },
                    dataZoom: [
                        {
                            show: true,
                            realtime: true,
                            start: 30,
                            end: 70,
                            xAxisIndex: [0]
                        }
                    ],
                    legend: {
                        show: true,
                        selectedMode: 'single',
                        data: legendData
                    },
                    toolbox: {
                        feature: {
                        }
                    },
                    xAxis: [
                        {
                            type: 'category',
                            boundaryGap: false,
                            data: unique(xData)
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            axisLabel : {
                                formatter: function (value, index) {
                                    return getLevelName(value);
                                }
                            }
                        }
                    ],
                    series: seriesData
                };

                // 使用刚指定的配置项和数据显示图表。
                myChart.setOption(option);
                //myChart.dispatchAction({
                //    type: 'legendUnSelect',
                //    // 图例名称
                //    name: [legendData[0],legendData[1]]
                //});
            }).error(function (xlr) {

            });

            //更新状态
            $scope.UpdateSymptom = function () {
                $location.url("/UpdateSymptom");
            };
        }
]);

app.controller('UpdateSymptomCtrl', ['$scope', '$http', '$location', '$routeParams', 'SymptomServices',
        function ($scope, $http, $location, $routeParams, SymptomServices) {
            $http.get('/api/Dictionary?keyword=SymptomName').success(function (response) {
                $scope.dict = response.Data;
            }).error(function (xlr) {

            });

            $http.get('/api/symptom').success(function (response) {
                $scope.symptom = response;
            }).error(function (xlr) {

            });

            $scope.Date = new Date().getFullYear()+'-'+(new Date().getMonth()+1)+'-'+new Date().getDate();
            $scope.symptom = [];
            $scope.Modal = function () {
                $('#modalSymptom').modal('toggle');
            };

            //选择症状
            $scope.Select = function (o) {
                for (var i = 0; i < $scope.symptom.length; i++) {
                    if ($scope.symptom[i]['SymptomName'] == o.text) {
                        KMAlert({
                            msg:'该症状已存在'
                        });
                        return;
                    }
                }

                var _item = { SymptomName: o.text, DictsymptomId: o.nodeId };
                $scope.symptom.push(_item);
                $('#modalSymptom').modal('toggle');
            };

            //选择症状状态
            $scope.CheckLevel = function (i, j) {
                if ($scope.Date == undefined || $scope.Date == "") {
                    KMAlert({
                        msg: '请先选择日期'
                    });
                    return;
                }

                var tr = $('#symptomBody tr').eq(i);
                var btn = tr.find('button');
                switch (j) {
                    case 0:
                        btn.eq(j).addClass('btn-success');
                        btn.not(j).removeClass('btn-info btn-warning btn-danger');
                        break;
                    case 1:
                        btn.eq(j).addClass('btn-info');
                        btn.not(j).removeClass('btn-success btn-warning btn-danger');
                        break;
                    case 2:
                        btn.eq(j).addClass('btn-warning');
                        btn.not(j).removeClass('btn-info btn-success btn-danger');
                        break;
                    case 3:
                        btn.eq(j).addClass('btn-danger');
                        btn.not(j).removeClass('btn-info btn-warning btn-success');
                        break;
                    default:
                        break;
                }

                $scope.symptom[i].SymptomLevel = j;
            };

            //保存
            $scope.Save = function () {
                $http.post('/api/symptom', { Data: $scope.symptom,ID:$scope.Date }).success(function (data) {
                    if (data == "ok") {
                        KMAlert(
                            { mgs: '更新成功！' }
                            );
                    }
                }).error(function (xlr) {

                });
            };

            //返回
            $scope.GoBack = function () {
                $location.url('/UserCenterSymptom');
            }
        }
]);