using BL.ServicePattern;
using Core.Models;
using Data.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Impl
{
    public class PhotoService: ServicePattern<Photo>, IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IConfiguration _configuration;

        public PhotoService(IPhotoRepository photoRepo, IConfiguration configuration): base(photoRepo)
        {
            _photoRepository = photoRepo;
            _configuration = configuration;
        }

        public string GetFilesFolder(int campaignId, int businessId)
        {
            var campaignFolderName = campaignId + _configuration.GetSection("SuffixCampaignPhotosFolderName").Value.ToString();
            var businessFolderName = businessId + _configuration.GetSection("SuffixbusinessFolderName").Value.ToString();

            var filesFolderName = campaignFolderName + "/" + businessFolderName + "/";

            return filesFolderName;
        }

        public void AddListPhotos(CampaignBusiness campaignBusiness, List<string> photosNames)
        {
            var fileFolder = GetFilesFolder(campaignBusiness.Campaign.Id, campaignBusiness.CampaignBusinessId);
            
            // add list campaignBusiness photos
            foreach(var name in photosNames)
            {
                var photo = new Photo()
                {
                    ImageName = name,
                    CampaignBusiness = campaignBusiness,
                    FileFolder = fileFolder
                };

                base.Insert(photo);
                Commit();
            }           
        }

        public List<Photo> GetPhotosByBusiness(int businessId)
        {
            var list = _photoRepository.GetPhotoByBusiness(businessId);

            return list;
        }
    }
}
