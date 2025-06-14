using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EvansTocker_Proyecto_PrograA.Vistas
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            // Redirige al dashboard o a la página deseada
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            // Redirige al dashboard o a la página deseada
            Response.Redirect("~/Dashboard.aspx");
        }
    }
}