using Angle.Interfaces;
using LUNA.Models.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Angle.Repositories
{
    /// <summary>
    /// In this class are done all CRUD-Operations
    /// Create = Insert(Index_QR object)
    /// Read = GetAll() and GetById(ProductId)
    /// Update = Update(Index_QR object)
    /// Delete = Detele(Index_QRo bject )
    /// </summary>
    public class Index_QRRepo : IIndex_QR
    {
        private readonly ProjectDataContext _context;
        /// <summary>
        /// Contructor receives and FrameworkElement.DataContext object and refences it to classvariable "_context"
        /// </summary>
        /// <param name="context">FrameworkElement.DataContext</param>
        public Index_QRRepo(ProjectDataContext context)
        {
            _context = context;
        }
     
        /// <summary>
        /// It reads all element from the Index_QR table in database to an Object List
        /// </summary>
        /// <returns>Returns an List with all Index_QR entities as Index_QR Objects, and Includes Action_QR and Controller QR_Objects </returns>
        List<Index_QR> IIndex_QR.GetAll()
        {
            return _context.Index_QR.Include(x=>x.Action_QR).Include(y=>y.Controller_QR).Include(j => j.Product).ToList();
        }

        /// <summary>
        /// Read the Index_QR entitie from Database
        /// </summary>
        /// <param name="id">ProductID of required QR</param>
        /// <returns>Returns an Object with with Product required Index_QR entitie as Index_QR Object, and Includes Action_QR and Controller QR_Objects</returns>
        Index_QR IIndex_QR.GetByProductId(long id)
        {
            return _context.Index_QR.Include(x => x.Action_QR).Include(y => y.Controller_QR).Include(b => b.Product).FirstOrDefault(j => j.ProductID == id);
        }
        
        /// <summary>
        /// Creates an new Entitie in Index_QR Table with the passed Parameter Index_QR Object
        /// </summary>
        /// <param name="index_QR">Index_QR object passed from Controller and created from User in View (ProductDetails view)</param>
        public void Insert(Index_QR index_QR)
        {
            if (!QRExist(index_QR))
            {
                _context.Index_QR.Add(index_QR);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Update the Table Entitie in Database
        /// </summary>
        /// <param name="index_QR">Index_QR object received from Controller, managed in Index_QR Admin View by User</param>
        public void Update(Index_QR index_QR)
        {
            _context.Index_QR.Update(index_QR);
            _context.SaveChanges();
        }

        public bool QRExist(Index_QR index_QR)
        {
            var QR = _context.Index_QR.FirstOrDefault(d => d.ProductID == index_QR.Product.ID && d.ActionID == index_QR.Action_QR.ID && d.ControllerID == index_QR.Controller_QR.ID);
            if(QR != null)
            {
                return true;
            }
            return false;


        }

        /// <summary>
        /// Deletes Index_QR entitie from Database
        /// </summary>
        /// <param name="index_QR">Object that should deleted, , managed in Index_QR Admin View by User </param>
        public void Delete(Index_QR index_QR)
        {
            _context.Index_QR.Remove(index_QR);
            _context.SaveChanges();
        }


        public Index_QR GetById(long id)
        {
            return  _context.Index_QR.Include(b=>b.Product).Include(c=>c.Action_QR).Include(d=>d.Controller_QR).Include(e=>e.User).FirstOrDefault(x => x.Id == id);
         
        }
    }
}
