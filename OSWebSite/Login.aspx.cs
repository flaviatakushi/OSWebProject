using System;
using System.Configuration;
using OSWebCore;
using System.Data;

namespace OSWebSite
{
    public partial class Login : System.Web.UI.Page
    {
        private DataAccess dataAccess = new DataAccess(DataProvider.SqlServer, ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            ////
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            this.dataAccess.Dispose();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataAccess.OpenConnection();

                //// Aqui você verifica se o usuário informado existe, se a senha está correta, etc...
                //// Use dataAccess.ExecuteScalar(<tipo de comando>, <sql query>) para retornar o primeiro registro
                //// que atende a query passada por parâmetro.
            }
            finally
            {
                this.dataAccess.CloseConnection();
            }
        }
    }
}