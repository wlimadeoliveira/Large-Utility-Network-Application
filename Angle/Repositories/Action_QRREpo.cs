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
    public class Action_QRRepo : IAction_QR
    {
        private readonly ProjectDataContext _context;
        /// <summary>
        /// Contructor receives and FrameworkElement.DataContext object and refences it to classvariable "_context"
        /// </summary>
        /// <param name="context">FrameworkElement.DataContext</param>
        public Action_QRRepo(ProjectDataContext context)
        {
            _context = context;
        }
     
 

        List<Action_QR> IAction_QR.GetAll()
        {
            return _context.Action_QR.ToList();
        }

        Action_QR IAction_QR.GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Action_QR action_QR)
        {
            throw new NotImplementedException();
        }

        public void Update(Action_QR action_QR)
        {
            throw new NotImplementedException();
        }

        public void Delete(Action_QR action_QR)
        {
            throw new NotImplementedException();
        }
    }
}
