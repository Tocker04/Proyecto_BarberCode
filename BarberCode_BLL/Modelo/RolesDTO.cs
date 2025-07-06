using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Entidad;

namespace BarberCode_BLL.Modelo
{
   public class RolesDTO
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        public RolesDTO(BarberCode_DAL.Dto.RolesDTO RolesDTO)
        {
            this.Id = RolesDTO.Id;
            this.Nombre = RolesDTO.Nombre;
        }
    }
}
