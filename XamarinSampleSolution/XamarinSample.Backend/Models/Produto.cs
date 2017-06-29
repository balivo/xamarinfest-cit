using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace XamarinSample.Backend.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }

        [Required]
        public decimal Valor { get; set; }
    }
}