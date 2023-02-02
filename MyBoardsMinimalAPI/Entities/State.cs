namespace MyBoardsMinimalAPI.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // relation one to many (WorkItem)
        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }
}
