var app = angular.module("Member", []).controller("MemberCtrl", [
    "$scope", "$http", "$location", "$routeParams", "MemberServices","DictionaryServices",
    function ($scope, $http, $location, $routeParams, memberServices, dictionaryServices) {
        $scope.CurrentPage = 1;

        $scope.MemberLevelList = {};
        //加载数据
        dictionaryServices.get({ Keyword: "MemberLevel" }, function (obj) {
            if (obj) {
                $scope.MemberLevelList = obj.Data[0].nodes;
            }
        });


        $scope.SearchCondition = function() {
            $scope.CurrentPage = 1;
            $scope.Search();
        }

        $scope.Search = function() {
            memberServices.get({ CurrentPage: $scope.CurrentPage, PageSize: 10, Type: $scope.Keyword },
                function(obj) {
                    $scope.ListItems = obj.Data;
                    var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function(curPage) {
                        $scope.CurrentPage = curPage;
                        $scope.Search();
                    });
                });
        };
        $scope.Search();

        $scope.Remove = function(id) {
            if (id == "") {
                return "";
            }
            if (confirm("确定删除该会员吗？")) {
                memberServices.delete({ memberId: id }, function () {
                    $scope.Search();
                });
            }
        };

   
    }
]).controller("MemberInfoCtrl", [
    "$scope", "$http", "$location", "$routeParams", "MemberServices","DictionaryServices","ProductsServices",
    function ($scope, $http, $location, $routeParams, memberServices, dictionaryServices, productsServices) {

        $scope.MemberLevelList = {};
        //加载数据
        dictionaryServices.get({ Keyword: "MemberLevel" }, function(obj) {
            if (obj) {
                $scope.MemberLevelList = obj.Data[0].nodes;
            }
        });

        dictionaryServices.get({ Keyword: "MemberServiceUnit" }, function (obj) {
            if (obj) {
                $scope.ServiceUnitList = obj.Data[0].nodes;
            }
        });

        productsServices.get({}, function (data) {
            $scope.productsList = data.Data;
        }, function (errRespose) {
            alert("发生错误,查询失败!");
            console.log("ProductServices.get:" + errRespose.data.Message);
        });

        var memberId = $routeParams.mId; //会员ID




        $scope.Model = {};
        if ($routeParams.mId != 0) {
            memberServices.get({ ID: memberId }, function(obj) {
                if (obj) {
                    $scope.Model = obj.Data;
                }
            });
        } else {
            var data= {
                "MEMBERID": "",
                "MEMBERNAME": "",
                "MEMBERLEVEL": "",
                "MEMBERLEVELName": null,
                "MEMBERPRICE": "",
                "MEMBERUNIT": null,
                "MEMBERDESCRIPT": "",
                "menberProductList": []
            }
            $scope.Model =data;
        }

        $scope.Back=function() {
            location = "/#/MemberList";
        }


        $scope.AddOrUpdateFlag = { Action: "", Title: "" };//用于标识是添加还是更新
        $scope.AddFlag = function() {
            $scope.AddOrUpdateFlag = { Action: "Add", Title: "添加" };//添加操作
        };

        $scope.UpdateFlag = function (productId) {
            $scope.AddOrUpdateFlag = { Action: "Update", Title: "更新" };//更新操作


            //从数组中取出该数据
            for (var index = 0; index < $scope.Model.menberProductList.length; index++) {
                if ($scope.Model.menberProductList[index].PRODUCTID == productId) {

                    $scope.Item = $scope.Clone($scope.Model.menberProductList[index]);

                    $scope.Item.MEMBERNAME = $scope.Model.menberProductList[index].PRODUCTID + "," + $scope.Model.menberProductList[index].MEMBERNAME;

                    $scope.Item.PRODUCTUNIT = $scope.Model.menberProductList[index].PRODUCTUNIT + "," + $scope.Model.menberProductList[index].PRODUCTUNITName;

                    break;
                }
            }

        };

        $scope.AddMemPro = function() {
            var product = $scope.Item.MEMBERNAME;
            var unit = $scope.Item.PRODUCTUNIT;

            if ($scope.AddOrUpdateFlag.Action == "Add") { //添加操作
                var flag = true;
                for (var index = 0; index < $scope.Model.menberProductList.length; index++) {
                    if ($scope.Model.menberProductList[index].PRODUCTID == product.split(',')[0]) {
                        flag = false;
                        break;
                    }
                }

                if (flag) {
                    var data = {
                        "MEMBERPRODUCTID": new GUID().newGUID(),
                        "MEMBERNAME": product.split(',')[1],
                        "MEMBERID": memberId,
                        "PRODUCTNAME": product.split(',')[0],
                        "PRODUCTID": product.split(',')[0],
                        "PRODUCTUNIT": unit.split(',')[0],
                        "PRODUCTUNITName": unit.split(',')[1],
                        "PRODUCTNUMBER": $scope.Item.PRODUCTNUMBER,
                        "MEMPRODESCRIPT": $scope.Item.MEMPRODESCRIPT
                    };
                    $scope.Model.menberProductList.push(data);

                } else {
                    alert("该项已添加，请选择其他项");
                }
            } else if ($scope.AddOrUpdateFlag.Action == "Update") { //更新操作

                for (var index = 0; index < $scope.Model.menberProductList.length; index++) {
                    //1.从数据中取出对象
                    //2.判断更新的产品是否已经添加了
                    if ($scope.Model.menberProductList[index].MEMBERPRODUCTID == $scope.Item.MEMBERPRODUCTID) {
                        var flag = true;
                        for (var i = 0; i < $scope.Model.menberProductList.length; i++) {
                            if ($scope.Model.menberProductList[i].PRODUCTID == product.split(',')[0]  && index!=i) {
                                flag = false;
                                break;
                            }
                        }

                        if (flag) {
                            $scope.Model.menberProductList[index].MEMBERNAME = $scope.Model.menberProductList[index].MEMBERNAME;
                            $scope.Model.menberProductList[index].PRODUCTNAME = product.split(',')[0];
                            $scope.Model.menberProductList[index].PRODUCTID = product.split(',')[0];
                            $scope.Model.menberProductList[index].PRODUCTUNIT = unit.split(',')[0];
                            $scope.Model.menberProductList[index].PRODUCTUNITName = unit.split(',')[1];
                            $scope.Model.menberProductList[index].PRODUCTNUMBER = $scope.Item.PRODUCTNUMBER;
                            $scope.Model.menberProductList[index].MEMPRODESCRIPT = $scope.Item.MEMPRODESCRIPT;
                            break;
                        } else {
                            alert("该项已添加，请选择其他项");
                        }
                    }
                }
            }
            $scope.Item = {};
            $("#addItem").modal("toggle");
        };

        
        //删除产品事件
        $scope.Remove = function(id) {
            if (id == "") {
                return "";
            }
            for (var index = 0; index < $scope.Model.menberProductList.length; index++) {
                if ($scope.Model.menberProductList[index].MEMBERPRODUCTID == id) {
                    $scope.Model.menberProductList.splice(index, 1);
                    break;
                }
            }
        };

        //保存
        $scope.SaveMemberProduct = function () {
            $scope.Model.MEMBERLEVEL = Number($scope.Model.LevelString);    
            memberServices.save({ Data: $scope.Model }, function (obj) {
                if (obj) {
                    if (obj.Data==true) { //执行成功
                        location = "/#/MemberList";
                    }
                }
            });
        };

        $scope.Clone = function (obj) {
            var txt = angular.toJson(obj);
            return JSON.parse(txt);
        };
    }
]);