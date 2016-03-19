var app = angular.module("CondItemCtrl", []);
///元数据
app.controller('CondItemCtrl', ['$scope', '$http', '$location', '$routeParams', 'ConditionItemServices','DictionaryServices',
function ($scope, $http, $location, $routeParams, ConditionItemServices, DictionaryServices) {
         $scope.SelectedMetaDataID = 0;
         $scope.SelectedMetaDataName = "";
         $scope.SelectedCondItemID = 0;
         $scope.SelectedCondItemName = "";
         
         $scope.Init = function () {
             DictionaryServices.get({ keyWord: "DataType" }
                 , function (data) {
                     $scope.DicDataTypeList = data.Data
                 }, function (errRespose) {
                     alert("发生错误,查询失败!")
                     console.log("DictionaryServices.get:" + errRespose.data.Message);
                 });
             DictionaryServices.get({ keyWord: "Operator" }
               , function (data) {
                   $scope.DicOperatorList = data.Data
               }, function (errRespose) {
                   alert("发生错误,查询失败!")
                   console.log("DictionaryServices.get:" + errRespose.data.Message);
               });
             DictionaryServices.get({ keyWord: "LogicOperator" }
               , function (data) {
                   $scope.DicLogicOperatorList = data.Data
               }, function (errRespose) {
                   alert("发生错误,查询失败!")
                   console.log("DictionaryServices.get:" + errRespose.data.Message);
               });
             $scope.Search();
         };
         $scope.Search = function () {
             ConditionItemServices.get({ Keyword: $scope.scKeyWord }
            , function (data) {
                $scope.CondItemList = data.Data;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("ConditionItemServices.get:" + errRespose.data.Message);
            });
         };
         $scope.Init();
        //MetaDataPicker
         $scope.SetMetaData = function (text, value) {
             $scope.SelectedMetaDataID = value;
             $scope.SelectedMetaDataName = text;
         };
         $scope.SaveMetaDataPicker = function () {
             $scope.CRUDCondItem.MetaDataName = $scope.SelectedMetaDataName;
             $scope.CRUDCondItem.MetaDataID = $scope.SelectedMetaDataID;
             $("#modalMetaDataPicker").modal("hide");
         };
    //--MetaDataPicker

    //CondPicker
         $scope.SetCondItem = function (text, value) {
             $scope.SelectedCondItemID = value;
             $scope.SelectedCondItemName = text.replace("（与）","").replace("（或）","").replace("（非）","");
         };
         $scope.SaveCondPicker = function () {
             $scope.CRUDCondItem.ComboCondItemList[$scope.CurrentCondItemIndex] = { DisplayName: $scope.SelectedCondItemName, ID: $scope.SelectedCondItemID };
             $("#modalCondPicker").modal("hide");
         };
    //--CondPicker

         $scope.AddCondItem = function () {
             $scope.CRUDCondItem = { CondType: 0,ComboCondItemList:[] };
             $("#modalCondItem").modal("toggle");
         };
         $scope.SaveCondItem = function () {
             if ($scope.CheckData())
             {
                 ConditionItemServices.save({Data:$scope.CRUDCondItem}
                 , function (data) {
                     $scope.Search();
                     $("#modalCondItem").modal("toggle");
                 }, function (errRespose) {
                     alert("发生错误,查询失败!")
                     console.log("ConditionItemServices.get:" + errRespose.data.Message);
                 });
             }
         };
         $scope.CheckData = function () {
             if ($scope.CRUDCondItem.CondType == 0) return true;
             var find = $.grep($scope.CRUDCondItem.ComboCondItemList, function (item) {
                 return item.ID == $scope.CRUDCondItem.ID;
             });
             if($scope.CRUDCondItem.LogicalOperator==2 && $scope.CRUDCondItem.ComboCondItemList.length<1)
             {
                 alert("请至少选择一个条件!");
                 return false;
             }
             if(($scope.CRUDCondItem.LogicalOperator==0 ||$scope.CRUDCondItem.LogicalOperator==1)&& $scope.CRUDCondItem.ComboCondItemList.length<2)
             {
                 alert("请至少选择两个条件!");
                 return false;
             }
             if (find != null && find.length > 0) {
                 alert("组合条件不能以自己为逻辑条件!");
                 return false;
             } 

             return true;
         };
         $scope.EditCondItem = function (m) {
             $scope.CRUDCondItem = $scope.Clone(m);
             $("#modalCondItem").modal("toggle");
         };
         $scope.DeleteCondItem = function (m) {
             if (confirm("确定删除？")) {
                 ConditionItemServices.remove({ id: m.ID }
                    , function (data) {
                        alert('删除成功');
                        $scope.Search();
                    }, function (errRespose) {
                        alert("发生错误,查询失败!")
                        console.log("ConditionItemServices.remove:" + errRespose.data.Message);
                    });
             }
         };
         $scope.AddComboCondItem = function () {
             $scope.CRUDCondItem.ComboCondItemList.push({ ID: 0, DisplayName: "" });
         };
         $scope.DeleteComboCondItem = function (c) {
             if (confirm("确定删除？")) {
                 $scope.CRUDCondItem.ComboCondItemList.splice($scope.CRUDCondItem.ComboCondItemList.indexOf(c), 1);
             }
         };
         
         $scope.ShowCondPicker = function (c) {
             $scope.CurrentCondItemIndex = $scope.CRUDCondItem.ComboCondItemList.indexOf(c);
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
             }
             else
             {
                 angular.forEach(condItemTree.getSelected(), function (n) {
                     condItemTree.unselectNode(n);
                 });
             }
             $("#modalCondPicker").modal("toggle");
         };
         
         $scope.SetCondType = function (value) {
             $scope.CRUDCondItem.CondType = value;
             if (value == 1) {
                 $scope.CRUDCondItem.DataType = 3;
             }
             else
             {
                 $scope.CRUDCondItem.DataType = null;
             }
         };

         $scope.Clone = function (obj) {
             var txt = angular.toJson(obj);
             return JSON.parse(txt);
         };
     }
]);