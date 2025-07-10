using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_BLL.Modelo;
using BarberCode_DAL;
using BarberCode_DAL.Entidad;


namespace BarberCode_BLL
{
    public class CitaService
    {
        public static List<CitaDTO> ConsultarCitas()
        {
            List<BarberCode_DAL.Dto.CitaDTO> Citas = BarberCode_DAL.CitaService.ConsultarCitas();
            List<CitaDTO> CitasDTOs = new List<CitaDTO>();
            if (Citas != null)
            {
                foreach (BarberCode_DAL.Dto.CitaDTO Cita in Citas)
                {
                    CitasDTOs.Add(new CitaDTO(Cita));
                }
            }
            return CitasDTOs;
        }

        /// ReporteCitas metodo
        public static List<CitaDTO> ReporteCitas(CitaDTO idCita)
        {
            BarberCode_DAL.Dto.CitaDTO Cita = ConvertirHaciaAAADTOCapaDAL(idCita);
            List<BarberCode_DAL.Dto.CitaDTO> Citas = BarberCode_DAL.CitaService.ReporteCitas(Cita);
            List<CitaDTO> CitasDTOs = new List<CitaDTO>();
            if (Citas != null)
            {
                foreach (BarberCode_DAL.Dto.CitaDTO cita in Citas)
                {
                    CitasDTOs.Add(new CitaDTO(cita));
                }
            }
            return CitasDTOs;
        }

        private static BarberCode_DAL.Dto.CitaDTO ConvertirHaciaAAADTOCapaDAL(CitaDTO CitaDTO)
        {
            BarberCode_DAL.Dto.CitaDTO Cita = new BarberCode_DAL.Dto.CitaDTO();
            if (CitaDTO != null)
            {
                Cita = new BarberCode_DAL.Dto.CitaDTO()
                {
                    CitaId = CitaDTO.CitaId,
                    //Nombre = UsuarioDTO.Nombre
                };
            }
            return Cita;
        }

        /// ////////////////////////////////////////////////////////////////////////////////////



        public static bool AgregarCita(CitaDTO CitaDTO)
        {
            BarberCode_DAL.Dto.CitaDTO Cita = ConvertirHaciaCitaDTOCapaDAL(CitaDTO);
            string resultado = BarberCode_DAL.CitaService.AgregarCita(Cita);
            return resultado == null;
        }
        public static bool ModificarCita(CitaDTO CitaDTO)
        {
            BarberCode_DAL.Dto.CitaDTO Cita = ConvertirHaciaCitaDTOCapaDAL(CitaDTO);
            string resultado = BarberCode_DAL.CitaService.ModificarCita(Cita);
            return resultado == null;
        }

        public static bool EliminarCita(long Id)
        {
            string resultado = BarberCode_DAL.CitaService.EliminarCita(Id);
            return resultado == null;
        }

        private static BarberCode_DAL.Dto.CitaDTO ConvertirHaciaCitaDTOCapaDAL(CitaDTO CitaDTO)
        {
            BarberCode_DAL.Dto.CitaDTO Cita = new BarberCode_DAL.Dto.CitaDTO()
            {
                Fecha = CitaDTO.Fecha,
                Hora = CitaDTO.Hora          
            };
            if (CitaDTO.CitaId > 0)
            {

                Cita.CitaId = CitaDTO.CitaId;
            }
            if (CitaDTO.Servicio != null)
            {
                Cita.Servicio = new BarberCode_DAL.Dto.ServicioDTO()
                {
                    ServicioId = CitaDTO.Servicio.ServicioId,
                    Nombre = CitaDTO.Servicio.Nombre
                };
            }


            if (CitaDTO.UsuarioCli != null)
            {
                Cita.UsuarioCli = new BarberCode_DAL.Dto.UsuarioDTO()
                {
                    UsuarioId = CitaDTO.UsuarioCli.UsuarioId,
                    Nombre = CitaDTO.UsuarioCli.Nombre,
                };
            }

            if (CitaDTO.UsuarioBar != null)
            {
                Cita.UsuarioBar = new BarberCode_DAL.Dto.UsuarioDTO()
                {
                    UsuarioId = CitaDTO.UsuarioBar.UsuarioId,
                    Nombre = CitaDTO.UsuarioBar.Nombre,
                };
            }
            return Cita;
        }

    }
}
