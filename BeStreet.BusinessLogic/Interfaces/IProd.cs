using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
