using System.Collections.Generic;
using System.Threading.Tasks;
using NexTechAssessmentAPI.Models;

namespace NexTechAssessmentAPI.Interfaces
{
    public interface IStoryService
    {
        Task<List<string>> GetAllIdsAsync();
        Task<Story> GetStoryById(string id);
        Task<List<Story>> GetNewestStories();
        Task<PagedList<Story>> GetNewestStoriesPagedList(PagingParams pagingParams);
        Task<List<Story>> GetStoriesByIdList(List<string> storyIds);
        Task<IEnumerable<Story>> GetStoriesInParallelFixed();
        IEnumerable<Story> GetStoriesByFieldSearch(string field, string search, IEnumerable<Story> stories);
    }
}
