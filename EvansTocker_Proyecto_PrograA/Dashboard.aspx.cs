using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EvansTocker_Proyecto_PrograA
{
    public partial class Dashboard : Page
    {
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
            }
        }

        protected void btnAgregarCita_Click(object sender, EventArgs e)
        {
            //aun no tiene nada

        }

        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {

            // Redirige a INDEX
            Session.Clear(); // Borra todas las variables de sesión
            Response.Redirect("~/Index.aspx");
        }
    }
}