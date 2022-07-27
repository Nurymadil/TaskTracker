using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskTracker.BusinessLogic.Interfaces;
using TaskTracker.DataAccess.Models;

namespace TaskTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private ITaskRepository taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await taskRepository.All();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await taskRepository.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectTask task)
        {
            var result = await taskRepository.Add(task);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProjectTask task)
        {
            var result = await taskRepository.Update(task);
            return Ok(result);
        }
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int id, DataAccess.Models.TaskStatus status)
        {
            var result = await taskRepository.UpdateStatus(id, status);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await taskRepository.Delete(id);
            return Ok(result);
        }
    }
}
