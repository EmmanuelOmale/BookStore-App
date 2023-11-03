using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQService.MessageModels
{
    public interface IRabbitMQProducer 
    {
        void SendOrderMessage<T>(T message);
    }
}
