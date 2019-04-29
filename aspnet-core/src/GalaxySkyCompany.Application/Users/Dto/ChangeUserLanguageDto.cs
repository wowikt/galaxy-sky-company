using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}