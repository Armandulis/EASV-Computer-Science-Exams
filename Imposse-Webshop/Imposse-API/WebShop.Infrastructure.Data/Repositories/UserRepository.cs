using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Core.DomainService;
using WebShop.Core.Entity;

namespace WebShop.Infrastructure.Data.Repositories
{
   
   public class UserRepository : IUserRepository
    {
        readonly WebShopContext _ctx;

        public UserRepository(WebShopContext context)
        {
            _ctx = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _ctx.Users.Include(user => user.Stories).ToList();
        }

        public User UserGet(int id)
        {
            return _ctx.Users.Include(user => user.Stories).Include(u => u.Reviews).FirstOrDefault(b => b.Id == id);
        }

        public User UserAdd(User entity)
        {
            _ctx.Attach(entity).State = EntityState.Added;

            entity.Basket = new Basket() { User = entity };
            _ctx.Entry(entity).Reference(u => u.Basket).IsModified = true;
            _ctx.SaveChanges();
            return entity;
        }

        public User UserEdit(User entity)
        {
            var updatedUser = _ctx.Users.Update(entity).Entity;
            
            _ctx.SaveChanges();
            return updatedUser;
        }

        public User UserRemove(int id)
        {
            //var storiesToDelete = _ctx.Story.Where(story => story.User.Id == id);
            //_ctx.RemoveRange(storiesToDelete);
            var item = _ctx.Users.FirstOrDefault(b => b.Id == id);
            _ctx.Users.Remove(item);
            _ctx.SaveChanges();
            return item;
        }

    }
}
