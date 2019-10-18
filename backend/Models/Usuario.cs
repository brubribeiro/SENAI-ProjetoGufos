using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Senha { get; set; }
        public int? IdTipoUsuario { get; set; }

        [ForeignKey(nameof(IdTipoUsuario))]
        [InverseProperty(nameof(Tipousuario.Usuario))]
        public virtual Tipousuario IdTipoUsuarioNavigation { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
