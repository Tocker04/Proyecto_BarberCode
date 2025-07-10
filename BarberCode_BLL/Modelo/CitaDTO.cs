using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode_BLL.Modelo
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

        public CitaDTO(BarberCode_DAL.Dto.CitaDTO CitaDTO)
        {
            this.CitaId = CitaDTO.CitaId;
            this.Fecha = CitaDTO.Fecha;
            this.Hora = CitaDTO.Hora;
            ///////////
            if (CitaDTO.Servicio != null)
            {
                this.Servicio = new ServicioDTO(CitaDTO.Servicio);
            }
            ///////////
            if (CitaDTO.UsuarioCli != null)
            {
                this.UsuarioCli = new UsuarioDTO(CitaDTO.UsuarioCli);
            }
            ////////////
            if (CitaDTO.UsuarioBar != null)
            {
                this.UsuarioBar = new UsuarioDTO(CitaDTO.UsuarioBar);
            }
        }
    }
}
