using Application.Commands;
using AutoMapper;
using Azul.Framework.Events;
using Azul.Framework.Events.Adapters.AzureServiceBus;
using Azul.Framework.Events.Configuration;
using Azul.Framework.Events.Models;
using Infrastructure.Subscribers.Interfaces;
using Infrastructure.Subscribers.Messages;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Subscribers.Subscribers
{
    public class PartNumberSubscriber : EventSubscriber<PartNumberMessage>, IPartNumberSubscriber
    {
        public PartNumberSubscriber(IMediator mediator,
                                    ILoggerFactory loggerFactory,
                                    IMapper mapper
                                    ) : base(mediator, loggerFactory, mapper,
                                        new EventCustomSettings
                                        {
                                            AzureServiceBusSettings = new AzureServiceBusSettings
                                            {
                                                AutoComplete = bool.Parse(Settings.Parameters.FirstOrDefault(p => p.Key == "AutoComplete").Value.ToString()),
                                                MaxConcurrentCalls = int.Parse(Settings.Parameters.FirstOrDefault(p => p.Key == "MaxConcurrentCalls").Value.ToString())
                                            }
                                        })
        {
        }

        private static EventSetting Settings => EventConfiguration.Settings.EventSetting.FirstOrDefault(e => e.Id == nameof(PartNumberSubscriber));
        public override string TopicName => Settings.Parameters.FirstOrDefault(p => p.Key == "Topic").Value.ToString();

        public override string SubscriptionName => Settings.Parameters.FirstOrDefault(p => p.Key == "Subscription").Value.ToString();

        public override string ConnectionId => nameof(PartNumberSubscriber);

        public override async Task<ProcessedMessageResponse> ConsumeAsync(PartNumberMessage message, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            LogService.LogInformation($"Consuming message {JsonConvert.SerializeObject(message)}");
            await MediatorService.Send(MapperService.Map<PartNumberCommand>(message));
            return new ProcessedMessageResponse(true);
        }
    }
}
