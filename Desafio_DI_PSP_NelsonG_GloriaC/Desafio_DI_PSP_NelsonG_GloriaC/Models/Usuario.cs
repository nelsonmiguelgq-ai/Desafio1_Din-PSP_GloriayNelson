using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Desafio_DI_PSP_NelsonG_GloriaC.Models;

[Table("usuario")]
[Index("Nombre", Name = "nombre", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("contraseña")]
    [StringLength(50)]
    public string Contraseña { get; set; } = null!;

    [Column("rol")]
    [StringLength(100)]
    public string Rol { get; set; } = null!;

    [InverseProperty("IdUsuarioNavigation")]
    public virtual Cliente? Cliente { get; set; }
}
