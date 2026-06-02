using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Enums
{
    public enum SexEnum
    {
        [Display(Name = "Чоловік")]
        Male = 1,
        [Display(Name = "Жінка")]
        Famele = 0,
    }
}
