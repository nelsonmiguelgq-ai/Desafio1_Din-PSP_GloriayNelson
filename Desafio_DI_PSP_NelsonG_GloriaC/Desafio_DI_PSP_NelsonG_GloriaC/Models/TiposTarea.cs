using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Desafio_DI_PSP_NelsonG_GloriaC.Models;

[Table("tipos_tarea")]
public partial class TiposTarea
{
    [Key]
    [Column("id_tip_tarea")]
    public int IdTipTarea { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("precio_hora")]
    public double PrecioHora { get; set; }
}
