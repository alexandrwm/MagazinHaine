using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;

namespace BeStreet.BusinessLogic.Interfaces
{
    public interface IProd
    {
        int CreateProduct(Product obj);
        List<PdFilterVM> GetProducts(string category);
        List<PdVM> GetAllProducts();
        Product GetProductById(int? id);
        bool DeleteProductById(int id);
        bool UpdateProductById(int? id, Product obj);
        List<TEntity> GetFilter<TEntity>() where TEntity : class;
        List<PdFilterVM> GetFilteredProducts(int[] typeIds, int[] sizeIds, int[] colorIds, string targetName);
        List<PdFilterVM> GetDetailedProductById(int id);
    }
}
