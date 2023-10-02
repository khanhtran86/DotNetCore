using System.ComponentModel.DataAnnotations;

namespace DotnetCoreVCB.Models
{
    public class SimpleUser
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
