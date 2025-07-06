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
    public class UsuarioService
    {

        public static List<UsuarioDTO> ConsultarUsuarios()
        {
            List<UsuarioDTO> UsuariosDTOs = new List<UsuarioDTO>();

            using (var contexto = new UsuarioDbContext())
            {
                foreach (Usuario Usuario in contexto.Usuarios.ToList())
                {
                    UsuariosDTOs.Add(new UsuarioDTO(Usuario));
                }
            }


            return UsuariosDTOs;
        }

        public static Usuario ConsultarUsuario(long Id)
        {
            using (var contexto = new UsuarioDbContext())
            {
                return contexto.Usuarios.Find(Id);
            }
        }

        public static List<UsuarioDTO> ReporteUsuarios(UsuarioDTO idUsuario)
        {
            List<UsuarioDTO> UsuariosDTOs = new List<UsuarioDTO>();

            using (var contexto = new UsuarioDbContext())
            {
                foreach (Usuario Usuario in contexto.Usuarios.ToList())
                {
                    //bool UsuarioValida = idUsuario == null || Usuario.idUsuario.Id.Equals(idUsuario.Id);

                    //if (UsuarioValida)
                    {
                        UsuariosDTOs.Add(new UsuarioDTO(Usuario));
                    }
                }
            }

            return UsuariosDTOs;
        }

        ///////////////////////////////////Metodos CRUD//////////////////////////////////////////
        //ya tiene la logica para agregar Usuarios desde la pagina
        public static string AgregarUsuario(UsuarioDTO UsuarioDTO)
        {
            try
            {
                Usuario Usuario = new Usuario()
                {
                    usuario = UsuarioDTO.usuario,
                    Contrasenia = UsuarioDTO.Contrasenia,
                    Nombre = UsuarioDTO.Nombre,
                    Correo = UsuarioDTO.Correo,
                    Telefono = UsuarioDTO.Telefono,
                    rolId = UsuarioDTO.RolesId.Id,
                };
                using (var contexto = new UsuarioDbContext())
                {
                    contexto.Usuarios.Add(Usuario);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }

        //ya tiene la logica para editar los Usuarios desde la pagina
        public static string ModificarUsuario(UsuarioDTO UsuarioDTO)
        {
            try
            {
                using (var contexto = new UsuarioDbContext())
                {
                    Usuario Usuario = contexto.Usuarios.Find(UsuarioDTO.Id);
                    Usuario.usuario = UsuarioDTO.usuario;
                    Usuario.Contrasenia = UsuarioDTO.Contrasenia;
                    Usuario.Nombre = UsuarioDTO.Nombre;
                    Usuario.Correo = UsuarioDTO.Correo;
                    Usuario.Telefono = UsuarioDTO.Telefono;
                    Usuario.rolId = UsuarioDTO.RolesId.Id;
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }

        //ya tiene la logica para eliminar los  Usuarios desde la pagina
        public static string EliminarUsuario(long Id)
        {
            try
            {
                using (var contexto = new UsuarioDbContext())
                {
                    Usuario Usuario = contexto.Usuarios.Find(Id);
                    if (Usuario != null)
                    {
                        contexto.Usuarios.Remove(Usuario);
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

