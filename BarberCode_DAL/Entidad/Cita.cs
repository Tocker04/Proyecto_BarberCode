using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BarberCode_DAL.Dto;

namespace BarberCode_DAL.Entidad
{
    [Table ("cita")]
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long CitaId { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        //fk de Servicios
        public long servicioId { get; set; }
        [ForeignKey ("servicioId")]
        public virtual Servicio Servicio { get; set; }

        //fk de cliente
        public long clienteId { get; set; }
        [ForeignKey("clienteId")]
        public virtual Usuario UsuarioCli { get; set; }

        //fk de barbero
        public long barberoId { get; set; }
        [ForeignKey("barberoId")]
        public virtual Usuario UsuarioBar  { get; set; }
        public Cita()
        {

        }
    }
}
