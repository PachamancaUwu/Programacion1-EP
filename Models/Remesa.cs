using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace examenparcial.Models
{
    [Table("remesa")]
    public class Remesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public string NombreRemitente { get; set; }
        public string NombreDestinatario { get; set; }
        public string PaisOrigen { get; set; }
        public string Destino { get; set; }
        public Decimal MontoEnviado { get; set; }
        public string TipoMoneda  { get; set; }
        public Decimal TasaCambio { get; set; }
        public Decimal MontoFinal { get; set; }
        public string Estado { get; set; } = "PENDIENTE";
    }
}