using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Entidad;

namespace BarberCode_DAL.Dto
{
    public class ServicioDTO
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public double Precio { get; set; }

        public ServicioDTO()
        {

        }

        public ServicioDTO(Servicio Servicio)
        {
            this.Id = Servicio.Id;
            this.Nombre = Servicio.Nombre;
            this.Descripcion = Servicio.Descripcion;
            this.Precio = Servicio.Precio;

        }
    }
}
