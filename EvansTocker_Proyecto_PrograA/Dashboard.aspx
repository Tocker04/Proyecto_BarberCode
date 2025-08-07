
<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EvansTocker_Proyecto_PrograA.Dashboard" %>

<asp:Content ID="ContentHead3" ContentPlaceHolderID="HeadContent3" runat="server">
    <!-- Esto llama el CSS solo en esta página (Dashboard.css) -->
    <link href="Estilos/Dashboard.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="bienvenida-container"> 
    <h1>Bienvenido</h1> 
   <asp:Label ID="lblNombreUsuario" runat="server" Text="" CssClass="nombre-usuario"></asp:Label>
        </div>

    <div id="second-content">
        <h1>Mis citas</h1>
        <div>
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Cliente</th>
                        <th>Servicio</th>
                        <th>Fecha</th>
                        <th>Hora</th>
                        <th>Barbero</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpCitas" runat="server">
                        <ItemTemplate>
                           <tr id='<%# "filaCita" + Eval("CitaId") %>'>
                                <td><%# Eval("CitaId") %></td>
                                <td style="visibility: hidden; display: none;"><%# Eval("UsuarioCli.UsuarioId") %></td>
                                <td><%# Eval("UsuarioCli.Nombre") %></td>
                                <td style="visibility: hidden; display: none;"><%# Eval("Servicio.ServicioId") %></td>
                                <td><%# Eval("Servicio.Nombre") %></td>
                                <td><%# Eval("Fecha", "{0:yyyy-MM-dd}") %></td>
                                <td><%# Eval("Hora", "{0:hh\\:mm}") %></td>
                                <td style="visibility: hidden; display: none;"><%# Eval("UsuarioBar.UsuarioId") %></td>
                                <td><%# Eval("UsuarioBar.Nombre") %></td>
                                <td>
                                    <button type="button" class="btn btn-primary" onclick='editarCita(<%# Eval("CitaId") %>)'>
                                        <span class="glyphicon glyphicon-pencil"></span>
                                    </button>
                                    <button type="button" class="btn btn-danger" onclick='eliminarCita(<%# Eval("CitaId") %>)'>
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </button>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
             <!-- luego aca va el script para los onclick de los botones EDITAR y ELIMINAR para que funcionen -->
            <!-- Script para los onclick de los botones EDITAR y ELIMINAR para que funcionen -->
            <script>
                //Funcion al boton de editarCita
                function editarCita(idCita) {
                    var elementoId = document.getElementById('txtCitaId')
                    var elementoNombreCliente = document.getElementById('txtNombre')
                    var elementoServicio = document.getElementById('ddlServicio')
                    var elementoFecha = document.getElementById('txtFecha')
                    var elementoHora = document.getElementById('txtHora')
                    var elementoNombreBarbero = document.getElementById('ddlBarbero')

                    var elementoTr = document.getElementById('filaCita' + idCita)
                    var elementosTd = elementoTr.getElementsByTagName('td')

                    elementoId.value = elementosTd[0].textContent
                    elementoNombreCliente.value = elementosTd[2].textContent
                    elementoServicio.value = elementosTd[4].textContent
                    elementoFecha.value = elementosTd[5].textContent
                    elementoHora.value = elementosTd[6].textContent
                    elementoNombreBarbero.value = elementosTd[8].textContent

                }

                //Funcion para eliminarCita
                function eliminarCita(idCita) {
                    $.ajax({
                        url: 'Gestion.aspx?accion=eliminarCita&id=' + idCita,
                        type: 'POST',
                        success: function (response) {
                            location.reload()
                        },
                        error: function (xhr, status, error) {
                        }
                    });
                }

            </script>
        </div>      
    </div>


    <div id="first-content">
        <h1>Agendar una cita</h1>
        <!-- Formulario para agregar o editar una cita -->
        <div class="form-inline">
            <asp:TextBox ID="txtCitaId" runat="server" placeholder="ID de Cita" Style="display: none" ClientIDMode="Static" />
            <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre del Cliente" Width="200px" class="form-control" ClientIDMode="Static" />

            <asp:DropDownList ID="ddlServicio" runat="server" class="form-control" Width="160px" ClientIDMode="Static"></asp:DropDownList>

            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" Width="170px" class="form-control" ClientIDMode="Static" />

            <asp:TextBox ID="txtHora" runat="server" TextMode="Time" Width="130px" class="form-control" ClientIDMode="Static" />

            <asp:DropDownList ID="ddlBarbero" runat="server" class="form-control" Width="180px" ClientIDMode="Static">
                <asp:ListItem Text="Seleccionar Barbero" Value="" /></asp:DropDownList>

             </div>
        <asp:Button ID="btnAgregarCita" runat="server" Text="Agregar Cita" OnClick="btnAgregarCita_Click" CssClass="btn-AgregarCita" />
      

    </div>

    <asp:Button ID="BtnCerrarSesion" runat="server" CssClass="btn-CerrarSesion" Text="Cerrar Sesión" OnClick="BtnCerrarSesion_Click"/>
              
</asp:Content>
