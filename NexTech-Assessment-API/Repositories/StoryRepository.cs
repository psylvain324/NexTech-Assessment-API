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
		private readonly IStoryService _service;
		private readonly ILogger _logger;

		public StoryRepository(DatabaseContext context, ILogger<StoryRepository> logger, IStoryService service)
		{
			_context = context;
			_logger = logger;
			_service = service;
		}

		public bool Add(Story item)
		{
			try
			{
				_context.StaticTestStories.Add(item);
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
					_context.StaticTestStories.Remove(Item);
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
				_context.StaticTestStories.Update(item);
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
			if (_context.StaticTestStories.Count(x => x.Id == id) > 0)
			{
				return _context.StaticTestStories.First(x => x.Id == id);
			}
			return null;
		}

		public Story Get(int? id)
		{
			if (id == null)
			{
				throw new ArgumentNullException();
			}
			if (_context.StaticTestStories.Count(x => x.Id == id) > 0)
			{
				return _context.StaticTestStories.FirstOrDefault(x => x.Id == id);
			}
			return null;
		}

		public IEnumerable<Story> GetAll()
		{
			return _context.StaticTestStories;
		}

		public bool Exists(int id)
		{
			return _context.StaticTestStories.SingleOrDefault(e => e.Id == id) != null;
		}

    }

}