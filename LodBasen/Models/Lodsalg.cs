﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LodBasen.Models;

[Table("Lodsalg")]
public partial class Lodsalg
{
    public Lodsalg()
    {
       
    }
    public Lodsalg(int sælgerId, int modtagerId, int lodseddelId)
    {
        Sælger = new Sælger { SælgerId = sælgerId };
        Modtager = new Modtager { ModtagerId = modtagerId };
        Lodseddel = new Lodseddel { LodseddelId = lodseddelId };
    }

    [Key]
    [Column("Lodsalgs_ID")]
    public int LodsalgsId { get; set; }

    [Required]
    [Column("Sælger_ID")]
    public int SælgerId { get; set; }

    [Required]
    [Column("Modtager_ID")]
    public int ModtagerId { get; set; }

    [Required]
    [Column("Lodseddel_ID")]
    public int LodseddelId { get; set; }

    [ForeignKey("LodseddelId")]
    [InverseProperty("Lodsalgssamling")]
    public virtual Lodseddel Lodseddel { get; set; }

    [ForeignKey("ModtagerId")]
    [InverseProperty("Lodsalgssamling")]
    public virtual Modtager Modtager { get; set; }

    [ForeignKey("SælgerId")]
    [InverseProperty("Lodsalgssamling")]
    public virtual Sælger Sælger { get; set; }
}