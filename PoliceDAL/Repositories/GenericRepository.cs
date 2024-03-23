using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Repositories
{
    internal class GenericRepository<T> : IRepository where T : class
    {
        protected readonly DbSet<T> dbSet;
    }
}
