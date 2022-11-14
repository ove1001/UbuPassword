<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="AplicacionWeb.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            height: 44px;
        }
        .auto-style4 {
            height: 44px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="LblUbuPassword" runat="server" Text="UbuPassword"></asp:Label>
                </td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Llb_user" runat="server" Text="Nombre de usuario"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="TxtBx_usuario" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Lab_password" runat="server" Text="Contraseña"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="TxtBx_pass" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Button ID="BtnAceptar" runat="server" OnClick="BtnAceptar_Click" Text="Aceptar" />
                </td>
                <td class="auto-style3">
                    <asp:Button ID="Btn_Cancelar" runat="server" OnClick="Btn_Cancelar_Click" Text="Cancelar" />
                </td>
                <td class="auto-style3"></td>
                <td class="auto-style3"></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblError" runat="server" Text="Label"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
