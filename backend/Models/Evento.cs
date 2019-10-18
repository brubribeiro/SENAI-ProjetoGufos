using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("evento")]
    public partial class Evento
    {
        public Evento()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        public int IdEvento { get; set; }
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEvento { get; set; }
        [Required]
        public bool? AcessoLivre { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdLocal { get; set; }

        [ForeignKey(nameof(IdCategoria))]
        [InverseProperty(nameof(Categoria.Evento))]
        public virtual Categoria IdCategoriaNavigation { get; set; }
        [ForeignKey(nameof(IdLocal))]
        [InverseProperty(nameof(Localizacao.Evento))]
        public virtual Localizacao IdLocalNavigation { get; set; }
        [InverseProperty("IdEventoNavigation")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
