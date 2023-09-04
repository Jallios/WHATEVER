using System;
using System.Collections.Generic;

namespace WHATEVER_ZAPROS.Models
{
    public partial class Article
    {
        public int? IdArticle { get; set; }
        public string? Header { get; set; } = null!;
        public string? Text { get; set; } = null!;
        public DateTime? DateTimeArticle { get; set; }
        public int? LanguageProgrammingId { get; set; }
        public int? StatusArticleId { get; set; }

        public int? UserId { get; set; }

        public StatusArticle? statusArticle { get; set; }

        public LanguageProgramming? language { get; set; }

        public User? user { get; set; }

    }
}
