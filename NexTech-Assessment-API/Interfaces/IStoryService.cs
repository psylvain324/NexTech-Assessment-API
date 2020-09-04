using System.Collections.Generic;
using System.Threading.Tasks;
using TechAssessment.Models;

namespace TechAssessment.Interfaces
{
    public interface IStoryService
    {
        Task<List<string>> GetAllIdsAsync();
        Task<Story> GetStoryAsync(int id);
        Task<List<Story>> GetNewestStories();
        Task<PagedList<Story>> GetPaginatedNewestStories(PagingParams pagingParams);
    }
}
