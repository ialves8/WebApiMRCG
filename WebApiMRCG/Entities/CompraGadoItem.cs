using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMRCG.Entities
{
    public class CompraGadoItem
    {
        public int CompraGadoItemId { get; set; }

        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage = "Quantidade inválida.")]
        public int Quantidade { get; set; }

        public int CompraGadoId { get; set; }
        public CompraGado CompraGado { get; set; }

        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}
