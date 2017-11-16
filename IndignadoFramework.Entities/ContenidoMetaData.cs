//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template made by Louis-Guillaume Morand.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace IndignadoFramework.Entities
{
    
    
    public class ContenidoMetaData
    {
    	[Display(Name ="Id")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual int Id { get; set; }
    	
    	[Display(Name ="Ubicacion")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual string Ubicacion { get; set; }
    	
    	[Display(Name ="Tipo")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual string Tipo { get; set; }
    	
    	[Display(Name ="FechaPosteado")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual System.DateTime FechaPosteado { get; set; }
    	
    	[Display(Name ="CategoriaTematicaId")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual int CategoriaTematicaId { get; set; }
    	
    	[Display(Name ="Inadecuado")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual int Inadecuado { get; set; }
    	
    	[Display(Name ="EspecificacionUsuarioId")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual int EspecificacionUsuarioId { get; set; }
    	
    	[Display(Name ="Titulo")]
    	[StringLength(45, ErrorMessage="Debe ingresar menos de 45 caracteres")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual string Titulo { get; set; }
    	
    	[Display(Name ="CantMeGusta")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual int CantMeGusta { get; set; }
    	
    	[Display(Name ="Habilitado")]
        [Required(ErrorMessage="Campo requerido")]
        public virtual bool Habilitado { get; set; }
    	
    }
}
