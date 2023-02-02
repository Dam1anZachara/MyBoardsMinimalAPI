namespace MyBoardsMinimalAPI.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Category { get; set; }

        //relation many to many before .Net5
        //public List<WorkItemTag> WorkItemTags { get; set; } = new List<WorkItemTag>();

        //relation many to many after .Net5
        public List<WorkItem> WorkItems { get; set; }
    }
}
