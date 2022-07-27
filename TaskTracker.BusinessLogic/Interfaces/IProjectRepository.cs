using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskTracker.DataAccess.Models;

namespace TaskTracker.BusinessLogic.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> All();
        Task<Project> GetById(int id);
        Task<bool> Add(Project entity);
        Task<bool> Delete(int id);
        Task<bool> Update(Project entity);
        Task<IEnumerable<Project>> Find(Expression<Func<Project, bool>> predicate);
        Task<bool> UpdateStatus(int id, ProjectStatus status);
        Task<IEnumerable<ProjectTask>> AllTasks(int id);
        Task<bool> AddTasks(int id,IEnumerable<ProjectTask> entities);
        Task<bool> RemoveTasks(int id, IEnumerable<ProjectTask> entities);
    }
}

