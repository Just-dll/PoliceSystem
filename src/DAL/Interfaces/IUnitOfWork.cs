using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface IUnitOfWork
{
    ICaseFileRepository CaseFileRepository { get; }
    ITicketRepository TicketRepository { get; }
    IReportRepository ReportRepository { get; }
    IDrivingLicenseRepository DrivingLicenseRepository { get; }
    void SaveChangesAsync();
}