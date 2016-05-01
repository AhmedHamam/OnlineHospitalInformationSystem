

using System.ComponentModel.DataAnnotations;

namespace OnlineHospitalInformationSystem.Models
{
    public class HospitalUserLoginMetaData
    {
    [Display(Name = "Username")]
    [Required(ErrorMessage = "Username Required")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Password Required")]
    [Display(Name = "Password")]
    public string Password { get; set; }
    [Display(Name = "Hospital name")]
    [Required(ErrorMessage = "Hospitalame Required")]
     public int HospitalId { get; set; }
    }
}