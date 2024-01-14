using BL.ServicePattern;
using Core.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Impl
{
    public class BusninessTypeService : ServicePattern<BusinessType>, IBusninessTypeService
    {
        private readonly IBusninessTypeRepository _businessTypeRepo;

        public BusninessTypeService(IBusninessTypeRepository businessTypeRepo): base(businessTypeRepo) 
        {
            this._businessTypeRepo = businessTypeRepo;
        }

        public ICollection<BusinessType> GetActivatedBusinessTypes()
        {
            return _businessTypeRepo.GetActivatedBusinessTypes();
        }
    }
}
