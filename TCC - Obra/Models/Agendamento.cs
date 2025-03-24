using System.ComponentModel.DataAnnotations;

namespace TCC___Obra.Models
{
    public class Agendamento
    {

        public int AgendamentoId { get; set; }


        [Display(Name = "Título")]
        public string Titulo  { get; set; }


        [Display(Name = "Data do Agendamento")]
        [DataType(DataType.Date)]
        public DateTime DataAgendamento { get; set; }


        [Display(Name = "Hora da Visita")]
        [DataType(DataType.Time)]
        public DateTime Hora { get; set; }


        [Display(Name = "Descrição do Agendamento")]
        public string DescricaoAgendamento { get; set; }

        public string Status { get; set; }
    }
}
