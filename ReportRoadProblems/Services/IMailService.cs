using ReportRoadProblems.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportRoadProblems.Services
{
    public interface IMailService
    {
        void SendEmail(Report mailRequest);
    }
}
