using InterviewBackApp.Models;
using System.ComponentModel.DataAnnotations;

namespace InterviewBackApp.ViewModels
{
    public class RequestCreateUser
    {
        [Required(ErrorMessage = "اجباری می باشد")]
        [MinLength(2,  ErrorMessage = "حداکثر کاراکتر مجاز {0} می باشد")]
        [MaxLength(100,  ErrorMessage = "حداقل کاراکتر مجاز {0} می باشد")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "اجباری می باشد")]
        [MinLength(2, ErrorMessage = "حداکثر کاراکتر مجاز {0} می باشد")]
        [MaxLength(100, ErrorMessage = "حداقل کاراکتر مجاز {0} می باشد")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }

    public class RequestUpdateUser
    {
        [Required(ErrorMessage = "اجباری می باشد")]
        [Display(Name = "ایدی")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "اجباری می باشد")]
        [MinLength(2, ErrorMessage = "حداکثر کاراکتر مجاز {0} می باشد")]
        [MaxLength(100, ErrorMessage = "حداقل کاراکتر مجاز {0} می باشد")]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "اجباری می باشد")]
        [MinLength(2, ErrorMessage = "حداکثر کاراکتر مجاز {0} می باشد")]
        [MaxLength(100, ErrorMessage = "حداقل کاراکتر مجاز {0} می باشد")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }

    public class ResponseCreateUser
    {
        public ResponseCreateUser()
        {
            this.User = new User();
        }
        public User User { get; set; }
    }

    public class ResponseUpdateUser
    {
        public ResponseUpdateUser()
        {
            this.User = new User();
        }
        public User User { get; set; }
    }

    public class UserToList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ResponseGetUser
    {
        public ResponseGetUser()
        {
            this.User = new User();
        }
        public User User { get; set; }
    }

    public class ResponseGetUserPagination
    {
        public ResponseGetUserPagination()
        {
            this.Users = new List<UserToList>();
        }
        public IList<UserToList> Users { get; set; }
        public int Count { get; set; }
    }


}
