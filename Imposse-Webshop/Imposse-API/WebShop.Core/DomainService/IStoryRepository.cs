using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.DomainService
{
    public interface IStoryRepository
    {
        /// <summary>
        /// Add a new story to the database.
        /// </summary>
        /// <param name="story"></param>
        /// <returns></returns>
        Story CreateStory(Story story);

        /// <summary>
        /// Get a story from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Story GetStory(int id);

        /// <summary>
        /// Get all stories in the database
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<Story> GetAllStories();

        /// <summary>
        /// Update a story in the database.
        /// </summary>
        /// <param name="story"></param>
        /// <returns></returns>
        Story UpdateStory(Story story);

        /// <summary>
        /// Delete a story in the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Story DeleteStory(int id);


        
    }
}
