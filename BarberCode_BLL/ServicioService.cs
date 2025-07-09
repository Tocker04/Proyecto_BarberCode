using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Entidad;
using BarberCode_DAL.Util;
using BarberCode_BLL.Modelo;

namespace BarberCode_BLL
{
    public class ServicioService
    {
        public static List<ServicioDTO> ConsultarServicios()
        {
            List<BarberCode_DAL.Dto.ServicioDTO> Servicios = BarberCode_DAL.ServicioService.ConsultarServicios();
            List<ServicioDTO> ServiciosDTOs = new List<ServicioDTO>();
            if (Servicios != null)
            {
                foreach (BarberCode_DAL.Dto.ServicioDTO Servicio in Servicios)
                {
                    ServiciosDTOs.Add(new ServicioDTO(Servicio));
                }
            }
            return ServiciosDTOs;
        }

        public static bool AgregarServicio(ServicioDTO ServicioDTO)
        {
            BarberCode_DAL.Dto.ServicioDTO Servicio = ConvertirHaciaServicioDTOCapaDAL(ServicioDTO);
            string resultado = BarberCode_DAL.ServicioService.AgregarServicio(Servicio);
            return resultado == null;
        }
        public static bool ModificarServicio(ServicioDTO ServicioDTO)
        {
            BarberCode_DAL.Dto.ServicioDTO Servicio = ConvertirHaciaServicioDTOCapaDAL(ServicioDTO);
            string resultado = BarberCode_DAL.ServicioService.ModificarServicio(Servicio);
            return resultado == null;
        }

        public static bool EliminarServicio(long Id)
        {
            string resultado = BarberCode_DAL.ServicioService.EliminarServicio(Id);
            return resultado == null;
        }

        private static BarberCode_DAL.Dto.ServicioDTO ConvertirHaciaServicioDTOCapaDAL(ServicioDTO ServicioDTO)
        {
            BarberCode_DAL.Dto.ServicioDTO Servicio = new BarberCode_DAL.Dto.ServicioDTO()
            {
                Nombre = ServicioDTO.Nombre,
                Descripcion = ServicioDTO.Descripcion,
                Precio = ServicioDTO.Precio
            };
            if (ServicioDTO.ServicioId > 0)
            {

                Servicio.ServicioId = ServicioDTO.ServicioId;
            }
            
            return Servicio;
        }

    }
}
