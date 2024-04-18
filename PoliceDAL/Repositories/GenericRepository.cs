using AngularApp1.Server.Data;
using Microsoft.EntityFrameworkCore;
using PoliceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> dbSet;
        
        public GenericRepository(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
        }

        public GenericRepository(PolicedatabaseContext context)
        {
            dbSet = context.Set<T>();
        }
    }
}
