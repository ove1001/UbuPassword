<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioNormal.aspx.cs" Inherits="AplicacionWeb.UsuarioNormal" %>

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
        .auto-style2 {
            text-align: left;
            height: 43px;
        }
        .auto-style3 {
            width: 310px;
        }
        .auto-style4 {
            width: 74px;
        }
        .auto-style5 {
            height: 43px;
        }
        .auto-style6 {
            text-align: center;
            height: 43px;
            width: 590px;
        }
        .auto-style7 {
            width: 590px;
        }
        .auto-style8 {
            width: 74px;
            height: 37px;
        }
        .auto-style9 {
            width: 310px;
            height: 37px;
        }
        .auto-style10 {
            width: 590px;
            height: 37px;
        }
        .auto-style11 {
            height: 37px;
        }
        .auto-style12 {
            width: 590px;
            height: 37px;
            text-align: center;
            margin-left: 40px;
        }
        .auto-style13 {
            width: 310px;
            height: 37px;
            text-align: center;
        }
        .auto-style14 {
            width: 310px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style1">
        <div>
            <table style="width: 100%;">
                <tr>
                    <td class="auto-style5" colspan="2">
                        <asp:Label ID="Lbl_panel_usuario" runat="server" Text="Panel de Usuario"></asp:Label>
                    </td>
                    <td class="auto-style6">
                        <asp:Button ID="Btn_cerrar_sesion" runat="server" BorderStyle="Solid" OnClick="Btn_cerrar_sesion_Click" Text="Cerrar sesion" />
                    </td>
                    <td class="auto-style2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8">
                        <asp:Label ID="Lbl_saludo" runat="server" Text="Hola"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="Lbl_nombre_usuario" runat="server" Text="Usuario"></asp:Label>
                    </td>
                    <td class="auto-style10"></td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style8"></td>
                    <td class="auto-style13">
                        <asp:Label ID="LblUsuariosLectores" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style12">&nbsp;</td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style8"></td>
                    <td class="auto-style9">
                        <asp:GridView ID="gvwListaEntradasLegibles" runat="server" AutoGenerateColumns="false" 
                        onrowcommand="gvwListaEntradasLegibles_RowCommand" Width="90%" HorizontalAlign="Center">
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                            <asp:BoundField DataField="Id." HeaderText="Id." />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                            <asp:BoundField DataField="Contraseña" HeaderText="Contraseña" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                            <asp:BoundField DataField="Usuarios Lectores" HeaderText="Usuarios Lectores" />
                                    </Columns>
                        </asp:GridView>
                    </td>
                    <td class="auto-style10">
                        &nbsp;</td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style14">
                        <asp:Label ID="LblEntradasLegibles" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style3">
                        <asp:GridView ID="gvwListaEntradasCreadas" runat="server" AutoGenerateColumns="false" 
                        onrowcommand="gvwListaEntradasLegibles_RowCommand" Width="90%" HorizontalAlign="Center">
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                            <asp:BoundField DataField="Id." HeaderText="Id." />
                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                            <asp:BoundField DataField="Contraseña" HeaderText="Contraseña" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    </Columns>
                        </asp:GridView>
                    </td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
