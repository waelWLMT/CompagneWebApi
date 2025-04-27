using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBillingReportService
    {
        byte[] GenerateCampaignDevisReport(int devisId);

        byte[] GenerateBillingReport(int id, bool isBill);
    }
}
