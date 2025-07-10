using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Entidad;

namespace BarberCode_DAL.Dto
{
    public class CitaDTO
    {
        public long CitaId { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public ServicioDTO Servicio { get; set; }

        public UsuarioDTO UsuarioCli { get; set; } //Cliente

        public UsuarioDTO UsuarioBar { get; set; } //Barbero

        public CitaDTO()
        {

        }

        public CitaDTO(Cita Cita)
        {
            this.CitaId = Cita.CitaId;
            this.Fecha = Cita.Fecha;
            this.Hora = Cita.Hora;
            ///////////
            if (Cita.Servicio != null)
            {
                this.Servicio = new ServicioDTO(Cita.Servicio);
            }
            ///////////
            if (Cita.UsuarioCli != null)
            {
                this.UsuarioCli = new UsuarioDTO(Cita.UsuarioCli);
            }
            ////////////
            if (Cita.UsuarioBar != null)
            {
                this.UsuarioBar = new UsuarioDTO(Cita.UsuarioBar);
            }
        }


    }
}
