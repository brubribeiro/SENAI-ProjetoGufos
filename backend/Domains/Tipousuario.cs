﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domains
{
    [Table("tipousuario")]
    public partial class Tipousuario
    {
        public Tipousuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int IdTipoUsuario { get; set; }
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        [InverseProperty("IdTipoUsuarioNavigation")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
