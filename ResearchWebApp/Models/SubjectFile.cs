namespace ResearchWebApp.Models
{
    public class SubjectFile
    {
        public int Id { get; set; }    
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } 
       
        public string FilePath { get; set; }        
        public string FileName { get; set; }    
        public DateTime DateUploaded { get; set; }
        public ICollection<RelatedLiterature> RelatedLiteratures { get; set; }
    }
}
