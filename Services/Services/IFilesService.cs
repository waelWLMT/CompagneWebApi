using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IFilesService
    {
        public string CreateCampaignBusinessFilesDirectoryIfNotExist(int campaignId, int businessId);
        public string UploadFile(string path, IFormFile file);
        public List<string> UploadListFiles(List<IFormFile> files, string path);
        string GetBusinessPhotosFolderPath(int campaignId, int businessId);
    }
}
