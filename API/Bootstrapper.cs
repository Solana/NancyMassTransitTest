using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using MassTransit;
using System.Configuration;

namespace API
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        string MessageQueueServer = ConfigurationManager.AppSettings["MessageQueueServer"];
        string username = ConfigurationManager.AppSettings["MessageQueueUsername"];
        string password = ConfigurationManager.AppSettings["MessageQueuePassword"];
        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var uri = new Uri(string.Format("rabbitmq://{0}/", MessageQueueServer));

            IBusControl bus = null;
                bus = MassTransit.Bus.Factory.CreateUsingRabbitMq(x =>
                {
                    x.Host(uri, h =>
                    {
                        h.Username(username);
                        h.Password(password);
                        h.Heartbeat(5);
                    });
                        
                    x.Durable = true;
                });
                
            bus.StartAsync().Wait();
            
        }
        

    }
}