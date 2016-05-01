


using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineHospitalInformationSystem.Models
{
    public class ImageValidationAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = (HttpPostedFileBase) value;
            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".JPEG" ,".png"};

            if (file==null)
            {
                ErrorMessage = "* Please Upload an Image.";
                return false;
            }
            if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload a Photo of type: " + string.Join(", ", allowedFileExtensions);
                return false;
            }
                return true;
       
        }
    }
}