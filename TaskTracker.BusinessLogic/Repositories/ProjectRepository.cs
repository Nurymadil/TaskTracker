using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskTracker.BusinessLogic.Interfaces;
using TaskTracker.DataAccess.Data;
using TaskTracker.DataAccess.Models;

namespace TaskTracker.BusinessLogic.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _dataContext;
        public ProjectRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Add(Project entity)
        {
            try
            {
                _dataContext.Projects.Add(entity);
                return (await _dataContext.SaveChangesAsync()) > 0;
            }
            catch { return false; }
        }

        public async Task<bool> AddTasks(int id, IEnumerable<ProjectTask> entities)
        {
            try
            {
                var list = _dataContext.Projects.ToList();
                var entity = _dataContext.Projects.Find(id);
                if (entity == null) return false;
                entity.Tasks.AddRange(entities);
                return (await _dataContext.SaveChangesAsync()) > 0;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Project>> All()
        {
            return await _dataContext.Projects.ToListAsync();
        }

        public async Task<IEnumerable<ProjectTask>> AllTasks(int id)
        {
            var entity = await _dataContext.Projects.Where(x => x.Id == id).Include(x => x.Tasks).AsNoTracking().FirstOrDefaultAsync();
            if (entity != null) return entity.Tasks.ToList();
            return new List<ProjectTask>();
        }

        public async Task<bool> Delete(int id)
        {
            var project = _dataContext.Projects.Find(id);
            if (project == null) return false;

            _dataContext.Projects.Remove(project);
            return (await _dataContext.SaveChangesAsync()) > 0;
        }

        public async Task<IEnumerable<Project>> Find(Expression<Func<Project, bool>> predicate)
        {
            return await _dataContext.Projects.Where(predicate).Include(x => x.Tasks).ToListAsync();
        }

        public async Task<Project> GetById(int id)
        {
            return await _dataContext.Projects.Include(x => x.Tasks).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveTasks(int id, IEnumerable<ProjectTask> entities)
        {
            try
            {
                var entity = await _dataContext.Projects.Where(x => x.Id == id).Include(x => x.Tasks).FirstOrDefaultAsync();
                if (entity == null) return false;
                foreach (var task in entities)
                {
                    entity.Tasks.Remove(task);
                }
                return (await _dataContext.SaveChangesAsync()) > 0;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Project entity)
        {
            try
            {
                _dataContext.Entry(await _dataContext.Projects.FirstOrDefaultAsync(x => x.Id == entity.Id)).CurrentValues.SetValues(entity);
                return (await _dataContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStatus(int id, ProjectStatus status)
        {
            try
            {
                var entity = _dataContext.Projects.Find(id);
                if (entity == null) return false;

                entity.ProjectStatus = status;

                _dataContext.Entry(await _dataContext.Projects.FirstOrDefaultAsync(x => x.Id == entity.Id)).CurrentValues.SetValues(entity);
                return (await _dataContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
