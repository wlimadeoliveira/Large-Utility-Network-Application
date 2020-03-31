using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angle.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Angle.Controllers
{
    public class LoggingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public static void writeLog(dynamic model, string user, string action, string controller)
        {
            var logWriter = new System.IO.StreamWriter("log.txt",true);
          


            string entity = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            Logs log = new Logs()
            {
                Date = DateTime.Now.ToString(),
                User = user,
                Action = action,
                Controller = controller,
                Entity = entity
            };


            string message = JsonConvert.SerializeObject(log);

           

          
            logWriter.WriteLine(message);
            logWriter.Dispose();
        }

    }
}