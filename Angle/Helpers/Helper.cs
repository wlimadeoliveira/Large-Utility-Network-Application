using Angle.Models.ViewModels.AttributeViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUNA.Models.Models;
using Attribute = LUNA.Models.Models.Attribute;
using Angle.Models.ViewModels.TypeViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Angle.Helpers
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<Attribute, AttributeViewModel>().ReverseMap();
            CreateMap<CreateAttributeViewModel, Attribute>();
            CreateMap<PType, TypeViewModel>().ReverseMap();
            CreateMap<TypeCreateViewModel, PType>();
        }





    }
    public class FileService
    {
        private readonly string path = Directory.GetCurrentDirectory();
        private readonly IWebHostEnvironment _env;
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        private string GetMimeType(string fileName)
        {
            // Make Sure Microsoft.AspNetCore.StaticFiles Nuget Package is installed
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        public async Task<FileContentResult> GetFile(string filename)
        {


            
                var filepath = Path.Combine($"{this._env.WebRootPath}\\uploads\\{filename}");

                var mimeType = this.GetMimeType(filename);

            byte[] fileBytes = null;

               if (File.Exists(filepath))
                {
                    fileBytes =  File.ReadAllBytes(filepath);
                }
                else
                {
                return null;
            }

                return new FileContentResult(fileBytes, mimeType)
                {
                   FileDownloadName = filename
                };
            

            /*var filepath = Path.Combine($"{path}\\wwwroot\\uploads\\{filename}");

              var mimeType = this.GetMimeType(filename);

              byte[] fileBytes =  null;

              if (File.Exists(filepath))
              {
                  fileBytes = File.ReadAllBytes(filepath);
              }
              else
              {

              }

              return new FileContentResult(fileBytes, mimeType)
              {
                  FileDownloadName = filename
              };*/
        }


    }
}
