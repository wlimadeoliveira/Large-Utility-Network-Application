using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IProduct
    {
        List<Product> GetAll();
        List<Product> getAvaibleProducts();
        List<Product> getChilds(long parentID);
        Product GetById(long Id);
        Product GetByIdDetailed(long id);
        Product GetByIdForDelete(long id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(Product product);
        Product GetByIdWithAttributes(long Id);
        List<Product> getProductsByType(long typeID);
        List<Product> getProductByCustomers(long customerID);
        List<Product> getAllProductsHistory();
        Product getProductWithHistory(long id);
        List<Product> getParents(Product product, List<Product> parents);
    }
}
