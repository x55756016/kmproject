using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMHC.CTMS.DAL.Database;
using KMHC.CTMS.Model.Product;
using KMHC.CTMS.Model.Repository.Interface;

namespace KMHC.CTMS.Model.Repository.Implement
{
    public class EFProductsRepository : BaseRepository<CTMS_PRODUCTS>, IProductsRepository
    {
        public bool AddProducts(Products model)
        {
            return Insert(LoadEntityFromModel(model));
        }

        public bool UpdateProducts(Products model)
        {
            return Update(LoadEntityFromModel(model));
        }

        public bool DeleteProductsById(string productId)
        {
            return DeleteById(productId);
        }

        public Products GetProductsById(string productId)
        {
            return LoadModelFromEntity(FindOne(p => p.PRODUCTID == productId));
        }

        public List<Products> GetAllProducts()
        {
            return FindAll().Select(LoadModelFromEntity).ToList();
        }

        protected CTMS_PRODUCTS LoadEntityFromModel(Products model)
        {
            if (model == null)
                return null;

            CTMS_PRODUCTS entity = new CTMS_PRODUCTS();
            entity.PRODUCTID = model.ProductId;
            entity.PRODUCTNAME = model.ProductName;
            entity.PRODUCTPRICE = model.ProductPrice;
            entity.PRODUCTREMARK = model.ProductRemark;
            entity.PRODUCTTYPE = model.ProductType;
            entity.PRODUCTUNIT = model.ProductUnit;
            entity.ISFREE = model.IsFree;
            entity.SALEPRICE = model.SalePrice;

            return entity;
        }

        protected Products LoadModelFromEntity(CTMS_PRODUCTS entity)
        {
            if (entity == null)
                return null;

            Products model = new Products();
            model.ProductUnit = (int)entity.PRODUCTUNIT;
            model.ProductType = (int)entity.PRODUCTTYPE;
            model.ProductRemark = entity.PRODUCTREMARK;
            model.ProductPrice = (decimal)entity.PRODUCTPRICE;
            model.ProductName = entity.PRODUCTNAME;
            model.ProductId = entity.PRODUCTID;
            model.IsFree = (int)entity.ISFREE;
            model.SalePrice = (decimal)entity.SALEPRICE;

            return model;
        }
    }
}
