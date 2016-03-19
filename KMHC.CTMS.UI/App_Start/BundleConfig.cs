using System.Web;
using System.Web.Optimization;

namespace KMHC.CTMS.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css"
                , "~/Content/sb-admin.css"
                , "~/Content/font-awesome.min.css"
                ,"~/Content/bootstrap-multiselect.css"
                ,"~/Content/webuploader.css"
                ,"~/Scripts/kindeditor/themes/default/default.css"
                ,"~/Scripts/kindeditor/themes/prettify.css"
                ));





            bundles.Add(new StyleBundle("~/Content/GuidLine").Include(
                "~/Content/Flowdesign/css/bootstrap/css/bootstrap.css"
                ,"~/Content/Flowdesign/css/site.css"
                ,"~/Content/Flowdesign/js/flowdesign/flowdesign.css"
                ,"~/Content/Flowdesign/js/jquery.multiselect2side/css/jquery.multiselect2side.css"
                ,"~/Content/bootstrap-multiselect.css"
                , "~/Content/font-awesome.min.css"
                ));


            bundles.Add(new ScriptBundle("~/Script/GuidLine").Include(
                  "~/Scripts/angular.js"
                        , "~/Scripts/angular-route.js"
                        , "~/Scripts/angular-resource.js"
                        , "~/Scripts/angular-animate.js",
                "~/Content/Flowdesign/js/jquery-1.7.2.min.js",
                "~/Content/Flowdesign/css/bootstrap/js/bootstrap.min.js",
                "~/Content/Flowdesign/js/jquery-ui/jquery-ui-1.9.2-min.js",
                "~/Content/Flowdesign/js/jsPlumb/jquery.jsPlumb-1.3.16-all-min.js",
                "~/Content/Flowdesign/js/jquery.contextmenu.r2.js",
                "~/Content/Flowdesign/js/jquery.multiselect2side/js/jquery.multiselect2side.js",
                "~/Content/Flowdesign/js/flowdesign/leipi.flowdesign.v3.js",
                "~/AppScripts/common/JSGuid.js",
                 "~/AppScripts/common/services.js"
                ));








            bundles.Add(new ScriptBundle("~/Script/bootstrap").Include("~/Scripts/jquery-2.1.4.min.js"
                 , "~/Scripts/bootstrap.js"
                , "~/Scripts/bootstrap-datepicker*"
                , "~/Scripts/paging.js"
                , "~/Scripts/webuploader.js"
                , "~/Scripts/bootstrap-typeahead.js"
                , "~/AppScripts/common/pager.js"
                , "~/AppScripts/common/common.js"
                ));

            bundles.Add(new ScriptBundle("~/Script/angular").Include(
                        "~/Scripts/angular.js"
                        , "~/Scripts/angular-route.js"
                        , "~/Scripts/angular-resource.js"
                        , "~/Scripts/angular-animate.js"
                        , "~/AppScripts/common/navi-controllers.js"
                        , "~/AppScripts/common/services.js"
                        , "~/AppScripts/common/utility.js"
                        , "~/AppScripts/common/area.js"
                        , "~/AppScripts/common/AppRoute.js"
                        , "~/AppScripts/common/JSGuid.js"

                        ,"~/AppScripts/common/Authorization.js"

                        , "~/AppScripts/HealthRecord/HealthRecord.js"
                        , "~/AppScripts/HealthRecord/Exam.js"
                        , "~/AppScripts/HealthRecord/ExamCategory.js"
                        , "~/AppScripts/HealthRecord/ExamTemplate.js"
                        , "~/AppScripts/HealthRecord/ExamItem.js"
                        , "~/AppScripts/HealthRecord/ScoreScale.js"
                        , "~/AppScripts/HealthRecord/Community.js"
                        , "~/AppScripts/HealthRecord/DiseaseHistory.js"
                        , "~/AppScripts/HealthRecord/Family.js"
                        , "~/AppScripts/HealthRecord/CancerRecord/CancerUser.js"
                        , "~/AppScripts/HealthRecord/Finance.js"
                        , "~/AppScripts/HealthRecord/CancerRecord/UserGene.js"
                        , "~/AppScripts/HealthRecord/CancerRecord/main.js"
                        , "~/AppScripts/HealthRecord/Doctor.js"
                        , "~/AppScripts/GeneDic/Gene.js"
                        , "~/AppScripts/GeneDic/GeneAllele.js"
                        , "~/AppScripts/GeneDic/GeneAlleleLocus.js"
                        , "~/AppScripts/GeneDic/DrugAllele.js"
                        , "~/AppScripts/Drug/Dug.js"
                        , "~/AppScripts/User/main.js"
                        , "~/AppScripts/User/UserInfo.js"
                        , "~/AppScripts/Drug/DrugGeneSearch.js"
                        , "~/AppScripts/Drug/Prescrition.js"
                        , "~/AppScripts/Template/Template.js"
                        , "~/AppScripts/Template/ItemStandVal.js"
                        , "~/AppScripts/Cond/CondItem.js"
                        , "~/AppScripts/Cond/MetaData.js"
                        , "~/AppScripts/GuideLine/GuideLine.js"
                        , "~/Scripts/Chart.min.js"
                        , "~/Scripts/md5.js"
                        , "~/AppScripts/Adm/info.js"
                        , "~/Scripts/bootstrap-treeview.js"
                        , "~/Scripts/kindeditor/kindeditor.js"
                        , "~/Scripts/kindeditor/zh_CN.js"
                        , "~/Scripts/kindeditor/prettify.js"
                        ,"~/AppScripts/GuideLine/GuideLineFlowList.js"
                        , "~/AppScripts/Authorization/User.js"
                        , "~/AppScripts/Authorization/Permission.js"
                        , "~/AppScripts/Authorization/Role.js"
                        , "~/AppScripts/Authorization/Forbidden.js"
                        , "~/AppScripts/GuideLine/DoctorControl.js"
                        , "~/AppScripts/Adm/examine.js"
                        , "~/AppScripts/GuideLine/DoTreatment.js"
                        ,"~/AppScripts/GuideLine/EventDetailForDoc.js"
                        , "~/AppScripts/GuideLine/Symptom.js"
                        , "~/AppScripts/Adm/product.js"
                        , "~/AppScripts/User/MyHouseKeeper.js"
                        , "~/AppScripts/User/MyQuestion.js"
                        , "~/AppScripts/User/AccountRecord.js"
                        , "~/AppScripts/User/MyProduct.js"
                        , "~/AppScripts/Member/Member.js"
                        , "~/AppScripts/HealthRecord/MedicalArrange.js"
                        , "~/AppScripts/DictManage/DictionaryManage.js"
                        , "~/Scripts/bootstrap-combobox.js"
                        , "~/AppScripts/common/user.js"
                        , "~/AppScripts/Authorization/Function.js"
                        , "~/Content/web-im-1.0.7.2/sdk/strophe.js"
                        , "~/Content/web-im-1.0.7.2/sdk/json2.js"
                        , "~/Content/web-im-1.0.7.2/sdk/easemob.im-1.0.7.js"
                        , "~/Content/web-im-1.0.7.2/easemob.im.config.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}