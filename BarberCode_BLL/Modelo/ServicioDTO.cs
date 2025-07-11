﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode_BLL.Modelo
{
    public class ServicioDTO
    {

        public long ServicioId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public double Precio { get; set; }

        public ServicioDTO()
        {

        }

        public ServicioDTO(BarberCode_DAL.Dto.ServicioDTO ServicioDTO)
        {
            this.ServicioId = ServicioDTO.ServicioId;
            this.Nombre = ServicioDTO.Nombre;
            this.Descripcion = ServicioDTO.Descripcion;
            this.Precio = ServicioDTO.Precio;

        }
    }
}
