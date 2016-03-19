using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.Model.Product
{
    public class Products
    {
        public string ProductId { get; set; }

        public int ProductType { get; set; }

        public string ProductTypeText { get; set; }

        public string ProductName { get; set; }

        public int IsFree { get; set; }

        public string IsFreeText
        {
            get
            {
                switch (IsFree)
                {
                    case 1:
                        return "是";
                    case 0:
                        return "否";
                    default:
                        return string.Empty;
                }
            }
        }

        public decimal ProductPrice { get; set; }

        public int ProductUnit { get; set; }

        public string ProductUnitText { get; set; }

        public decimal SalePrice { get; set; }

        public string ProductRemark { get; set; }
    }
}
