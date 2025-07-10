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
    [Table("roles")]
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RolId { get; set; }

        public string Nombre { get; set; }

        public Roles()
        {

        }

        public Roles(RolesDTO RolesDTO)
        {
            RolId = RolesDTO.RolId;
            Nombre = RolesDTO.Nombre;
        }
    }
}
