namespace MyBoardsMinimalAPI.Entities
{
    // Tabel wymagana do konfiguracji relacji Many to many przed .Net 5
    public class WorkItemTag
    {
        public virtual WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }

        public virtual Tag Tag { get; set; }
        public int TagId { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
