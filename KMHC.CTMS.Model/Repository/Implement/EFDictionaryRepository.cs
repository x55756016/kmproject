using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Common;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFDictionaryRepository : BaseRepository<HR_DICTIONARY>,IDictionaryRepository
    {
        public List<Dictionary> GetDictionaryByCategory(string category)
        {
            List<HR_DICTIONARY> list = FindAll(p => p.DICTIONCATEGORY == category).ToList();

            List<Dictionary> list3 = new List<Dictionary>();
            list.ForEach((p) => { list3.Add(LoadModelFromEntity(p)); });

            var list2 = list3.FindAll(p => p.parentId == "0");

            List<Dictionary> listResult = new List<Dictionary>();

            foreach (var item in list2)
            {
                listResult.Add(Foo(list3, item));
            }

            return listResult;
        }

        private Dictionary Foo(List<Dictionary> list,Dictionary dic)
        {
            var listSearch = list.FindAll(p => p.parentId == dic.nodeId);
            foreach (var item in listSearch)
            {
                dic.nodes.Add(Foo(list, item));
            }
            return dic;
        }

        protected Dictionary LoadModelFromEntity(HR_DICTIONARY entity)
        {
            if (entity == null)
                return null;

            Dictionary model = new Dictionary();
            model.nodeId = entity.DICTIONARYID;
            model.parentId = entity.FATHERID;
            model.DictionCategory = entity.DICTIONCATEGORY;
            model.DictionCategoryName = entity.DICTIONCATEGORYNAME;
            model.text = entity.DICTIONARYNAME;
            model.value = entity.DICTIONARYVALUE;
            model.CreateUser = entity.CREATEUSER;
            model.CreateDate = entity.CREATEDATE;
            model.UpdateUser = entity.UPDATEUSER;
            model.UpdateDate = entity.UPDATEDATE;
            model.Remark = entity.REMARK;

            return model;

        }

        protected HR_DICTIONARY LoadEntityFromModel(Dictionary model)
        {
            if (model == null)
                return null;

            HR_DICTIONARY entity = new HR_DICTIONARY();
            entity.DICTIONARYID = model.nodeId;
            entity.FATHERID = model.parentId;
            entity.DICTIONCATEGORY = model.DictionCategory;
            entity.DICTIONCATEGORYNAME = model.DictionCategoryName;
            entity.DICTIONARYNAME = model.text;
            entity.DICTIONARYVALUE = model.value;
            entity.CREATEDATE = model.CreateDate;
            entity.CREATEUSER = model.CreateUser;
            entity.UPDATEDATE = model.UpdateDate;
            entity.UPDATEUSER = model.UpdateUser;
            entity.REMARK = model.Remark;

            return entity;
        }

        public bool AddDictionary(Dictionary dic)
        {
            HR_DICTIONARY d = LoadEntityFromModel(dic);
            bool result = Insert(d);
            return result;
        }

        public bool EditDictionary(Dictionary dic)
        {
            HR_DICTIONARY d = LoadEntityFromModel(dic);
            bool result = Update(d);
            return result;
        }

        public Dictionary GetDictionaryById(string dictionaryId)
        {
            HR_DICTIONARY DIC = FindOne(p => p.DICTIONARYID == dictionaryId);
            List<HR_DICTIONARY> list = FindAll(p => p.DICTIONCATEGORY == DIC.DICTIONCATEGORY).ToList();

            List<Dictionary> list2 = new List<Dictionary>();
            list.ForEach((p) => { list2.Add(LoadModelFromEntity(p)); });

            Dictionary d = Foo(list2, LoadModelFromEntity(DIC));

            return d;
        }

        public void DeleteDictionary(string dictionaryId)
        {
            Dictionary dic = GetDictionaryById(dictionaryId);
            List<string> list = new List<string>();
            list.Add(dic.nodeId);
            Foo(list,dic);
            foreach(string item in list)
                DeleteById(item);
        }

        private void Foo(List<string> nodeIds,Dictionary dic)
        {
            foreach (var item in dic.nodes)
            {
                if (item.nodes.Count > 0)
                    Foo(nodeIds,item);
                nodeIds.Add(item.nodeId);
            }
        }
    }
}
