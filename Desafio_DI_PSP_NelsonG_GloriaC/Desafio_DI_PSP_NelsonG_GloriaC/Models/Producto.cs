using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Desafio_DI_PSP_NelsonG_GloriaC.Models;

[Table("producto")]
public partial class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("nombre")]
    [StringLength(70)]
    public string Nombre { get; set; } = null!;

    [Column("tipo_material", TypeName = "enum('Pendiente','Metal','Cemento','Hormigon','plastico')")]
    public string TipoMaterial { get; set; } = null!;

    [Column("dimensiones")]
    [StringLength(50)]
    public string? Dimensiones { get; set; }

    [Column("proveedor")]
    [StringLength(50)]
    public string Proveedor { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Column("cantidad_stock")]
    public int CantidadStock { get; set; }

    [Column("precio_venta")]
    [Precision(15, 2)]
    public decimal PrecioVenta { get; set; }

    [Column("precio_proveedor")]
    [Precision(15, 2)]
    public decimal PrecioProveedor { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<PresupuestoDetalle> PresupuestoDetalles { get; set; } = new List<PresupuestoDetalle>();
}
