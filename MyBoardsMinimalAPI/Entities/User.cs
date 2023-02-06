namespace MyBoardsMinimalAPI.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        //relation one to one (One adress with one user)
        public virtual Address Address { get; set; } // virual allows for lazy loading

        //rel one to many (One User with many WorkItems) 
        public virtual List<WorkItem> WorkItems { get; set; } = new List<WorkItem>(); // virual allows for lazy loading

        //rel one to many (One User has many Comments)
        public virtual List<Comment> Comments { get; set; } = new List<Comment>(); // virual allows for lazy loading
    }
}
