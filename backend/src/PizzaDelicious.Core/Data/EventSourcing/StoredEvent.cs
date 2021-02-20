using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        public StoredEvent(Guid id, string type, DateTime occurrenceDate, string data)
        {
            Id = id;
            Type = type;
            OccurrenceDate = occurrenceDate;
            Data = data;
        }

        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public DateTime OccurrenceDate { get; private set; }
        public string Data { get; private set; }
    }
}
