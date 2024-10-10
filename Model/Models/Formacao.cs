﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.Models;

public partial class Formacao
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(14)]
    [Unicode(false)]
    public string CPF_Candidato { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Instituicao { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Curso { get; set; }

    public DateOnly? DataInicio { get; set; }

    public DateOnly? DataFim { get; set; }

    [StringLength(400)]
    [Unicode(false)]
    public string Descricao { get; set; }

    [ForeignKey("CPF_Candidato")]
    [InverseProperty("Formacao")]
    public virtual Candidato CPF_CandidatoNavigation { get; set; }
}