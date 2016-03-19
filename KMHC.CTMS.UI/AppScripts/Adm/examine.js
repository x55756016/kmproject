var app = angular.module('Examine', []);

app.controller('ExamineTemplateCtrl', ['$scope', '$http', '$timeout', '$routeParams', '$location', 'ExamineServices','DictionaryServices',
    function ($scope, $http, $timeout, $routeParams, $location, ExamineServices, dictionaryServices) {
    $scope.search = {};
    $scope.List = [];
    $scope.loading = false;

    $scope.templateTypeList = {};
        $scope.LoadData = function() {
            dictionaryServices.get({ Keyword: "TemplateManagement" }, function(obj) {
                if (obj) {
                    $scope.templateTypeList = obj.Data[0].nodes;
                    console.log(JSON.stringify($scope.templateTypeList));
                }
            });
        };
    $scope.LoadData();

    $scope.CurrentPage = 1;

    $scope.ResetSearch = function () {
        $scope.search = {};
    };
    $scope.Search = function () {
        $scope.loading = true;

        //$http.get('/api/Examine?Keyword=' + $scope.search.kwd + '&CurrentPage=' + $scope.CurrentPage + '&PageSize=' + 15 + '&DicType=' + $scope.search.type)
        //    .success(function(data) {
        //        $scope.List = data.Data;
        //        $scope.loading = false;
        //        var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function(curPage) {
        //            $scope.CurrentPage = parseInt(curPage);
        //            $scope.Search();
        //        });
        //    });
        ExamineServices.get({
            CurrentPage: $scope.CurrentPage,
            PageSize: 15,
            Keyword: $scope.search.kwd == undefined ? "" : $scope.search.kwd,
            DicType: $scope.search.type == undefined ? "" : $scope.search.type
            //Params: testData,
        }, function (data) {
            $scope.List = data.Data;
            $scope.loading = false;
            var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                $scope.CurrentPage = parseInt(curPage);
                $scope.Search();
            });
        }, function (errRespose) {
            alert("发生错误,操作失败!");
            console.log("ExamineServices.get:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.Search();

    $scope.Add = function () {
        $scope.ExamineTemplates = {};
    };
    $scope.Edit = function (id) {
        ExamineServices.get({ Keyword: id, CurrentPage: $scope.CurrentPage, PageSize: 15 }, function (data) {

            $scope.ExamineTemplates = data.Data[0];
        }, function (errRespose) {
            alert("发生错误,操作失败!");
            console.log("ExamineServices.get:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.ProcessForm = function () {
        $scope.loading = true;
        ExamineServices.save({ Data: $scope.ExamineTemplates }, function (response) {
            $(".close").trigger('click');
            $scope.search = {};
            $scope.Search();
            $scope.loading = false;
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineServices.post:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.Remove = function (model) {
        if (!confirm("确定删除？"))
            return;
        ExamineServices.delete({ Keyword: model.Id }, function (response) {
            $scope.Search();
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineServices.delete:" + errRespose.data.Message);
        });
    };

}]);

app.controller('ExamineTemplateItemCtrl', ['$scope', '$http', '$timeout', '$routeParams', '$location', 'ExamineItemServices', function ($scope, $http, $timeout, $routeParams, $location, ExamineItemServices) {
    $scope.templateId = $routeParams.templateId;
    $scope.search = {};
    $scope.Data = {};
    $scope.loading = false;
    $scope.ItemType = [
        { Text: '单选', Value: 0 },
        { Text: '多选', Value: 1 }
    ];
    $scope.ResetSearch = function () {
        $scope.search = {};
    };
    $scope.CurrentPage = 1;
    $scope.Search = function () {
        $scope.loading = true;
        ExamineItemServices.get({ Keyword: $scope.search.kwd, CurrentPage: $scope.CurrentPage, PageSize: 15, ParentId: $scope.templateId }, function (data) {
            $scope.Data = data.Data;
            $scope.loading = false;
            var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                $scope.CurrentPage = parseInt(curPage);
                $scope.Search();
            });
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineItemServices.get:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.Search();
    $scope.Add = function () {
        $scope.ExamineTemplateItems = {};
    };
    $scope.Edit = function (id) {
        ExamineItemServices.get({ Keyword: id, CurrentPage: $scope.CurrentPage, PageSize: 15, ParentId: $scope.templateId }, function (data) {
            $scope.ExamineTemplateItems = data.Data.Data[0];
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineItemServices.get:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.ProcessForm = function () {
        $scope.loading = true;
        $scope.ExamineTemplateItems.ExamineTemplateId = $scope.templateId;
        ExamineItemServices.save({ Data: $scope.ExamineTemplateItems }, function (response) {
            $(".close").trigger('click');
            $scope.search = {};
            $scope.Search();
            $scope.loading = false;
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineItemServices.post:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.Remove = function (model) {
        if (!confirm("确定删除？"))
            return;
        ExamineItemServices.delete({ Keyword: model.Id }, function (response) {
            $scope.Search();
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineItemServices.delete:" + errRespose.data.Message);
        });
    };
}]);

app.controller('ExamineTemplateItemOptionCtrl', ['$scope', '$http', '$timeout', '$routeParams', '$location', 'ExamineItemOptionServices', function ($scope, $http, $timeout, $routeParams, $location, ExamineItemOptionServices) {
    $scope.templateItemId = $routeParams.templateItemId;
    $scope.search = {};
    $scope.Data = {};
    $scope.loading = false;
    $scope.ResetSearch = function () {
        $scope.search = {};
    };
    $scope.Search = function () {
        $scope.loading = true;
        ExamineItemOptionServices.get({ Keyword: $scope.search.kwd, CurrentPage: $scope.CurrentPage, PageSize: 15, ParentId: $scope.templateItemId }, function (data) {
            $scope.Data = data.Data;
            $scope.loading = false;
            var pager = new Pager('pager', $scope.CurrentPage, data.PagesCount, function (curPage) {
                $scope.CurrentPage = parseInt(curPage);
                $scope.Search();
            });
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineItemOptionServices.get:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.Search();
    $scope.Add = function () {
        $scope.ExamineTemplateItemOptions = {};
    };
    $scope.Edit = function (id) {
        ExamineItemOptionServices.get({ Keyword: id, CurrentPage: $scope.CurrentPage, PageSize: 15, ParentId: $scope.templateItemId }, function (data) {
            $scope.ExamineTemplateItemOptions = data.Data[0];
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineItemOptionServices.get:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.ProcessForm = function () {
        $scope.loading = true;
        $scope.ExamineTemplateItemOptions.ExamineItemId = $scope.templateItemId;
        ExamineItemOptionServices.save({ Data: $scope.ExamineTemplateItemOptions }, function (response) {
            $(".close").trigger('click');
            $scope.search = {};
            $scope.Search();
            $scope.loading = false;
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineItemOptionServices.post:" + errRespose.data.Message);
            $scope.loading = false;
        });
    };
    $scope.Remove = function (model) {
        if (!confirm("确定删除？"))
            return;
        ExamineItemOptionServices.delete({ Keyword: model.Id }, function (response) {
            $scope.Search();
        }, function (errRespose) {
            alert("发生错误,操作失败!")
            console.log("ExamineItemOptionServices.delete:" + errRespose.data.Message);
        });
    };
}]);

app.controller('ExamineReportCtrl', ['$scope', '$http', '$timeout', '$routeParams', '$location', 'ExamineReportServices', 'UserArrangeServices', 'ExamineServices', function ($scope, $http, $timeout, $routeParams, $location, ExamineReportServices, UserArrangeServices, ExamineServices) {
    $scope.userId = $routeParams.uid;
    $scope.examineNo = $routeParams.examineNo;
    $scope.Hid = $routeParams.hid;
    $scope.loading = false;
    $scope.ExamineResult = {};

    $scope.CurrentPage = 1;

    //获取用户上传附件
    $.get('/api/Upload?ModelCode=ImageExamination&FormID=' + $routeParams.hid).success(function (data) {
        $scope.Files = data;
    }).error(function (xlr) {
        $scope.Files = [];
    });
    

    ExamineServices.get({ templateType: 0 }, function (data) {
        $scope.TemplateTypes = data.Data;
    });

    $scope.initTemplate = function (templateid) {
        ExamineServices.get({ templateCode: $scope.ExamineResult.TemplateType }, function (data) {
            $scope.TemplateList = data.Data;
        });
    };

    $scope.initData = function () {
        if ($scope.examineNo != undefined && $scope.examineNo != 'undefined') {
            ExamineReportServices.get({ examineNo: $scope.examineNo, userId: $scope.userId }, function (data) {
                $scope.ExamineResult = data.Data;
                $scope.ExamineResult.ReportDetail = JSON.parse($scope.ExamineResult.ReportDetail);
                $scope.initTemplate();
            });
        }
    }
    $scope.initData();

    $scope.imgShow = function (formId,filePath) {
        $scope.imgSource = '/Upload/' + formId + '/' + filePath;
    };

    $scope.processForm = function () {
        $scope.loading = true;
        $scope.ExamineResult.ReportDetail = JSON.stringify($scope.ExamineResult.ReportDetail);
        $scope.ExamineResult.UserId = $scope.userId;
        ExamineReportServices.save({ Data: $scope.ExamineResult }, function (response) {
            alert("操作成功");
            $location.url('DiseaseInfo?hid=' + $routeParams.hid + '&uid=' + $routeParams.uid);
            $scope.loading = false;
        });
    };

    $scope.goBack = function () {

        if ($routeParams.isFlow == "true") { //判断是否由待办那边链接过来
            $location.url('EventDetailForDoc?id=' + $routeParams.flowId + '&ApplyId=' + $routeParams.ApplyId);
        } else {
            $location.url('DiseaseInfo?hid=' + $routeParams.hid + '&uid=' + $routeParams.uid);
        }

        //$location.url("/ExamineReportList/" + $scope.userId);
    };
}]);

app.controller('ExamineReportListCtrl', ['$scope', '$http', '$timeout', '$routeParams', '$location', 'ExamineReportServices', function ($scope, $http, $timeout, $routeParams, $location, ExamineReportServices) {
    $scope.userId = $routeParams.uid;
    $scope.loading = false;

    $scope.init = function () {
        ExamineReportServices.get({ examineNo: '', userId: $scope.userId }, function (data) {
            $scope.List = data.Data;
        });
    };
    $scope.init();
    $scope.Add = function () {
        $location.url("/ExamineReport/" + $scope.userId + "/undefined");
    };
    $scope.Edit = function (model) {
        $location.url("/ExamineReport/" + model.UserId + "/" + model.Id);
    };
    $scope.Remove = function (model) {
        ExamineReportServices.delete({ Keyword: model.Id }, function (data) {
            $scope.init();
        });
    };
    $scope.goBack = function () {
        $location.url("/DoctorHistory?uid=" + $scope.userId);
    };
}]);