using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using KMHC.CTMS.BLL;
using KMHC.CTMS.Common.Helper;
using KMHC.CTMS.Model.CancerRecord;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.PrecisionMedicine;
using KMHC.CTMS.Model.Repository.Implement.CancerRecord;
using KMHC.CTMS.Model.Repository.Interface;
using KMHC.CTMS.UI.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace KMHC.CTMS.UI.Controllers.API
{
    [RoutePrefix("Api")]
    public class AdmController : ApiController
    {
        private MongoServer server;
        private MongoDatabase database;
        private UserInfoService _userinfo;
        private DictionaryService _dictionary;
        IFileUploadRepository _file;

        #region 属性
        public string MongoConnStr
        {
            get
            {
                return ConfigurationManager.AppSettings["MongoConnStr"];
            }
        }

        public string SolrUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SolrUrl"];
            }
        }

        public string MongoDBName
        {
            get
            {
                return ConfigurationManager.AppSettings["MongoDBName"];
            }
        }

        public int PageSize
        {
            get
            {
                return ConfigurationManager.AppSettings["PageSize"].ToInt(5);
            }
        }
        #endregion

        public AdmController()
        {
            server = MongoServer.Create(MongoConnStr);
            database = server.GetDatabase(MongoDBName, SafeMode.True);
            _userinfo = new UserInfoService();
            _dictionary = new DictionaryService();
            _file = new EFFileUploadRepository();
        }

        public IHttpActionResult Get([FromUri]Request<Info> request)
        {
            if (request.Type == -1)
            {
                MongoCollection data = database.GetCollection("info");
                GroupByBuilder groupbyBuilder = new GroupByBuilder(new string[] { "CategoryCode", "CategoryName" });
                Dictionary<string, int> dic_F = new Dictionary<string, int>();
                dic_F["num"] = 0;
                var result_F = data.Group(null, groupbyBuilder, BsonDocument.Create(dic_F), BsonJavaScript.Create("function(doc,prev){prev.num++;}"), BsonJavaScript.Create("function(doc){doc.Count=doc.num;delete doc.num;}")).ToList();
                /*List<InfoAllList> list = new List<InfoAllList>();
                if (result_F.Count > 0)
                {
                    foreach (var item in result_F)
                    {
                        InfoAllList temp = new InfoAllList();
                        temp.CategoryCode = item["CategoryCode"].AsString;
                        temp.CategoryName = item["CategoryName"].AsString;
                        temp.Count = item["Count"].AsDouble.ToString().ToInt(0);
                        list.Add(temp);
                    }
                }*/
                Response<List<BsonDocument>> rsp = new Response<List<BsonDocument>>();
                rsp.Data = result_F;
                return Ok(rsp);
            }
            if (request.ID == "SearchInfo")
            {
                Response<List<InfoModel>> rsp = new Response<List<InfoModel>>();
                if (string.IsNullOrEmpty(request.Keyword))
                    return Ok(rsp);

                List<InfoModel> list = new List<InfoModel>();

                #region 1、在mongodb查询匹配的Info对象
                IMongoQuery query = null;
                if (request.UserName == "Title")
                    query = Query<Info>.Matches(c => c.Title, new BsonRegularExpression(new Regex(request.Keyword + ".*")));
                else if (request.UserName == "Author")
                    query = Query<Info>.Matches(c => c.Author, new BsonRegularExpression(new Regex(request.Keyword + ".*")));
                else if (request.UserName == "Source")
                    query = Query<Info>.Matches(c => c.Source, new BsonRegularExpression(new Regex(request.Keyword + ".*")));
                else if (request.UserName == "Digest")
                    query = Query<Info>.Matches(c => c.Digest, new BsonRegularExpression(new Regex(request.Keyword + ".*")));
                else if (request.UserName == "KeyWord")
                    query = Query<Info>.Matches(c => c.KeyWord, new BsonRegularExpression(new Regex(request.Keyword + ".*")));
                else if (request.UserName == "CategoryCode")
                    query = Query<Info>.Matches(c => c.CategoryCode, new BsonRegularExpression(new Regex(request.Keyword)));

                if (query != null)
                {
                    var data = database.GetCollection("info").Find(query).ToList();
                    foreach (var item in data)
                    {
                        InfoModel temp = new InfoModel();
                        temp.InfoId = item.GetValue("_id").ToString();
                        temp.Title = item.GetValue("Title").ToString();
                        Dictionary dic = getArticleClassNameFoo(item.GetValue("CategoryCode").ToString());
                        if (dic != null)
                            temp.ArticleClass = dic.value;
                        temp.ArticleCategory = item.GetValue("CategoryName").ToString();
                        temp.Author = item.GetValue("Author").ToString();
                        string extStr = item.GetValue("ArticleType").ToString();
                        string[] arr = extStr.Split('、');
                        if (arr.Length > 0)
                            temp.FileType = arr[0];
                        else
                            temp.FileType = "1176708";

                        string fileUrl = "";
                        string dbFileName = item.GetValue("FileName").AsString.Trim();
                        if(!string.IsNullOrEmpty(dbFileName))
                            fileUrl = "/Upload/" + dbFileName;
                        /*if (item.GetValue("FileName").ToString().Trim().EndsWith(".xls") || item.GetValue("FileName").ToString().Trim().EndsWith(".xlsx"))
                        {
                            fileUrl += "XLS/";
                        }
                        fileUrl += item.GetValue("FileId").ToString().Trim() + "/" + item.GetValue("FileName").ToString().Trim();*/
                        temp.FilePath = fileUrl;
                        temp.PublishTime = item.GetValue("PublishTime").AsDateTime;
                        temp.ArticleType = item.GetValue("ArticleType").AsString;
                        temp.ModifyTime = item.GetValue("ModifyTime").AsDateTime;
                        temp.ModifyUser = item.GetValue("ModifyUser").AsString;

                        list.Add(temp);
                    }
                }
                #endregion

                if (request.UserName == "Content")
                {

                    #region 2、查询solr服务器来搜索文件内容
                    string q = "text:" + request.Keyword;
                    string url = SolrUrl + "select?q=" + HttpUtility.UrlEncode(q) + "&rows=1000000&wt=json&_=" + StringExtension.getTimeStamp(DateTime.Now);
                    HttpClient client = new HttpClient();
                    string result = client.GetStringAsync(url).Result;
                    SolrContext sc = result.JsonDeserialize<SolrContext>();
                    #endregion

                    #region 3、通过solr搜索结果来匹配数据库
                    foreach (var item in sc.response.docs)
                    {
                        FileInfo fi = new FileInfo(item.uri);
                        string dirName = fi.Directory.Name;
                        var query2 = Query.EQ("FileId", dirName);
                        Info info = database.GetCollection("info").FindAs<Info>(query2).FirstOrDefault();
                        if (info == null)
                            continue;
                        if (info.FileName != fi.Name)
                            continue;

                        InfoModel temp = new InfoModel();
                        temp.InfoId = info._id.ToString();
                        temp.Title = info.Title;
                        Dictionary dic = getArticleClassNameFoo(info.CategoryCode);
                        if (dic != null)
                            temp.ArticleClass = dic.value;
                        temp.ArticleCategory = info.CategoryName;
                        temp.Author = info.Author;

                        string extStr = info.ArticleType;
                        string[] arr = extStr.Split('、');
                        if (arr.Length > 0)
                            temp.FileType = arr[0];
                        else
                            temp.FileType = "1176708";

                        string fileUrl = "/Upload/";
                        if (fi.Extension == ".xls" || fi.Extension == ".xlsx")
                        {
                            fileUrl += "XLS/";
                        }
                        fileUrl += info.FileId + "/" + info.FileName;
                        temp.FilePath = fileUrl;

                        if (list.Contains(temp))
                            continue;
                        list.Add(temp);
                    }
                    #endregion

                }

                rsp.Data = list;
                if (request.UserName == "CategoryCode")
                {
                    List<InfoModel> result = list.Skip((request.CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    int count = list.Count / PageSize;
                    if (list.Count % PageSize > 0)
                    {
                        count += 1;
                    }
                    rsp.PagesCount = count;
                    rsp.Data = result;
                }

                return Ok(rsp);
            }
            else
            {
                Guid g = new Guid();
                if (Guid.TryParse(request.Keyword, out g))
                {
                    Dictionary dic = _dictionary.GetDictionaryById(g.ToString());
                    List<string> nodeIds = new List<string>();
                    nodeIds.Add(dic.nodeId);
                    Foo(nodeIds, dic);

                    Response<List<Info>> response = new Response<List<Info>>();
                    var list = database.GetCollection<Info>("info").FindAll().ToList();
                    List<Info> result = new List<Info>();
                    foreach (string item in nodeIds)
                    {
                        Info temp = list.Find(p => p.CategoryCode == item);
                        if (temp != null)
                            result.Add(temp);
                    }
                    response.Data = result;
                    return Ok(response);
                }
                else
                {
                    Response<List<Info>> response = new Response<List<Info>>();
                    var list = database.GetCollection<Info>("info").FindAll().ToList();
                    if (!string.IsNullOrEmpty(request.Keyword))
                        list = list.FindAll(p => p.Title.Contains(request.Keyword) || p.Html.Contains(request.Keyword) || p.Author.Contains(request.Keyword));
                    response.Data = list;
                    return Ok(response);
                }
            }
        }

        private void Foo(List<string> nodeIds, Dictionary dic)
        {
            foreach (var item in dic.nodes)
            {
                if (item.nodes.Count > 0)
                    Foo(nodeIds, item);
                nodeIds.Add(item.nodeId);
            }
        }

        public IHttpActionResult Get(string fileId)
        {
            Response<Info> response = new Response<Info>();
            var collection = database.GetCollection<Info>("info");
            IMongoQuery query = Query.EQ("FileId", fileId);
            Info info = collection.FindAs<Info>(query).FirstOrDefault();
            response.Data = info;
            return Ok(response);
        }

        public IHttpActionResult Post([FromBody]Request<Info> request)
        {
            Response<Info> response = new Response<Info>();
            Info info = request.Data as Info;
            UserInfo userInfo = _userinfo.GetCurrentUser();
            if (userInfo == null)
                return BadRequest("用户未登录");

            FileUpload fu = _file.Get("Info", info.FileId).FirstOrDefault();

            if (string.IsNullOrEmpty(request.ID))
            {
                info.Author = userInfo.LoginName;
                info.CreateTime = DateTime.Now;
                info.ModifyTime = DateTime.Now;
                info.ModifyUser = userInfo.LoginName;
                info.ArticleType = info.ArticleType;
                info.OriginalFileName = fu == null ? "" : fu.FileName;
                info.FileName = fu == null ? "" : fu.FilePath;
                var collection = database.GetCollection<Info>("info");
                collection.Insert(info);
            }
            else
            {
                var collection = database.GetCollection<Info>("info");

                IMongoQuery query = Query.EQ("FileId", info.FileId);
                Info editInfo = collection.FindAs<Info>(query).FirstOrDefault();
                editInfo.CategoryCode = info.CategoryCode;
                editInfo.CategoryName = info.CategoryName;
                editInfo.Title = info.Title;
                editInfo.Html = info.Html;
                editInfo.ArticleType = info.ArticleType;
                editInfo.FileName = fu == null ? "" : fu.FilePath;
                editInfo.OriginalFileName = fu == null ? "" : fu.FileName;
                editInfo.ModifyUser = userInfo.LoginName;
                editInfo.Source = info.Source;
                editInfo.Digest = info.Digest;
                editInfo.KeyWord = info.KeyWord;
                editInfo.ModifyTime = DateTime.Now;
                collection.Save(editInfo);
                info = editInfo;
            }

            #region 更新搜索引擎
            HttpClient client = new HttpClient();
            client.GetStringAsync(SolrUrl + "dataimport?commit=true&clean=false&command=full-import");
            //下面是搜索的例子,其中参数q是搜索条件，条件需要url编码
            //string result = client.GetStringAsync("http://10.2.21.68:8983/solr/tika/select?q=text%3A%E6%9D%8F%E4%BB%81&rows=1000000&wt=json").Result;
            #endregion

            response.Data = info;
            return Ok(response); ;
        }

        private Dictionary getArticleClassNameFoo(string categoryCode)
        {
            Dictionary dic = _dictionary.GetDictionaryById(categoryCode);
            if (dic.parentId == "0")
                return dic;
            return getArticleClassNameFoo(dic.parentId);
        }
    }

    public class InfoModel
    {
        public string InfoId { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// 文章分类
        /// </summary>
        public string ArticleClass { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public string ArticleCategory { get; set; }

        public string Author { get; set; }

        public string FileType { get; set; }

        /// <summary>
        /// 文件路径，以供下载
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishTime { get; set; }

        public string ArticleType { get; set; }

        public DateTime ModifyTime { get; set; }

        public string ModifyUser { get; set; }
    }

    public class InfoAllList
    {
        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public int Count { get; set; }
    }
}