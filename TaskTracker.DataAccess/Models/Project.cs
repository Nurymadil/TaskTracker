using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TaskTracker.DataAccess.Models
{
    public class Project:Entity
    {
        public string Title { get;set;}
        public virtual List<ProjectTask> Tasks { get;set;}
        public int Priority { get;set;}
        public ProjectStatus ProjectStatus { get;set;}
        public DateTime? StartDate { get;set;}
        public DateTime? CompletationDate { get;set;}
    }
    public enum ProjectStatus { NotStarted, Active, Completed }
}
