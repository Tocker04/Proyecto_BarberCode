<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EvansTocker_Proyecto_PrograA.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Bienvenido</title>
    <link rel="stylesheet" href="../Estilos/index.css"/>
</head>
<body>
    <form id="form1" runat="server">

        <div id="main-content" class="fade-in">
            <h1>¡Bienvenido!</h1>
           <!--<img src="../Imagenes/fondo.jpg" alt="fondo de barberia" class="fondo-barber"/> --> 
            <img src="../Imagenes/logo_v3.png" alt="Logo de la barberia" class="logo-barber"/>

            <asp:Button ID="BtnIngresar" runat="server" CssClass="btn-ingresar" Text="Ingresar" OnClick="BtnIngresar_Click" />
        </div>

    <div id="second-content" class="fade-in">
        <h2>Servicios</h2>

        <div id="corte-pelo"> 
            <img src="../Imagenes/pelo.jpg" alt="corte de pelo" class="corte-barber"/>
            <div class="info-servicio">
             <h3>Corte de Pelo</h3>
            <p>Elegir el corte de pelo ideal puede marcar la diferencia en tu estilo. En BarberCode, 
                te ofrecemos asesoría personalizada para que encuentres el look que mejor se adapte a tu rostro, 
                personalidad y preferencias. Nos especializamos en cortes masculinos de todos los estilos: desde los clásicos
                atemporales hasta los más modernos y vanguardistas. Nuestra experiencia y pasión por la barbería nos
                permiten brindarte un servicio de calidad en cada visita.</p>
                   </div>
                </div>
       
        <div id="barba"> 
            <img src="../Imagenes/barba.jpg" alt="corte de barba" class="barba-barber"/>
            <div class="info-servicio">
             <h3>Perfilado y Cuidado de Barba</h3>
            <p>Una barba bien cuidada no solo resalta tu imagen, también refleja estilo y personalidad. 
                En BarberCode te ayudamos a mantenerla en su mejor forma, ya sea con un perfilado preciso, 
                un recorte equilibrado o un afeitado completo. Trabajamos cada detalle con dedicación, utilizando
                técnicas profesionales y productos de calidad para garantizar suavidad, definición y un acabado limpio. 
                Ya sea que busques un estilo pulcro y elegante o algo más desenfadado, nuestro equipo te asesora para que tu 
                barba complemente tu rostro y realce tu presencia.Confía en nosotros para lograr una barba que hable bien de ti.</p>
            </div>
        </div>

        <div id="lavado">
            <img src="../Imagenes/lavado.png" alt="lavado de cabello" class="lavado-barber"/>
            <div class="info-servicio">
            <h3>Lavado de cabello</h3>
            <p>El lavado de cabello es más que una rutina: es un momento de cuidado y frescura. En BarberCode, lo convertimos 
               en una experiencia relajante y revitalizante que prepara tu cabello para cualquier servicio posterior. Utilizamos 
               productos de alta calidad que limpian profundamente, hidratan el cuero cabelludo y dejan el cabello suave, 
               manejable y con un aroma agradable. Ya sea antes de un corte o como un servicio individual, nuestro lavado 
               de cabello te brinda confort y bienestar en cada visita. Déjate consentir con una atención que va más allá de lo básico.</p>
                </div>
            </div>
        
        <div id="tinte">
            <img src="../Imagenes/tinte.png" alt="lavado de cabello" class="lavado-barber"/>
            <div class="info-servicio">
            <h3>Tintes</h3>
            <p> Renueva tu estilo con un cambio de color que hable por ti. En BarberCode, ofrecemos servicios de tintes 
                para cabello pensados especialmente para hombres que buscan destacar, cubrir canas o simplemente experimentar 
                con un nuevo look. Contamos con una gama de colores modernos y naturales, y utilizamos productos de alta 
                calidad que protegen la salud de tu cabello mientras logran resultados duraderos y vibrantes. 
                Nuestro equipo te asesora para encontrar el tono que mejor se adapte a tu estilo y personalidad.
                Atrévete a un cambio.</p>
                   </div>
            </div>

        <br />

    </div>

        <div id="third-content" class="fade-in">
            <h2><span class="icono-reloj">🕐</span>Horario de atención:</h2>
            <h3>Lunes a Sábado de 8:00 A.M a 6:00 P.M</h3><br />
        </div>

        <footer> ©2025 BarberCode Costa Rica by ETS</footer>
    </form>
    <script src="../Scripts_1/index.js"></script>
</body>
</html>
