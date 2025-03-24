using System.ComponentModel.DataAnnotations;

namespace TCC___Obra.Models
{
    public class Incidente
    {

        public int IncidenteId { get; set; }


        [Display(Name = "Obra Relacionada")]
        public string ObraRelacionada { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string Impacto { get; set; }


        [Display(Name = "Data do Incidente")]
        [DataType(DataType.Date)]
        public DateTime DataIncidente { get; set; }

        public string Acompanhamento { get; set; }


    }
}
