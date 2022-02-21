using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Core.ApplicationService;
using WebShop.Core.ApplicationService.Services;
using WebShop.Core.Entity;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {

        readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        // GET: api/Story
        [HttpGet]
        public IEnumerable<Story> Get([FromQuery] Filter filter)
        {
            if (filter.CurrentPage <= 0 || filter.ItemsPerPage <= 0)
            {
                return _storyService.GetAllStories();
            }
            return _storyService.GetAllStories(filter);
        }

        // GET: api/Story/5
        
        [HttpGet("{id}", Name = "GetStory")]
        public ActionResult<Story> Get(int id)
        {

            if (id < 1) return BadRequest("Id must be greater then 0");
            else return _storyService.GetStory(id);
        }

        // POST: api/Story
        [Authorize]
        [HttpPost]
        public ActionResult<Story> Post([FromBody] Story story)
        {
            Story storythis = _storyService.CreateStory(story);

            if (story.Text != null && story.Text != "") return storythis;
            else return BadRequest("Story has to have Text in it");
        }

        // PUT: api/Story/5
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Story> Put(int id, [FromBody] Story story)
        {
            if (id != story.Id)
            {
                return BadRequest();
            }
            if (_storyService.UpdateStory(story) != null) return story;
            else return BadRequest("There is no story with such id");
        }

        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<Story> Delete(int id)
        {
            Story storyDeleted = _storyService.DeleteStory(id);
            if (storyDeleted != null) return storyDeleted;
            else return BadRequest("There is no story with such id");

        }
    }
}
