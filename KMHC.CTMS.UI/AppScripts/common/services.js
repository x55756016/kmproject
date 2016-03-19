(function () {
    "use strict";
    var app = angular.module('HR.services',[]);


    var services = ['users'
        , 'options'
        , 'healthrecords'
        , 'exams'
        , 'examcategories'
        , 'historyDisease'
        , 'examTemplates'
        , 'examitems'
        , 'family'
        , 'familyMember'
        , 'community'
        , 'scoreTemplate'
        , 'diseasehistory'
        , 'cancerUser'
        , 'UserGene'
        , 'UserGeneList'
        , 'score'
        , 'familyDisease'
        , 'familyDiseaseMember'
        , 'familyRelation'
        , 'finance'
        , 'doctor'
        , 'TreatmentHistory'
        , 'Gene'
        , 'GeneAllele'
        , 'GeneAlleleLocus'
        , 'DrugAllele'
        , 'Dug'
        , 'CnDrug'
        , 'DrugControl'
        , 'DrugInterSearch'
        , 'DrugGeneSearch'
        , 'Prescription'
        , 'disease'
        , 'Template'
        , 'Adm'
        , 'Dictionary'
        , 'doctorResult'
        , 'MetaData'
        , 'MetaDataPicker'
        , 'MetaDataParam'
        , 'ConditionItem'
        , 'CondPicker'
        , 'ItemStandVal'
        , 'GuideLine'
        , 'userUpload'
        , 'UserArrange'
        , 'GuideLineFlow'
        , 'Role'
        , 'UserManage'
        , 'DoctorControl'
        , 'Examine'
        , 'ExamineItem'
        , 'ExamineItemOption'
        , 'DoTreatment'
        , "UserApply"
        ,'ExamineReport'
        , 'UserApply'
        , 'Symptom'
        ,'ServiceTask'
        , 'BaseTemplateResult'
        , 'Products'
        , 'MyHouseKeeper'
        , 'MyQuestion'
        , 'MyProduct'
        , 'UserInfo'
        , 'Member'
        , 'AccountRecord'
        , 'MedicalArrange'
        , 'Area'
        , 'DictionaryManage'
        , 'UserType'
        , 'SysFunction'
    ];// the services without customized actions
    function addService(name, actions) {
        app.factory(name + 'Services', ['$resource',function($resource){
            return $resource('/api/' + name + '/:id', null, actions);
        }]);
    }
    angular.forEach(services, function (name) {
        addService(name, null);
    });
    app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push(function ($rootScope,$location, $q) {
            return {
                'request': function (config) {
                    // console.log('request:' + config);
                    return config || $q.when(config);
                },
                'requestError': function (rejection) {
                    // console.log('requestError:' + rejection);
                    return rejection;
                },
                //success -> don't intercept
                'response': function (response) {

                    if (response.status === 204) {
                        alert("登录超时，请您重新登录");
                        location = "/User/Login#/Login";
                    }

                    // console.log('response:' + response);
                    return response || $q.when(response);
                },
                //error -> if 401 save the request and broadcast an event
                'responseError': function (response) {

                    var api = "" || response.config.url;
                    var apiMethod = "" || response.config.method;
                    var errorMsg = "" || response.data.Message;
                    var now = new Date().toString();


                    console.log(response.status);


                    if (response.status === 401) {
                        //Todo:正式上线时是否删除log
                        console.log("[" + now + "]访问[" + api + "][" + apiMethod + "]:" + "无权限操作!");
                        $location.path("/Forbidden");
                    }
                    else if (response.status === 403) {
                        console.log("[" + now + "]访问[" + api + "][" + apiMethod + "]:" + "无权限访问页面!");
                        alert("无权进行此操作!");
                    }

                       else if (response.status >= 400 && response.status < 500) {
                        console.log("[" + now + "]访问[" + api + "][" + apiMethod + "]:" + "状态码：[" + response.status + "] 消息：[" + errorMsg + "]");
                        //alert("访问异常!");
                    } else if (response.status >= 500 && response.status < 700) {
                        console.log("[" + now + "]访问[" + api + "][" + apiMethod + "]:" + "状态码：[" + response.status + "] 消息：[" + errorMsg + "]");
                        alert("访问服务器出错!");
                    }
                    return $q.reject(response);
                }

            };
        });
    }]); 
})();


