using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    protected PolicedatabaseContext _context;
    public UnitOfWork(PolicedatabaseContext context)
    {
        _context = context;
    }

    public ICaseFileRepository CaseFileRepository => new CaseFileRepository(_context);
    public IReportRepository ReportRepository => new ReportRepository(_context);
    public ITicketRepository TicketRepository => new TicketRepository(_context);
    public IDrivingLicenseRepository DrivingLicenseRepository => new DrivingLicenseRepository(_context);

    public async void SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
