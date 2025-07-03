using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberCode_DAL.Entidad
{
    [Table ("usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long Id { get; set; }

        public string usuario { get; set; }

        public string Contrasenia { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public long rolId { get; set; }
        [ForeignKey ("rolId")]
        public virtual Roles RolesId { get; set; }

        public Usuario()
        {

        }
    }
}
