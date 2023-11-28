﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LodBasen.Models;

[Table("Gruppe")]
public partial class Gruppe
{

    [Key]
    [Column("Gruppe_ID")]
    public int GruppeId { get; set; }

    [Required]
    [Column("Gruppe_Navn")]
    [StringLength(30)]
    [Unicode(false)]
    public string GruppeNavn { get; set; }

    [InverseProperty("Gruppe")]
    public virtual ICollection<Barn> Børn { get; set; } = new List<Barn>();

    [InverseProperty("Gruppe")]
    public virtual ICollection<Leder> Ledere { get; set; } = new List<Leder>();
}