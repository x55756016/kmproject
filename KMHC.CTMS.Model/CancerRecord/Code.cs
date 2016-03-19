/*
 * 描述:数据字典
 *  
 * 修订历史: 
 * 日期       修改人              Email                  内容
 * 20151016   郝元彦              haoyuanyan@gmail.com   创建 
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace KMHC.CTMS.Model.CancerRecord
{
    public class Code
    {
        public Int32 CodeId { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary>
        public string CodeNo { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string CodeName { get; set; }
        /// <summary>
        /// 字典内容，XML格式表示
        /// </summary>
        public string CodeValue { get; set; }

        private List<Option> _options;
        /// <summary>
        /// 字典内容，XML转换成Option后
        /// </summary>
        public List<Option> Options
        {
            get
            {
                if (_options == null || _options.Count == 0)
                {
                    if (CodeValue != null)
                    {
                        //CodeValue不为空才生成
                        _options = new List<Option>();
                        var root = XDocument.Parse(this.CodeValue).Root;
                        if (root != null)
                        {
                            foreach (var e in root.Elements("Item"))
                            {
                                _options.Add(new Option()
                                {
                                    Value = e.Attribute("Value").Value,
                                    Name = e.Attribute("Name").Value,
                                    //Desc = e.Attribute("Desc").Value,
                                    Items =
                                        e.Element("Items") == null
                                            ? new List<Option>()
                                            : GenerateSubOptions(e.Element("Items"), e.Attribute("Value").Value)
                                });
                            }

                        }
                    }
                }
                return _options;
            }
        }

        /// <summary>
        /// 生成子项
        /// </summary>
        /// <param name="option"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private List<Option> GenerateSubOptions(XElement option, string parent)
        {
            if (option == null) return null;
            return option.Elements("Item").Select(c => new Option()
            {
                Value = c.Attribute("Value").Value,
                Name = c.Attribute("Name").Value,
                //Desc = c.Attribute("Desc").Value,
                Items = c.Element("Items") == null ? new List<Option>() : GenerateSubOptions(c.Element("Items"), c.Attribute("Value").Value)
            }).ToList();
        }
    }
    public class Option
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public string Desc { get; set; }
        public List<Option> Items { get; set; }

    }
}
