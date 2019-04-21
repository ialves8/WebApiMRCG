using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiMRCG.Helpers;

namespace WebApiMRCG.Entities
{
    public class Pecuarista
    {
        public int PecuaristaId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(50, ErrorMessage = "Use menos caracteres.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]
        [PrimeiraMaiuscula]
        public string Nome { get; set; }

        public ICollection<CompraGado> CompraGados { get; set; }
    }
}
