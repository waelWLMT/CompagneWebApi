using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services.Impl
{
    public class FilesService : IFilesService
    {
        private readonly IConfiguration _configuration;

        public FilesService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateCampaignBusinessFilesDirectoryIfNotExist(int campaignId, int businessId)
        {
            var parentDirectory = _configuration.GetSection("CampaignsFolder").Value;
            var suffixFolderName = _configuration.GetSection("SuffixCampaignPhotosFolderName").Value.ToString();
            
            
            var folderName = campaignId + suffixFolderName;

            var businessFolderName = businessId + _configuration.GetSection("SuffixbusinessFolderName").Value.ToString();
            var folderPath = Path.Combine(parentDirectory, folderName);

            // Determine whether the campaign directory exists.
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Determine whether the business directory exists.
            folderPath = Path.Combine(folderPath, businessFolderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;

        }

        public List<string> UploadListFiles(List<IFormFile> files, string path)
        {
            var listfileNames = new List<string>();
            foreach (var file in files)
            {
                var fileName = UploadFile(path, file);

                if (!String.IsNullOrEmpty(fileName))
                    listfileNames.Add(fileName);
            }

            return listfileNames;
        }

        public string UploadFile(string path, IFormFile file)
        {
            var filePath = Path.Combine(path, file.FileName);

            // if Exists delete
            if (File.Exists(filePath))
                File.Delete(filePath);

            file.CopyTo(new FileStream(filePath, FileMode.Create));

            return file.FileName;
        }

        public string GetBusinessPhotosFolderPath(int campaignId, int businessId)
        {
            // Get all campaigns Folder path
            var folderPath = "";                
            var campaignFolderName = campaignId + _configuration.GetSection("SuffixCampaignPhotosFolderName").Value;
            var businessPhotosFolderName = businessId + _configuration.GetSection("SuffixbusinessFolderName").Value;

            // get Campaign Folder path
            folderPath = Path.Combine(folderPath, campaignFolderName);

            // get BusinessFolderName
            folderPath = Path.Combine(folderPath, businessPhotosFolderName);

            return folderPath;
        }
    }
}
