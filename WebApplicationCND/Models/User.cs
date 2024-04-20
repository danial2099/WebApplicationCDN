namespace WebApplicationCND.Models
{
    public class User
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? email { get; set; }
        public string? phoneNum { get; set; }
        public string? skillsets { get; set; }
        public string? Hobby { get; set; }
        public int IsActive { get; set; }
    }
}
