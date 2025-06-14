<%@ Page Title="Catalogo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="EvansTocker_Proyecto_PrograA.Catalogo" %>

<asp:Content ID="ContentHead4" ContentPlaceHolderID="HeadContent3" runat="server">
    <!-- Esto llama el CSS solo en esta página (Catalogo.css) -->
    <link href="Estilos/Catalogo.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="first-content"> 
    <h1>Encuentra tu Estilo Ideal</h1>
    <p> Sabemos que elegir un nuevo corte o look puede ser difícil. Por eso, en BarberCode te ofrecemos esta galería 
        con estilos modernos, clásicos y atrevidos que están marcando tendencia. Aquí podrás ver distintos tipos de cortes, 
        degradados, estilos con barba y opciones de tintes para inspirarte y descubrir qué look va más con tu personalidad 
        y tus gustos. ¿Te gustó alguno? Mostráselo a tu barbero al llegar o reservá tu cita desde ya.  
        Estamos listos para hacer realidad tu próximo estilo.</p>
    </div>

    <div id="catalogo-container">

        <div class="card">
            <label>Permanente Zoomer</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2025/01/corte-brocoli-hombre.jpg"
                 alt="Permanente Zoomer" />
        </div>

         <div class="card">
            <label>Slicked Back</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2025/01/corte-slicked-back.jpg" 
                 alt="Slicked Back" />
        </div>

         <div class="card">
            <label>Bro Flow</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2023/02/Cabello-largo-texturizado.webp" 
                 alt="Bro Flow" />
        </div>

         <div class="card">
            <label>Curly Crop Fade</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2023/02/french-crop-fade-e1738683180665.webp" 
                 alt="Curly Crop Fade" />
        </div>

         <div class="card">
            <label>Surfer Curtains</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2023/02/surfer-curtains-hair.webp" 
                 alt="Surfer Curtains" />
        </div>

         <div class="card">
            <label>Buzz Cut</label><br />
            <img src="https://i.pinimg.com/originals/fa/6d/7b/fa6d7b1a1302059e7968c7c5bebe46b0.jpg" 
                 alt="Buzz Cut" />
        </div>

         <div class="card">
            <label>Decoloración con corte Razor High Fade + Crop</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2023/02/razor-high-fade-crop-580x580.jpg" 
                 alt="Decoloración con corte Razor High Fade + Crop" />
        </div>

        <div class="card">
            <label>Mohawk Personalizado</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2023/02/mohicano-personalizado-580x580.jpg" 
                 alt="Mohawk Personalizado" />
        </div>

        <div class="card">
            <label>Rizado + Fade alto</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2023/02/pelo-rizado-fade-alto-580x580.jpg" 
                 alt="Rizado + Fade alto" />
        </div>

         <div class="card">
            <label>Mullet</label><br />
            <img src="https://menshaircuts.com/wp-content/uploads/2023/09/burst-fade-mullet-taper-curly-low-bang-mohawk-1-768x1152.jpg" 
                 alt="Mullet" />
        </div>

         <div class="card">
            <label>Skin Fade + Spiky Texture</label><br />
            <img src="https://tahecosmetics.com/trends/wp-content/uploads/2023/02/skin-fade-spiky-texture-580x580.jpg" 
                 alt="Skin Fade + Spiky Texture" />
        </div>

          <div class="card">
            <label>Taper Fade</label><br />
            <img src="https://th.bing.com/th/id/OIP.XitdwSqUeTIXIvkbtxTKXwHaHa?cb=thvnextc1&rs=1&pid=ImgDetMain" 
                 alt="Taper Fade" />
        </div>

        <div class="card">
            <label>Burst Fade</label><br />
            <img src="https://wellgroomedgentleman.com/wp-content/uploads/The-Curly-Contour-Burst-Fade-Haircut-for-Natural-Texture.jpg" 
                 alt="Burst Fade" />
        </div>

         <div class="card">
            <label>Textura Crop Top Desvanecida</label><br />
            <img src="https://estilando.com/wp-content/uploads/2022/02/1645218436_657_125-Best-Haircuts-For-Men-2022-Cuts-Styles.jpg" 
                 alt="Textura Crop Top Desvanecida" />
        </div>

          <div class="card">
            <label>Low Fade con Textura</label><br />
            <img src="https://content.latest-hairstyles.com/wp-content/uploads/low-fade-with-thick-textured-fringe-for-men.jpg" 
                 alt="Low Fade con Textura" />
        </div>

          <div class="card">
            <label>Fade con Diseño (Hair Tattoo)</label><br />
            <img src="https://th.bing.com/th/id/R.c8f0dae393da39c29c3362b2f073e34e?rik=QtnykFMKMdJB1g&riu=http%3a%2f%2fmachohairstyles.com%2fwp-content%2fuploads%2f2016%2f01%2fHaircut-Designs-for-Men-53-650x650.jpg&ehk=typcZHRp2YRHzStY69XGxs6J120tfGCUyLjjlUzDgaw%3d&risl=&pid=ImgRaw&r=0" 
                 alt="Fade con Diseño (Hair Tattoo)" />
        </div>





    </div>
</asp:Content>
