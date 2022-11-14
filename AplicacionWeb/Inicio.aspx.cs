using DBDatos;
using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{
    public partial class Inicio : System.Web.UI.Page
    {
        Usuario usuarioAutenticado;
        ICapaDatos bd = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //LblError.Visible = false;

            bd = (DBPruebas)Application["Base de datos"];
            
           
            if (bd == null)
            {
                bd = new DBPruebas();
                InicializarBd();
                Application["Base de datos"] = bd;
            }
            usuarioAutenticado = (Usuario)Session["usuarioAutenticado"];

            if(usuarioAutenticado != null)
            {
                if (usuarioAutenticado.EsAdmin())
                    Server.Transfer("UsuarioGestor.aspx"); 
                else
                    Server.Transfer("UsuarioNormal.aspx");
            }
            this.LblError.Text = "";
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            //this.LblError.Text = this.TxtBx_usuario.Text + " / " + this.TxtBx_pass.Text;
            Usuario usuario = bd.LoggIn(this.TxtBx_usuario.Text, this.TxtBx_pass.Text);
            if (usuario == null)
            {
                //this.TxtBx_usuario.Text = "";
                this.TxtBx_pass.Text = "";
                //LblError.Visible = true;
                this.LblError.Text = "Credenciales invalidas";
                //Server.Transfer("Inicio.aspx", false); //esto oculta el nombre de la pagina a la que yo voy
            }
            else
            {
                this.TxtBx_usuario.Text = "";
                this.TxtBx_pass.Text = "";
                this.LblError.Text = "";
                Session["Usuario actual"] = usuario;
                if (usuario.EsAdmin())
                    Server.Transfer("UsuarioGestor.aspx"); //esto oculta el nombre de la pagina a la que yo voy
                else
                    Server.Transfer("UsuarioNormal.aspx");
            }
        }

        protected void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.TxtBx_usuario.Text = "";
            this.TxtBx_pass.Text = "";
            this.LblError.Text = "";
        }

        private void InicializarBd()
        {
            Usuario admin = bd.LoggIn("admin@admin.com", "admin");
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            bd.CrearEntrada(marcos, "marcos", "entrada1", "esta es la primera entrada creada");
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            bd.CrearEntrada(oscar, "oscar", "entrada2", "esta es la segunda entrada creada");
            bd.CrearUsuario(admin, "prueba", "prueba", "prueba@prueba.com", "Ef3,", 1);
            Usuario prueba = bd.LoggIn("prueba@prueba.com", "Ef3,");
            bd.CrearEntrada(prueba, "prueba", "entrada3", "esta es la tercera entrada creada");
            bd.AsociarLectorYEntrada(1, "marcos@guzman.com", oscar);
            bd.AsociarLectorYEntrada(2, "marcos@guzman.com", prueba);
            bd.AsociarLectorYEntrada(0, "prueba@prueba.com", marcos);
            bd.AsociarLectorYEntrada(0, "oscar@valverde.com",marcos);
        }
    }
}