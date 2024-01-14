using BL.ServicePattern;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public interface IPhotoService: IServicePattern<Photo>
    {
        public void AddListPhotos(CampaignBusiness campaignBusiness, List<string> photosNames);
        public List<Photo> GetPhotosByBusiness(int businessId);
        public string GetFilesFolder(int campaignId, int businessId);
    }
}
