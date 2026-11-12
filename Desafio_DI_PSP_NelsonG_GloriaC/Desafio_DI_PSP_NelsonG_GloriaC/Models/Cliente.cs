using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Desafio_DI_PSP_NelsonG_GloriaC.Models;

[Table("cliente")]
[Index("Nif", Name = "NIF", IsUnique = true)]
public partial class Cliente
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("NIF")]
    [StringLength(50)]
    public string Nif { get; set; } = null!;

    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    [Column("telefono")]
    public int Telefono { get; set; }

    [Column("direccion")]
    [StringLength(100)]
    public string Direccion { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Cliente")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
}
