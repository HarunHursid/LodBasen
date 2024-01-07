﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LodBasen.Models
{
    [Table("Barn")]
    public partial class Barn
    {
      [Key]
      [Column("Barn_ID")]
      public int BarnId { get; set; }

      [Required(ErrorMessage = "Navn på barn er påkrævet")]
     [StringLength(30)]
     [Unicode(false)]
     public string Navn { get; set; }

      public int Antal { get; set; }

       public int Solgt { get; set; }

      [Column("Gruppe_ID")]
      public int GruppeId { get; set; }

      [ForeignKey("GruppeId")]
     [InverseProperty("Børn")]
      public virtual Gruppe Gruppe { get; set; }

     [InverseProperty("Barn")]
      public virtual ICollection<Modtager> Modtagere { get; set; } = new List<Modtager>();
    }
}