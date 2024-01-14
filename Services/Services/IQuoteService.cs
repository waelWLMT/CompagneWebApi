using BL.ServicePattern;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services
{
    public interface IQuoteService : IServicePattern<Quote>
    {
        void CreateDevis(int campaignId);
        Quote InitialiseQuote(Campaign campaign);
        void SetProductLines(ref Quote devis, Campaign campaign);
        void SetBusinessTypesLines(ref Quote devis, Campaign campaign);
        List<Quote> GetQuotesByRoleUser(int userRoleId, int clientId);
        Quote GetQuoteFullDataById(int devisId);
        Quote GetQuoteByCamapignId(int campaignId);
    }
}
