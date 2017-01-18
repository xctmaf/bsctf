namespace Web.Application.Models.User.Input
{
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;

    [DataContract]
    public class RegisterUserModel
    {
        [DataMember]
        [Required(ErrorMessage = "Введите имя")]
        [MinLength(3, ErrorMessage = "не менее трех символов")]
        public string Username { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Введите логин")]
        [MinLength(3, ErrorMessage = "не менее трех символов")]
        public string Login { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(3,ErrorMessage = "не менее трех символов")]
        public string Password { get; set; }
    }
}