using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temp
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginAction_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }

            if (Membership.ValidateUser(UsernameText.Text, PasswordText.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(UsernameText.Text, false);
            }
            else
            {
                LegendStatus.Text = "Вы неправильно ввели имя пользователя или пароль!";
            }
        }
    }
}