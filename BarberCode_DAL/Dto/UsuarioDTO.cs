using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Entidad;

namespace BarberCode_DAL.Dto
{
    public class UsuarioDTO
    {
        public long Id { get; set; }

        public string usuario { get; set; }

        public string Contrasenia { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public RolesDTO RolesId { get; set; }

        public UsuarioDTO()
        {

        }

        public UsuarioDTO(Usuario Usuario)
        {
            this.Id = Usuario.Id;
            this.usuario = Usuario.usuario;
            this.Contrasenia = Usuario.Contrasenia;
            this.Nombre = Usuario.Nombre;
            this.Correo = Usuario.Correo;
            this.Telefono = Usuario.Telefono;
            if (Usuario.RolesId != null)
            {
                this.RolesId = new RolesDTO(Usuario.RolesId);
            }
        }
    }

    
}
