namespace KUSYS_Demo.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public int? Age { get; set; }
    }
}
