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
        //Cita//
        private static List<CitaDTO> CitaA;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["accion"]) && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string accion = Request.QueryString["accion"];
                string id = Request.QueryString["id"];

                //cambio 3
                // Solo elimina lo que corresponde según el valor de 'accion'
                if (accion.Equals("eliminarCita")) //Eliminar Cita
                {
                    EliminarCita(long.Parse(id));
                }
            }
            if (!IsPostBack)
            {
                if (Session["NombreUsuario"] != null)
                {
                    lblNombreUsuario.Text = Session["NombreUsuario"].ToString();
                    txtNombre.Text = Session["NombreUsuario"].ToString(); // Esto llena el textbox

                    // Validar rol para permisos en el botón Agregar Cita
                    int rolId = Convert.ToInt32(Session["RolId"]);
                    if (rolId == 1 || rolId == 2) // admin o barbero  no pueden agendar una cita, para eso tienen el acceso de hacerlo desde Gestion
                    {
                        btnAgregarCita.Enabled = false;
                        btnAgregarCita.Visible = false;
                    }
                }
                else
                {
                    // Redirige si no hay sesión activa
                    Response.Redirect("Vistas/Login.aspx");
                }

                CargarServicios();//cargar la los datos de la bd (tabla Citas) a la pagina de gestionar Citas
                CargarBarberos(); //cargar los barberos registrados de la bd

                RecargarCitas(); //Cargar las citas
            }
        }

        protected void btnAgregarCita_Click(object sender, EventArgs e)
        {
            //aun no tiene nada

            if (Session["UsuarioId"] == null)
            {
                // Si no hay sesión, enviarlo al login
                Response.Redirect("Vistas/Login.aspx");
                return;
            }

            CitaDTO CitaDTO = new CitaDTO();

            // ✅ Usar siempre el usuario en sesión, no lo que está escrito en el textbox
            long usuarioIdSesion = Convert.ToInt64(Session["UsuarioId"]);
            CitaDTO.UsuarioCli = UsuarioService.ConsultarUsuarios()
                .FirstOrDefault(u => u.UsuarioId == usuarioIdSesion);


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
            {
                EnviarSmsConfirmacionCita(
                    CitaDTO.UsuarioCli.Telefono,
                    CitaDTO.UsuarioCli.Nombre,
                    CitaDTO.Fecha,
                    CitaDTO.Hora,
                    CitaDTO.UsuarioBar.Nombre
                );
                Response.Redirect("Dashboard.aspx");
            }
            LimpiarTextBoxCita();
        }

        /// /////////////////////////////////////////////////////////////////////////////////////
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

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////
      
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
            //CitaA = ObtenerCitas();
            //CargarDatosTablaCitas(CitaA);
            //cambio
            var todasLasCitas = CitaService.ConsultarCitas();

            if (Session["UsuarioId"] != null && Session["RolId"] != null)
            {
                long usuarioId = Convert.ToInt64(Session["UsuarioId"]);
                int rolId = Convert.ToInt32(Session["RolId"]);

                if (rolId == 3) // Cliente
                {
                    CitaA = todasLasCitas.Where(c => c.UsuarioCli.UsuarioId == usuarioId).ToList();
                }
                else if (rolId == 2) // Barbero
                {
                    CitaA = todasLasCitas.Where(c => c.UsuarioBar.UsuarioId == usuarioId).ToList();
                }
                else
                {
                    // Admin u otro rol, ve todo
                    CitaA = todasLasCitas;
                }
            }
            else
            {
                CitaA = new List<CitaDTO>();
            }

            CargarDatosTablaCitas(CitaA);

        }

        //4) Limpiar campos de texto
        private void LimpiarTextBoxCita()
        {
            txtCitaId.Text = "";
            txtNombre.Text = "";
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

        //Enviar SMS de confirmacion de la cita
        private void EnviarSmsConfirmacionCita(string telefono, string nombreCliente, DateTime fecha, TimeSpan hora, string nombreBarbero)
        {
            try
            {
                string accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
                string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
                string fromNumber = ConfigurationManager.AppSettings["TwilioPhoneNumber"];

                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber(telefono);
                var from = new PhoneNumber(fromNumber);

                string mensaje = $"Hola {nombreCliente}, tu cita ha sido agendada para el {fecha:dd/MM/yyyy} a las {hora:hh\\:mm} con el barbero {nombreBarbero}. ¡Gracias por preferir BarberCode!";

                var message = MessageResource.Create(
                    to: to,
                    from: from,
                    body: mensaje
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al enviar SMS: " + ex.Message);
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