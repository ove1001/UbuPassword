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
    public partial class UsuarioNormal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario actual"];
            
            if (usuario != null)
            {
                Lbl_nombre_usuario.Text = usuario.Nombre;
            }
            else if (usuario == null)
            {
                Server.Transfer("Inicio.aspx");
            }
            else if (usuario.EsAdmin())
            {
                Server.Transfer("UsuarioGestor.aspx");
            }

            ICapaDatos bd = (DBPruebas)Application["Base de datos"];
            DataTable tablaEntradasLegibles = new DataTable();
            tablaEntradasLegibles.Columns.Add("Id.", typeof(Int16));
            tablaEntradasLegibles.Columns.Add("Usuario", typeof(String));
            tablaEntradasLegibles.Columns.Add("Contraseña", typeof(String));
            tablaEntradasLegibles.Columns.Add("Descripcion", typeof(String));
            tablaEntradasLegibles.Columns.Add("Usuarios Lectores", typeof(String));
            Entrada entradaCreada = null;
            foreach (Int32 entrada in usuario.ListaEntradasCreadas)
            {
                entradaCreada = bd.RecuperarEntrada(usuario, entrada);
                tablaEntradasLegibles.Rows.Add(entradaCreada.IdEntrada, entradaCreada.Usuario, entradaCreada.Pass, entradaCreada.Descripcion, entradaCreada.UsuariosLectores());

            }
            gvwListaEntradasLegibles.DataSource = tablaEntradasLegibles;
            gvwListaEntradasLegibles.DataBind();
            if (gvwListaEntradasLegibles != null)
            {
                LblEntradasLegibles.Text = "Entradas Legibles";
            }
            else LblEntradasLegibles.Text = "No Existen Entradas Legibles";

            /**************************************************/

            DataTable tablaEntradasCreadas = new DataTable();
            tablaEntradasCreadas.Columns.Add("Id.", typeof(Int16));
            tablaEntradasCreadas.Columns.Add("Usuario", typeof(String));
            tablaEntradasCreadas.Columns.Add("Contraseña", typeof(String));
            tablaEntradasCreadas.Columns.Add("Descripcion", typeof(String));
            Entrada entradaLegibles = null;
            foreach (Int32 entrada in usuario.ListaEntradasLegibles)
            {
                entradaLegibles = bd.RecuperarEntrada(usuario, entrada);
                tablaEntradasCreadas.Rows.Add(entradaLegibles.IdEntrada, entradaLegibles.Usuario, entradaLegibles.Pass, entradaLegibles.Descripcion);

            }
            gvwListaEntradasCreadas.DataSource = tablaEntradasCreadas;
            gvwListaEntradasCreadas.DataBind();
            if (gvwListaEntradasCreadas != null)
            {
                LblUsuariosLectores.Text = "Usuarios Lectores de mi entrada";
            }
            else LblUsuariosLectores.Text = "No Existen Usuarios";
        }

        protected void Btn_cerrar_sesion_Click(object sender, EventArgs e)
        {
            Session["Usuario actual"] = null;
            Server.Transfer("Inicio.aspx");
        }

        protected void gvwListaEntradasLegibles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int row = Convert.ToInt32(e.CommandArgument);
            Session["proyectoSeleccionado"] = gvwListaEntradasLegibles.Rows[row].Cells[0].Text;
            Response.Redirect("InfoProyectos.aspx", false);
        }

        protected void gvwListaEntradasCreadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int row = Convert.ToInt32(e.CommandArgument);
            Session["proyectoSeleccionado"] = gvwListaEntradasCreadas.Rows[row].Cells[0].Text;
            Response.Redirect("InfoProyectos.aspx", false);
        }
    }
}