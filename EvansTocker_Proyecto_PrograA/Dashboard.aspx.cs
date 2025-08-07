using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BarberCode_BLL;
using BarberCode_BLL.Modelo;


//Para enviar SMS de confirmacion con API Twilio
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

using WebListItem = System.Web.UI.WebControls.ListItem;

namespace EvansTocker_Proyecto_PrograA
{
    public partial class Dashboard : Page
    {
        //Servicios//
        private static List<ServicioDTO> ServicioSS;
        //Usuarios barberos//
        private static List<UsuarioDTO> UsuarioO;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NombreUsuario"] != null)
                {
                    lblNombreUsuario.Text = Session["NombreUsuario"].ToString();
                }
                else
                {
                    // Redirige si no hay sesión activa
                    Response.Redirect("Login.aspx");
                }

                CargarServicios();//cargar la los datos de la bd (tabla Citas) a la pagina de gestionar Citas
                CargarBarberos(); //cargar los barberos registrados de la bd
            }
        }

        protected void btnAgregarCita_Click(object sender, EventArgs e)
        {
            //aun no tiene nada

        }

        //0)Metodo para cargar la tabla(de BD) de Servicios en el dllBarbero y obtener el nombre
        private void CargarServicios()
        {
            ServicioSS = ServicioService.ConsultarServicios();
            ddlServicio.Items.Clear();

            // Agregar manualmente la opción por defecto
            ddlServicio.Items.Add(new WebListItem("Seleccionar Servicio", ""));
            foreach (ServicioDTO Servicio in ServicioSS)
            {
                ddlServicio.Items.Add(Servicio.Nombre);
                //ddlServicio.Items.Add(new WebListItem(Servicio.Nombre, Servicio.ServicioId.ToString()));

            }
        }

        //0)Metodo para cargar la tabla(de BD) de Usuarios en el dllBarbero y obtener el nombre
        private void CargarBarberos()
        {
            UsuarioO = UsuarioService.ConsultarUsuarios();
            ddlBarbero.Items.Clear();

            // Agregar manualmente la opción por defecto
            ddlBarbero.Items.Add(new WebListItem("Seleccionar Barbero", ""));
            foreach (UsuarioDTO Usuario in UsuarioO)
            {
                // Verificamos que el rol del usuario sea "barbero" (rolId = 2)
                if (Usuario.RolesId != null && Usuario.RolesId.RolId == 2)
                {
                    ddlBarbero.Items.Add(Usuario.Nombre);
                    //ddlBarbero.Items.Add(new WebListItem(Usuario.Nombre, Usuario.UsuarioId.ToString()));
                }
            }
        }




        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {

            // Redirige a INDEX
            Session.Clear(); // Borra todas las variables de sesión
            Response.Redirect("~/Index.aspx");
        }
    }
}