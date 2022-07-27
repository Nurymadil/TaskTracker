using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskTracker.DataAccess.Models;
using TaskStatus = TaskTracker.DataAccess.Models.TaskStatus;

namespace TaskTracker.BusinessLogic.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ProjectTask>> All();
        Task<ProjectTask> GetById(int id);
        Task<bool> Add(ProjectTask entity);
        Task<bool> Delete(int id);
        Task<bool> Update(ProjectTask entity);
        Task<IEnumerable<ProjectTask>> Find(Expression<Func<ProjectTask, bool>> predicate);
        Task<bool> UpdateStatus(int id,TaskStatus status);
    }
}

