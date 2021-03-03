using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetInfoServer.Models
{
    public class News
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
    }

    public class Article
    {
        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string URLToImage { get; set; }
        public string PublishedAt { get; set; }
        public string Content { get; set; }
    }

    public class Source
        {
        public string Id { get; set; }
        public string Name { get; set; }
        }
}
