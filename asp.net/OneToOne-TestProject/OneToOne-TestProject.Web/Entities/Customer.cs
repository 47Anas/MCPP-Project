namespace OneToOne_TestProject.Web.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Cart Cart { get; set; }
    }
}
