using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Desafio_DI_PSP_NelsonG_GloriaC.Models;

[PrimaryKey("IdPresupuesto", "IdProducto")]
[Table("presupuesto_detalle")]
[Index("IdProducto", Name = "fk_detalle_producto")]
public partial class PresupuestoDetalle
{
    [Key]
    [Column("id_presupuesto")]
    public int IdPresupuesto { get; set; }

    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_aplicado")]
    [Precision(15, 2)]
    public decimal PrecioAplicado { get; set; }

    [ForeignKey("IdPresupuesto")]
    [InverseProperty("PresupuestoDetalles")]
    public virtual Presupuesto IdPresupuestoNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("PresupuestoDetalles")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
