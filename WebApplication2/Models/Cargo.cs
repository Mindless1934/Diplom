//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class Cargo
{
    public int idCargo { get; set; }
    public Nullable<int> idShipment { get; set; }
    public string Number { get; set; }
    public Nullable<double> Weight { get; set; }
    public string State { get; set; }
    public Nullable<int> idRawMaterial { get; set; }
    public Nullable<int> idBranch { get; set; }

    public virtual Shipment Shipment { get; set; }
    public virtual RawMaterials RawMaterials { get; set; }
    public virtual Branches Branches { get; set; }
}
