namespace WHATEVER_ZAPROS.Models
{
    public class ViewModel
    {
        public int? IdArticle { get; set; }
        public string? Header { get; set; } = null!;
        public string? Text { get; set; } = null!;
        public DateTime? DateTimeArticle { get; set; }
        public int? LanguageProgrammingId { get; set; }
        public int? StatusArticleId { get; set; }
        public int? UserId { get; set; }
        public IEnumerable<LanguageProgramming?> language { get; set; }
        public int? IdLanguageProgramming { get; set; }
        public string? LanguageProgramming1 { get; set; } = null!;
    }
}