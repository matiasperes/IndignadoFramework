using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using IndignadoFramework.Entities;
using IndignadoFramework.UI.Mvc4.Models;

namespace IndignadoFramework.UI.Mvc4.Models
{

    public class EditModel
    {
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        [Display(Name = "Longitud")]
        public double longitud { get; set; }

        //[Required]
        [Display(Name = "Latitud")]
        public double latitud { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual")]
        public string vcontraseña { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string ncontraseña { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("ncontraseña", ErrorMessage = "La nueva contraseña y la confirmacion no coinciden.")]
        public string ccontraseña { get; set; }


    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contraseña { get; set; }

        [Display(Name = "¿Recordar?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {

        [Display(Name = "Nombre")]
        public string nombre { get; set; }


        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contraseña { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("contraseña", ErrorMessage = "La contraseña y la confirmación no coinciden")]
        public string ccontraseña { get; set; }

        //[Required]
        [Display(Name = "Longitud")]
        public double longitud { get; set; }

        //[Required]
        [Display(Name = "Latitud")]
        public double latitud { get; set; }

        /*
        [Display(Name = "Categorias")]
        public ICollection<CategoriaTematica> categs { get; set; }*/

    }
}
