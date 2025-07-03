using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberCode_DAL.Entidad;
using System.Data.Entity;

namespace BarberCode_DAL.Util
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext() : base("ConexionBD")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
