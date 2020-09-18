using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NexTechAssessmentAPI.Data;
using NexTechAssessmentAPI.Interfaces;
using NexTechAssessmentAPI.Models;

namespace NexTechAssessmentAPI.Repositories
{
    public class StoryRepository : IRepository<Story>
	{
		private readonly DatabaseContext _context;
		private readonly ILogger<StoryRepository> _logger;

        public StoryRepository(DatabaseContext context, ILogger<StoryRepository> logger)
        {
			_context = context;
			_logger = logger;
        }

        public StoryRepository(DatabaseContext context)
		{
			_context = context;
		}

		public bool Add(Story story)
		{
			try
			{
				_context.TestStories.Add(story);
				_context.SaveChanges();
				return true;
			}
            catch
			{
				_logger.LogError("Failed to add Story!");
				throw;
			}
		}

		public bool Delete(Story Item)
		{
			try
			{
                Story employee = GetById(Item.Id);
				if (employee != null)
				{
					_context.TestStories.Remove(Item);
					_context.SaveChanges();
					return true;
				}
				return false;
			}
			catch
			{
				_logger.LogError("Unable to delete Story!");
				throw;
			}
		}

		public bool Edit(Story item)
		{
			try
			{
				_context.TestStories.Update(item);
				_context.SaveChanges();
				return true;
			}
			catch
			{
				_logger.LogError("Unable to save Story!");
				throw;
			}
		}

		public Story GetById(int id)
		{
			if (_context.TestStories.Any(x => x.Id == id))
			{
				return _context.TestStories.First(x => x.Id == id);
			}
			return null;
		}

		public IEnumerable<Story> GetAll()
		{
			return _context.TestStories;
		}

		public bool Exists(int id)
		{
			return _context.TestStories.SingleOrDefault(e => e.Id == id) != null;
		}

    }

}