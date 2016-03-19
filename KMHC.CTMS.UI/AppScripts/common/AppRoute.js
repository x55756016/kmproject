angular.module('userApp', [
        'ngRoute',
        'ngResource',
        'HR.services',
        'Utility',
        'common',
        'navigation.controllers',
        'userApp.controllers',
        'examCategoryApp.controllers',
        'examTemplateApp.controllers',
        'examApp.controllers',
        'examItemApp.controllers',
        'scoreScale',
        'community',
        'Family.Controllers',
        'cancer',
        'diseasehis',
        'UserGene',
        'hpnApp',
        'finance',
        'doctor',
        'GeneDic.Ctrls',
        'Dug',
        'DrugGeneSearch',
        'Prescription',
        'TemplateMan',
        'ItemStandVal',
        'Adm',
        'Cond.Ctrls',
        'CondItemCtrl',
        'GuideLineCtrl'
        , 'GuideLineFlow'
        , 'UserManageCtrl'
        , 'Role'
        , 'Permission'
        , 'DoctorControlFlow'
        , 'Examine'
        , 'DoTreatment'
        , 'EventDetailForDoc'
        , 'Symptom'
        , 'ForbiddenCtrl'
        , 'Products'
        , 'MyHouseKeeper'
        , 'MyQuestion'
        , 'MyProduct'
        , 'UserInfoManager'
        , 'Member'
        , 'AccountRecord'
        , 'MedicalArrange'
        , 'DictionaryManage'
        , 'SysFunction'
        , 'Authorization'
]).
    config([
        '$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $routeProvider.when('/', { templateUrl: '/Views/User/DoTreatment.html', controller: 'DoTreatmentCtrl' });
            $routeProvider.when('/CreateRecord/:uid', { templateUrl: '/Views/BasicRecord/CreateRecord.html', controller: 'createRecordCtrl' });
            $routeProvider.when('/HistoryDisease/:recordID', { templateUrl: '/Views/BasicRecord/HistoryDisease.html', controller: 'historyDiseaseCtr' });
            $routeProvider.when('/HistoryDisease', { templateUrl: '/Views/BasicRecord/HistoryDisease.html', controller: 'historyDiseaseCtr' });
            $routeProvider.when('/FamilyDiseaseHistory/:uid', { templateUrl: '/Views/BasicRecord/FamilyDiseaseHistory.html', controller: 'familyDiseaseHistoryCtr' });
            $routeProvider.when('/FamilyDiseaseHistory/:recordID', { templateUrl: '/Views/BasicRecord/FamilyDiseaseHistory.html', controller: 'familyDiseaseHistoryCtr' });
            $routeProvider.when('/HealthRecord', { templateUrl: '/Views/BasicRecord/HealthRecord.html', controller: 'healthRecordCtr' });

            //$routeProvider.when('/ElderlyManageCardCreate', { templateUrl: '/Views/HealthManage/ElderlyManageCardCreate.html', controller: 'userCtrl' });
            $routeProvider.when('/ElderlyManageCardCreate', { templateUrl: '/Views/HealthManage/ElderAnnualExam.html' });

            //老年人建档
            $routeProvider.when('/CreateElderRecord', { templateUrl: '/ElderManage/CreateElderRecord', controller: 'examCtrl' });
            //$routeProvider.when('/CreateElderRecord/', { templateUrl: '/ElderManage/CreateElderRecord/T001', controller: 'examCtrl' });
            //$routeProvider.when('/CreateElderRecord/pid/:pid', { templateUrl: '/ElderManage/CreateElderRecord', controller: 'examCtrl' });

            //高血压管理建档
            $routeProvider.when('/CreateHBpRecord', { templateUrl: '/DiseaseManage/CreateHBpRecord', controller: 'examCtrl' });
            $routeProvider.when('/HighBloodManageCreate', { templateUrl: 'Views/HealthManage/HighBlood.html', controller: 'highBloodManageCreateCtr' });
            //糖尿病管理建档
            $routeProvider.when('/CreateDIABRecord', { templateUrl: '/DiseaseManage/CreateDIABRecord', controller: 'examCtrl' });

            $routeProvider.when('/Exams/pid/:pid', { templateUrl: '/Views/HealthManage/Exams.html', controller: 'examsCtrl' });

            //分类
            $routeProvider.when('/ExamCategory/:id', { templateUrl: '/Views/HealthManage/examCategory.html', controller: 'examCategoryCtrl' });
            $routeProvider.when('/ExamCategory/pid/:pid', { templateUrl: '/Views/HealthManage/examCategory.html', controller: 'examCategoryCtrl' });
            $routeProvider.when('/ExamCategories/pid/:pid', { templateUrl: '/Views/HealthManage/examCategories.html', controller: 'examCategoriesCtrl' });
            $routeProvider.when('/ExamCategories', { templateUrl: '/Views/HealthManage/examCategories.html', controller: 'examCategoriesCtrl' });

            //模版
            $routeProvider.when('/ExamTemplates', { templateUrl: '/Views/HealthManage/ExamTemplates.html', controller: 'examTemplatesCtrl' });
            $routeProvider.when('/ExamTemplate/id/:id', { templateUrl: '/Views/HealthManage/ExamTemplate.html', controller: 'examTemplateCtrl' });
            $routeProvider.when('/ExamTemplate', { templateUrl: '/Views/HealthManage/ExamTemplate.html', controller: 'examTemplateCtrl' });


            //检查项目
            $routeProvider.when('/ExamItems', { templateUrl: '/Views/HealthManage/ExamItems.html', controller: 'examItemsCtrl' });
            $routeProvider.when('/ExamItem/id/:id', { templateUrl: '/Views/HealthManage/ExamItem.html', controller: 'examItemCtrl' });
            $routeProvider.when('/ExamItem', { templateUrl: '/Views/HealthManage/ExamItem.html', controller: 'examItemCtrl' });

            //评分量表

            $routeProvider.when('/ScoreScaleTemplateManage', { templateUrl: '/Views/ScoreScale/TemplateManage.html', controller: 'scoreScaleTemplateManageCtrl' });
            $routeProvider.when('/CreateScoreScaleTemplate', { templateUrl: '/Views/ScoreScale/CreateTemplate.html', controller: 'createScoreScaleTemplateCtrl' });
            $routeProvider.when('/EditScoreScaleTemplate/id/:id', { templateUrl: '/Views/ScoreScale/CreateTemplate.html', controller: 'createScoreScaleTemplateCtrl' });
            $routeProvider.when('/CreateScoreScale/id/:id', { templateUrl: '/Views/ScoreScale/CreateScoreScale.html', controller: 'createScoreScaleCtrl' });

            //社区管理
            $routeProvider.when('/CommunityInfo', { templateUrl: '/Views/CommunityManage/CommunityInfo.html', controller: 'communityCtrl' });
            //$routeProvider.when('/CommunityInfo/id/:id', { templateUrl: '/Views/CommunityManage/CommunityInfo.html', controller: 'communityCtrl' });

            //家庭档案
            $routeProvider.when('/Familys', { templateUrl: '/Views/FamilyRecord/Familys.html', controller: 'FamilysCtrl' });
            $routeProvider.when('/Family/id/:id', { templateUrl: '/Views/FamilyRecord/Family.html', controller: 'FamilyCtrl' });
            $routeProvider.when('/Family', { templateUrl: '/Views/FamilyRecord/Family.html', controller: 'FamilyCtrl' });

            //肿瘤档案
            $routeProvider.when('/CancerRecord', { templateUrl: '/Views/CancerRecord/MainPage.html', controller: 'cancer_MainPageCtrl' });
            $routeProvider.when('/PhysicalCondition/uid/:uid', { templateUrl: '/Views/CancerRecord/PhysicalCondition.html', controller: 'cancer_PhysicalConditionCtrl' });

            //财务信息
            $routeProvider.when('/FinanceManage', { templateUrl: '/Views/FinanceManage/index.html', controller: 'cancer_MainPageCtrl' });

            //基因信息
            $routeProvider.when('/UserGene', { templateUrl: '/Views/CancerRecord/UserGene.html', controller: 'UserGene_Controller' });

            //营养模块
            $routeProvider.when('/Template/:templatename/:userid', { templateUrl: '/Views/CancerRecord/HPN/Template.html', controller: 'TemplateCtrl' });
            $routeProvider.when('/result/:testno', { templateUrl: '/Views/CancerRecord/HPN/Result.html', controller: 'ResultCtrl' });
            $routeProvider.when('/message/:msg', { templateUrl: '/Views/CancerRecord/HPN/Message.html', controller: 'MessageCtrl' });
            $routeProvider.when('/hpnlist', { templateUrl: '/Views/CancerRecord/HPN/List.html', controller: 'HPNListCtrl' });

            //就诊档案
            $routeProvider.when('/DoctorHistory', { templateUrl: '/Views/DoctorHistory/Index.html', controller: 'doctorCtrl' });
            $routeProvider.when('/DoctorResult', { templateUrl: '/Views/DoctorHistory/ResultList.html', controller: 'resultCtrl' });
            $routeProvider.when('/DoctorResultInfo', { templateUrl: '/Views/DoctorHistory/ResultInfo.html', controller: 'resultInfoCtrl' });
            $routeProvider.when('/ImageEx', { templateUrl: '/Views/DoctorHistory/ImageEx.html', controller: 'imageExCtrl' });
            $routeProvider.when('/DiseaseInfo', { templateUrl: '/Views/DoctorHistory/DiseaseInfo.html', controller: 'DiseaseInfoCtrl' });
            $routeProvider.when('/PersonHistory', { templateUrl: '/Views/DoctorHistory/PersonHistory.html', controller: 'PersonHistoryCtrl' });
            $routeProvider.when('/UserArrange', { templateUrl: '/Views/DoctorHistory/UserArrange.html', controller: 'UserArrangeCtrl' });

            //既往病史
            $routeProvider.when('/DiseaseHistory', { templateUrl: '/Views/DiseaseHistory/Index.html', controller: 'diseasehisCtrl' });
            //$routeProvider.when('/DiseaseHistory/:uids', { templateUrl: '/Views/DiseaseHistory/Index.html', controller: 'diseasehisCtrl' });

            $routeProvider.when('/LabTestRecord', { templateUrl: '/Views/DoctorHistory/LabTestRecord.html', controller: 'labTestCtrl' });

            //药品管理
            $routeProvider.when('/Dug', { templateUrl: '/Views/Dug/CnDrug.html', controller: 'CnDrugCtrl' });
            $routeProvider.when('/AddCnDrug', { templateUrl: '/Views/Dug/AddCnDrug.html', controller: 'AddCnDrugCtrl' });
            $routeProvider.when('/CnDrugInfo', { templateUrl: '/Views/Dug/CnDrugInfo.html', controller: 'CnDrugInfoCtrl' });
            $routeProvider.when('/DrugControl', { templateUrl: '/Views/Dug/DrugControl.html', controller: 'DrugControlCtrl' });

            //基因字典库
            $routeProvider.when('/Gene', { templateUrl: '/Views/GeneDic/Gene.html', controller: 'GeneCtrl' });
            $routeProvider.when('/GeneAllele/:id', { templateUrl: '/Views/GeneDic/GeneAllele.html', controller: 'GeneAlleleCtrl' });
            $routeProvider.when('/GeneAlleleLocus/:id', { templateUrl: '/Views/GeneDic/GeneAlleleLocus.html', controller: 'GeneAlleleLocusCtrl' });
            $routeProvider.when('/DrugAllele', { templateUrl: '/Views/GeneDic/DrugAllele.html', controller: 'DrugAlleleCtrl' });

            //精准用药
            $routeProvider.when('/DrugInteractionSearch', { templateUrl: '/Views/Dug/DrugInteractionSearch.html', controller: 'DrugInterationSearchCtrl' });
            $routeProvider.when('/DrugGeneSearch', { templateUrl: '/Views/Dug/DrugGeneSearch.html', controller: 'DrugGeneSearchCtrl' });
            $routeProvider.when('/Prescription', { templateUrl: '/Views/Dug/Prescription.html', controller: 'PrescriptionCtrl' });


            //知识管理
            $routeProvider.when('/InfoList', { templateUrl: '/Views/Adm/info_list.html', controller: 'InfoListCtrl' });
            $routeProvider.when('/InfoEdit/:FileId', { templateUrl: '/Views/Adm/info_edit.html', controller: 'InfoEditCtrl' });
            $routeProvider.when('/Dictionary/:CategoryCode', { templateUrl: '/Views/Adm/dictionary.html', controller: 'DictionaryCtrl' });
            $routeProvider.when('/InfoSearch', { templateUrl: '/Views/Adm/info_search.html', controller: 'InfoSearchCtrl' });

            //模板管理
            $routeProvider.when('/TemplateMan', { templateUrl: '/Views/TemplateManager/TemplateList.html', controller: 'TemplateManCtrl' });
            $routeProvider.when('/TemplateInfo', { templateUrl: '/Views/TemplateManager/TemplateInfo.html', controller: 'TemplateInfoCtrl' });
            $routeProvider.when('/TemplateInfo/:id', { templateUrl: '/Views/TemplateManager/TemplateInfo.html', controller: 'TemplateInfoCtrl' });

            $routeProvider.when('/ItemStandVal', { templateUrl: '/Views/TemplateManager/ItemValueSet.html', controller: 'ItemStandValCtrl' });

            //全流程
            //元数据
            $routeProvider.when('/MetaData', { templateUrl: '/Views/Cond/MetaData.html', controller: 'MetaDataCtrl' });
            $routeProvider.when('/MetaDataParam/:mid', { templateUrl: '/Views/Cond/MetaDataParam.html', controller: 'MetaDataParamCtrl' });
            $routeProvider.when('/MetaDataPicker', { templateUrl: '/Views/Common/MetaDataPicker.html', controller: 'MetaDataPickerCtrl' });
            $routeProvider.when('/CondItem', { templateUrl: '/Views/Cond/CondItem.html', controller: 'CondItemCtrl' });
            $routeProvider.when('/CondList', { templateUrl: '/Views/Cond/CondList.html', controller: 'CondListCtrl' });

            //GuidLine管理
            $routeProvider.when('/GuideLineFlowList', { templateUrl: '/Views/GuideLine/FlowList.html', controller: 'GuidLineFlowCtrl' });
            $routeProvider.when('/GuideLine', { templateUrl: '/Views/GuidLineManager/GuidLineList.html', controller: 'GuideLineCtrl' });
            $routeProvider.when('/LungCancerDiag', { templateUrl: '/Views/GuidLineManager/LungCancerDiag.html', controller: 'GuideLineCtrl' });

            $routeProvider.when('/EventDetailForDoc', { templateUrl: '/Views/GuideLine/EventDetailForDoc.html', controller: 'EventDetailForDocCtrl' });

            //权限管理
            $routeProvider.when('/UserManage', { templateUrl: '/Views/Authorization/User.html', controller: 'UserManageCtrl' });
            $routeProvider.when('/Role', { templateUrl: '/Views/Authorization/Role.html', controller: 'RoleCtrl' });
            $routeProvider.when('/AddRole', { templateUrl: '/Views/Authorization/RoleInfo.html', controller: 'AddRoleCtrl' });
            $routeProvider.when('/RoleView', { templateUrl: '/Views/Authorization/RoleView.html', controller: 'RoleViewCtrl' });
            $routeProvider.when('/EditRole', { templateUrl: '/Views/Authorization/RoleInfo.html', controller: 'EditRoleCtrl' });
            $routeProvider.when('/Permission', { templateUrl: '/Views/Authorization/Permission.html', controller: 'PermissionCtrl' });
            $routeProvider.when('/UserType', { templateUrl: '/Views/Authorization/usertype.html', controller: 'UserTypeCtrl' });

            //用户管理
            $routeProvider.when('/DoTreatment', { templateUrl: '/Views/User/DoTreatment.html', controller: 'DoTreatmentCtrl' });
            $routeProvider.when('/DoTreatmentView', { templateUrl: '/Views/User/DoTreatmentView.html', controller: 'DoTreatmentViewCtrl' });
            $routeProvider.when('/UserCenterSymptom', { templateUrl: '/Views/User/Symptom.html', controller: 'SymptomCtrl' });
            $routeProvider.when('/UpdateSymptom', { templateUrl: '/Views/User/UpdateSymptom.html', controller: 'UpdateSymptomCtrl' });
            $routeProvider.when('/ViewUpload', { templateUrl: '/Views/User/ViewUpload.html', controller: 'ViewUploadCtrl' });

            $routeProvider.otherwise({ redirectTo: '/' });

            //医生控制页面
            $routeProvider.when('/DoctorControlFlow', { templateUrl: '/Views/GuideLine/DoctorControl.html', controller: 'DoctorControlFlowCtrl' });

            //检验模版维护
            $routeProvider.when('/ExamineTemplate', { templateUrl: '/Views/Adm/examine_template.html', controller: 'ExamineTemplateCtrl' });
            $routeProvider.when('/ExamineTemplateItem/:templateId', { templateUrl: '/Views/Adm/examine_item.html', controller: 'ExamineTemplateItemCtrl' });
            $routeProvider.when('/ExamineTemplateItemOption/:templateItemId', { templateUrl: '/Views/Adm/examine_option.html', controller: 'ExamineTemplateItemOptionCtrl' });
            $routeProvider.when('/ExamineReport/:uid/:examineNo/:hid', { templateUrl: '/Views/Adm/examine_report.html', controller: 'ExamineReportCtrl' });

            $routeProvider.when('/ExamineReport', { templateUrl: '/Views/Adm/examine_report.html', controller: 'ExamineReportCtrl' });
            $routeProvider.when('/ExamineReportList/:uid', { templateUrl: '/Views/Adm/examine_reportlist.html', controller: 'ExamineReportListCtrl' });

            $routeProvider.when('/Forbidden', { templateUrl: '/Views/Forbidden.html', controller: 'ForbiddenCtrl' });

            //客服待办
            $routeProvider.when('/ServiceTask', { templateUrl: '/Views/User/servicetask.html', controller: 'ServiceTaskCtrl' });
            $routeProvider.when('/ServiceTraceList', { templateUrl: '/Views/User/servicetracelist.html', controller: 'ServiceTraceListCtrl' });
            
            //产品维护
            $routeProvider.when('/ProductsList', { templateUrl: '/Views/Adm/product_list.html', controller: 'ProductsListCtrl' });

            //会员管理
            $routeProvider.when('/MemberList', { templateUrl: '/Views/Member/MemberList.html', controller: 'MemberCtrl' });
            $routeProvider.when('/MemberInfo', { templateUrl: '/Views/Member/MemberInfo.html', controller: 'MemberInfoCtrl' });

            //用户中心
            $routeProvider.when('/UserInfo', { templateUrl: '/Views/User/UserInfo.html', controller: 'UserInfoManagerCtrl' });

            //我的管家
            $routeProvider.when('/MyHouseKeeper', { templateUrl: '/Views/User/MyHouseKeeper.html', controller: 'MyHouseKeeperCtrl' });

            //我的管家
            $routeProvider.when('/MyQuestion', { templateUrl: '/Views/User/MyQuestion.html', controller: 'MyQuestionCtrl' });

            //我的会员与服务
            $routeProvider.when('/MyProduct', { templateUrl: '/Views/User/MyProduct.html', controller: 'MyProductCtrl' });

            //我的账单
            $routeProvider.when('/AccountRecord', { templateUrl: '/Views/User/AccountRecord.html', controller: 'AccountRecordCtrl' });

            //医学编辑
            $routeProvider.when('/MedicalCenterArrangement', { templateUrl: '/Views/DoctorHistory/MedicalCenterArrangement.html', controller: 'MedicalArrangeCtrl' });
            $routeProvider.when('/MedicalArrange', { templateUrl: '/Views/DoctorHistory/MedicalArrange.html', controller: 'ArrangeEditCtrl' });

            //字典管理
            $routeProvider.when('/DictionaryManage', { templateUrl: '/Views/DictionaryManage/Index.html', controller: 'DictManageCtrl' });
            $routeProvider.when('/AddDictionary', { templateUrl: '/Views/DictionaryManage/Info.html', controller: 'AddDictCtrl' });
            $routeProvider.when('/EditDictionary', { templateUrl: '/Views/DictionaryManage/Info.html', controller: 'EditDictCtrl' });

            //系统菜单管理
            $routeProvider.when('/SysFunction', { templateUrl: '/Views/Authorization/Function.html', controller: 'SysFunctionCtrl' });
            $routeProvider.when('/AddSysFunction', { templateUrl: '/Views/Authorization/FunctionInfo.html', controller: 'AddSysFunctionCtrl' });
            $routeProvider.when('/EditSysFunction', { templateUrl: '/Views/Authorization/FunctionInfo.html', controller: 'EditSysFunctionCtrl' });
        }
    ]).controller('MyController', function ($scope, $http) {
        $scope.logout = function () {
            KMConfirm(
                {
                    msg: '确定注销登录？',
                    btnMsg:'确定'
                }, function (e) {
                    $http({
                        method: 'POST',
                        url: '/User/UserLogout'
                    }).success(function (data) {
                        window.location = "/User/Login#/Login";
                    }).error(function (data) {
                    });
                }
            )
        };
    });
