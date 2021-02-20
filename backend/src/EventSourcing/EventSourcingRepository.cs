using EventStore.ClientAPI;
using Newtonsoft.Json;
using PizzaDelicious.Core.Data.EventSourcing;
using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
        {
            var events = await _eventStoreService.GetConnection()
                .ReadStreamEventsForwardAsync(aggregateId.ToString(), 0, 500, false);

            var listEvents = new List<StoredEvent>();

            foreach(var resolvedEvent in events.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

                var eventItem = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded
                    );

                listEvents.Add(eventItem);
            }

            return listEvents.OrderBy(x => x.OccurrenceDate);
        }

        public async Task SaveEvent<TEvent>(TEvent events) where TEvent : Event
        {
            await _eventStoreService.GetConnection().AppendToStreamAsync(
               events.AggregateId.ToString(),
               ExpectedVersion.Any,
               FormateEvent(events)
                );
        }

        private static IEnumerable<EventData> FormateEvent<TEvent>(TEvent events) where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                events.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(events)),
                null
                );
        }
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}
