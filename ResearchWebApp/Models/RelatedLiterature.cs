namespace ResearchWebApp.Models
{
    public class RelatedLiterature
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public string Link { get; set; }
        public int SubjectFileId { get; set; } // Foreign key to SubjectFile
        public SubjectFile SubjectFile { get; set; } // Navigation property
    }

}
