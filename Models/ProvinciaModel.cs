using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MVCApplication.Models
{
    public class ProvinciaModel
    {
        [Required]
        [CustomValidation(typeof(ProvinciaValidations), "IsValidIdProvincia")]
        [IsValidIdProvinciaAttributte(ErrorMessage="El id de la provincia es invalido")]
        public int Id { get; set; }

        [Required]
        [Display(Name="NombreProvincia", ResourceType = typeof(Resources.Global.Global))]
        [StringLength(20)]
        public string Descripcion { get; set; }
    }

    internal class IsValidIdProvinciaAttributte : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int id = Convert.ToInt32(value);
            return id > 0;
        }
    }

    internal static class ProvinciaValidations
    {
        public static ValidationResult IsValidIdProvincia(int value)
        {
            if (value > 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("El id de la provincia debe ser mayor a 0");
            }
        }
    }
}