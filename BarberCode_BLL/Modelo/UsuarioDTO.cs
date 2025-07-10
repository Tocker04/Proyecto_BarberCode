using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL;

namespace BarberCode_BLL.Modelo
{
    public class UsuarioDTO
    {
        public long UsuarioId { get; set; }

        public string usuario { get; set; }

        public string Contrasenia { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public RolesDTO RolesId { get; set; }

        public UsuarioDTO()
        {

        }

        public UsuarioDTO(BarberCode_DAL.Dto.UsuarioDTO UsuarioDTO)
        {
            this.UsuarioId = UsuarioDTO.UsuarioId;
            this.usuario = UsuarioDTO.usuario;
            this.Contrasenia = UsuarioDTO.Contrasenia;
            this.Nombre = UsuarioDTO.Nombre;
            this.Correo = UsuarioDTO.Correo;
            this.Telefono = UsuarioDTO.Telefono;
            if (UsuarioDTO.RolesId != null)
            {
                this.RolesId = new RolesDTO(UsuarioDTO.RolesId);
            }
        }
    }
}
