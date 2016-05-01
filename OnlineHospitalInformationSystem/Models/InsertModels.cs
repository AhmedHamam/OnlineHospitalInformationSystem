

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace OnlineHospitalInformationSystem.Models
{
    public class InsertModels
    {
    }

    public class SerialBookingModel
    {
        [Required(ErrorMessage="* Required")]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Patient Age")]
        public int PatientAge { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Doctor ")]
        public string Doctor { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "* Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Visit Date")]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Patient Type")]
        public string PatientType { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Hospital")]
        public int HospitalId { get; set; }
    }
   
    public static class HtmlExtensions
    {
        public static MvcHtmlString File(this HtmlHelper html, string name)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);
            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName(expression);
            return html.File(name);
        }

        #region Helpers

        static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (!TryFindMemberExpression(exp.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            }
            while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null)
                return true;

            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression)exp).Operand as MemberExpression;

                if (memberExp != null)
                    return true;
            }

            return false;
        }

        static bool IsConversion(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Convert || exp.NodeType == ExpressionType.ConvertChecked);
        }

        #endregion
    }
    public class DegreeAddModel
    {
        [Display(Name = "Name of Degree")]
        [Required(ErrorMessage = "This field is required!")]
        public string Degree { get; set; }
    }
    public class VisitAddModel
    {
        [Display(Name = "Visit (Taka)")]
        [Required(ErrorMessage = "This field is required!")]
        public int Visit { get; set; }
    }
    public class DepartmentAddModel
    {
        [Display(Name = "Name of Department")]
        [Required(ErrorMessage = "This field is required!")]
        public string Department { get; set; }
    }
    public class SpecilizationAddModel
    {
        [Display(Name = "Name of Specilization")]
        [Required(ErrorMessage = "This field is required!")]
        public string Specilization { get; set; }
    }
    
    public class InstitutionSelectList
    {

        public int InstitutionId { get; set; }
        public string Institution { get; set; }
    }
    public class DegreeSelectList
    {

        public int DegreeId { get; set; }
        public string Degree { get; set; }
    }
    public class SpecilizationSelectList
    {

        public int SpecilizationId { get; set; }
        public string Specilization { get; set; }
    }
    public class TestAddModel
    {
        [Display(Name = "Name of Test")]
        [Required(ErrorMessage = "This field is required!")]
        public string Test { get; set; }
        [Display(Name = "Coast")]
        [Required(ErrorMessage = "This field is required!")]
        public int Coast { get; set; }
        [Display(Name = "Hospitalname")]
        [Required(ErrorMessage = "This field is required!")]
          public string HospitalName { get; set; }

    }

    public class HospitalAddModel
{
    [Display(Name = "Name of Hospital")]
    [Required(ErrorMessage = "This field is required!")]
    public string Name { get; set; }

    [Display(Name = "Location (Address)")]
    [Required(ErrorMessage = "This field is required!")]
    public string Location { get; set; }
    [Display(Name = "HospitalType")]
    [Required(ErrorMessage = "This field is required!")]
    public string HospitalType { get; set; }

    [Display(Name = "Description(Short if any)")]
    public byte[] Image { get; set; }
    public string Description { get; set; }
    [ImageValidationAttribute]
    public HttpPostedFileBase File { get; set; }
       
}
    public class DoctorAddModel
    {
        [Display(Name = "Name of Doctor")]
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "This field is required!")]
        public int DepartmentId { get; set; }
  

        [Display(Name = "Hospital")]
        [Required(ErrorMessage = "This field is required!")]
        public int HospitalId { get; set; }
        [Display(Name = "Visit (Taka)")]
        [Required(ErrorMessage = "This field is required!")]
        public int Visit { get; set; }
        [Display(Name = "Degree")]
        [Required(ErrorMessage = "This field is required!")]
        public List<int> DegreeIdList { get; set; }
        [Display(Name = "Institution")]
        [Required(ErrorMessage = "This field is required!")]
        public List<int> InstitutionIdList { get; set; }
        [Display(Name = "Specilize In")]
        [Required(ErrorMessage = "This field is required!")]
        public int SpecilizationId { get; set; }
        [Display(Name = "Working In ")]
        public int? GovHospitalId { get; set; }
    }

    public class SelectListId
    {
        public int DegreeId { get; set; }
        public int InstitutionId { get; set; }
  
    }
    
}