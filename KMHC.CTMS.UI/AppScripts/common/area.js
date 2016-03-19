var app = angular.module("common", []);

//地区control
//值相互传递如<div class="panel-group" ng-include="'/Views/Common/Area.html'" ng-init="CurrentAreas = Areas"></div>
//必须定义Areas变量，格式如下：
//Areas格式如下 { ProvinceId: "320000", CityId: "320100", CountryId: "320102", TownId: "32010201", Address: "test" } 
app.controller("areaCtr", ["$scope", "$http", "dictionary", "AreaServices", function ($scope, $http, dictionary, areaServices) {
        $scope.Data = {};

        $scope.Data.Province = "";
        $scope.Data.City = "";
        $scope.Data.Country ="";
        $scope.Data.Town = "";
        $scope.Data.Address ="";
    
        $scope.Area = {};
        $scope.Area.Provinces = {};
        $scope.Area.Citys = {};
        $scope.Area.Countrys = {};
        $scope.Area.Towns = {};
        $scope.Area.IsShow = false; //四级地区是否显示标记

        $scope.Area.Defaut = [{ AreaId: "", AreaName: "请选择", Parent: "", Desc: "", Items: [] }];


        $scope.$on("AreaLoadSuccess", function (event, data) {
            $scope.CurrentAreas = data;
            if (data.ProvinceId != "") {
                $scope.Data.Province = $scope.CurrentAreas.ProvinceId;

                $scope.getAreaList("Citys", $scope.CurrentAreas.ProvinceId);
                $scope.Data.City = $scope.CurrentAreas.CityId;

                $scope.getAreaList("Countrys", $scope.CurrentAreas.CityId);
                $scope.Data.Country = $scope.CurrentAreas.CountryId;

                $scope.getAreaList("Towns", $scope.CurrentAreas.CountryId);
                $scope.Data.Town = $scope.CurrentAreas.TownId;

                $scope.Data.Address = $scope.CurrentAreas.Address;
                if ($scope.CurrentAreas.TownId != undefined) {
                    $scope.Area.IsShow = true;
                }
            }
        });
        //一级地区更改事件
        $scope.provincesChanged = function (provinceId) {

            $scope.Area.IsShow = false;
            $scope.CurrentAreas.ProvinceId = provinceId;
            $scope.CurrentAreas.CityId = "";
            $scope.CurrentAreas.CountryId = "";
            $scope.CurrentAreas.TownId = "";

            //$scope.Area.Citys = $scope.Area.Defaut;
            $scope.getAreaList("Citys", provinceId);
            $scope.Area.Countrys = $scope.Area.Defaut;
            $scope.Area.Towns = $scope.Area.Defaut;

            $scope.Data.City = "";
            $scope.Data.Country = "";
            $scope.Data.Town = "";
            //$scope.setCitys(provinceId);
        };

        //二级地区更改事件
        $scope.citysChanged = function (cityId) {
            if (cityId == null) {
                $scope.Area.IsShow = false;
                $scope.Area.Countrys = $scope.Area.Defaut;
                $scope.Area.Towns = $scope.Area.Defaut;

                $scope.CurrentAreas.CityId = "";
                $scope.CurrentAreas.CountryId = "";
                $scope.CurrentAreas.TownId = "";
            } else {

                $scope.Area.IsShow = false;

                $scope.getAreaList("Countrys", cityId);
                $scope.Area.Towns = $scope.Area.Defaut;

                $scope.CurrentAreas.CityId = cityId;
                $scope.CurrentAreas.CountryId = "";
                $scope.CurrentAreas.TownId = "";

                $scope.Data.Country = "";
                $scope.Data.Town = "";
            }
        };

        //三级地区更改事件
        $scope.CountrysChanged = function(countryId) {
            $scope.Area.IsShow = false;
            $scope.CurrentAreas.CountryId = countryId;
            $scope.CurrentAreas.TownId = "";

            $scope.getAreaList("Towns", countryId);
            if ($scope.CurrentAreas.TownId != undefined) {
                $scope.Area.IsShow = true;
                $scope.Data.Town = "";
            }
        };

        //四级地区更改事件
        $scope.townsChanged = function(townId) {
            $scope.CurrentAreas.TownId = townId;
        };

        //详细地址更改事件
        $scope.addressChanged = function() {
            $scope.CurrentAreas.Address = $scope.Data.Address;
        };

       
        $scope.getAreaList = function(model,id) {
            areaServices.get({ "parentId": id }, function (obj) {
                if (model == "Provinces") {
                    $scope.Area.Provinces = obj.Data;
                } else if (model == "Citys") {
                    $scope.Area.Citys = obj.Data;
                } else if (model == "Countrys") {
                    $scope.Area.Countrys = obj.Data;
                } else if (model == "Towns") {
                    $scope.Area.Towns = obj.Data;
                }
            });
        }

        $scope.Init = function() {
            if ($scope.CurrentAreas == undefined || $scope.CurrentAreas == null) {  //如果不存在或者为空
                $scope.getAreaList("Provinces", 0);
                $scope.Data.Provinces = "";
                $scope.Data.City = "";
                $scope.Data.Country = "";
                $scope.Data.Town = "";
                $scope.Area.IsShow = false;
                $scope.CurrentAreas = {};
            }
        };
        $scope.Init();

    }
]);

//疾病选择器
app.controller("diseaseCtr", ["$scope", "$http", "diseaseServices", function ($scope, $http, services) {
    $http.get('/api/disease').success(function (data) {
        var treeData = eval(data);
        $scope.initSelectableTree = function () {
            return $('#tree').treeview({
                data: treeData,
                levels: 1,
                onNodeSelected: function (event, node) {
                    $scope.CurrentPage = 1;
                    $scope.Keyword = "";
                    $scope.Search(node.value);
                }
            });
        };
        $scope.findSelectableNodes = function () {
            if ($scope.KeywordType === "") {
                $scope.tree.treeview('collapseAll', { silent: true });
            }
            return $scope.tree.treeview('search', [$scope.KeywordType, { ignoreCase: false, exactMatch: false }]);
        };
        $scope.tree = $scope.initSelectableTree();
        $scope.selectableNodes = $scope.findSelectableNodes();
        $scope.SearchType = function () {
            $scope.selectableNodes = $scope.findSelectableNodes();
            $('.select-node').prop('disabled', !($scope.selectableNodes.length >= 1));
        }
    });

    $scope.CurrentPage = 1;
    $scope.Keyword = "";
    $scope.Search = function (types) {
        services.get({ currentPage: $scope.CurrentPage, pageSize: 10, types: types, key: $scope.Keyword }, function (res) {
            var obj = eval(res);
            $scope.ListItems = obj.Data;
            var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function (curPage) {
                $scope.CurrentPage = curPage;
                $scope.Search(types);
            });
        });
    };

    //$scope.chkAll = function () {
    //    $('#tbDisease :checkbox').attr("checked", !$scope.all);
    //    $scope.all = !$scope.all;
    //};
    //$scope.DiseaseCode = [];
    //$scope.DiseaseName = [];
    var updateSelected = function (action, id, name) {
        if (action === 'add' && $scope.DiseaseCode.indexOf(id) == -1) {
            $scope.DiseaseCode.push(id);
            $scope.DiseaseName.push(name);
        }
        if (action === 'remove' && $scope.DiseaseCode.indexOf(id) != -1) {
            var idx = $scope.DiseaseCode.indexOf(id);
            $scope.DiseaseCode.splice(idx, 1);
            $scope.DiseaseName.splice(idx, 1);
        }
    }

    $scope.updateSelection = function ($event, id) {
        var checkbox = $event.target;
        var action = (checkbox.checked ? 'add' : 'remove');
        updateSelected(action, id, checkbox.name);
    }
}
]);

//元数据选择器
app.controller("MetaDataPickerCtrl", ["$scope", "$http", "MetaDataPickerServices",
    function ($scope, $http, MetaDataPickerServices) {
        $scope.SearchMetaData = function () {
            MetaDataPickerServices.get({ Keyword: $scope.smKeyword }
                , function (treeData) {
                   var tree= $('#metaDataTree').treeview({
                        data: treeData.Data,
                        levels: 2,
                        onNodeSelected: function (event, node) {
                            if ($scope.$parent != null && $scope.SetMetaData!=null &&node.value!=null && node.value>0)
                            {
                                $scope.SetMetaData(node.text, node.value);
                                $scope.$parent.$apply();
                            }
                        }
                    });
                }, function (errRespose) {
                    alert("发生错误,查询失败!")
                    console.log("MetaDataPickerServices.get:" + errRespose.data.Message);
                });
        };
        $scope.SearchMetaData();
    }
]);

//条件选择器
app.controller("CondPickerCtrl", ["$scope", "$http", "CondPickerServices",'$routeParams',
    function ($scope, $http, CondPickerServices, $routeParams) {
        $scope.SearchCondItem = function () {
            CondPickerServices.get({ Keyword: $scope.scKeyword }
                , function (treeData) {
                    $('#condItemTree').treeview({
                        data: treeData.Data,
                        levels: 2,
                        onNodeSelected: function (event, node) {
                            if ($scope.$parent != null && $scope.SetCondItem != null && node.value != null && node.value > 0) {
                                $scope.SetCondItem(node.text, node.value);
                                $scope.$parent.$apply();
                            }
                        }
                    });
                }, function (errRespose) {
                    alert("发生错误,查询失败!")
                    console.log("CondPickerServices.get:" + errRespose.data.Message);
                });
        };
        $scope.SearchCondItem();
    }
]);


app.controller("userCtrl", ["$scope", '$rootScope', "$http", "UserTypeServices", 'DictionaryServices', function ($scope, $rootScope, $http, UserTypeServices, DictionaryServices) {

    $scope.Searcher = {};
    $scope.Searcher_CurrentPage = 1;
    $scope.ResultUser = {};

    DictionaryServices.get({ keyWord: "UserType" }
              , function (data) {
                  $scope.Searcher_DicUserTypeList = data.Data
              });

    $scope.processSearch = function () {
        $scope.Searcher_CurrentPage = 1;
        $scope.search();
    };

    $scope.search = function () {
        $scope.Searcher.Name = $scope.Searcher.Name == undefined ? "" : $scope.Searcher.Name;
        $scope.Searcher.IdCard = $scope.Searcher.IdCard == undefined ? "" : $scope.Searcher.IdCard;
        $scope.Searcher.UserType = $scope.Searcher.UserType == undefined ? 0 : $scope.Searcher.UserType;
        $http({
            method: 'GET',
            url: '/User/SearchUser' + '?pageIndex=' + $scope.Searcher_CurrentPage + '&Name=' + $scope.Searcher.Name + '&IdCard=' + $scope.Searcher.IdCard + '&UserType=' + $scope.Searcher.UserType
        }).success(function (data) {
            if (data.Status != 1) {
                alert(data.Msg);
                return;
            }
            $scope.Searcher_UserList = data.Data;
            var pager = new Pager('searcher_pager', $scope.Searcher_CurrentPage, data.PagesCount, function (curPage) {
                $scope.Searcher_CurrentPage = parseInt(curPage);
                $scope.search();
            });
        }).error(function (data) {
            alert(data.Msg);
        });
    };

    $scope.choiceItem = function ($event, model) {
        $scope.ResultUser = model;
        $($event.target).parent().siblings('tr').css("background", "#fff");
        $($event.target).parent().css("background", "#d9edf7");
    };

    $scope.Ok = function () {
        $scope.$parent.FinishUserSearch($scope.ResultUser);
        $('.close').last().trigger('click');
    };
}]);