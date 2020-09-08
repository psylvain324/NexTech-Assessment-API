using System.Collections.Generic;
using System.Threading.Tasks;
using TechAssessment.Models;

namespace TechAssessment.Interfaces
{
    public interface IStoryService
    {
        Task<List<string>> GetAllIdsAsync();
        Task<Story> GetStoryById(string id);
        Task<List<Story>> GetNewestStories();
        Task<IEnumerable<Story>> GetStoriesInParallelFixed();
        Task<List<Story>> GetStoryByNumberAndSize(int pageNumber, int pageSize);
    }
}
