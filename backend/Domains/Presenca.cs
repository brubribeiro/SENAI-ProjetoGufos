using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Domains
{
    [Table("presenca")]
    public partial class Presenca
    {
        [Key]
        public int IdPresenca { get; set; }
        [Required]
        [StringLength(255)]
        public string PresencaStatus { get; set; }
        public int? IdEvento { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdEvento))]
        [InverseProperty(nameof(Evento.Presenca))]
        public virtual Evento IdEventoNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuario.Presenca))]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
