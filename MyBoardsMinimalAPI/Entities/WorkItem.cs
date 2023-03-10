using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBoardsMinimalAPI.Entities
{
    public class Epic : WorkItem
    {
        // Epic
        public DateTime? StartDate { get; set; }
        //[Precision(3)]
        public DateTime? EndDate { get; set; }
    }
    public class Issue : WorkItem
    {
        // Issue
        //[Column(TypeName = "decimal(5,2)")]
        public decimal Efford { get; set; }
    }
    public class Task : WorkItem
    {
        // Task
        //[MaxLength(200)]
        public string Activity { get; set; }
        //[Precision(14,2)]
        public decimal RemainingWork { get; set; }
    }
    public abstract class WorkItem
    {
        public int Id { get; set; }
        //[Required]
        //public string State { get; set; }
        //[Column(TypeName = "varchar(200)")]
        public string Area { get; set; }
        //[Column("Iteration_Path")]
        public string IterationPath { get; set; }
        public int Priority { get; set; }

        
        //relation one to many (One WorkItem can have many Comments)
        public virtual List<Comment> Comments { get; set; } = new List<Comment>(); // can be IEnumerable

        //relation one to Many (One User has relation with many WorkItems)
        public virtual User Author { get; set; }
        public Guid AuthorId { get; set; }

        //relation many to many before .Net5
        //public List<WorkItemTag> WorkItemTags { get; set; } = new List<WorkItemTag>();

        //relation many to many after .Net5
        public virtual List<Tag> Tags { get; set; }

        //relation one to many (State)
        public virtual State State { get; set; }
        public int StateId { get; set; }
    }
}
