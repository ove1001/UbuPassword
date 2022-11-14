<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioGestor.aspx.cs" Inherits="AplicacionWeb.UsuarioGestor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 2010px;
            height: 143px;
        }
        .auto-style4 {
            width: 74px;
        }
        .auto-style7 {
            width: 273px;
        }
        .auto-style8 {
            width: 74px;
            height: 37px;
        }
        .auto-style10 {
            width: 273px;
            height: 37px;
        }
        .auto-style11 {
            height: 37px;
            width: 219px;
        }
        .auto-style14 {
            width: 100%;
        }
        .auto-style15 {
            width: 219px;
        }
        .auto-style16 {
            width: 54px;
            height: 37px;
        }
        .auto-style17 {
            width: 54px;
        }
        .auto-style18 {
            width: 74px;
            height: 26px;
        }
        .auto-style19 {
            width: 54px;
            height: 26px;
        }
        .auto-style20 {
            width: 273px;
            height: 26px;
        }
        .auto-style21 {
            width: 219px;
            height: 26px;
        }
        .auto-style22 {
            width: 74px;
            height: 37px;
            text-align: center;
        }
        .auto-style23 {
            width: 74px;
            height: 26px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style1">
        <div>
            <table class="auto-style14">
                <tr>
                    <td class="auto-style8">
                        <asp:Label ID="Lbl_panel_gestor" runat="server" Text="Panel de gestor"></asp:Label>
                    </td>
                    <td class="auto-style16">
                        <asp:Button ID="Btn_cerrar_sesion" runat="server" BorderStyle="Solid" OnClick="Btn_cerrar_sesion_Click" Text="Cerrar sesion" />
                    </td>
                    <td class="auto-style10"></td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style18">
                        <asp:Label ID="Lbl_saludo" runat="server" Text="Hola"></asp:Label>
                    </td>
                    <td class="auto-style19">
                        <asp:Label ID="Lbl_nombre_usuario" runat="server" Text="Usuario"></asp:Label>
                    </td>
                    <td class="auto-style20"></td>
                    <td class="auto-style21"></td>
                </tr>
                <tr>
                    <td class="auto-style22">
                        &nbsp;</td>
                    <td class="auto-style16"></td>
                    <td class="auto-style10"></td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style23">
                        <asp:Label ID="LblUsuario" runat="server" Text="LblUsuarios"></asp:Label>
                    </td>
                    <td class="auto-style19"></td>
                    <td class="auto-style20"></td>
                    <td class="auto-style21"></td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <asp:GridView ID="gvwListaUsuarios" runat="server" AutoGenerateColumns="false" 
                        onrowcommand="gvwListaUsuarios_RowCommand" Width="90%" HorizontalAlign="Center">
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                            <asp:BoundField DataField="Id." HeaderText="Id." />
                                            <asp:BoundField DataField="Apellidos Usuario" HeaderText="Apellido" />
                                            <asp:BoundField DataField="Nombre Usuario" HeaderText="Nombre" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="EsAdmin" HeaderText="EsAdmin" />
                                    </Columns>
                        </asp:GridView>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style17">&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
