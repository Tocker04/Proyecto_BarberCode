using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Entidad;

namespace BarberCode_DAL.Dto
{
    public class RolesDTO
    {
        public long RolId { get; set; }

        public string Nombre { get; set; }

        public RolesDTO()
        {

        }

        public RolesDTO(Roles Roles)
        {
            this.RolId = Roles.RolId;
            this.Nombre = Roles.Nombre;
        }

        //public static implicit operator RolesDTO(UsuarioDTO v)
        //{
         //   throw new NotImplementedException();
        //}
    }
}
