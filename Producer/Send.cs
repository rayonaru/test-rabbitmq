using System;
using RabbitMQ.Client;
using System.Text;

namespace Producer
{
    class Send
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = "localhost", 
                UserName = "admin",
                Password = "admin"
            };
            
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "raru",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var message = "Hello World!";
                    
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "raru",
                        basicProperties: null,
                        body: body
                    );

                    Console.WriteLine($" [x] Sent {message}");
                }

                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
