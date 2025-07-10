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
        //De Gestion de Usuarios//
        private static List<UsuarioDTO> UsuarioO;
        private static List<RolesDTO> Roles;
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
                    EliminarUsuario(long.Parse(id));
                }
            }
            if (!IsPostBack)
            {
                //Usuario
                CargarRoles();
                RecargarUsuarios();//cargar la los datos de la bd (tabla Usuario) a la pagina de gestionar Usuarios
                //Servicio
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
            
            UsuarioDTO UsuarioDTO = new UsuarioDTO();
            UsuarioDTO.RolesId = ObtenerRolesPorNombre(ddlRol.SelectedValue);
            UsuarioDTO.usuario = txtUsuario.Text;
            UsuarioDTO.Contrasenia = txtContrasenia.Text;
            UsuarioDTO.Nombre = txtNombreCompleto.Text;
            UsuarioDTO.Correo = txtCorreo.Text;
            UsuarioDTO.Telefono = txtTelefono.Text;

            bool Resultado;
            if (txtUsuarioId.Text.Equals(""))
            {
                Resultado = UsuarioService.AgregarUsuario(UsuarioDTO);
            }
            else
            {
                UsuarioDTO.UsuarioId = long.Parse(txtUsuarioId.Text);
                Resultado = UsuarioService.ModificarUsuario(UsuarioDTO);
            }
            if (Resultado)
            {
                Response.Redirect("Gestion.aspx");
            }

            // Limpiar los TextBox después de agregar el producto exitosamente
            LimpiarTextBoxUsuario();


        }


        //0)Metodo para cargar la tabla(de BD) de Roles en el ddlRol y obtener el nombre
        private void CargarRoles()
        {
            Roles = RolesService.ConsultarRolesS();
            ddlRol.Items.Clear();
            foreach (RolesDTO Roles in Roles)
            {
                ddlRol.Items.Add(Roles.Nombre);
            }
        }

        //1)Para ver traer el consultar Usuarios de BLL
        private List<UsuarioDTO> ObtenerUsuarios()
        {
            return UsuarioService.ConsultarUsuarios();
        }
        //2)Cargar los datos de la tabla Usuarios a la tabla, en Gestionar Usuarios
        private void CargarDatosTablaUsuarios(List<UsuarioDTO> Usuarios)
        {
            rpUsuarios.DataSource = Usuarios;
            rpUsuarios.DataBind();
        }

        //3)se recarga la tabla de Usuarios en caso de que se edite, agregue o elimine algun Usuario
        private void RecargarUsuarios()
        {
            UsuarioO = ObtenerUsuarios();
            CargarDatosTablaUsuarios(UsuarioO);
        }

        //4) Limpiar campos de texto
        private void LimpiarTextBoxUsuario() { 
        txtUsuarioId.Text = "";
        ddlRol.ClearSelection(); // Limpiamos la selección del DropDownList
        txtUsuario.Text = "";
        txtContrasenia.Text = "";
        txtNombreCompleto.Text = "";
        txtCorreo.Text = "";
        txtTelefono.Text = "";
        }

        //5) Obtener roles por nombre para guardar
        private RolesDTO ObtenerRolesPorNombre(string nombre)
        {
            if (Roles != null)
            {
                foreach (RolesDTO Roles in Roles)
                {
                    if (Roles.Nombre.Equals(nombre))
                    {
                        return Roles;
                    }
                }
            }
            return null;
        }

        //6)Eliminar Usuario de la tabla y de la BD directamente
        private bool EliminarUsuario(long id)
        {
            return BarberCode_BLL.UsuarioService.EliminarUsuario(id);
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
            LimpiarTextBoxServicio();

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


        private void LimpiarTextBoxServicio()
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