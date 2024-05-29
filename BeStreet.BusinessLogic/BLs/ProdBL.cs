using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;

namespace BeStreet.BusinessLogic.BLs
{
    public class ProdBL : ProdApi, IProd
    {
        public int CreateProduct(Product obj)
        {
            return CreateProductAction(obj);
        }

        public List<PdFilterVM> GetProducts(string category)
        {
            return GetProductsAction(category);
        }

        public List<PdVM> GetAllProducts()
        {
            return GetAllProductsAction();
        }

        public Product GetProductById(int? id)
        {
            return GetProductByIdAction(id);
        }

        public bool DeleteProductById(int id)
        {
            return DeleteProductByIdAction(id);
        }

        public bool UpdateProductById(int? id, Product obj)
        {
            return UpdateProductByIdAction(id, obj);
        }

        public List<TEntity> GetFilter<TEntity>() where TEntity : class
        {
            return GetFilterAction<TEntity>();
        }

        public List<PdFilterVM> GetFilteredProducts(int[] typeIds, int[] sizeIds, int[] colorIds, string targetName)
        {
            return GetFilteredProductsAction(typeIds, sizeIds, colorIds, targetName);
        }

        public List<PdFilterVM> GetDetailedProductById(int id)
        {
            return GetDetailedProductByIdAction(id);
        }
    }
}
