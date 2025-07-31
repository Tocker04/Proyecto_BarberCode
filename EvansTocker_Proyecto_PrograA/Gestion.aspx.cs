using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BarberCode_BLL;
using BarberCode_BLL.Modelo;

using iTextSharp.text;
using iTextSharp.text.pdf; //para PDF
using System.IO;

//Para enviar SMS de confirmacion con API Twilio
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

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

                //cambio 3
                // Solo elimina lo que corresponde según el valor de 'accion'
                if (accion.Equals("eliminarUsuario")) //Eliminar usuario 
                {
                    EliminarUsuario(long.Parse(id));
                }
                else if (accion.Equals("eliminarServicio")) //Eliminar Servicio
                {
                    EliminarServicio(long.Parse(id));
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

        /////////////////////PARA GENERAR REPORTE EN PDF DE LO QUE HAY EN BD///////////////// 
        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);

            document.Open();

            // Encabezado
            document.Add(new Paragraph("REPORTE GENERAL", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD)));
            document.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));
            document.Add(new Paragraph(" "));

            // Título: Usuarios Registrados
            Paragraph tituloUsuarios = new Paragraph("Usuarios Registrados", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
            tituloUsuarios.SpacingBefore = 15f;
            document.Add(tituloUsuarios);

            PdfPTable tablaUsuarios = new PdfPTable(7);
            tablaUsuarios.WidthPercentage = 100;

            tablaUsuarios.AddCell("ID");
            tablaUsuarios.AddCell("Rol");
            tablaUsuarios.AddCell("Usuario");
            tablaUsuarios.AddCell("Contraseña");
            tablaUsuarios.AddCell("Nombre");
            tablaUsuarios.AddCell("Correo");
            tablaUsuarios.AddCell("Teléfono");

            foreach (var usuario in UsuarioService.ConsultarUsuarios())
            {
                tablaUsuarios.AddCell(usuario.UsuarioId.ToString());
                tablaUsuarios.AddCell(usuario.RolesId?.Nombre ?? "");
                tablaUsuarios.AddCell(usuario.usuario);
                tablaUsuarios.AddCell("********");
                tablaUsuarios.AddCell(usuario.Nombre);
                tablaUsuarios.AddCell(usuario.Correo);
                tablaUsuarios.AddCell(usuario.Telefono);
            }

            document.Add(tablaUsuarios);

            // Título: Citas Agendadas
            Paragraph tituloCitas = new Paragraph("Citas Agendadas", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
            tituloCitas.SpacingBefore = 20f;
            document.Add(tituloCitas);

            PdfPTable tablaCitas = new PdfPTable(6);
            tablaCitas.WidthPercentage = 100;

            tablaCitas.AddCell("ID");
            tablaCitas.AddCell("Nombre");
            tablaCitas.AddCell("Servicio");
            tablaCitas.AddCell("Fecha");
            tablaCitas.AddCell("Hora");
            tablaCitas.AddCell("Barbero");

            foreach (var cita in CitaService.ConsultarCitas())
            {
                tablaCitas.AddCell(cita.CitaId.ToString());
                // tablaCitas.AddCell(cita.UsuarioCli); // si tuvieras nombre de cliente
                // tablaCitas.AddCell(cita.Servicio);   // si tuvieras nombre de servicio
                tablaCitas.AddCell(cita.Fecha.ToString("dd/MM/yyyy"));
                tablaCitas.AddCell(cita.Hora.ToString(@"hh\:mm"));
                // tablaCitas.AddCell(cita.BarberoCliente); // si tuvieras nombre de barbero
            }

            document.Add(tablaCitas);

            // Título: Servicios Registrados
            Paragraph tituloServicios = new Paragraph("Servicios Registrados", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
            tituloServicios.SpacingBefore = 20f;
            document.Add(tituloServicios);

            PdfPTable tablaServicios = new PdfPTable(4);
            tablaServicios.WidthPercentage = 100;

            tablaServicios.AddCell("ID");
            tablaServicios.AddCell("Nombre");
            tablaServicios.AddCell("Descripción");
            tablaServicios.AddCell("Precio");

            foreach (var servicio in ServicioService.ConsultarServicios())
            {
                tablaServicios.AddCell(servicio.ServicioId.ToString());
                tablaServicios.AddCell(servicio.Nombre);
                tablaServicios.AddCell(servicio.Descripcion);
                tablaServicios.AddCell("₡" + servicio.Precio.ToString("N2"));
            }

            document.Add(tablaServicios);

            document.Close();
            writer.Close();

            // Descargar PDF
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteGestion.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
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



            // ✅ Convertir a formato internacional si no empieza con "+"
            if (!UsuarioDTO.Telefono.StartsWith("+"))
            {
                UsuarioDTO.Telefono = "+506" + UsuarioDTO.Telefono.TrimStart('0');
            }

            // ⚠️ Validar si ya existe un usuario con el mismo correo
            if (UsuarioService.ExisteCorreo(UsuarioDTO.Correo))
            {
                // ❌ Mostrar alerta flotante
                ClientScript.RegisterStartupScript(this.GetType(), "alertaCorreo", "alert('Ya existe un usuario registrado con este correo.');", true);
                return;
            }

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
                // Enviar SMS de confirmación
                EnviarSmsConfirmacion(UsuarioDTO.Telefono, UsuarioDTO.Nombre);

               

                // ✅ Recargar los datos en el Repeater directamente
                Response.Redirect("Gestion.aspx"); // último
            }
            // Limpiar campos
            LimpiarTextBoxUsuario();
            //CAMBIO 2
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

        //7) Enviar confirmacion por SMS usando Twilio
        private void EnviarSmsConfirmacion(string telefono, string nombre)
        {
            // Leer credenciales desde web.config
            try
            {
                string accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
                string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
                string fromNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];

                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber(telefono);
                var from = new PhoneNumber(fromNumber);

                var message = MessageResource.Create(
                    to: to,
                    from: from,
                    body: $"Hola {nombre}, tu usuario ha sido creado exitosamente en BarberCode. ¡Bienvenido!");
            }
            catch (Exception ex)
            {
                // Puedes registrar el error o mostrarlo en pantalla si quieres
                System.Diagnostics.Debug.WriteLine("Error al enviar SMS: " + ex.Message);
            } //cambio
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