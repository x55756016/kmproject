/**用于测试的数据**/
var TEMPLATES = [{ "Questions": [{ "Question": "你对生活基本上满意吗？", "Options": [{ "Text": "是", "Value": 10 }, { "Text": "否", "Value": 0 }] }, { "Question": "你是否放弃了许多活动和乐趣？", "Options": [{ "Text": "是", "Value": 10 }, { "Text": "否", "Value": 0 }] }, { "Question": "你是否常感到厌倦？", "Options": [{ "Text": "经常", "Value": 0 }, { "Text": "偶尔", "Value": 5 }, { "Text": "从不", "Value": 10 }] }, { "Question": "你是否觉得生活空虚？", "Options": [{ "Text": "经常", "Value": 0 }, { "Text": "偶尔", "Value": 5 }, { "Text": "从不", "Value": 10 }] }, { "Question": "你是否常常担心未来？", "Options": [{ "Text": "经常", "Value": 0 }, { "Text": "偶尔", "Value": 5 }, { "Text": "从不", "Value": 10 }] }, { "Question": "你是否因脑子里有些想法拜托不掉而烦恼？", "Options": [{ "Text": "经常", "Value": 0 }, { "Text": "偶尔", "Value": 5 }, { "Text": "从不", "Value": 10 }] }, { "Question": "你是否害怕有不幸的事情降临到你身上？", "Options": [{ "Text": "经常", "Value": 0 }, { "Text": "偶尔", "Value": 5 }, { "Text": "从不", "Value": 10 }] }, { "Question": "你是否感到孤立无援？ ", "Options": [{ "Text": "经常", "Value": 0 }, { "Text": "偶尔", "Value": 5 }, { "Text": "从不", "Value": 10 }] }, { "Question": "你是否经常坐立不安，心烦意乱？", "Options": [{ "Text": "经常", "Value": 0 }, { "Text": "偶尔", "Value": 5 }, { "Text": "从不", "Value": 10 }] }, { "Question": "你是否常常担心儿女遇到不顺？", "Options": [{ "Text": "经常", "Value": 0 }, { "Text": "偶尔", "Value": 5 }, { "Text": "从不", "Value": 10 }] }], "TemplateID": 1, "TemplateTitle": "老年人情感状态抑郁表", "TemplateDescription": "测试老年人生活抑郁状况！", "TemplateLabel": "选择最切合你最近一周来的感受的答案:", "Grades": [{ "Grade": "理想", "minValue": 91, "maxValue": 100, "Description": "健康处于理想状态，但需注意平时的营养和锻炼！" }, { "Grade": "良好", "minValue": 81, "maxValue": 90, "Description": "健康处于良好状态，但需注意平时的营养和锻炼！" }, { "Grade": "普通", "minValue": 71, "maxValue": 80, "Description": "健康处于普通状态，但需注意平时的营养和锻炼！" }, { "Grade": "较差", "minValue": 0, "maxValue": 70, "Description": "健康处于较差状态，建议就诊！" }], "ShowGrade": true }];

/****/

var app = angular.module('scoreScale', []);

app.controller('createScoreScaleCtrl', ['$scope', '$http', '$location', '$routeParams', 'scoreTemplateServices',
    function ($scope, $http, $location, $routeParams, scoreTemplateServices) {
            var model = {};
            if ($routeParams.id != null) {
                //var query = $.grep(TEMPLATES, function (t, i) {
                //    return t.TemplateID == $routeParams.id;
                //});
                //if (query.length >= 1) {
                //    model = query[0];
                //}
                scoreTemplateServices.get({ id: $routeParams.id }, function (data) {
                    $scope.Data = data.Data;
                });
            } else {
                $scope.Data = model;
            }
           
            $scope.ShowResult = false;
            $scope.Save = function () {
                var noAnNos = [];
                var totalScore = 0;
                angular.forEach($scope.Data.Questions, function (q) {
                    if (q.Score!=null) {
                        totalScore = totalScore + parseInt(q.Score);
                    }
                    else {
                        noAnNos.push(q.No);
                    }
                });
                if (noAnNos.length == 0)
                {
                    $scope.Data.TotalScore = totalScore;
                    //$scope.ShowResult = true;
                    if ($scope.Data.IsShowResult == false)
                    {
                        alert("保存成功!");
                        $location.path('CancerRecord');
                    }
                }
                else
                {
                    alert('还有' + noAnNos.length + '题未作答!');
                }
            }
            $scope.ShowResultScore = function () {
                if ($scope.Data.ShowGrade == false) {
                    return $scope.Data.TotalScore;
                }
                else
                {
                    console.log("总分：" + $scope.Data.TotalScore);
                    var grade="";
                    for (var i=0; i < $scope.Data.Grades.length; i++) {
                        var g = $scope.Data.Grades[i];
                        if ($scope.Data.TotalScore >= g.minValue && $scope.Data.TotalScore <= g.maxValue)
                        {
                            console.log("g：" + angular.toJson(g));
                            grade = g.Grade;
                            console.log("grade：" + grade);
                            break;
                        }
                    }
                    return grade;
                }
            }
            $scope.SetResultCss=function(g)
            {
                if ($scope.Data.TotalScore >= g.minValue && $scope.Data.TotalScore <= g.maxValue) {
                    return "alert-warning";
                }
                else {
                    return "";
                }
            }
            $scope.GoBack = function () {
                //$location.path('ExamItems');
            };
        }
]);

//创建评分量表模板
app.controller('createScoreScaleTemplateCtrl', ['$scope', '$http', '$location', '$routeParams','scoreTemplateServices',
    function ($scope, $http, $location, $routeParams,scoreTemplateServices, services) {
            var model = {
                Questions: [{Question: null,Options: [{ Text: null, Value: null }, { Text: null, Value: null }]}],
                TemplateTitle: null,
                TemplateID: null,
                TemplateDescription: null,
                TemplateLabel: null,
                Grades: [{ Grade: null, minValue: null, maxValue: null, Description: null }],
                ShowGrade:false
            }
           
            $scope.PageTitle = "创建评分量表模板";
            
            if ($routeParams.id != null) {
                scoreTemplateServices.get({ id: $routeParams.id }, function (data) {
                    $scope.CurrentTemplateID = $routeParams.id;
                    $scope.PageTitle = "修改评分量表模板";
                    $scope.Data = data.Data;
                });
                //var query = $.grep(TEMPLATES, function (t, i) {
                //    return t.TemplateID == $routeParams.id;
                //});
                //if (query.length >= 1) {
                //    model = query[0];
                //    $scope.CurrentTemplateID = $routeParams.id;
                //    $scope.PageTitle = "修改评分量表模板";
                //}
            }
            else {
                $scope.Data = model;
            }
            
            $scope.AddQuestion = function () {
                $scope.Add($scope.Data.Questions,
                    {
                        Question: $scope.impQuestion,
                        Options: [{ Text: null, Value: null }, { Text: null, Value: null }]
                    });
            }
            $scope.AddOption = function (question) {
                $scope.Add(question.Options,
                    { Text: null, Value: null });
            }
            $scope.AddGrade= function () {
                $scope.Add($scope.Data.Grades,
                    { Grade: $scope.impGrade, minValue: null, maxValue: null, Description: null });
            }

            $scope.Cancel = function () {
                $location.path('ScoreScaleTemplateManage');
            }

            //公共方法
            $scope.Add = function (arr, obj) {
                arr.push(obj);
            }
            $scope.Remove = function (arr,q) {
                arr.splice(arr.indexOf(q), 1);
            }
            $scope.MoveUpp = function (arr,q) {
                var idx=arr.indexOf(q);
                $scope.Swap(arr,idx,idx - 1);
            }
            $scope.MoveDown = function (arr,q) {
                var idx = arr.indexOf(q);
                $scope.Swap(arr, idx, idx + 1);
            }
            $scope.Swap = function (arr,i,j) {
                var temp=arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            $scope.Copy = function (arr, obj) {
                var c = JSON.parse(angular.toJson(obj));
                arr.push(c);
            }
            //事件
            $scope.Save = function () {
                scoreTemplateServices.save({ Data: $scope.Data }, function (response) {
                    alert("保存成功!");
                    $location.path('ScoreScaleTemplateManage');
                });
               
                //if ($scope.CurrentTemplateID != null) {
                //    //Todo:修改模板
                //    //var resultJson = angular.toJson($scope.Data);
                //    //console.log(resultJson);
                //    //$location.path('ScoreScaleTemplateManage');

                //    scoreTemplateServices.save({ Data: $scope.Data }, function (response) {
                //        alert("修改成功!");
                //    });
                //}
                //else {
                //    var resultJson = angular.toJson($scope.Data);
                //    console.log(resultJson);
                //    $scope.Data.TemplateID = new Date().getTime();
                //    TEMPLATES.push($scope.Data);
                //    $location.path('ScoreScaleTemplateManage');
                //}
                
            }
            $scope.GoBack = function () {
                //$location.path('ExamItems');
            };
        }
]);

//评分量表模板管理
app.controller('scoreScaleTemplateManageCtrl', ['$scope', '$http', '$location', '$routeParams', 'scoreTemplateServices',
        function ($scope, $http, $location, $routeParams, scoreTemplateServices) {
            $scope.PageTitle = "评分量表模板管理";
            $scope.Innit = function () {
                scoreTemplateServices.get({}, function (data) {
                    $scope.Data = data.Data;
                })
                //$scope.Data = TEMPLATES;
            }
            $scope.Innit();
            $scope.Add = function () {
                $location.path('CreateScoreScaleTemplate');
            }
            $scope.Edit = function (tempid) {
                $location.path('EditScoreScaleTemplate/id/'+tempid);
            }
            $scope.Remove = function (t) {
                //TEMPLATES.splice(TEMPLATES.indexOf(t), 1);
            }
            //事件
            $scope.Save = function () {
                //var resultJson = angular.toJson($scope.Data);
                //console.log(resultJson);
                //alert(angular.toJson($scope.Data))
            }
            $scope.GoBack = function () {
                //$location.path('ExamItems');
            };
        }
]);