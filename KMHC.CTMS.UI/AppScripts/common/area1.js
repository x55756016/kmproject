var app = angular.module("common", []);

//地区control
//值相互传递如<div class="panel-group" ng-include="'/Views/Common/Area.html'" ng-init="CurrentAreas = Areas"></div>
//必须定义Areas变量，格式如下：
//Areas格式如下 { ProvinceId: 320000, CityId: 320100, CountryId: 320102, TownId: 32010201, Address: "test" } 
app.controller("areaCtr", ["$scope", "$http", "dictionary", function ($scope, $http, dictionary) {
    $scope.Data = {};
    $scope.Area = {};
    $scope.Area.Provinces = {};
    $scope.Area.Citys = {};
    $scope.Area.Countrys = {};
    $scope.Area.Towns = {};
    $scope.Area.IsShow = false;//四级地区是否显示标记
    $scope.Area.Defaut = [{ AreaId: null, AreaName: "请选择", ParentId: null }];

    $scope.getAreas=function (type,pid)
    {
        switch (type) {
            case 1:
                $scope.Area.Provinces = [{ Value: null, Name: "请选择", ParentId: null }];
                break;
            case 2: //Area.Provinces
                $scope.Area.Citys = [{ Value: null, Name: "请选择", ParentId: null }];
                break;
            case 3:
                $scope.Area.Countrys = [{ Value: null, Name: "请选择", ParentId: null }];
                break; //Area.Provinces
            case 4:
                $scope.Area.Towns = [{ Value: null, Name: "请选择", ParentId: null }];
                break;
        default:
        }
    }

    $scope.$on("AreaLoadSuccess", function (event, data) {
        $scope.CurrentAreas = data;
        $scope.Data.Province = $scope.CurrentAreas.ProvinceId;
        $scope.Data.City = $scope.CurrentAreas.CityId;
        $scope.Data.Country = $scope.CurrentAreas.CountryId;
        $scope.Data.Town = $scope.CurrentAreas.TownId;
        $scope.Data.Address = $scope.CurrentAreas.Address;
        $scope.setCitys($scope.Data.Province);
        $scope.setCountrys($scope.Data.City);
        $scope.setTowns($scope.Data.Country);
        if ($scope.Area.Towns.length > 1) {
            $scope.Area.IsShow = true;
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
        $scope.Area.Countrys = $scope.Area.Defaut;
        $scope.Area.Towns = $scope.Area.Defaut;
        $scope.Data.City = "";
        $scope.Data.Country = "";
        $scope.Data.Town = "";
        $scope.setCitys(provinceId);
    };

    //二级地区更改事件
    $scope.citysChanged = function (cityId) {
        $scope.Area.IsShow = false;
        $scope.CurrentAreas.CityId = cityId;
        $scope.CurrentAreas.CountryId = "";
        $scope.CurrentAreas.TownId = "";
        //$scope.Area.Countrys = $scope.Area.Defaut;
        $scope.Area.Towns = $scope.Area.Defaut;
        $scope.Data.Country = "";
        $scope.Data.Town = "";
        $scope.setCountrys(cityId);
    };

    //三级地区更改事件
    $scope.CountrysChanged = function (countryId) {
        $scope.Area.IsShow = false;
        $scope.CurrentAreas.CountryId = countryId;
        $scope.CurrentAreas.TownId = "";
        //$scope.Area.Towns = $scope.Area.Defaut;
        $scope.setTowns(countryId);
        if ($scope.Area.Towns.length > 1) {
            $scope.Area.IsShow = true;
            $scope.Data.Town = "";
        } 
    };

    //四级地区更改事件
    $scope.townsChanged = function (townId) {
        $scope.CurrentAreas.TownId = townId;
    };

    //详细地址更改事件
    $scope.addressChanged = function () {
        $scope.CurrentAreas.Address = $scope.Data.Address;
    };

    //更新二级地区,例如城市数组
    $scope.setCitys = function (provinceId) {
        $scope.Area.Citys = $scope.Area.Defaut;
        if (provinceId == "") {
            return;
        }
            for (var i = 0; i < $scope.Area.Provinces.length; i++) {
                if ($scope.Area.Provinces[i].Value == provinceId) {
                    $scope.Area.Citys = $scope.Area.Citys.concat($scope.Area.Provinces[i].Items);
                    break;
                }
            }
            //return $scope.Area.Citys;
        };

    //更新三级地区,例如地级市、县数组
    $scope.setCountrys = function (cityId) {
        $scope.Area.Countrys = $scope.Area.Defaut;
        if (cityId == "") {
            return;
        }
            for (var i = 0; i < $scope.Area.Citys.length; i++) {
                if ($scope.Area.Citys[i].Value == cityId) {
                    $scope.Area.Countrys = $scope.Area.Countrys.concat($scope.Area.Citys[i].Items);
                    break;
                }
            }
            //return $scope.Area.Countrys;
        };

    //更新四级地区，例如街道办数组
    $scope.setTowns = function (countryId) {
        $scope.Area.Towns = $scope.Area.Defaut;
        if (countryId == "") {
            return;
        }
        for (var i = 0; i < $scope.Area.Countrys.length; i++) {
                if ($scope.Area.Countrys[i].Value == countryId) {
                    $scope.Area.Towns = $scope.Area.Towns.concat($scope.Area.Countrys[i].Items);
                    break;
                }
            }
            //return $scope.Area.Towns;
    };

    //初始化地区
    dictionary.dictionary(function (dic) {
        $scope.Area.Provinces = $scope.Area.Defaut.concat(dic.CensusAddressCode);
        if ($scope.CurrentAreas == undefined || $scope.CurrentAreas == null) {
            $scope.Data.Province = $scope.Area.Provinces[0].Value;
            $scope.Area.Citys = $scope.Area.Defaut;
            $scope.Area.Countrys = $scope.Area.Defaut;
            $scope.Area.Towns = $scope.Area.Defaut;
            $scope.Data.City = "";
            $scope.Data.Country = "";
            $scope.Data.Town = "";
            $scope.Area.IsShow = false;
            $scope.CurrentAreas = {};
        } else {
            $scope.Data.Province = $scope.CurrentAreas.ProvinceId;
            $scope.Data.City = $scope.CurrentAreas.CityId;
            $scope.Data.Country = $scope.CurrentAreas.CountryId;
            $scope.Data.Town = $scope.CurrentAreas.TownId;
            $scope.Data.Address = $scope.CurrentAreas.Address;
            $scope.setCitys($scope.Data.Province);
            $scope.setCountrys($scope.Data.City);
            $scope.setTowns($scope.Data.Country);
            if ($scope.Area.Towns.length > 1) {
                $scope.Area.IsShow = true;
            }
        }
    });
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
                    var tree = $('#metaDataTree').treeview({
                        data: treeData.Data,
                        levels: 2,
                        onNodeSelected: function (event, node) {
                            if ($scope.$parent != null && $scope.SetMetaData != null && node.value != null && node.value > 0) {
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
app.controller("CondPickerCtrl", ["$scope", "$http", "CondPickerServices", '$routeParams',
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