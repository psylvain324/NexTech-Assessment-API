using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NexTech_Assessment_API.Models;
using NexTechAssessmentAPI.Models;

namespace NexTech_Assessment_API.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsByStoryId(int id);
        Task<List<StoryCommentViewModel>> GetStoryCommentsViewModel();
    }
}
