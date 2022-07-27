namespace TaskTracker.DataAccess.Models
{
    public class ProjectTask:Entity
    {
        public string Name { get;set;}
        public string Description { get;set;}
        public int Priority { get; set; }
        public TaskStatus TaskStatus { get;set;}
        public int ProjectId { get;set;}
        public virtual Project Project { get;set;}  
    }
    public enum TaskStatus { ToDo, InProgress, Done }
}
