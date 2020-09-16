using System.Collections.Generic;
using System.Threading.Tasks;
using NexTech_Assessment_API.Models;

namespace NexTech_Assessment_API.Interfaces
{
    public interface IStoryService
    {
        Task<List<string>> GetAllIdsAsync();
        Task<Story> GetStoryById(string id);
        Task<List<Story>> GetNewestStories();
        PagedList<Story> GetNewestStoriesPagedList(PagingParams pagingParams);
        Task<IEnumerable<Story>> GetStoriesInParallelFixed();
        IEnumerable<Story> GetStoriesByFieldSearch(string field, string search, IEnumerable<Story> stories);
    }
}
