using System;
using NexTech_Assessment_API.Repositories;

namespace NextTech_Assessment_Xunit
{
    public class StoryTestService
    {
        private readonly StoryRepository _repository;

        public StoryTestService(StoryRepository repository)
        {
            _repository = repository;
        }


    }
}
