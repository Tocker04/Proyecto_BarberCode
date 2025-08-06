<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="EvansTocker_Proyecto_PrograA.Vistas.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />
    <link rel="stylesheet" href="../Estilos/login.css"/>
</head>

<body>
    <form id="form1" runat="server">
        <h1 id="title1">
            <img src="../Imagenes/posteBarber.png" alt="Ícono de barbería" class="icono-barberia"/>
            <span>BarberCode</span> 
            <img src="../Imagenes/posteBarber.png" alt="Icono barbería" class="icono-barberia"/>
        </h1> <br />
       <div class="container" id="container">
            <div class="form-container sign-up"> <!-- Registro de usuario-->
                <div class="form-content">
                    <h1>Crear una cuenta</h1>
                    <div class="social-icons">
                      <!--  <a href="#" class="icon"><i class="fa-brands fa-google-plus-g"></i></a>
                        <a href="#" class="icon"><i class="fa-brands fa-facebook-f"></i></a>
                        <a href="#" class="icon"><i class="fa-brands fa-github"></i></a>
                        <a href="#" class="icon"><i class="fa-brands fa-linkedin-in"></i></a> -->
                    </div>
                    <span>Ingrese la siguiente información</span>
                    <!--<asp:TextBox ID="txtRol" runat="server" CssClass="input-estilo" Text="3" Visible="false"></asp:TextBox> -->
                    <asp:TextBox ID="txtUsuarioId" runat="server" CssClass="input-estilo"  Visible="false" Placeholder="Usuario"></asp:TextBox>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="input-estilo" Placeholder="Usuario"></asp:TextBox>
                    <asp:TextBox ID="txtContrasenia" runat="server" CssClass="input-estilo" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="input-estilo" Placeholder="Nombre"></asp:TextBox>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="input-estilo" TextMode="Email" Placeholder="Correo"></asp:TextBox>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="input-estilo" TextMode="Phone" Placeholder="Teléfono"></asp:TextBox>
                   <!--  <button type="button">Registrarse</button> -->
                     <asp:Button ID="BtnRegistrar" runat="server" CssClass="btn-registrar" Text="Registrarse" OnClick="BtnRegistrar_Click"/>
                </div>
            </div>
           
            <div class="form-container sign-in">   <!-- Inicio de sesion de usuario-->
                <div class="form-content">
                    <h1>Iniciar Sesión</h1>
                    <div class="social-icons">
                       <!-- <a href="#" class="icon"><i class="fa-brands fa-google-plus-g"></i></a>
                        <a href="#" class="icon"><i class="fa-brands fa-facebook-f"></i></a>
                        <a href="#" class="icon"><i class="fa-brands fa-github"></i></a>
                        <a href="#" class="icon"><i class="fa-brands fa-linkedin-in"></i></a> -->
                    </div>
                    <span>Ingrese sus datos</span>
                    <asp:TextBox ID="txtUsuarioLog" runat="server" CssClass="input-estilo" Placeholder="Usuario"></asp:TextBox>
                    <asp:TextBox ID="txtContraseniaLog" runat="server" CssClass="input-estilo" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>
                    <!-- <a href="#">¿Olvido su contraseña?</a> -->
                    <asp:Button ID="BtnIniciarSesion" runat="server" CssClass="btn-iniciar" Text="Iniciar Sesión" OnClick="BtnIniciarSesion_Click"/>
                </div>
            </div>
           

           <!-- Banners laterales-->

            <div class="toggle-container">
                <div class="toggle">
                    <div class="toggle-panel toggle-left"> <!-- Banner que sale en Registrarse-->
                        <h1>¡Bienvenido de nuevo!</h1>
                        <p>Ingrese sus datos para disfrutar de las funcionalidades</p>
                        <button class="hidden" id="login" type="button">Iniciar Sesión</button>
                    </div>
                    <div class="toggle-panel toggle-right"> <!-- Banner que sale en Iniciar Sesión-->
                        <h1>¿Qué tal?</h1>
                        <p>Regristre sus datos para disfrutar de las funcionalidades</p>
                        <button class="hidden" id="register" type="button">Registrarse</button>
                    </div>
                </div>
            </div>
        </div>
        

        <script src="../Scripts_1/login.js"></script>

    </form>
</body>
</html>
