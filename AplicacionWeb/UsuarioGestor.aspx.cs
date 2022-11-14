using DBDatos;
using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{
    public partial class UsuarioGestor : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = (Usuario)Session["Usuario actual"];
            LblUsuario.Text = "Usuarios del sistema";
           
            if (usuario != null)
            {
                Lbl_nombre_usuario.Text = usuario.Nombre;
            }
            if (usuario == null)
            {
                Server.Transfer("Inicio.aspx");
            }
            else if (!usuario.EsAdmin())
            {
                Server.Transfer("UsuarioGestor.aspx");
            }

            ICapaDatos bd = (DBPruebas)Application["Base de datos"];
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Id.", typeof(Int16));
            tabla.Columns.Add("Apellidos Usuario", typeof(String));
            tabla.Columns.Add("Nombre Usuario", typeof(String));
            tabla.Columns.Add("EMail", typeof(String));
            tabla.Columns.Add("EsAdmin", typeof(Boolean));
            foreach (var pair in bd.RecuperarDiccionarioUsuarios(usuario))
            {
                tabla.Rows.Add(pair.Value.IdUsuario, pair.Value.Apellidos, pair.Value.Nombre, pair.Value.Email, pair.Value.EsAdmin());
                
            }
            gvwListaUsuarios.DataSource = tabla;
            gvwListaUsuarios.DataBind();
            if (gvwListaUsuarios != null)
            {
                LblUsuario.Text = "Usuarios Existentes";
            }
            else LblUsuario.Text = "No Existen Usuarios";
        }
        protected void Btn_cerrar_sesion_Click(object sender, EventArgs e)
        {
            Session["Usuario actual"] = null;
            Server.Transfer("Inicio.aspx");
        }

        protected void gvwListaUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int row = Convert.ToInt32(e.CommandArgument);
            Session["proyectoSeleccionado"] = gvwListaUsuarios.Rows[row].Cells[0].Text;
            Response.Redirect("InfoProyectos.aspx", false);
        }
        
    }
}