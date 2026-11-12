using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Desafio_DI_PSP_NelsonG_GloriaC.Models;

[Table("presupuesto")]
[Index("Codigo", Name = "codigo", IsUnique = true)]
[Index("IdCliente", Name = "fk_presupuesto_cliente")]
public partial class Presupuesto
{
    [Key]
    [Column("id_presupuesto")]
    public int IdPresupuesto { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("codigo")]
    public int Codigo { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("estado", TypeName = "enum('Pendiente','Aprobado','Rechazado')")]
    public string Estado { get; set; } = null!;

    [Column("total_materiales")]
    [Precision(10, 2)]
    public decimal TotalMateriales { get; set; }

    [Column("total")]
    [Precision(10, 2)]
    public decimal Total { get; set; }

    [ForeignKey("IdCliente")]
    [InverseProperty("Presupuestos")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [InverseProperty("IdPresupuestoNavigation")]
    public virtual ICollection<PresupuestoDetalle> PresupuestoDetalles { get; set; } = new List<PresupuestoDetalle>();
}
