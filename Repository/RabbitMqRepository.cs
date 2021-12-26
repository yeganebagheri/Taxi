using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Contracts;
using Taxi.Models;

namespace Taxi.Repository
{
    public class RabbitMqRepository : IRabbitMqRepository
    {
        public void Producer(Trip trip)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //var message = string.Format(" {0} to {1} With price {2} Finished!", trip.Source, trip.Destination, trip.Cost);
                //var body = Encoding.UTF8.GetBytes(message);
                var json = JsonConvert.SerializeObject(trip);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

            }


        }

    }
}
