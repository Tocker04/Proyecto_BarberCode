using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BarberCode_DAL.Entidad;
using BarberCode_DAL.Dto;
using BarberCode_DAL.Util;

namespace BarberCode_DAL
{
    public class RolesService
    { 
        public static List<RolesDTO> ConsultarRolesS()
        {
            List<RolesDTO> RolesSDTOs = new List<RolesDTO>();

                List<Roles> RolesS = new List<Roles>();
                using (var contexto = new RolesDbContext())
                {
                RolesS = contexto.Roles.ToList();
                    if (RolesS != null)
                    {
                        foreach (Roles Roles in RolesS)
                        {
                        RolesSDTOs.Add(new RolesDTO(Roles));
                        }
                    }
                }
            
            
            return RolesSDTOs;
        }


        ///////////////////////////////////Metodos CRUD//////////////////////////////////////////
        public static Roles ConsultarRoles(long Id)
        {
            using (var contexto = new RolesDbContext())
            {
                return contexto.Roles.Find(Id);
            }
        }

        public static string AgregarRoles(Roles Roles)
        {
            try
            {
                using (var contexto = new RolesDbContext())
                {
                    contexto.Roles.Add(Roles);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }

        public static string ModificarRoles(Roles Roles)
        {
            try
            {
                using (var contexto = new RolesDbContext())
                {
                    contexto.Entry(Roles).CurrentValues.SetValues(Roles);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.ToString();
            }
            return null;
        }

        public static string EliminarRoles(long Id)
        {
            try
            {
                using (var contexto = new RolesDbContext())
                {
                    Roles Roles = contexto.Roles.Find(Id);
                    if (Roles != null)
                    {
                        contexto.Roles.Remove(Roles);
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
