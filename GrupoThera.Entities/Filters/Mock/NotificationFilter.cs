using System;

namespace GrupoThera.Entities.Filters
{
    public class NotificationFilter
    {
        public string username { get; set; }

        public long? active { get; set; }

        public DateTime date { get; set; }

        public int? page { get; set; }

        public int? pageSize { get; set; }
    }
}
