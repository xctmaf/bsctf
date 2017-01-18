namespace Web.Application.Models.User.Input
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class LoginUserModel
    {
        [DataMember]
        [Required(ErrorMessage = "Введите логин")]
        [MinLength(3, ErrorMessage = "не менее трех символов")]
        public string Login { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(3, ErrorMessage = "не менее трех символов")]
        public string Password { get; set; }
    }
}