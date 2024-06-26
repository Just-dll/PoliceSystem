using AngularApp1.Server.Data;
using PoliceDAL.Interfaces;
using System.Linq.Expressions;

namespace PoliceDAL.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(PolicedatabaseContext context) : base(context)
        {
        }
    }
}