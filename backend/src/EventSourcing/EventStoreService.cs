using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace EventSourcing
{
    public class EventStoreService : IEventStoreService
    {

        private readonly IEventStoreConnection _connection;

        public EventStoreService(IConfiguration configuration)
        {
            _connection = EventStoreConnection.Create(
                configuration.GetConnectionString("EventStoreConnection"));
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }
    }
}
