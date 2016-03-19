var App = angular.module('GuidLineApp', ['ngRoute',
        'ngResource', 'HR.services','common']);
//loginApp.directive('pwCheck', [function () {
//    }
//}]);
App.config([
    '$routeProvider',
    function($routeProvider) {
        $routeProvider.when('/Index', { templateUrl: '/Views/GuideLine/Index.html', controller: 'GuidIndexCtrl' });
    }
]).controller('GuidIndexCtrl', [
    '$scope', '$http', '$location', '$routeParams', 'GuideLineServices', 'DictionaryServices', 'GuideLineFlowServices',
    function($scope, $http, $location, $routeParams, guideLineServices, dictionaryServices, guideLineFlowServices) {
        var _canvas = null;

        var processData = {};
        var flow_id = $routeParams.Fid;

        var mLeft = 0;
        var mTop = 0;

        $scope.AddProcessFunction = function (model) {
            /*创建流程设计器*/
            _canvas = $("#flowdesign_canvas").Flowdesign({
                "processData": model
                /*画面右键*/,
                canvasMenus: {
                    "cmAdd": function (t) {
                        mLeft = $("#jqContextMenu").css("left");
                        mTop = $("#jqContextMenu").css("top");

                        //页面数据加载
                        var alertModal2 = $('#divAddProcess'), attributeModal = $("#divAddProcess");
                        alertModal2.find(".modal-body");
                        alertModal2.modal('toggle');
                    },
                    "cmHelp": function (t) {
                        //mAlert('<ul><li><a href="http://flowdesign.leipi.org/doc.html" target="_blank">流程设计器 开发文档</a></li><li><a href="http://formdesign.leipi.org/doc.html" target="_blank">表单设计器 开发文档</a></li><li><a href="http://formdesign.leipi.org/demo.html" target="_blank">表单设计器 示例DEMO</a></li></ul>', 20000);
                    }
                }
                /*步骤右键*/,
                processMenus: {
                    "pmDelete": function (t) {
                        if (confirm("你确定删除步骤吗？")) {
                            var activeId = _canvas.getActiveId(); //右键当前的ID
                            jsPlumb.detachAllConnections($("#window" + activeId));
                            _canvas.delProcess(activeId);
                        }
                    },
                    "pmAttribute": function (t) {
                        var activeId = _canvas.getActiveId(); //右键当前的ID

                        guideLineServices.get({ ID: activeId }, function (data) {
                            $scope.CRUDGuideLine = data.Data;
                        }, function (errRespose) {
                            alert("发生错误,查询失败!");
                            console.log("GuideLineServices.get:" + errRespose.data.Message);
                        });

                        var alertModal2 = $('#modalGuideLine'), attributeModal = $("#modalGuideLine");
                        alertModal2.find(".modal-body");
                        alertModal2.modal('toggle');
                    }
                },
                fnRepeat: function () {
                    mAlert("步骤连接重复了，请重新连接");
                }
            });
        };

        if (flow_id != undefined) {
            guideLineFlowServices.get({ ID: flow_id }, function (obj) {
                if (obj) {
                    //console.log(JSON.stringify(obj.Data.GUIDELINEINFO));
                    //$scope.GuideLineFlow = obj.Data;
                    //processData = obj.Data.GUIDELINEINFO;
                    //$scope.AddProcessFunction($.parseJSON(processData));


                    var template = $.parseJSON(obj.Data);
                    console.log(JSON.stringify(template.list));
                    //$scope.GuideLineFlow = obj.Data;
                    processData = template;
                    $scope.AddProcessFunction(processData);


                }

            });
        } else {
            $scope.AddProcessFunction($.parseJSON(processData));
        }


        //加载弹出窗体的数据
        $scope.Init = function() {
            dictionaryServices.get({ keyWord: "LogicOperator" }, function(data) {
                $scope.DicLogicOperatorList = data.Data;
            }, function(errRespose) {
                alert("发生错误,查询失败!");
                console.log("DictionaryServices.get:" + errRespose.data.Message);
            });
            dictionaryServices.get({ keyWord: "GuideLineMetaData" }, function(data) {
                if (data.Data != null && data.Data.length > 0) {
                    var datalist = data.Data[0].value.replace("，", ",").split(',');
                    var metaDataList = [];
                    angular.forEach(datalist, function(d) {
                        metaDataList.push({ Text: d, Value: null });
                    });
                    $scope.MetaDataList = metaDataList;
                }
            }, function(errRespose) {
                alert("发生错误,查询失败!");
                console.log("DictionaryServices.get:" + errRespose.data.Message);
            });
            $scope.BindGuideLine();
            $scope.Search();
        };
        $scope.BindGuideLine = function() {
            guideLineServices.get({ Keyword: "" }, function(data) {
                $scope.AllGuideLineList = data.Data;

            }, function(errRespose) {
                alert("发生错误,查询失败!");
                console.log("GuideLineServices.get:" + errRespose.data.Message);
            });
        };
        $scope.Search = function() {
            guideLineServices.get({ Keyword: $scope.keyWord }, function(data) {
                $scope.GuideLineList = data.Data;

            }, function(errRespose) {
                alert("发生错误,查询失败!");
                console.log("GuideLineServices.get:" + errRespose.data.Message);
            });
        };
        $scope.Init();

        $scope.CheckData = function() {
            var checkResult = true;
            var alertMsg = "";
            //if ($scope.CRUDGuideLine.ParentID == $scope.CRUDGuideLine.ID) {
            //    alertMsg += "不能选择自己做父临床路径!";
            //    checkResult = false;
            //}
            if ($scope.CRUDGuideLine.EnterLogicalOperator == 2 && $scope.CRUDGuideLine.EnterCondItemList.length < 1) {
                alertMsg += "请至少选择一个进入条件!";
                checkResult = false;
            }
            if (($scope.CRUDGuideLine.EnterLogicalOperator == 0 || $scope.CRUDGuideLine.EnterLogicalOperator == 1) && $scope.CRUDGuideLine.EnterCondItemList.length < 2) {
                alertMsg += "请至少选择两个进入条件!";
                checkResult = false;
            }

            if ($scope.CRUDGuideLine.OutLogicalOperator == 2 && $scope.CRUDGuideLine.OutCondItemList.length < 1) {
                alertMsg += (alertMsg == "" ? "" : "\r\n") + "请至少选择一个退出条件!";
                checkResult = false;
            }
            if (($scope.CRUDGuideLine.OutLogicalOperator == 0 || $scope.CRUDGuideLine.OutLogicalOperator == 1) && $scope.CRUDGuideLine.OutCondItemList.length < 2) {
                alertMsg += (alertMsg == "" ? "" : "\r\n") + "请至少选择两个退出条件!";
                checkResult = false;
            }
            if (!checkResult) {
                alert(alertMsg);
            }
            return checkResult;
        };
        $scope.SaveGuideLine = function() {
            if ($scope.CheckData()) {
                guideLineServices.save({ Data: $scope.CRUDGuideLine }, function(data) {
                    //$scope.Search();
                    //$scope.BindGuideLine();
                    $("#modalGuideLine").modal("toggle");
                }, function(errRespose) {
                    alert("发生错误,查询失败!");
                    console.log("GuideLineServices.get:" + errRespose.data.Message);
                });
            }
        };

        $scope.SetCondItem = function(text, value) {
            $scope.SelectedCondItemID = value;
            $scope.SelectedCondItemName = text.replace("（与）", "").replace("（或）", "").replace("（非）", "");
        };
        $scope.SaveCondPicker = function() {
            if ($scope.CurrentCondItmeList == "Enter") {
                $scope.CRUDGuideLine.EnterCondItemList[$scope.CurrentEnterCondItemIndex] = { DisplayName: $scope.SelectedCondItemName, ID: $scope.SelectedCondItemID };
            } else if ($scope.CurrentCondItmeList == "Out") {
                $scope.CRUDGuideLine.OutCondItemList[$scope.CurrentOutCondItemIndex] = { DisplayName: $scope.SelectedCondItemName, ID: $scope.SelectedCondItemID };
            }
            $("#modalCondPicker").modal("hide");
        };

        $scope.AddEnterCondItem = function() {
            $scope.CRUDGuideLine.EnterCondItemList.push({});
        };

        $scope.ShowEnterCondPicker = function(c) {
            $scope.CurrentCondItmeList = "Enter";
            $scope.CurrentEnterCondItemIndex = $scope.CRUDGuideLine.EnterCondItemList.indexOf(c);
            $scope.SelectedCondItemID = c.ID;
            $scope.SelectedCondItemName = c.DisplayName;

            var condItemTree = $('#condItemTree').data('treeview');
            var nodes = condItemTree.getNodeByValue(c.ID);
            if (nodes != null && nodes.length > 0) {
                for (var i = 0; i < nodes.length; i++) {

                    if (nodes[i].parentId != null) {
                        if (condItemTree.getNode(nodes[i].parentId).value <= 0) {
                            condItemTree.selectNode(nodes[i]);
                            break;
                        }
                    }
                }
            } else {
                angular.forEach(condItemTree.getSelected(), function(n) {
                    condItemTree.unselectNode(n);
                });
            }
            $("#modalCondPicker").modal("toggle");
        };

        $scope.DeleteEnterCondItem = function(c) {
            if (confirm("确定删除？")) {
                $scope.CRUDGuideLine.EnterCondItemList.splice($scope.CRUDGuideLine.EnterCondItemList.indexOf(c), 1);
            }
        };

        //--Enter
        //Out
        $scope.AddOutCondItem = function() {
            $scope.CRUDGuideLine.OutCondItemList.push({});
        };
        $scope.ShowOutCondPicker = function(c) {
            $scope.CurrentCondItmeList = "Out";
            $scope.CurrentOutCondItemIndex = $scope.CRUDGuideLine.OutCondItemList.indexOf(c);
            $scope.SelectedCondItemID = c.ID;
            $scope.SelectedCondItemName = c.DisplayName;

            var condItemTree = $('#condItemTree').data('treeview');
            var nodes = condItemTree.getNodeByValue(c.ID);
            if (nodes != null && nodes.length > 0) {
                for (var i = 0; i < nodes.length; i++) {

                    if (nodes[i].parentId != null) {
                        if (condItemTree.getNode(nodes[i].parentId).value <= 0) {
                            condItemTree.selectNode(nodes[i]);
                            break;
                        }
                    }
                }
            } else {
                angular.forEach(condItemTree.getSelected(), function(n) {
                    condItemTree.unselectNode(n);
                });
            }
            $("#modalCondPicker").modal("toggle");
        };

        $scope.DeleteOutCondItem = function(c) {
            if (confirm("确定删除？")) {
                $scope.CRUDGuideLine.OutCondItemList.splice($scope.CRUDGuideLine.OutCondItemList.indexOf(c), 1);
            }
        };
        //--Out
        $scope.Clone = function(obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };

        //添加节点
        $scope.AddProcessItem = function(model) {
            var processInfo = _canvas.getProcessInfo(); //连接信息
            var flag = true;
            $.each($.parseJSON(processInfo), function(name, value) {
                if (name == model.ID) {
                    flag = false;
                    return flag;
                }
            });

            if (flag) {
                var data = {
                    "status": 1,
                    "msg": "success",
                    "info": {
                        "id": model.ID,
                        "flow_id": flow_id,
                        "process_name": model.Name,
                        "process_to": "",
                        "icon": "",
                        "style": "left:" + mLeft + ";top:" + mTop + ";color:#0e76a8;"
                    }
                };

                if (!data.status) {
                    mAlert(data.msg);
                } else if (!_canvas.addProcess(data.info)) //添加
                {
                    mAlert("添加失败");
                } else {
                    $("#divAddProcess").modal('toggle');
                }
            } else {
                alert("该节点已添加");
            }
        }



        /*保存*/
        $("#leipi_save").bind('click', function() {
            //判断是否存在多对一的情况
            //var processInfo = _canvas.getProcessInfo();
            var list = $.parseJSON(_canvas.getProcessInfo());

            $.each(list, function(name, value) {
                var firstNode = name;
                var i = 0;
                $.each(list, function(name, value) {
                    var processTo = value.ProcessTo;
                    if (processTo.indexOf(firstNode) >= 0) {
                        i++;
                        if (i >= 2)return false;
                    }
                });
                if (i >= 2) {
                    alert("存在多节点指向一节点的情况，该节点名称为：" + value.process_Name);
                    return false;
                }
            });

            $("#divSave").modal("toggle");
        });


        $scope.SaveFlow = function() {
            $scope.GuideLineFlow.GUIDELINEINFO = _canvas.getProcessInfo();
            guideLineFlowServices.save({ Data: $scope.GuideLineFlow }, function(obj) {
                if (obj.Data) {
                    if (confirm("保存成功，是否返回列表页面?")) {
                        window.location = "/#/GuideLineFlowList";
                    } else {
                        $("#divSave").modal("toggle");
                    }
                }
            }, function (errRespose) {
                alert("发生错误,获取数据失败!");
                console.log("guideLineFlowServices.get:" + errRespose.data.Message);
            });
        }


        /*清除*/
        $("#leipi_clear").bind('click', function () {

            if (_canvas.clear()) {
                //alert("清空连接成功");
                mAlert("清空连接成功，你可以重新连接");
            } else {
                //alert("清空连接失败");
                mAlert("清空连接失败");
            }
        });

        function AddProcessFunc(model) {

            var data = {
                "status": 1,
                "msg": "success",
                "info": {
                    "id": $(model).attr("id"),
                    "flow_id": flow_id,
                    "process_name": $(model).attr("name"),
                    "process_to": "",
                    "icon": "",
                    "style": "left:" + $(model).attr("left") + ";top:" + $(model).attr("top") + ";color:#0e76a8;"
                }
            };

            if (!data.status) {
                mAlert(data.msg);
            } else if (!_canvas.addProcess(data.info)) //添加
            {
                mAlert("添加失败");
            }
            $("#divAddProcess").hide();
        }

        /**
          * 弹出窗选择用户部门角色
          * showModalDialog 方式选择用户
          * URL 选择器地址
          * viewField 用来显示数据的ID
          * hidField 隐藏域数据ID
          * isOnly 是否只能选一条数据
          * dialogWidth * dialogHeight 弹出的窗口大小
          */
        function superDialog(URL, viewField, hidField, isOnly, dialogWidth, dialogHeight) {
            dialogWidth || (dialogWidth = 620), dialogHeight || (dialogHeight = 520), loc_x = 500, loc_y = 40, window._viewField = viewField, window._hidField = hidField;
            // loc_x = document.body.scrollLeft+event.clientX-event.offsetX;
            //loc_y = document.body.scrollTop+event.clientY-event.offsetY;
            if (window.ActiveXObject) { //IE
                var selectValue = window.showModalDialog(URL, self, "edge:raised;scroll:1;status:0;help:0;resizable:1;dialogWidth:" + dialogWidth + "px;dialogHeight:" + dialogHeight + "px;dialogTop:" + loc_y + "px;dialogLeft:" + loc_x + "px");
                if (selectValue) {
                    //callbackSuperDialog(selectValue);
                }
            } else { //非IE
                var selectValue = window.open(URL, 'newwindow', 'height=' + dialogHeight + ',width=' + dialogWidth + ',top=' + loc_y + ',left=' + loc_x + ',toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');

            }
        }

        var alertModal = $('#alertModal'), attributeModal = $("#attributeModal");
        //消息提示
        mAlert = function(messages, s) {
            if (!messages) messages = "";
            if (!s) s = 2000;
            alertModal.find(".modal-body").html(messages);
            alertModal.modal('toggle');
            setTimeout(function() { alertModal.modal("hide") }, s);
        }

        //属性设置
        attributeModal.on("hidden", function() {
            $(this).removeData("modal"); //移除数据，防止缓存
        });
        ajaxModal = function(url, fn) {
            url += url.indexOf('?') ? '&' : '?';
            url += '_t=' + new Date().getTime();
            attributeModal.find(".modal-body").html('<img src="Public/images/loading.gif"/>');
            attributeModal.modal({
                remote: url
            });

            //加载完成执行
            if (fn) {
                attributeModal.on('shown', fn);
            }
        }

        //刷新页面
        function page_reload() {
            location.reload();
        }

    }
]);