﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.Models;

public partial class Candidato
{
    [Key]
    [StringLength(14)]
    [Unicode(false)]
    public string CPF { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Senha { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Telefone { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Endereco { get; set; }

    public DateOnly? DataNascimento { get; set; }

    [InverseProperty("CPF_CandidatoNavigation")]
    public virtual ICollection<Avaliacao> Avaliacao { get; set; } = new List<Avaliacao>();

    [InverseProperty("CPF_CandidatoNavigation")]
    public virtual ICollection<Experiencia> Experiencia { get; set; } = new List<Experiencia>();

    [InverseProperty("CPF_CandidatoNavigation")]
    public virtual ICollection<Formacao> Formacao { get; set; } = new List<Formacao>();
}