<%@ Page Title="Gestion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="EvansTocker_Proyecto_PrograA.Gestion" %>


<asp:Content ID="ContentHead2" ContentPlaceHolderID="HeadContent2" runat="server">
    <!-- Esto llama el CSS solo en esta página (Nosotros.css) -->
    <link href="Estilos/Gestion.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 id="titulo1">Gestión</h1>

    <asp:Button ID="btnGenerarPDF" runat="server" Text="Generar PDF" OnClick="btnGenerarPDF_Click" ClientIDMode="Static" />
     <!-- //////////////////////////////////////INICIA GESTIONAR CITAS////////////////////////////////// -->
    <div id="first-content">
        <h1>Gestionar Citas</h1>

        <!-- Formulario para agregar o editar una cita -->
        <div class="form-inline">
            <asp:TextBox ID="txtCitaId" runat="server" placeholder="ID de Cita" Style="display: none" ClientIDMode="Static" />
            <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre del Cliente" Width="200px" class="form-control" ClientIDMode="Static" />
            &nbsp;&nbsp;

            <asp:DropDownList ID="ddlServicio" runat="server" class="form-control" Width="160px" ClientIDMode="Static">
                <asp:ListItem Text="Seleccionar Servicio" Value="" />
                <asp:ListItem Text="Corte Clásico" Value="1" />
                <asp:ListItem Text="Barba Completa" Value="2" />
                <asp:ListItem Text="Tinte" Value="3" />
            </asp:DropDownList>
            &nbsp;&nbsp;

            <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" Width="170px" class="form-control" ClientIDMode="Static" />
            &nbsp;&nbsp;

            <asp:TextBox ID="txtHora" runat="server" TextMode="Time" Width="130px" class="form-control" ClientIDMode="Static" />
            &nbsp;&nbsp;

            <asp:DropDownList ID="ddlBarberoCliente" runat="server" class="form-control" Width="180px" ClientIDMode="Static">
                <asp:ListItem Text="Seleccionar Barbero" Value="" />
                <asp:ListItem Text="Juan Pérez" Value="1" />
                <asp:ListItem Text="Carlos Gómez" Value="2" />
                <asp:ListItem Text="Esteban Ramírez" Value="3" />
            </asp:DropDownList>
            &nbsp;&nbsp;

            <asp:Button ID="btnAgregarCita" runat="server" Text="Agregar Cita" OnClick="BtnAgregarCita_Click" class="btn btn-success btn-lg" />
        </div>

        <br />
        <br />

        <!-- Tabla de citas -->
        <div>
            <h1>Citas Agendadas</h1>
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

       <!-- luego aca va el script para los onclick de los botones EDITAR y ELIMINAR para que funcionen -->
    

        </div>
    </div>
        
   
    <!-- //////////////////////////////////////INICIA GESTIONAR USUARIOS////////////////////////////////// -->

    <div id="second-content">

        <h1>Gestionar Usuarios</h1>

    <!-- Formulario para agregar o editar un usuario -->
    <div class="form-inline">
        <asp:TextBox ID="txtUsuarioId" runat="server" placeholder="ID de Usuario" Style="display: none" ClientIDMode="Static" />

        <asp:DropDownList ID="ddlRol" runat="server" class="form-control" Width="180px" ClientIDMode="Static">           </asp:DropDownList>
        &nbsp;&nbsp;

        <asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario" class="form-control" Width="180px" ClientIDMode="Static" />
        &nbsp;&nbsp;

        <asp:TextBox ID="txtContrasenia" runat="server" placeholder="Contraseña" TextMode="Password" class="form-control" Width="180px" ClientIDMode="Static" />
        &nbsp;&nbsp;

        <asp:TextBox ID="txtNombreCompleto" runat="server" placeholder="Nombre Completo" class="form-control" Width="220px" ClientIDMode="Static" />
        &nbsp;&nbsp;

        <asp:TextBox ID="txtCorreo" runat="server" placeholder="Correo Electrónico" class="form-control" Width="220px" ClientIDMode="Static" />
        &nbsp;&nbsp;

        <asp:TextBox ID="txtTelefono" runat="server" placeholder="Teléfono" class="form-control" Width="160px" ClientIDMode="Static" />
        &nbsp;&nbsp;

        <asp:Button ID="btnAgregarUsuario" runat="server" Text="Agregar Usuario" OnClick="BtnAgregarUsuario_Click" class="btn btn-primary btn-lg" />
   
      

    </div>

    <br />
    <br />

    <!-- Tabla de usuarios -->
    <div>
        <h1>Usuarios Registrados</h1>
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Rol</th>
                    <th>Usuario</th>
                    <th>Contraseña</th>
                    <th>Nombre</th>
                    <th>Correo</th>
                    <th>Teléfono</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rpUsuarios" runat="server">
                    <ItemTemplate>
                        <tr id='<%# "filaUsuario" + Eval("UsuarioId") %>'>
                            <td><%# Eval("UsuarioId") %></td>
                            <td style="visibility: hidden; display: none;"><%# Eval("RolesId.RolId") %></td>
                            <td><%# Eval("RolesId.Nombre") %></td>
                            <td><%# Eval("Usuario") %></td>
                            <td><%# new string('*', 8) %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Correo") %></td>
                            <td><%# Eval("Telefono") %></td>
                            <td>
                                <button type="button" class="btn btn-primary" onclick='editarUsuario(<%# Eval("UsuarioId") %>)'>
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </button>
                                <button type="button" class="btn btn-danger" onclick='eliminarUsuario(<%# Eval("UsuarioId") %>)'>
                                    <span class="glyphicon glyphicon-remove"></span>
                                </button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <!--Script para los onclick de los botones EDITAR y ELIMINAR para que funcionen -->
        <script>
          //Funcion de editar Usuarios
            function editarUsuario(idUsuario) {
                var elementoId = document.getElementById('txtUsuarioId')
                var elementoRolId = document.getElementById('ddlRol')
                var elementoUsuario = document.getElementById('txtUsuario')
                var elementoContrasenia = document.getElementById('txtContrasenia')
                var elementoNombreCompleto = document.getElementById('txtNombreCompleto')
                var elementoCorreo = document.getElementById('txtCorreo')
                var elementoTelefono = document.getElementById('txtTelefono')

                var elementoTr = document.getElementById('filaUsuario' + idUsuario)
                var elementosTd = elementoTr.getElementsByTagName('td')

                elementoId.value = elementosTd[0].textContent
                elementoRolId.value = elementosTd[2].textContent
                elementoUsuario.value = elementosTd[3].textContent
                elementoContrasenia.value = elementosTd[4].textContent
                elementoNombreCompleto.value = elementosTd[5].textContent
                elementoCorreo.value = elementosTd[6].textContent
                elementoTelefono.value = elementosTd[7].textContent
            }

            //Funicio de eliminar Usuario
            function eliminarUsuario(idUsuario) {
                $.ajax({
                    url: 'Gestion.aspx?accion=eliminarUsuario&id=' + idUsuario,
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
        
    

   
    <!-- //////////////////////////////////////INICIA GESTIONAR SERVICIOS////////////////////////////////// -->
    <div id="third-content">

        <h1>Gestionar Servicios</h1>

    <!-- Formulario para agregar o editar un servicio -->
    <div class="form-inline">
        <asp:TextBox ID="txtServicioId" runat="server" placeholder="ID de Servicio" Style="display: none" ClientIDMode="Static" />
        
        <asp:TextBox ID="txtNombreServicio" runat="server" placeholder="Nombre del Servicio" class="form-control" Width="220px" ClientIDMode="Static" />
        &nbsp;&nbsp;

        <asp:TextBox ID="txtDescripcion" runat="server" placeholder="Descripción" class="form-control" Width="320px" ClientIDMode="Static" TextMode="MultiLine" Rows="2" />
        &nbsp;&nbsp;

        <asp:TextBox ID="txtPrecio" runat="server" placeholder="Precio" class="form-control" Width="140px" ClientIDMode="Static" />
        &nbsp;&nbsp;

        <asp:Button ID="btnAgregarServicio" runat="server" Text="Agregar Servicio" OnClick="BtnAgregarServicio_Click" class="btn btn-primary btn-lg" />
    </div>

    <br /><br />
        <!---->
    <!-- Tabla de servicios -->
    <div>
        <h1>Servicios Registrados</h1>
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Descripción</th>
                    <th>Precio</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rpServicios" runat="server">
                    <ItemTemplate>
                        <tr id='<%# "filaServicio" + Eval("ServicioId") %>'>
                            <td><%# Eval("ServicioId") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Descripcion") %></td>
                            <td><%# Eval("Precio", "{0:C}") %></td>
                            <td>
                                <button type="button" class="btn btn-primary" onclick='editarServicio(<%# Eval("ServicioId") %>)'>
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </button>
                                <button type="button" class="btn btn-danger" onclick='eliminarServicio(<%# Eval("ServicioId") %>)'>
                                    <span class="glyphicon glyphicon-remove"></span>
                                </button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <!-- luego aca va el script para los onclick de los botones EDITAR y ELIMINAR para que funcionen -->
        <script>
            //Funcion de editar Servicios 
            function editarServicio(idServicio) {
                var elementoServicioId = document.getElementById('txtServicioId')
                var elementoNombreServicio = document.getElementById('txtNombreServicio')
                var elementoDescripcion = document.getElementById('txtDescripcion')
                var elementoPrecio = document.getElementById('txtPrecio')

                var elementoTr = document.getElementById('filaServicio' + idServicio)
                var elementosTd = elementoTr.getElementsByTagName('td')

                elementoServicioId.value = elementosTd[0].textContent
                elementoNombreServicio.value = elementosTd[1].textContent
                elementoDescripcion.value = elementosTd[2].textContent
                elementoPrecio.value = elementosTd[3].textContent.replace(/[₡$,]/g, '').trim()
            }

            //Funcion de eliminar Servicios
            function eliminarServicio(idServicio) {
                $.ajax({
                    url: 'Gestion.aspx?accion=eliminarServicio&id=' + idServicio,
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



</asp:Content>
