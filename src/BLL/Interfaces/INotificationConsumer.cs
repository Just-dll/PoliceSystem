using AngularApp1.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface INotificationConsumer
    {
        Task<string?> Subscribe();
        User UserConsumer { get; set; }
    }
}
