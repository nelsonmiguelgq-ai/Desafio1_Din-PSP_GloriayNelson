using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PARTE1PrimerCRUD.Models;

[Table("estudiantes")]
public partial class Estudiante
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Apellido { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaNacimiento { get; set; }
}
