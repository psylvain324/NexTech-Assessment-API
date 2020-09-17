using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NexTech_Assessment_API.Data;
using NexTech_Assessment_API.Interfaces;
using NexTech_Assessment_API.Models;

namespace NexTech_Assessment_API.Repositories
{
	public class StoryRepository : IRepository<Story>
	{
		private DatabaseContext _context;
		private readonly ILogger<StoryRepository> _logger;

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
			catch (Exception ex)
			{
				_logger.LogError("Failed to add Story: " + ex.Message);
				return false;
			}
		}

		public bool Delete(Story Item)
		{
			try
			{
				Story employee = Get(Item.Id);
				if (employee != null)
				{
					_context.TestStories.Remove(Item);
					_context.SaveChanges();
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to delete Story: " + ex.Message);
				return false;
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
			catch (Exception ex)
			{
				_logger.LogError("Unable to save Story: " + ex.Message);
			}
			return false;
		}

		public Story Get(int id)
		{
			if (_context.TestStories.Count(x => x.Id == id) > 0)
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