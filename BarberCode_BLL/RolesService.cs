using BarberCode_BLL.Modelo;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BarberCode_BLL
{
    public class RolesService
    {
        public static List<RolesDTO> ConsultarRolesS()
        {
            List<BarberCode_DAL.Dto.RolesDTO> RolesS = BarberCode_DAL.RolesService.ConsultarRolesS();
            List<RolesDTO> RolesSDTOs = new List<RolesDTO>();
            if (RolesS != null)
            {
                foreach (BarberCode_DAL.Dto.RolesDTO Roles in RolesS)
                {
                    RolesSDTOs.Add(new RolesDTO(Roles));
                }
            }
            return RolesSDTOs;
        }

    }
}
