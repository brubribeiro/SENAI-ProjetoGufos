﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domains
{
    [Table("localizacao")]
    public partial class Localizacao
    {
        public Localizacao()
        {
            Evento = new HashSet<Evento>();
        }

        [Key]
        public int IdLocal { get; set; }
        [Required]
        [Column("CNPJ")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Required]
        [StringLength(255)]
        public string RazaoSocial { get; set; }
        [Required]
        [StringLength(255)]
        public string Endereco { get; set; }

        [InverseProperty("IdLocalNavigation")]
        public virtual ICollection<Evento> Evento { get; set; }
    }
}
