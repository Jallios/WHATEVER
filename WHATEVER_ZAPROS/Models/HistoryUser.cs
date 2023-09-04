using System;
using System.Collections.Generic;

namespace WHATEVER_ZAPROS.Models
{
    public partial class HistoryUser
    {
        public int? IdHistoryUser { get; set; }
        public string? InfoUser { get; set; } = null!;
        public DateTime? DateTimeHistoryUser { get; set; }
        public string? Action { get; set; } = null!;
    }
}
