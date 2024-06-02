using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularApp1.EventBus
{
    public interface IEventBus
    {
        Task Publish(string queueName, string message);
        Task<IEnumerable<string>> GetMessages(string queueName);
        Task ConnectPerson(string personId, string exchange);
    }
}
