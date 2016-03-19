var app = angular.module("Family.Controllers", []);

app.controller('FamilysCtrl', ['$scope', '$http', '$location', '$routeParams', 'familyServices',
          function ($scope, $http, $location, $routeParams, services) {
              $scope.CurrentPage = 1;
              $scope.Search = function () {
                  services.get({ Keyword: $scope.Keyword, CurrentPage: $scope.CurrentPage }, function (obj) {
                      $scope.ListItems = obj.Data;
                      var pager = new Pager('pager', $scope.CurrentPage, obj.PagesCount, function (curPage) {
                          $scope.CurrentPage = curPage;
                          $scope.Search();
                      });
                  });
              };
              $scope.Search();

              $scope.Add = function () {
                  $location.path('Family');
              };

              $scope.Open = function (id) {
                  $location.path('Family/id/' + id);
              };

              $scope.Remove = function (id) {
                  if (confirm('您确定要删除吗？')) {
                      services.remove({ ID: id }, function (response) {
                          $scope.Search();
                      });
                  }
              };
              //$scope.ShowAddr = function (provinceId, cityId, countryId, townId, address) {
              //    return address;
              //};
              $scope.ShowAddr = function (address) {
                  return address;
              };
          }
]);

app.controller('FamilyCtrl', ['$scope', '$http', '$location', '$routeParams', "dictionary", 'familyMemberServices', 'familyServices',
        function ($scope, $http, $location, $routeParams, dictionary, services, familyServices) {
            var familyid = $routeParams.id ? $routeParams.id : '';
            $scope.Family = {};
            $scope.Member = {};
            $scope.Members = [];
            $scope.Communitys = [{ Id: 0, Name: "请选择" }, { Id: 1, Name: "清湖村社区" }, { Id: 1, Name: "岗厦北社区" }];
            //$scope.Areas = { ProvinceId: "320000", CityId: "320100", CountryId: "320102", TownId: "32010201", Address: "test" }
            $scope.Areas = {};
            dictionary.dictionary(function (dic) {
                $scope.Dic.FamilyRelation = dic.FamilyRelation;
            });

            $scope.InitAreas = function () {
                if ($scope.Family.ID == undefined) {
                    $scope.Areas.ProvinceId = "";
                    $scope.Areas.CityId = "";
                    $scope.Areas.CountryId = "";
                    $scope.Areas.TownId = "";
                    $scope.Areas.Address = "";
                } else {
                    $scope.Areas.ProvinceId = $scope.Family.ProvinceId.toString();
                    $scope.Areas.CityId = $scope.Family.CityId.toString();
                    $scope.Areas.CountryId = $scope.Family.CountryId.toString();
                    $scope.Areas.TownId = $scope.Family.TownId.toString();
                    $scope.Areas.Address = $scope.Family.Address;
                }

            };

            if (familyid != '') {
                familyServices.get({ ID: familyid }, function(obj) {
                    $scope.Family = obj.Data;
                    //$scope.Areas = { ProvinceId: "320000", CityId: "320100", CountryId: "320102", TownId: "32010201", Address: "test" }
                    $scope.InitAreas();
                    $scope.$broadcast('AreaLoadSuccess', $scope.Areas);
                    $scope.Members = [{ FMemberId: 1, FamilyId: 1, MemberName: "小组长", CardNo: "431512236554411", RelateId: 1 }];
                });
            }
            //$scope.InitAreas();

            $scope.SaveFamily = function () {
                $scope.Family.ProvinceId = $scope.Areas.ProvinceId;
                $scope.Family.CityId = $scope.Areas.CityId;
                $scope.Family.CountryId = $scope.Areas.CountryId;
                $scope.Family.TownId = $scope.Areas.TownId;
                $scope.Family.Address = $scope.Areas.Address;
                familyServices.save({ Data: $scope.Family }, function (response) {
                    alert("保存成功");
                    $scope.Family.ID = response.Data.ID;
                });
            }
            $scope.GoBack = function () {
                $location.path('Familys');
            };
            $scope.RemoveMember = function (id) {
                if (confirm("您确定删除该条记录吗？")) {
                    services.remove({ ID: id }, function () {
                        //$scope.Search($scope.Family.RecordId);
                    });
                }
            }

            $scope.SaveMember = function (member) {
                if ($scope.Family.ID == undefined) {
                    alert("请先保存家庭基本资料!");
                    return;
                }
                member.FamilyId = $scope.Family.ID;
                services.save({ Data: member}, function (response) {
                    alert("添加成功!");
                });
            }
            $scope.DiseaseCode = [];//选择的疾病编码
            $scope.DiseaseName = [];//选择的疾病名称
            $("#btnOk").click(function () {
                if ($scope.DiseaseCode != null) {
                    $("#txtDisease").val($scope.DiseaseName);
                }
                $('#myModal').modal('hide');
            });
            //$scope.SearchMember = function (RecordId) {
            //    if (RecordId > 0) {
            //        $scope.Data.RecordId = RecordId;
            //        historyDiseaseServices.get({ RecordId: RecordId, Keyword: encodeURI("1,2,3,4") }, function (data) {
            //            $scope.Historys = data.Data;
            //        });
            //    }
            //};

        }
]);



