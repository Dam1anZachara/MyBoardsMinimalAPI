namespace MyBoardsMinimalAPI.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        //One to many relation (One WorkItem has Many Comments)
        public WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; } //- entity framework nie potrzebuje odwołania do Id przy relacji 1 do wielu ale dobrą praktyką jest aby wypisać to property

        //One to many relation (One User has Many Comments)
        public User User { get; set; }
        public Guid AuthorId { get; set; }
    }
}
