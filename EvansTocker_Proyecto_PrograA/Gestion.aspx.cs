using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BarberCode_BLL;
using BarberCode_BLL.Modelo;


namespace EvansTocker_Proyecto_PrograA
{
    public partial class Gestion : System.Web.UI.Page
    {
        //De Gestion de Servicios//
        private static List<ServicioDTO> ServicioSS;
        /// /////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["accion"]) && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string accion = Request.QueryString["accion"];
                string id = Request.QueryString["id"];
                if (accion.Equals("eliminar"))
                {
                    EliminarServicio(long.Parse(id));
                }
            }
            if (!IsPostBack)
            {
                
                RecargarServicios();//cargar la los datos de la bd (tabla Servicios) a la pagina de gestionar Servicios
            }

        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        


        ///////////////////////////////////////////SECCION DE CITA///////////////////////////////////////////////
        protected void BtnAgregarCita_Click(object sender, EventArgs e)
        {
           //aun no tiene nada
            
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////
      
       

        ///////////////////////////////////////////SECCION DE USUARIO///////////////////////////////////////////////
        protected void BtnAgregarUsuario_Click(object sender, EventArgs e)
        {
            //aun no tiene nada

        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        


        ///////////////////////////////////////////SECCION DE SERVICIO///////////////////////////////////////////////

        //Botón de agregar Servicio
        protected void BtnAgregarServicio_Click(object sender, EventArgs e)
        {
            
            ServicioDTO ServicioDTO = new ServicioDTO();
            ServicioDTO.Nombre = txtNombreServicio.Text;
            ServicioDTO.Descripcion = txtDescripcion.Text;
            ServicioDTO.Precio = double.Parse(txtPrecio.Text);

            bool Resultado;
            if (txtServicioId.Text.Equals(""))
            {
                Resultado = ServicioService.AgregarServicio(ServicioDTO);
            }
            else
            {
                ServicioDTO.ServicioId = long.Parse(txtServicioId.Text);
                Resultado = ServicioService.ModificarServicio(ServicioDTO);
            }
            if (Resultado)
            {
                Response.Redirect("Gestion.aspx");

            }
            LimpiarTextBox();

        }

        //1)Para traer el consultar Servicios
        private List<ServicioDTO> ObtenerServicios()
        {
            return ServicioService.ConsultarServicios();
        }

        //2)Cargar los datos de la tabla Servicio a la tabla en la pagina de gestionar Servicios
        private void CargarDatosTablaServicios(List<ServicioDTO> Servicios)
        {
            rpServicios.DataSource = Servicios;
            rpServicios.DataBind();
        }

        //3)se recarga la tabla de Servicios en caso de que se edite, agregue o elimine algun Servicio
        private void RecargarServicios()
        {
            ServicioSS = ObtenerServicios();
            CargarDatosTablaServicios(ServicioSS);
        }


        private void LimpiarTextBox()
        {
            txtServicioId.Text = ""; 
            txtNombreServicio.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
        }

        //Para eliminar Servicios de la tabla y bd directamente 
        private bool EliminarServicio(long id)
        {
            return BarberCode_BLL.ServicioService.EliminarServicio(id);
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}