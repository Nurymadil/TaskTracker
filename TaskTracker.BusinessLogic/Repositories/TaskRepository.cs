using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskTracker.BusinessLogic.Interfaces;
using TaskTracker.DataAccess.Data;
using ProjectTask = TaskTracker.DataAccess.Models.ProjectTask;

namespace TaskTracker.BusinessLogic.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _dataContext;
        public TaskRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
      
        public async Task<bool> Add(ProjectTask entity)
        {
            try
            {
                _dataContext.Tasks.Add(entity);
                return (await _dataContext.SaveChangesAsync()) > 0;
            }
            catch(Exception ex) { 
                return false; }
        }
     

        public async Task<IEnumerable<ProjectTask>> All()
        {
            return await _dataContext.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<ProjectTask>> Find(Expression<Func<ProjectTask, bool>> predicate)
        {
            return await _dataContext.Tasks.Where(predicate).ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var task = _dataContext.Tasks.Find(id);
            if (task == null) return false;

            _dataContext.Tasks.Remove(task);
            return (await _dataContext.SaveChangesAsync()) > 0;
        }

       

        public async Task<ProjectTask> GetById(int id)
        {
            return await _dataContext.Tasks.FindAsync(id);
        }

        public async Task<bool> Update(ProjectTask entity)
        {
            try
            {
                var task= await _dataContext.Tasks.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if(task == null) return false;
                //_dataContext.Entry(task).CurrentValues.SetValues(entity);
                task.Name=entity.Name;
                task.Description=entity.Description;
                task.TaskStatus=entity.TaskStatus;
                task.Priority=entity.Priority;


                _dataContext.Update(task);
                //_dataContext.Entry(task).CurrentValues.SetValues(entity);
                return (await _dataContext.SaveChangesAsync()) > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateStatus(int id, TaskTracker.DataAccess.Models.TaskStatus status)
        {
            try
            {
                var entity = _dataContext.Tasks.Find(id);
                if (entity == null) return false;

                entity.TaskStatus = status;

                _dataContext.Entry(await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == entity.Id)).CurrentValues.SetValues(entity);
                return (await _dataContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
