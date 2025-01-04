using ResearchWebApp.Models;
using System.Threading.Tasks;

namespace ResearchWebApp.Services
{
    public class SubjectStateService
    {
        private readonly ISubjectService _subjectService;

        // Property to store the current subject Id
        public int? CurrentSubjectId { get; private set; }

        // Constructor to inject ISubjectService
        public SubjectStateService(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // Method to set the subject Id
        public void SetCurrentSubjectId(int id)
        {
            CurrentSubjectId = id;
        }

        // Method to get the subject Id
        public int? GetCurrentSubjectId()
        {
            return CurrentSubjectId;
        }

        // Method to get the subject by Id
        public async Task<Subject> GetCurrentSubjectAsync()
        {
            if (CurrentSubjectId.HasValue)
            {
                return await _subjectService.GetSubjectsByIdAsync(CurrentSubjectId.Value); // Fetch subject by ID
            }

            return null; // Return null if no subject Id is set
        }

        // Optionally, you could have a method to clear the Id when navigating away
        public void ClearCurrentSubjectId()
        {
            CurrentSubjectId = null;
        }
    }
}
