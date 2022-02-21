using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService
{
    public interface IStoryService
    {
        /// <summary>
        /// Add a story to the database.
        /// </summary>
        /// <param name="story"></param>
        /// <returns></returns>
        Story CreateStory(Story story);

        /// <summary>
        /// Get a story by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Story GetStory(int id);

        /// <summary>
        /// Get all stories, with an optional filter for paging purposes.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<Story> GetAllStories(Filter filter = null);

        /// <summary>
        /// Update a story with new information.
        /// </summary>
        /// <param name="story"></param>
        /// <returns></returns>
        Story UpdateStory(Story story);

        /// <summary>
        /// Delete a story by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Story DeleteStory(int id);

    }
}
