
<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EvansTocker_Proyecto_PrograA.Dashboard" %>

<asp:Content ID="ContentHead3" ContentPlaceHolderID="HeadContent3" runat="server">
    <!-- Esto llama el CSS solo en esta página (Dashboard.css) -->
    <link href="Estilos/Dashboard.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="bienvenida-container"> 
    <h1>Bienvenido</h1> 
    <label id="lblNombreUsuario">*Nombre*</label>
        </div>

    <div id="second-content">
        <h1>Mis citas</h1>
        <div>
            <table class="table table-hover">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nombre</th>
                        <th>Servicio</th>
                        <th>Fecha</th>
                        <th>Hora</th>
                        <th>Barbero/Cliente</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpCitas" runat="server">
                        <ItemTemplate>
                            <tr id='<%# "filaCita" + Eval("CitaId") %>'>
                                <td><%# Eval("CitaId") %></td>
                                <td><%# Eval("Nombre") %></td>
                                <td><%# Eval("Servicio") %></td>
                                <td><%# Eval("Fecha", "{0:yyyy-MM-dd}") %></td>
                                <td><%# Eval("Hora", "{0:hh\\:mm}") %></td>
                                <td><%# Eval("BarberoCliente") %></td>
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
        </div>
        <!-- luego aca va el script para los onclick de los botones EDITAR y ELIMINAR para que funcionen -->
    </div>
    <div id="first-content">
        <h1>Agendar una cita</h1>
        <!-- Formulario para agregar o editar una cita -->
        <div class="form-inline">
            <asp:TextBox ID="txtCitaId" runat="server" placeholder="ID de Cita" Style="display: none" ClientIDMode="Static" />
            <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre del Cliente" Width="200px" class="form-control" ClientIDMode="Static" />

            <asp:DropDownList ID="ddlServicio" runat="server" class="form-control" Width="160px" ClientIDMode="Static">
                <asp:ListItem Text="Seleccionar Servicio" Value="" />
                <asp:ListItem Text="Corte Clásico" Value="1" />
                <asp:ListItem Text="Barba Completa" Value="2" />
                <asp:ListItem Text="Tinte" Value="3" />
            </asp:DropDownList>

            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" Width="170px" class="form-control" ClientIDMode="Static" />

            <asp:TextBox ID="txtHora" runat="server" TextMode="Time" Width="130px" class="form-control" ClientIDMode="Static" />

            <asp:DropDownList ID="ddlBarberoCliente" runat="server" class="form-control" Width="180px" ClientIDMode="Static">
                <asp:ListItem Text="Seleccionar Barbero" Value="" />
                <asp:ListItem Text="Juan Pérez" Value="1" />
                <asp:ListItem Text="Carlos Gómez" Value="2" />
                <asp:ListItem Text="Esteban Ramírez" Value="3" />
            </asp:DropDownList>

             </div>
        <asp:Button ID="btnAgregarCita" runat="server" Text="Agregar Cita" OnClick="btnAgregarCita_Click" CssClass="btn-AgregarCita" />
      

    </div>

    <asp:Button ID="BtnCerrarSesion" runat="server" CssClass="btn-CerrarSesion" Text="Cerrar Sesión" OnClick="BtnCerrarSesion_Click"/>
              
</asp:Content>
