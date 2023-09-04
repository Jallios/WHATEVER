using System;
using System.Collections.Generic;

namespace WHATEVER_ZAPROS.Models
{
    public partial class HistoryArticle
    {
        public int? IdHistoryArticle { get; set; }
        public string? HeaderHistoryArticle { get; set; } = null!;
        public string? TextHistoryArticle { get; set; } = null!;
        public DateTime? DateTimeArticleHistoryArticle { get; set; }
        public string? LanguageProgrammingHistoryArticle { get; set; } = null!;
        public string? LoginHistoryArticle { get; set; } = null!;
        public DateTime? DateTimeHistoryArticle { get; set; }
        public string? Action { get; set; } = null!;
    }
}
