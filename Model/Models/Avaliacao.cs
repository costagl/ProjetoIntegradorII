﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.Models;

public partial class Avaliacao
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(14)]
    [Unicode(false)]
    public string CPF_Candidato { get; set; }

    [Required]
    [StringLength(18)]
    [Unicode(false)]
    public string CNPJ_Empresa { get; set; }

    public byte Classificacao { get; set; }

    [Required]
    [StringLength(400)]
    [Unicode(false)]
    public string Descricao { get; set; }

    [ForeignKey("CNPJ_Empresa")]
    [InverseProperty("Avaliacao")]
    public virtual Empresa CNPJ_EmpresaNavigation { get; set; }

    [ForeignKey("CPF_Candidato")]
    [InverseProperty("Avaliacao")]
    public virtual Candidato CPF_CandidatoNavigation { get; set; }
}