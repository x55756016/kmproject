var app = angular.module("GuideLineCtrl", []);
///元数据
app.controller('GuideLineCtrl', ['$scope', '$http', '$location', '$routeParams', 'GuideLineServices', 'DictionaryServices',
    function($scope, $http, $location, $routeParams, GuideLineServices, DictionaryServices) {

        $scope.Init = function() {
            DictionaryServices.get({ keyWord: "LogicOperator" }, function(data) {
                $scope.DicLogicOperatorList = data.Data
            }, function(errRespose) {
                alert("发生错误,查询失败!")
                console.log("DictionaryServices.get:" + errRespose.data.Message);
            });
            DictionaryServices.get({ keyWord: "GuideLineMetaData" }, function(data) {
                if (data.Data != null && data.Data.length > 0) {
                    var datalist = data.Data[0].value.replace("，", ",").split(',');
                    var metaDataList = [];
                    angular.forEach(datalist, function(d) {
                        metaDataList.push({ Text: d, Value: null });
                    });
                    $scope.MetaDataList = metaDataList;
                }
            }, function(errRespose) {
                alert("发生错误,查询失败!")
                console.log("DictionaryServices.get:" + errRespose.data.Message);
            });
            //$scope.BindGuideLine();
            $scope.Search();
            $scope.GetProducts();
        };
        //$scope.BindGuideLine = function() {
        //    GuideLineServices.get({ Keyword: "" }
        //                   , function (data) {
        //                       $scope.AllGuideLineList = data.Data;

           
        //        }, function (errRespose) {
        //                       alert("发生错误,查询失败!")
        //                       console.log("GuideLineServices.get:" + errRespose.data.Message);
        //                   });
        //};

        $scope.CurrentPage = 1; //当前页面
        $scope.SearchCondition = function() {
            $scope.CurrentPage = 1;
            $scope.Search();
        };
        $scope.Search = function() {
            GuideLineServices.get({ Keyword: $scope.keyWord, CurrentPage: $scope.CurrentPage, PageSize: 10 }, function(data) {
                $scope.GuideLineList = data.Data;

                var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function(curPage) {
                    $scope.CurrentPage = curPage;
                    $scope.Search();
                });

            }, function(errRespose) {
                alert("发生错误,查询失败!");
                console.log("GuideLineServices.get:" + errRespose.data.Message);
            });
        };

        //重置条件
        $scope.ResetSearch = function() {
            $scope.keyWord = "";
        }




        $scope.GetProducts = function() {
            $http.get('/api/Products').success(function(data) {
                $scope.Products = data.Data;
            }).error(function(xlr) {
                $scope.Products = [];
            });
        };

        $scope.Init();

        //添加guideline
        $scope.AddGuideLine = function() {
            $scope.CRUDGuideLine = { IsInherit: true, EnterCondItemList: [{}, {}], OutCondItemList: [{}, {}], MetaDataList: $scope.Clone($scope.MetaDataList), Products: [], ParentList: [] };
            $("#modalGuideLine").modal("show");

            $scope.GetParentGLString();
        };

        //编辑guideline
        $scope.EditGuideLine = function(g) {
            $scope.CRUDGuideLine = $scope.Clone(g);

            console.log(JSON.stringify($scope.CRUDGuideLine));


            //$scope.CRUDGuideLine.Products = [];
            $("#modalGuideLine").modal("show");

            $scope.GetParentGLString();
        };

        $scope.DeleteGuideLine = function(g) {
            if (confirm("确定删除？")) {
                GuideLineServices.remove({ id: g.ID }, function(data) {
                    alert('删除成功');
                    $scope.Search();
                }, function(errRespose) {
                    alert("发生错误,查询失败!");
                    console.log("GuideLineServices.remove:" + errRespose.data.Message);
                });
            }
        };
        $scope.CheckData = function() {
            var checkResult = true;
            var alertMsg = "";
            //if ($scope.CRUDGuideLine.ParentID == $scope.CRUDGuideLine.ID) {
            //    alertMsg += "不能选择自己做父临床路径!";
            //    checkResult = false;
            //}
            /*Todo 讨论是否需要限制条件
            if ($scope.CRUDGuideLine.EnterLogicalOperator == 2 && $scope.CRUDGuideLine.EnterCondItemList.length < 1) {
                alertMsg += "请至少选择一个进入条件!";
                checkResult= false;
            }
            if (($scope.CRUDGuideLine.EnterLogicalOperator == 0 || $scope.CRUDGuideLine.EnterLogicalOperator == 1) && $scope.CRUDGuideLine.EnterCondItemList.length < 2) {
                alertMsg += "请至少选择两个进入条件!";
                checkResult= false;
            }
            
            if ($scope.CRUDGuideLine.OutLogicalOperator == 2 && $scope.CRUDGuideLine.OutCondItemList.length < 1) {
                alertMsg += (alertMsg == "" ? "" : "\r\n") + "请至少选择一个退出条件!";
                checkResult = false;
            }
            if (($scope.CRUDGuideLine.OutLogicalOperator == 0 || $scope.CRUDGuideLine.OutLogicalOperator == 1) && $scope.CRUDGuideLine.OutCondItemList.length < 2) {
                alertMsg += (alertMsg == "" ? "" : "\r\n") + "请至少选择两个退出条件!";
                checkResult = false;
            }
            */
            if (!checkResult) {
                alert(alertMsg);
            }
            return checkResult;
        };
        $scope.SaveGuideLine = function() {
            if ($scope.CheckData()) {
                GuideLineServices.save({ Data: $scope.CRUDGuideLine }, function(data) {
                    $scope.Search();
                    //$scope.BindGuideLine();
                    $("#modalGuideLine").modal("toggle");
                }, function(errRespose) {
                    alert("发生错误,查询失败!");
                    console.log("GuideLineServices.get:" + errRespose.data.Message);
                });
            }
        };
        //CondPicker
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
        //--CondPicker
        //Enter
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

        $scope.AddProductItem = function() {
            $('#modalProduct').modal('toggle');
        };

        $scope.CheckProduct = function(obj) {
            for (var i = 0; i < $scope.CRUDGuideLine.Products.length; i++) {
                if (obj.ProductId == $scope.CRUDGuideLine.Products[i].Productid) {
                    KMAlert({ msg: '该产品已存在' });
                    return;
                }
            }

            $('#modalProduct').modal('toggle');
            var _item = { GuidelineProductID: '', GuidelineID: '', Productid: obj.ProductId, ProductName: obj.ProductName, ProductPrice: obj.SalePrice, Productdes: obj.ProductRemark };
            $scope.CRUDGuideLine.Products.push(_item);
        };

        $scope.RemoveProduct = function(obj, i) {
            if (obj.GuidelineProductId != '')
                $http.delete('/api/GuideLine?gid=' + obj.GuidelineId + '&gpid=' + obj.GuidelineProductId);

            $scope.CRUDGuideLine.Products.splice(i, 1);
        }

        $scope.ParentGuideLineList = {}; //筛选父类的guideline
        $scope.ParentCurrentPage = 1;



        $scope.SearchParentGuideLineList=function() {
            $scope.ParentCurrentPage = 1;
            $scope.LoadParentGuideLineList();
        }



        //加载弹出窗体的选项
        $scope.LoadParentGuideLineList = function() {
            GuideLineServices.get({ Keyword: $scope.keyWordParent, CurrentPage: $scope.ParentCurrentPage, PageSize: 10 }, function (data) {

                var id = $scope.CRUDGuideLine.ID;
                if (id != undefined) { 
                    for (var i = 0; i < data.Data.length; i++) {
                        if (data.Data[i].ID == id) {
                            data.Data.splice(i, 1); //删除掉自身节点
                        }
                    }
                }

                $scope.ParentGuideLineList = data.Data;
                var pager2 = new Pager('pager2', $scope.ParentCurrentPage, data.PagesCount, function(curPage) {
                    $scope.ParentCurrentPage = curPage;
                    $scope.LoadParentGuideLineList();
                });
            }, function(errRespose) {
                alert("发生错误,查询失败!");
                console.log("GuideLineServices.get:" + errRespose.data.Message);
            });
        }


        $scope.GetCheckItem = function(id) {
            var flag = false;
            for (var i = 0; i < $scope.CRUDGuideLine.ParentList.length; i++) {

                if (id == $scope.CRUDGuideLine.ParentList[i].PARENTID) {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        $scope.Getchange = function($event, model) {
            var flag = $event.target.checked;

            if (flag) {
                var data = {
                    Id: "",
                    GuideLineId: "",
                    ParentName: model.Name,
                    PARENTID: model.ID
                };
                $scope.CRUDGuideLine.ParentList.push(data);
            } else {
                for (var i = 0; i < $scope.CRUDGuideLine.ParentList.length; i++) {

                    if (model.ID == $scope.CRUDGuideLine.ParentList[i].PARENTID) {
                        $scope.CRUDGuideLine.ParentList.splice(i, 1);
                        break;
                    }
                }
            }
            $scope.GetParentGLString();
        }

        $scope.ParentString = "";
        $scope.GetParentGLString = function() {
            var str = "";
            for (var i = 0; i < $scope.CRUDGuideLine.ParentList.length; i++) {
                str = str + $scope.CRUDGuideLine.ParentList[i].ParentName + ",";
            }
            $scope.ParentString= str;
        };
    }
]);