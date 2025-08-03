using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BarberCode_BLL;
using BarberCode_BLL.Modelo;

using WebListItem = System.Web.UI.WebControls.ListItem;

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
        /// 
        private static List<CitaDTO> CitaA;

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
                else if (accion.Equals("eliminarCita")) //Eliminar Cita
                {
                    EliminarCita(long.Parse(id));
                }
            }
            if (!IsPostBack)
            {
                //Usuario
                CargarRoles();
                RecargarUsuarios();//cargar la los datos de la bd (tabla Usuario) a la pagina de gestionar Usuarios
               
                //Servicio
                RecargarServicios();//cargar la los datos de la bd (tabla Servicios) a la pagina de gestionar Servicios

                //Cita
                CargarClientes();//carcgar los clientes registrados de la bd
                CargarBarberos(); //cargar los barberos registrados de la bd
                CargarServicios();//cargar los servicios registrados de la bd
                RecargarCitas();//cargar la los datos de la bd (tabla Citas) a la pagina de gestionar Citas
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
            CitaDTO CitaDTO = new CitaDTO();
            CitaDTO.UsuarioCli = ObtenerUsuarioPorNombre(ddlNombreCliente.SelectedValue);
            CitaDTO.Servicio = ObtenerServicioPorNombre(ddlServicio.SelectedValue);

            //manejo de errores en caso de fecha o hora mal escritas
            if (!DateTime.TryParse(txtFecha.Text, out DateTime fecha))
            {
                // Mostrar mensaje de error o validación
                return;
            }
            if (!TimeSpan.TryParse(txtHora.Text, out TimeSpan hora))
            {
                // Mostrar mensaje de error o validación
                return;
            }

            CitaDTO.Fecha = fecha;
            CitaDTO.Hora = hora;
            CitaDTO.UsuarioBar = ObtenerUsuarioPorNombre(ddlBarbero.SelectedValue);


            //cambiosss
            // ❗ Validación para evitar doble cita al mismo barbero
            // Validar existencia de cita duplicada
            long? citaIdActual = null;
            if (!string.IsNullOrWhiteSpace(txtCitaId.Text))
            {
                citaIdActual = long.Parse(txtCitaId.Text);
            }

            if (CitaYaExiste(CitaDTO.UsuarioBar.UsuarioId, fecha, hora, citaIdActual))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", "alert('Ya existe una cita con ese barbero a esa hora.');", true);
                return;
            }
            // Insertar o modificar
            bool Resultado;
            if (citaIdActual == null)
            {
                Resultado = CitaService.AgregarCita(CitaDTO);
            }
            else
            {
                CitaDTO.CitaId = citaIdActual.Value;
                Resultado = CitaService.ModificarCita(CitaDTO);
            }

            if (Resultado)
                Response.Redirect("Gestion.aspx");

            LimpiarTextBoxCita();


        }

        //0)Metodo para cargar la tabla(de BD) de Usuarios en el ddlNombreCliente y obtener el nombre
        private void CargarClientes()
        {
            UsuarioO = UsuarioService.ConsultarUsuarios();
            ddlNombreCliente.Items.Clear();

            // Agregar manualmente la opción por defecto
            ddlNombreCliente.Items.Add(new WebListItem("Seleccionar Cliente", ""));
            foreach (UsuarioDTO Usuario in UsuarioO)
            {
                // Verificamos que el rol del usuario sea "cliente" (rolId = 3)
                if (Usuario.RolesId != null && Usuario.RolesId.RolId == 3)
                {
                    ddlNombreCliente.Items.Add(Usuario.Nombre);
                    //ddlNombreCliente.Items.Add(new WebListItem(Usuario.Nombre, Usuario.UsuarioId.ToString()));
                }
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



        //1)Para ver traer el consultar Citas de BLL
        private List<CitaDTO> ObtenerCitas()
        {
            return CitaService.ConsultarCitas();
        }
        //2)Cargar los datos de la tabla Citas a la tabla, en Gestionar Citas
        private void CargarDatosTablaCitas(List<CitaDTO> Citas)
        {
            rpCitas.DataSource = Citas;
            rpCitas.DataBind();
        }

        //3)se recarga la tabla de Citas en caso de que se edite, agregue o elimine alguna Cita
        private void RecargarCitas()
        {
            CitaA = ObtenerCitas();
            CargarDatosTablaCitas(CitaA);
        }

        //4) Limpiar campos de texto
        private void LimpiarTextBoxCita()
        {
            txtCitaId.Text = "";
            ddlNombreCliente.ClearSelection(); // Limpiamos la selección del DropDownList
            ddlServicio.ClearSelection(); // Limpiamos la selección del DropDownList
            txtFecha.Text = "";
            txtHora.Text = "";
            ddlBarbero.ClearSelection();// Limpiamos la selección del DropDownList
        }

        //5) Obtener Usuarios por nombre para guardar
        private UsuarioDTO ObtenerUsuarioPorNombre(string nombre)
        {
            if (UsuarioO != null)
            {
                foreach (UsuarioDTO Usuario in UsuarioO)
                {
                    if (Usuario.Nombre.Equals(nombre))
                    {
                        return Usuario;
                    }
                }
            }
            return null;
        } //tal vez haya que hacer uno para cliente y otro para barbero

        //6) Obtener Servicios por nombre para guardar
        private ServicioDTO ObtenerServicioPorNombre(string nombre)
        {
            if (ServicioSS != null)
            {
                foreach (ServicioDTO Servicio in ServicioSS)
                {
                    if (Servicio.Nombre.Equals(nombre))
                    {
                        return Servicio;
                    }
                }
            }
            return null;
        }

        //7)Eliminar Cita de la tabla y de la BD directamente
        private bool EliminarCita(long id)
        {
            return BarberCode_BLL.CitaService.EliminarCita(id);
        }

        //8) Validacion de cita, verificar antes de crear otra cita, que no sea a la misma hora y dia que una ya existente
        private bool CitaYaExiste(long barberoId, DateTime fecha, TimeSpan hora, long? citaIdActual = null)
        {
            var citas = CitaService.ConsultarCitas(); // Obtiene todas las citas

            foreach (var cita in citas)
            {
                // Verifica si coincide barbero, fecha y hora
                if (cita.UsuarioBar.UsuarioId == barberoId &&
                    cita.Fecha.Date == fecha.Date &&
                    cita.Hora == hora)
                {
                    // Si estamos modificando, ignorar la misma cita
                    if (citaIdActual != null && cita.CitaId == citaIdActual)
                        continue;

                    return true; // Ya existe una cita en ese horario con ese barbero
                }
            }

            return false; // No existe, se puede registrar
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