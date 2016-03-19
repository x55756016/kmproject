var app = angular.module("community", []);

app.controller("communityCtrl", [
    "$scope", "$http", "$location", "$routeParams", "communityServices",
    function($scope, $http, $routeParams, $location, communityServices) {

        $scope.CurrentPage = 1;
        $scope.Search = function() {
            communityServices.get({ Keyword: $scope.Keyword, CurrentPage: $scope.CurrentPage }, function(obj) {
                $scope.ListItems = obj.Data;
                var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function(curPage) {
                    $scope.CurrentPage = curPage;
                    $scope.Search();
                });
            });
        };
        $scope.Search();
        $scope.Community = {};
        $scope.Areas = {};
        $scope.InitAreas = function() {
            if ($scope.Community.ID == undefined) {
                $scope.Areas.ProvinceId = "";
                $scope.Areas.CityId = "";
                $scope.Areas.CountryId = "";
                $scope.Areas.TownId = "";
                $scope.Areas.Address = "";
            } else {
                $scope.Areas.ProvinceId = $scope.Community.ProvinceId.toString();
                $scope.Areas.CityId = $scope.Community.CityId.toString();
                $scope.Areas.CountryId = $scope.Community.CountryId.toString();
                //$scope.Areas.TownId = $scope.Community.TownId.toString();
                $scope.Areas.Address = $scope.Community.Address;
            }
        };

        $scope.GetComInfo = function(comId) {
            if (comId != '') {
                communityServices.get({ ID: comId }, function(obj) {
                    $scope.Community = obj.Data;
                    $scope.InitAreas();
                });
            }
        }

        $scope.SaveCommunity = function(comModel) {
            //alert($scope.Community.ID);
            //if ( $scope.Community.CommunityId != undefined) {
            $scope.Community.ProvinceId = $scope.Areas.ProvinceId;
            $scope.Community.CityId = $scope.Areas.CityId;
            $scope.Community.CountryId = $scope.Areas.CountryId;
            $scope.Community.TownId = $scope.Areas.TownId;
            $scope.Community.Address = $scope.Areas.Address;
            communityServices.save({ Data: $scope.Community }, function(response) {
                alert("保存成功");
                $scope.Community.ID = response.Data.ID;
                $scope.Search();
                $('#myModal').modal('hide');
            });
            //} 
        }

        $scope.Add = function() {
            $scope.Community.ID = 0;
        }

        $scope.Remove = function(id) {
            if (confirm('您确定要删除吗？')) {
                communityServices.remove({ ID: id }, function(response) {
                    $scope.Search();
                    $('#myModal').modal('hide');
                });
            }
        };
    }
]);


//app.controller("historyDiseaseCtr", [
//    "$scope", "$http", "$routeParams", "$location", "optionsServices", "historyDiseaseServices", "dictionary",
//    function ($scope, $http, $routeParams, $location, optionsServices, historyDiseaseServices, dictionary) {


//        dictionary.dictionary(function (dic) {
//            $scope.Dic.Disease = dic.Disease;
//            $scope.Data.Disease = $scope.Dic.Disease[0].Value;
//        });


//        $scope.txt_RecordIdChang = function (RecordId) {
//            $scope.Search(RecordId);
//        }

//        $scope.Search = function (RecordId) {
//            if (RecordId > 0) {
//                $scope.Data.RecordId = RecordId;
//                historyDiseaseServices.get({ RecordId: RecordId, Keyword: encodeURI("1,2,3,4") }, function (data) {
//                    $scope.Historys = data.Data;
//                });
//            }
//        };

//        $scope.$on("userInfoLoadSuccess", function (event, data) {
//            if (data.Data.length > 0) {
//                $scope.Data.RecordId = data.Data[0].ID;
//                $scope.Search($scope.Data.RecordId);
//            } else {
//                $scope.Historys = [];
//            }
//        });

//        $scope.Remove = function (id) {
//            if (confirm("您确定删除该条记录吗？")) {
//                historyDiseaseServices.remove({ ID: id }, function () {
//                    $scope.Search($scope.Data.RecordId);
//                });
//            }
//        }

       
//    }
//]);
