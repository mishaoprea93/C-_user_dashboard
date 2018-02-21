using System.ComponentModel.DataAnnotations;
namespace user_dashboard.Models{
    public class RegisterViews{
        [Required]
        [MinLength(2)]
        public string FirstName{get;set;}
        [Required]
        [MinLength(2)]
        public string LastName{get;set;}
        [Required]
        [EmailAddress]
        public string Email{get;set;}
        [Required]
        public string Status{get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password{get;set;} 
        [Compare ("Password",ErrorMessage="Password and Password confirmation do not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation{get;set;}

        public RegisterViews(){
            Status="normal";
        }
    }
    
}