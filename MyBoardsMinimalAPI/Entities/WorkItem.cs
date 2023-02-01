using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBoardsMinimalAPI.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }
        //[Required]
        public string State { get; set; }
        //[Column(TypeName = "varchar(200)")]
        public string Area { get; set; }
        //[Column("Iteration_Path")]
        public string IterationPath { get; set; }
        public int Priority { get; set; }
        // Epic
        public DateTime? StartDate { get; set; }
        //[Precision(3)]
        public DateTime? EndDate { get; set; }
        // Issue
        //[Column(TypeName = "decimal(5,2)")]
        public decimal Efford { get; set; }
        // Tast
        //[MaxLength(200)]
        public string Activity { get; set; }
        //[Precision(14,2)]
        public decimal RemainingWork { get; set; }


        public string Type { get; set; }
        
        //relation one to many (One WorkItem can have many Comments)
        public List<Comment> Comments { get; set; } = new List<Comment>(); // can be IEnumerable

        //relation one to Many (One User has relation with many WorkItems)
        public User Author { get; set; }
        public Guid AuthorId { get; set; }

        //relation many to many before .Net5
        //public List<WorkItemTag> WorkItemTags { get; set; } = new List<WorkItemTag>();

        //relation many to many after .Net5
        public List<Tag> Tags { get; set; }
    }
}
