using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EvansTocker_Proyecto_PrograA
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            // Aquí va lo que querés que haga el botón al hacer clic.
            // Por ejemplo, redirigir a otra página:
            Response.Redirect("~/Vistas/login.aspx");
        }
    }
}