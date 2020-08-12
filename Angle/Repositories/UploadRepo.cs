using Angle.Interfaces;
using Angle.Models.Models;
using LUNA.Models;
using LUNA.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Angle.Repositories
{
    /// <summary>
    /// In this class are done all CRUD-Operations
    /// Create = Insert(File object)
    /// Read = GetAll() and GetById(ProductId)
    /// Update = Update(File object)
    /// Delete = Detele(File object )
    /// </summary>
    public class UploadRepo : IUpload
    {
        private readonly ProjectDataContext _context;
        /// <summary>
        /// Contructor receives and FrameworkElement.DataContext object and refences it to classvariable "_context"
        /// </summary>
        /// <param name="context">FrameworkElement.DataContext</param>
        public UploadRepo(ProjectDataContext context)
        {
            _context = context;
        }

        public void Delete(Upload file)
        {
            throw new NotImplementedException();
        }

        public List<Upload> GetAll()
        {
            throw new NotImplementedException();
        }

        public Upload GetById(long id)
        {
            return _context.Upload.FirstOrDefault(x => x.ID == id);
        }

        public List<Upload> GetFilesByProductID(long productID)
        {
            return null;
        }

        public void Insert(Upload file)
        {
            _context.Upload.Add(file);
        }

        public void Update(Upload file)
        {
            throw new NotImplementedException();
        }
    }
}
