using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.BusinessLogic.Interfaces;
using TaskTracker.DataAccess.Models;

namespace TaskTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private IProjectRepository projectRepository ;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
          var list= await projectRepository.All();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await projectRepository.GetById(id);
            return Ok(entity);
        }
       [HttpGet("GetTasks")]
        public async Task<IActionResult> GetTasks(int id)
        {
            var entity = await projectRepository.AllTasks(id);
            return Ok(entity);
        }
        [HttpPost("AddTasks")]
        public async Task<IActionResult> AddTasks(int id, IEnumerable<ProjectTask> tasks)
        {
            var result = await projectRepository.AddTasks(id, tasks);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            var result=await projectRepository.Add(project);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Project project)
        {
            var result = await projectRepository.Update(project);
            return Ok(result);
        }
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int id,ProjectStatus status)
        {
            var result = await projectRepository.UpdateStatus(id,status);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await projectRepository.Delete(id);
            return Ok(result);
        }
        [HttpDelete("DeleteTasks")]
        public async Task<IActionResult> Delete(int id, IEnumerable<ProjectTask>  tasks)
        {
            var result = await projectRepository.RemoveTasks(id,tasks);
            return Ok(result);
        }
    }
}
