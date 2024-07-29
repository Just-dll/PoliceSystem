using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories;

public class ReportRepository : Repository<Report>, IReportRepository
{
    public ReportRepository(PolicedatabaseContext context) : base(context)
    {
    }
}