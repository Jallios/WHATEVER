using System;
using System.Collections.Generic;

namespace WHATEVER_ZAPROS.Models
{
    public partial class Favorite
    {
        public int? IdFavorites { get; set; }
        public int? UserId { get; set; }
        public int? ArticleId { get; set; }

        public Article? article { get; set; }

        public User? user { get; set; }
    }
}
