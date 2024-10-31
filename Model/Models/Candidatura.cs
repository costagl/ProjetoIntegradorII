﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.Models;

public partial class Candidatura
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(14)]
    [Unicode(false)]
    public string CPF_Candidato { get; set; }

    public int idVaga { get; set; }

    public DateOnly? DataCandidatura { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(400)]
    [Unicode(false)]
    public string CartaApresetancao { get; set; }

    public bool? Curriculo { get; set; }

    [ForeignKey("CPF_Candidato")]
    [InverseProperty("Candidatura")]
    public virtual Candidato CPF_CandidatoNavigation { get; set; }

    [ForeignKey("idVaga")]
    [InverseProperty("Candidatura")]
    public virtual Vaga idVagaNavigation { get; set; }
}