using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiMRCG.Helpers;

namespace WebApiMRCG.Entities
{
    public class Animal
    {
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(300, ErrorMessage = "Use menos caracteres.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]
        [PrimeiraMaiuscula]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "Preço inválido.")]
        [Display(Name = "Preço")]
        public double Preco { get; set; }

        public ICollection<CompraGadoItem> CompraGadoItens { get; set; }
    }
}
