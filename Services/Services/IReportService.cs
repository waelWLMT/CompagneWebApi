using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IReportService
    {        
        public byte[] GenerateCampaignDevisReport(int devisId);
        public byte[] GenerateCampaignFactureReport(int billId);
    }
}
