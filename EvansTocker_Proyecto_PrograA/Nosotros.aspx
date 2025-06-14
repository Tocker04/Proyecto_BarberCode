<%@ Page Title="Nosotros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Nosotros.aspx.cs" Inherits="EvansTocker_Proyecto_PrograA.Nosotros" %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Esto llama el CSS solo en esta página (Nosotros.css) -->
    <link href="Estilos/Nosotros.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="main-content"> 
    <div id="nosotros">
        <h1>Sobre nosotros</h1>
        <p>
            En BarberCode, combinamos tradición y tecnología para ofrecer una experiencia moderna en el arte de la barbería. 
        Nuestro objetivo es brindar un servicio de calidad que vaya más allá del corte: buscamos crear un ambiente donde 
        cada cliente se sienta valorado, cómodo y bien atendido. Nacimos con la visión de transformar la forma en que se 
        gestionan las citas en barberías, facilitando a nuestros clientes reservar su espacio desde cualquier lugar, 
        en cualquier momento. Al mismo tiempo, mantenemos el trato personalizado, el estilo auténtico y la atención al 
        detalle que distinguen a una buena barbería. Contamos con un equipo profesional, apasionado por su oficio,
        y comprometido con ayudarte a lucir y sentirte bien. BarberCode es más que una barbería. 
        Es un espacio donde tu imagen importa, tu tiempo se respeta y tu estilo se perfecciona.
        </p>
    </div>

    <div id="ubicacion">
        <h1>Nuestra Ubicación</h1>
        <p>
            Te esperamos en el corazón de <strong>Guápiles de Limón</strong>, en un espacio pensado para tu comodidad y 
            estilo. En BarberCode, nos encontramos en una zona de fácil acceso, con parqueo cercano y rodeados de buen 
            ambiente.
            
            📍 Dirección exacta: 50 metros norte de la clínica Dr. Saborío, C. 6, Limón, Guápiles.
            
            Nuestro local está equipado para brindarte una experiencia moderna, cómoda y eficiente, desde tu llegada 
            hasta el momento en que sales renovado.
            
            ¿No sabes cómo llegar? También puedes encontrarnos fácilmente en Google Maps, o comunicarte con nosotros 
            si necesitas ayuda para ubicarte.
        </p>

        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1414.4940474490695!2d-83.79653865170181!3d10.20776746184252!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8fa0b868e872eb1f%3A0x33d4800ea6d1fca3!2sUISIL!5e1!3m2!1ses-419!2scr!4v1749685121088!5m2!1ses-419!2scr"
            width="600" height="450" style="border: 0;" allowfullscreen="" loading="lazy"
            referrerpolicy="no-referrer-when-downgrade"></iframe>
    </div>

    <div id="contacto">
        <h1>Contáctanos</h1>
        <p>
            ¿Tienes dudas, deseas agendar una cita o simplemente quieres saludarnos? 
            En BarberCode siempre estamos disponibles para vos.
        </p>
        <h4>📞 Teléfono / WhatsApp:</h4>
        <p>+506 8888-8888</p>

        <h4>📧 Correo electrónico:</h4>
        <p>info@barbercode.cr</p>

        <h4>🌐 Redes sociales:</h4>
        <p>Síguenos y mantente al tanto de nuestras promociones, estilos y horarios especiales:</p>
        <ul>
            <li><i class="bi bi-facebook icono-red"></i>Facebook: @BarberCodeCR</li>
            <li><i class="bi bi-instagram icono-red"></i>Instagram: @barbercode_cr</li>
            <li><i class="bi bi-tiktok icono-red"></i>TikTok: @barbercodecr</li>
        </ul>
    </div>

        </div>
</asp:Content>
