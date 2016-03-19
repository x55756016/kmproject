var app = angular.module("Cond.Ctrls", []);

///元数据
app.controller('MetaDataCtrl', ['$scope', '$http', '$location', '$routeParams', 'MetaDataServices', 'DictionaryServices',
     function ($scope, $http, $location, $routeParams, MetaDataServices, DictionaryServices) {
         $scope.Init = function () {
             DictionaryServices.get({ keyWord: "DataType" }
                 , function (data) {
                     $scope.DicDataTypeList=data.Data
                 }, function (errRespose) {
                     alert("发生错误,查询失败!")
                     console.log("DictionaryServices.get:" + errRespose.data.Message);
                 });
             DictionaryServices.get({ keyWord: "DataSourceType" }
                , function (data) {
                    $scope.DicDataSourceTypeList = data.Data
                }, function (errRespose) {
                    alert("发生错误,查询失败!")
                    console.log("DictionaryServices.get:" + errRespose.data.Message);
                });
             $scope.Search();
         }
         $scope.Search = function () {
             MetaDataServices.get({ Keyword: $scope.keyWord }
            , function (data) {
                $scope.MetaDataList = data.Data;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("MetaDataServices.get:" + errRespose.data.Message);
            });
         }
         $scope.Init();
         $scope.AddMetaData = function () {
             $scope.CRUDMetaData = { ID: 0 };
             $("#modalMetaData").modal("toggle");
         };

         $scope.EditMetaData = function (m) {
             $scope.CRUDMetaData = $scope.Clone(m);
             $("#modalMetaData").modal("toggle");
         };
         $scope.DeleteMetaData = function (m) {
             if (confirm("确定删除？")) {
                 MetaDataServices.remove({ id: m.ID }
                    , function (data) {
                        alert('删除成功');
                        $scope.Search();
                    }, function (errRespose) {
                        alert("发生错误,查询失败!")
                        console.log("MetaDataServices.remove:" + errRespose.data.Message);
                    });
             }
         };
         $scope.EditMetaDataParam = function (m) {
             $location.path("MetaDataParam/" + m.ID);
         };
         $scope.SaveMetaData = function () {
             MetaDataServices.save({ Data: $scope.CRUDMetaData }
             , function (data) {
                 $scope.Search();
                 $("#modalMetaData").modal("toggle");
             }, function (errRespose) {
                 alert("发生错误,查询失败!")
                 console.log("MetaDataServices.save:" + errRespose.data.Message);
             });
         };

         $scope.GetDataTypeText = function (x) {
             return "GetDataTypeText";
         };

         $scope.GetDataSourceTypeText = function (x) {
             return "GetDataSourceTypeText";
         };

         $scope.Clone = function (obj) {
             var txt = angular.toJson(obj);
             return JSON.parse(txt);
         };
     }
]);


///元数据参数
app.controller('MetaDataParamCtrl', ['$scope', '$http', '$location', '$routeParams', 'MetaDataServices', 'MetaDataParamServices',
     function ($scope, $http, $location, $routeParams, MetaDataServices, MetaDataParamServices) {
         $scope.Search = function () {
             MetaDataParamServices.get({ keyWord: $scope.MetaDataID }
                , function (data) {
                    $scope.MetaDataParamList = data.Data;
                }, function (errRespose) {
                    alert("发生错误,查询失败!")
                    console.log("MetaDataParamServices.get:" + errRespose.data.Message);
                });
         }
         $scope.Init = function () {
             $scope.MetaDataID = $routeParams.mid;
             MetaDataServices.get({ ID: $scope.MetaDataID }
            , function (data) {
                $scope.MetaData = data.Data;
            }, function (errRespose) {
                alert("发生错误,查询失败!")
                console.log("MetaDataServices.get:" + errRespose.data.Message);
            });
             $scope.Search();
         };
         $scope.Init();
         $scope.AddMetaDataParam = function () {
             $scope.CRUDMetaDataParam = { ID: 0, MetaDataID: $scope.MetaDataID };
             $("#modalMetaDataParam").modal("toggle");
         };

         $scope.EditMetaDataParam = function (m) {
             $scope.CRUDMetaDataParam = $scope.Clone(m);
             $("#modalMetaDataParam").modal("toggle");
         };
         $scope.DeleteMetaDataParam = function (m) {
             if (confirm("确定删除？")) {
                 MetaDataParamServices.remove({ id: m.ID }
                    , function (data) {
                        alert('删除成功');
                        $scope.Search();
                    }, function (errRespose) {
                        alert("发生错误,查询失败!")
                        console.log("MetaDataParamServices.save:" + errRespose.data.Message);
                    });
             }
         };
         $scope.ReBack = function () {
             $location.path("MetaData");
         };
         $scope.SaveMetaDataParam = function () {
             MetaDataParamServices.save({ Data: $scope.CRUDMetaDataParam }
             , function (data) {
                 $scope.Search();
                 $("#modalMetaDataParam").modal("toggle");
             }, function (errRespose) {
                 alert("发生错误,查询失败!")
                 console.log("MetaDataParamServices.save:" + errRespose.data.Message);
             });
         };
         $scope.Clone = function (obj) {
             var txt = angular.toJson(obj);
             return JSON.parse(txt);
         };
     }
]);