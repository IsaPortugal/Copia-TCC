using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace TCC___Obra.Models
{
    public class Obra
    {

        public int ObraId { get; set; }

        [Required(ErrorMessage = "O nome da obra é obrigatório")]
        [Display(Name = "Nome da Obra")]
        public string NomeObra { get; set; }

        [Required(ErrorMessage = "O nome do Cliente é obrigatório")]
        [Display(Name = "Nome do Cliente")]
        public string NomeCliente { get; set; }

        [Required]
        [Display(Name = "Pessoa Física/Razão Social")]
        public string TipoPessoa { get; set; }

        
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF em formato inválido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "CNPJ em formato inválido")]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }


        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Numero De Telefone Inválido")]
        public string Telefone { get; set; }

        [Required]
        [Display(Name = "Localização")]
        public string Localizacao { get; set; }

        [Required]
        [Display(Name = "Status da Obra")]
        public string StatusObra { get; set; }

        [Required]
        [Display(Name = "Data de Inicio")]
        [DataType(DataType.Date)]
        public DateTime? DtInicio { get; set; }

        [Required]
        [Display(Name = "Data Estimada do Termino")]
        [DataType(DataType.Date)]
        public DateTime? DtEstimada { get; set; }

        [Required]
        [Display(Name = "Responsável pela Obra")]
        public string RespObra { get; set; }

        
        [Display(Name = "Foto da Obra")]

        public string FotoObra { get; set; }


    }
}
