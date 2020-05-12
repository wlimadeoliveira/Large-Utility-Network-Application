using Angle.Interfaces;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LUNA.Models;
using Angle.Models.Models;

namespace Angle.Repositories
{
    /// <summary>
    /// In this class are done all CRUD-Operations
    /// Create = Insert(Index_QR object)
    /// Read = GetAll() and GetById(ProductId)
    /// Update = Update(Index_QR object)
    /// Delete = Detele(Index_QRo bject )
    /// </summary>
    public class Controller_QRRepo : IController_QR
    {
        private readonly ProjectDataContext _context;
        /// <summary>
        /// Contructor receives and FrameworkElement.DataContext object and refences it to classvariable "_context"
        /// </summary>
        /// <param name="context">FrameworkElement.DataContext</param>
        public Controller_QRRepo(ProjectDataContext context)
        {
            _context = context;
        }
     
 

        public List<Controller_QR> GetAll()
        {
            return _context.Controller_QR.ToList();
        }

        public Controller_QR GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Controller_QR controller_QR)
        {
            throw new NotImplementedException();
        }

        public void Update(Controller_QR controller_QR)
        {
            throw new NotImplementedException();
        }

        public void Delete(Controller_QR controller_QR)
        {
            throw new NotImplementedException();
        }
    }
}
