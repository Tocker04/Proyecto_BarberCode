using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Dto;
using BarberCode_DAL.Entidad;
using BarberCode_DAL.Util;



namespace BarberCode_DAL
{ 
    public class ServicioService
    {
        public static List<ServicioDTO> ConsultarServicios()
        {
            List<ServicioDTO> ServiciosDTOs = new List<ServicioDTO>();

            using (var contexto = new ServicioDbContext())
            {
                foreach (Servicio Servicio in contexto.Servicios.ToList())
                {
                    ServiciosDTOs.Add(new ServicioDTO(Servicio));
                }
            }


            return ServiciosDTOs;
        }

        public static Servicio ConsultarServicio(long Id)
        {
            using (var contexto = new ServicioDbContext())
            {
                return contexto.Servicios.Find(Id);
            }
        }

        ///////////////////////////////////Metodos CRUD//////////////////////////////////////////
        //ya tiene la logica para agregar Servicios desde la pagina
        public static string AgregarServicio(ServicioDTO ServicioDTO)
        {
            try
            {
                Servicio Servicio = new Servicio()
                {
                    Nombre = ServicioDTO.Nombre,
                    Descripcion = ServicioDTO.Descripcion,
                    Precio = ServicioDTO.Precio
                    
                };
                using (var contexto = new ServicioDbContext())
                {
                    contexto.Servicios.Add(Servicio);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }

        //ya tiene la logica para editar los Servicios desde la pagina
        public static string ModificarServicio(ServicioDTO ServicioDTO)
        {
            try
            {
                using (var contexto = new ServicioDbContext())
                {
                    Servicio Servicio = contexto.Servicios.Find(ServicioDTO.ServicioId);
                    Servicio.Nombre = ServicioDTO.Nombre;
                    Servicio.Descripcion = ServicioDTO.Descripcion;
                    Servicio.Precio = ServicioDTO.Precio;
                    
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }

        //ya tiene la logica para eliminar los  Servicios desde la pagina
        public static string EliminarServicio(long Id)
        {
            try
            {
                using (var contexto = new ServicioDbContext())
                {
                    Servicio Servicio = contexto.Servicios.Find(Id);
                    if (Servicio != null)
                    {
                        contexto.Servicios.Remove(Servicio);
                        contexto.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }


    }
}
