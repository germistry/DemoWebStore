using System.ComponentModel.DataAnnotations;


namespace WebStore.UI.ViewModels.Admin
{
    public class CreateManagerUserViewModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
