namespace MyBoardsMinimalAPI.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        //relation one to one (One adress with one user)
        public Address Address { get; set; }

        //rel one to many (One User with many WorkItems)
        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();

        //rel one to many (One User has many Comments)
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
