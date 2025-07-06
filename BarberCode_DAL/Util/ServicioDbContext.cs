using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BarberCode_DAL.Entidad;

namespace BarberCode_DAL.Util
{
    public class ServicioDbContext : DbContext
    {
        public ServicioDbContext() : base("ConexionBD")
        {

        }

        public DbSet<Servicio> Servicios { get; set; }

    }
}
