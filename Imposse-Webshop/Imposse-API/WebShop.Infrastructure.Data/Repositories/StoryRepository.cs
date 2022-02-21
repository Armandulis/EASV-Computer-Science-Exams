using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;

namespace WebShop.Infrastructure.Data.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        readonly WebShopContext _ctx;

        public StoryRepository(WebShopContext ctx)
        {
            _ctx = ctx;

        }

        public Story CreateStory(Story story)
        {
            var newStory = _ctx.Stories.Attach(story).Entity;
            _ctx.SaveChanges();
            return newStory;
        }

        public Story DeleteStory(int id)
        {
            
            var newStory = _ctx.Stories.Remove(GetStory(id)).Entity;
            _ctx.SaveChanges();
            return newStory;

        }

        public IEnumerable<Story> GetAllStories()
        {
            return _ctx.Stories.Include(story => story.User);     
        }

        public Story GetStory(int id)
        {
            return _ctx.Stories.Include(story => story.User).FirstOrDefault(story => story.Id == id);
        }

        public Story UpdateStory(Story story)
        {
            var updatedStory = _ctx.Stories.Update(story).Entity;
            _ctx.SaveChanges();
            return updatedStory;
        }
    }
}
