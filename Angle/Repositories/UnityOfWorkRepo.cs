using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Interfaces;
using Angle.Interfaces.Ivy;
using Angle.Repositories.Ivy;
using LUNA.Models.Models;

namespace Angle.Repositories
{
    public class UnityOfWorkRepo : IUnityOfWork
    {
        private readonly ProjectDataContext _context;
        private ProductRepo _productRepo;
        private AttributeRepo _attributeRepo;
        private TypeRepo _typeRepo;
        private FeatureRepo _featureRepo;
        private PersonRepo _personRepo;
        private StockInformationRepo _stockInformationRepo;
        private ProjectRepo _projectRepo;
        private CustomerRepo _customerRepo;
        private ManufacturerRepo _manufacturerRepo;
        private SupplierRepo _supplierRepo;
        private CategoryRepo _categoryRepo;
        private LocationRepo _locationRepo;
        private ArticleRepo _articleRepo;
        private HistoryRepo _historyRepo;

        public UnityOfWorkRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public IProduct Product
        {
            get
            {
                return _productRepo = _productRepo ?? new ProductRepo(_context);
            }
        }

        public IAttribute Attribute
        {
            get
            {
                return _attributeRepo = _attributeRepo ?? new AttributeRepo(_context);
            }
        }

        public IType Type
        {
            get
            {
                return _typeRepo = _typeRepo ?? new TypeRepo(_context);
            }
        }

        public IFeature Feature
        {
            get
            {
                return _featureRepo = _featureRepo ?? new FeatureRepo(_context);
            }
        }

        public ICustomer Customer
        {
            get
            {
                return _customerRepo = _customerRepo ?? new CustomerRepo(_context);
            }
        }

        public IPerson Person
        {
            get
            {
                return _personRepo = _personRepo ?? new PersonRepo(_context);
            }
        }

        public IProject Project
        {
            get
            {
                return _projectRepo = _projectRepo ?? new ProjectRepo(_context);
            }
        }

        public IStockInformation StockInformation
        {
            get
            {
                return _stockInformationRepo = _stockInformationRepo ?? new StockInformationRepo(_context);
            }
        }


        public IManufacturer Manufacturer
        {
            get
            {
                return _manufacturerRepo = _manufacturerRepo ?? new ManufacturerRepo(_context);
            }
        }
        public ISupplier Supplier
        {
            get
            {
                return _supplierRepo = _supplierRepo ?? new SupplierRepo(_context);
            }
        }

        public ICategory Category
        {
            get
            {
                return _categoryRepo = _categoryRepo ?? new CategoryRepo(_context);
            }
        }
        public ILocation Location
        {
            get
            {
                return _locationRepo = _locationRepo ?? new LocationRepo(_context);
            }
        }

        public IArticle Article
        {
            get
            {
                return _articleRepo = _articleRepo ?? new ArticleRepo(_context);
            }
        }

        public IHistory History
        {
            get
            {
                return _historyRepo = _historyRepo ?? new HistoryRepo(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
