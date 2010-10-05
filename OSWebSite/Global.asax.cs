using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace OSWebSite
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Código que é executado na inicialização do aplicativo

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Código que é executado no encerramento do aplicativo

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Código que é executado quando um erro sem tratamento ocorre

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Código que é executado quando uma nova sessão é iniciada

        }

        void Session_End(object sender, EventArgs e)
        {
            // Código que é executado quando uma sessão é encerrada. 
            // Observação: O evento Session_End é gerado apenas quando o modo sessionstate
            // é definido como InProc no arquivo Web.config. Se o modo de sessão é definido como StateServer 
            // ou SQLServer, o evento não é gerado.

        }

    }
}
