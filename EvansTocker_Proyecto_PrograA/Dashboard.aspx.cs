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

        }

        protected void btnAgregarCita_Click(object sender, EventArgs e)
        {
            //aun no tiene nada

        }

        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            
            // Redirige a INDEX
            Response.Redirect("~/Index.aspx");
        }
    }
}