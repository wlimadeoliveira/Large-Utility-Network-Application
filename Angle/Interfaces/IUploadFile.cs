using Angle.Models.Models;
using LUNA.Models;
using LUNA.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Interfaces
{
    public interface IUpload
    {
        List<Upload> GetAll();
        Upload GetById(long id);
        void Insert(Upload file);
        void Update(Upload file);
        void Delete(Upload file);
        List<Upload> GetFilesByProductID(long productID);
    }
}
