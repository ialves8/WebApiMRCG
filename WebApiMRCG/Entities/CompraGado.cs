using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMRCG.Entities
{
    public class CompraGado
    {
        public int CompraGadoId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Entrega")]
        public DateTime DataEntrega { get; set; }

        public int PecuaristaId { get; set; }
        public Pecuarista Pecuarista { get; set; }

        public ICollection<CompraGadoItem> CompraGadoItens { get; set; }
    }
}
