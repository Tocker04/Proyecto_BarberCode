﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Dto;
using BarberCode_DAL.Entidad;
using BarberCode_DAL.Util;

namespace BarberCode_DAL
{
    public class CitaService
    {
        public static List<CitaDTO> ConsultarCitas()
        {
            List<CitaDTO> CitasDTOs = new List<CitaDTO>();

            using (var contexto = new CitaDbContext())
            {
                foreach (Cita Cita in contexto.Citas.ToList())
                {
                    CitasDTOs.Add(new CitaDTO(Cita));
                }
            }


            return CitasDTOs;
        }

        public static Cita ConsultarCita(long Id)
        {
            using (var contexto = new CitaDbContext())
            {
                return contexto.Citas.Find(Id);
            }
        }

        public static List<CitaDTO> ReporteCitas(CitaDTO idCita)
        {
            List<CitaDTO> CitasDTOs = new List<CitaDTO>();

            using (var contexto = new CitaDbContext())
            {
                foreach (Cita Cita in contexto.Citas.ToList())
                {
                    //bool UsuarioValida = idUsuario == null || Usuario.idUsuario.Id.Equals(idUsuario.Id);

                    //if (UsuarioValida)
                    {
                        CitasDTOs.Add(new CitaDTO(Cita));
                    }
                }
            }

            return CitasDTOs;
        }

        ///////////////////////////////////Metodos CRUD//////////////////////////////////////////
        //ya tiene la logica para agregar Citas desde la pagina
        public static string AgregarCita(CitaDTO CitaDTO)
        {
            try
            {
                Cita Cita = new Cita()
                {
                    Fecha = CitaDTO.Fecha,
                    Hora = CitaDTO.Hora,
                    servicioId = CitaDTO.Servicio.ServicioId,
                    clienteId = CitaDTO.UsuarioCli.UsuarioId,
                    barberoId = CitaDTO.UsuarioBar.UsuarioId
                };
                using (var contexto = new CitaDbContext())
                {
                    contexto.Citas.Add(Cita);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }

        //ya tiene la logica para editar los Citas desde la pagina
        public static string ModificarCita(CitaDTO CitaDTO)
        {
            try
            {
                using (var contexto = new CitaDbContext())
                {
                    Cita Cita = contexto.Citas.Find(CitaDTO.CitaId);
                    Cita.Fecha = CitaDTO.Fecha;
                    Cita.Hora = CitaDTO.Hora;
                    Cita.servicioId = CitaDTO.Servicio.ServicioId;
                    Cita.clienteId = CitaDTO.UsuarioCli.UsuarioId;
                    Cita.barberoId = CitaDTO.UsuarioBar.UsuarioId;
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }

        //ya tiene la logica para eliminar los  Citas desde la pagina
        public static string EliminarCita(long Id)
        {
            try
            {
                using (var contexto = new CitaDbContext())
                {
                    Cita Cita = contexto.Citas.Find(Id);
                    if (Cita != null)
                    {
                        contexto.Citas.Remove(Cita);
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
