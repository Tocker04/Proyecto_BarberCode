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
    public class UsuarioService
    {
        public static List<UsuarioDTO> ConsultarUsuarios()
        {
            List<BarberCode_DAL.Dto.UsuarioDTO> Usuarios = BarberCode_DAL.UsuarioService.ConsultarUsuarios();
            List<UsuarioDTO> UsuariosDTOs = new List<UsuarioDTO>();
            if (Usuarios != null)
            {
                foreach (BarberCode_DAL.Dto.UsuarioDTO Usuario in Usuarios)
                {
                    UsuariosDTOs.Add(new UsuarioDTO(Usuario));
                }
            }
            return UsuariosDTOs;
        }

        /// ReporteUsuarios metodo
        public static List<UsuarioDTO> ReporteUsuarios(UsuarioDTO idUsuario)
        {
            BarberCode_DAL.Dto.UsuarioDTO Usuario = ConvertirHaciaAAADTOCapaDAL(idUsuario);
            List<BarberCode_DAL.Dto.UsuarioDTO> Usuarios = BarberCode_DAL.UsuarioService.ReporteUsuarios(Usuario);
            List<UsuarioDTO> UsuariosDTOs = new List<UsuarioDTO>();
            if (Usuarios != null)
            {
                foreach (BarberCode_DAL.Dto.UsuarioDTO usuario in Usuarios)
                {
                    UsuariosDTOs.Add(new UsuarioDTO(usuario));
                }
            }
            return UsuariosDTOs;
        }

        private static BarberCode_DAL.Dto.UsuarioDTO ConvertirHaciaAAADTOCapaDAL(UsuarioDTO UsuarioDTO)
        {
            BarberCode_DAL.Dto.UsuarioDTO Usuario = new BarberCode_DAL.Dto.UsuarioDTO();
            if (UsuarioDTO != null)
            {
                Usuario = new BarberCode_DAL.Dto.UsuarioDTO()
                {
                    UsuarioId = UsuarioDTO.UsuarioId,
                    Nombre = UsuarioDTO.Nombre
                };
            }
            return Usuario;
        }


        public static bool ExisteCorreo(string correo)
        {
            return BarberCode_DAL.UsuarioService.ExisteCorreo(correo);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////



        public static bool AgregarUsuario(UsuarioDTO UsuarioDTO)
        {
            BarberCode_DAL.Dto.UsuarioDTO Usuario = ConvertirHaciaUsuarioDTOCapaDAL(UsuarioDTO);
            string resultado = BarberCode_DAL.UsuarioService.AgregarUsuario(Usuario);
            return resultado == null;
        }
        public static bool ModificarUsuario(UsuarioDTO UsuarioDTO)
        {
            BarberCode_DAL.Dto.UsuarioDTO Usuario = ConvertirHaciaUsuarioDTOCapaDAL(UsuarioDTO);
            string resultado = BarberCode_DAL.UsuarioService.ModificarUsuario(Usuario);
            return resultado == null;
        }

        public static bool EliminarUsuario(long Id)
        {
            string resultado = BarberCode_DAL.UsuarioService.EliminarUsuario(Id);
            return resultado == null;
        }

        private static BarberCode_DAL.Dto.UsuarioDTO ConvertirHaciaUsuarioDTOCapaDAL(UsuarioDTO UsuarioDTO)
        {
            BarberCode_DAL.Dto.UsuarioDTO Usuario = new BarberCode_DAL.Dto.UsuarioDTO()
            {
                usuario = UsuarioDTO.usuario,
                Contrasenia = UsuarioDTO.Contrasenia,
                Nombre = UsuarioDTO.Nombre,
                Correo = UsuarioDTO.Correo,
                Telefono = UsuarioDTO.Telefono


            };
            if (UsuarioDTO.UsuarioId > 0)
            {

                Usuario.UsuarioId = UsuarioDTO.UsuarioId;
            }
            if (UsuarioDTO.RolesId != null)
            {
                Usuario.RolesId = new BarberCode_DAL.Dto.RolesDTO()
                {
                    RolId = UsuarioDTO.RolesId.RolId,
                    Nombre = UsuarioDTO.RolesId.Nombre
                };
            }
            return Usuario;
        }

    }
}
