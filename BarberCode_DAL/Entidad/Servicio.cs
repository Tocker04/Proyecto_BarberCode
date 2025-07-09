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
    [Table ("servicio")]

    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long ServicioId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public double Precio { get; set; }

        public Servicio()
        {

        }

        public Servicio(ServicioDTO ServicioDTO)
        {
            this.ServicioId = ServicioDTO.ServicioId;
            this.Nombre = ServicioDTO.Nombre;
            this.Descripcion = ServicioDTO.Descripcion;
            this.Precio = ServicioDTO.Precio;

        }
    }
}
