//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
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
using System.Runtime.Serialization;


namespace IndignadoFramework.Entities
{
    [Serializable]
    [DataContractAttribute(IsReference = true)]
    [KnownType(typeof(FuenteWEBProjection))]
    [MetadataType(typeof(FuenteWEBMetaData))]
    public partial class FuenteWEB 
    {
        #region Primitive Properties
    
        [DataMemberAttribute()]
        public virtual int Id
        {
            get;
            set;
        }
    
        [DataMemberAttribute()]
        public virtual string Url
        {
            get;
            set;
        }
    
        [DataMemberAttribute()]
        public virtual string Tipo
        {
            get;
            set;
        }
    
        [DataMemberAttribute()]
        public virtual int MovimientoId
        {
            get { return _movimientoId; }
            set
            {
                if (_movimientoId != value)
                {
                    if (Movimiento != null && Movimiento.Id != value)
                    {
                        Movimiento = null;
                    }
                    _movimientoId = value;
                }
            }
        }
        private int _movimientoId;
    
        [DataMemberAttribute()]
        public virtual string UrlDll
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        [DataMemberAttribute()]
        public virtual Movimiento Movimiento
        {
            get { return _movimiento; }
            set
            {
                if (!ReferenceEquals(_movimiento, value))
                {
                    var previousValue = _movimiento;
                    _movimiento = value;
                    FixupMovimiento(previousValue);
                }
            }
        }
        private Movimiento _movimiento;

        #endregion
        #region Association Fixup
    
        private void FixupMovimiento(Movimiento previousValue)
        {
            if (previousValue != null && previousValue.FuentesWEB.Contains(this))
            {
                previousValue.FuentesWEB.Remove(this);
            }
    
            if (Movimiento != null)
            {
                if (!Movimiento.FuentesWEB.Contains(this))
                {
                    Movimiento.FuentesWEB.Add(this);
                }
                if (MovimientoId != Movimiento.Id)
                {
                    MovimientoId = Movimiento.Id;
                }
            }
        }

        #endregion
    }
}
