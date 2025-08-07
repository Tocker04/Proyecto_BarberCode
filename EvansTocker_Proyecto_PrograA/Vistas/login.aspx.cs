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

namespace EvansTocker_Proyecto_PrograA.Vistas
{
    public partial class login : System.Web.UI.Page
    {
        //Registrar Usuarios
        private static List<UsuarioDTO> UsuarioO;
        private static List<RolesDTO> Roles;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRol.Text = "3";  // Siempre asignar rol 3 para registro
            }
        }

        protected void BtnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUsuarioLog.Text.Trim();
            string contraseniaIngresada = txtContraseniaLog.Text.Trim();

            // Validar campos vacíos
            if (string.IsNullOrEmpty(usuarioIngresado) || string.IsNullOrEmpty(contraseniaIngresada))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alertaCampos", "alert('Por favor, complete ambos campos.');", true);
                return;
            }

            // Consultar usuarios registrados
            List<UsuarioDTO> usuarios = UsuarioService.ConsultarUsuarios();

            // Buscar coincidencia
            var usuarioValido = usuarios.FirstOrDefault(u =>
                u.usuario == usuarioIngresado && u.Contrasenia == contraseniaIngresada);

            if (usuarioValido != null)
            {
                // ✅ Guardar datos en sesión
                Session["UsuarioId"] = usuarioValido.UsuarioId;
                Session["NombreUsuario"] = usuarioValido.Nombre;
                Session["RolId"] = usuarioValido.RolesId.RolId;
                Session["Correo"] = usuarioValido.Correo;

                // Redirigir al Dashboard
                Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
                // ❌ Usuario o contraseña incorrectos
                ClientScript.RegisterStartupScript(this.GetType(), "alertaError", "alert('Usuario o contraseña incorrectos.');", true);
            }
        }

        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            UsuarioDTO UsuarioDTO = new UsuarioDTO();
            // Asignar siempre el rol de "Cliente" (RolId = 3) al registrar nuevos usuarios
            UsuarioDTO.RolesId = new RolesDTO
            {
                RolId = 3
            };
            UsuarioDTO.usuario = txtUsuario.Text;
            UsuarioDTO.Contrasenia = txtContrasenia.Text;
            UsuarioDTO.Nombre = txtNombre.Text;
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

            bool Resultado = false;
            if (txtUsuarioId.Text.Equals(""))
            {
                Resultado = UsuarioService.AgregarUsuario(UsuarioDTO);
            }

            if (Resultado)
            {
                // Enviar SMS de confirmación
                EnviarSmsConfirmacion(UsuarioDTO.Telefono, UsuarioDTO.Nombre);



                // ✅ Recargar los datos en el Repeater directamente
                Response.Redirect("login.aspx"); // último
            }
            // Limpiar campos
            LimpiarTextBoxRegistro();
            //CAMBIO 2
        }

        //1)Para ver traer el consultar Usuarios de BLL
        private List<UsuarioDTO> ObtenerUsuarios()
        {
            return UsuarioService.ConsultarUsuarios();
        }
        
       

        //3)se recarga la tabla de Usuarios en caso de que se edite, agregue o elimine algun Usuario
        private void RecargarUsuarios()
        {
            UsuarioO = ObtenerUsuarios();
        }

        //4) Limpiar campos de texto
        private void LimpiarTextBoxRegistro()
        {
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
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
                //el mensaje se enviara a mi numero solamente, porque la cuenta de Twilio es version de prueba,
                //entonces solo deja mandar a mensajes a numeros verficados/agregados en la pagina de Twilio
            }
            catch (Exception ex)
            {
                // Puedes registrar el error o mostrarlo en pantalla si quieres
                System.Diagnostics.Debug.WriteLine("Error al enviar SMS: " + ex.Message);
            } //cambio
        }


    }
}