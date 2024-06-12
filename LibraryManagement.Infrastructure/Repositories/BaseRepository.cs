using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BaseRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly LibraryDbContext Context;
        protected readonly DbSet<T> DbSet;

        public BaseRepository(LibraryDbContext libraryDbContext)
        {
            Context = libraryDbContext;
            DbSet = Context.Set<T>();
        }

        public async Task<T> AddAsync(T model)
        {
            await DbSet.AddAsync(model);
            await Context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await Query().AnyAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Query().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<IQueryable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Query();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Where(x => !x.IsDeleted).Include(includeProperty);
            }

            return query;
        }

        public async Task<T> GetByIdIncludingAsync(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Query();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<T> GetOneAsync(Guid id)
        {
            return await Query().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task InactivateAsync(Guid id)
        {
            var model = await DbSet.FindAsync(id);
            if (model != null)
            {
                model.Inactivate();
                DbSet.Update(model);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<T> UpdateAsync(T model)
        {
            var entityToUpdate = DbSet.Find(model.Id);
            var entry = Context.Entry(entityToUpdate);
            entry.CurrentValues.SetValues(model);
            await Context.SaveChangesAsync();
            return model;
        }

        protected IQueryable<T> Query()
        {
            return DbSet.AsNoTrackingWithIdentityResolution();
        }
    }
}
