using Angle.Interfaces;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Angle.Repositories
{
    public class ProductRepo : IProduct
    {
        private readonly ProjectDataContext _context;

        public ProductRepo(ProjectDataContext context)
        {
            _context = context;
        }
        public void Delete(Product product)
        {
            _context.Product.Remove(product);
        }

        public List<Product> GetAll()
        {
            return _context.Product.Include(x => x.Customer).Include(b => b.Project).Include(c => c.Type).Include(d => d.Parent).ToList();
        }

        public Product GetById(long id)
        {
            return _context.Product.FirstOrDefault(x => x.ID == id);
        }

        public Product GetByIdDetailed(long id)
        {
            return _context.Product.Include(a => a.Customer).Include(b => b.Type).Include(c => c.Project).Include(d => d.Parent).Include
                (e => e.ProductAttributes).Include("ProductAttributes.Attribute").Include("ProductHistories.History").Include("ProductHistories.File")
                .Include(f => f.ProductHistories)
                .Include(g => g.StockInformation)
                .FirstOrDefault(x => x.ID == id);
        }

        public Product GetByIdForDelete(long id)
        {
            return _context.Product.Include(x => x.Customer).Include(y => y.Parent).Include(j => j.ProductAttributes).FirstOrDefault(x => x.ID == id);
        }
        public Product GetByIdWithAttributes(long id)
        {
            return _context.Product.Include(x => x.ProductAttributes).FirstOrDefault(x => x.ID == id);
        }
        public List<Product> getByType(long typeID)
        {
            return _context.Product.Where(x => x.DeviceTypeID == typeID).ToList();
        }
        public List<Product> getAvaibleProducts()
        {
            return _context.Product.Where(x => x.ParentID == null).Include(b=>b.Type).ToList();
        }
        public List<Product> getChilds(long id)
        {
            Product product = _context.Product.Include(b => b.Parent).FirstOrDefault(x => x.ID == id);
          /*  while()
            if (product.Parent != null) {
                Product parent = _context.Product.Include(a => a.Parent).FirstOrDefault(c => c.Parent == product.ParentID);
                    }
                    */
         
            return _context.Product.Where(x => x.ParentID == id).Include(y=>y.Type).ToList();
        }

        public  List<Product> getParents(Product product, List<Product> parents)
        {
            Product parent = new Product();
            if(product.ParentID != null)
            {
                parents.Add(_context.Product.Include(b=>b.Parent).FirstOrDefault(c => c.ParentID==product.ParentID));
                return getParents(product.Parent, parents);
            }

            return parents;            
        }

        




        public void productTree()
        {

        }


        public void Insert(Product product)
        {
            _context.Product.Add(product);
        }


        public void Update(Product product)
        {
            _context.Product.Update(product);
        }

        public List<Product> getProductsByType(long typeID)
        {
            return _context.Product.Include(b => b.ProductAttributes).Where(x => x.DeviceTypeID == typeID).ToList();
        }

        public List<Product> getProductByCustomers(long customerID)
        {
            return _context.Product.Where(x => x.CustomerID == customerID).Include(y => y.ProductAttributes).ToList();
        }

        public List<Product> getAllProductsHistory()
        {
            return _context.Product.Include(x => x.ProductHistories).Include("ProductHistories.History").ToList();
        }

        public Product getProductWithHistory(long id)
        {
            return _context.Product.Include(x => x.ProductHistories).Include("ProductHistories.History").FirstOrDefault(y => y.ID == id);
        }

    


  
        
    }
}
