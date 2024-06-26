using PoliceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceProject.BLL.Interfaces
{
    public interface ICache<T> where T : BaseEntity
    {
        Task<T> Get(int id);
        Task Set(T entity);
        void Delete(int id);
    }
}
