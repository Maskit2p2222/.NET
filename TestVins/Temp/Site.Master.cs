using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Temp.DataBase;

namespace Temp
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated && Request.RawUrl != "/Mylogin")
            {
                Response.Redirect("Mylogin.aspx");
            }
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (!Page.IsPostBack)
                {
                    if (Session["MenuItems"] == null || Session["AvailablePages"] == null)
                    {
                        FillOutASession();
                    }
                    Button1.Visible = true;
                    RenderMenue(0);
                }
                if (Request.RawUrl != "/")
                {
                    List<string> AvailablePages = (List<string>)Session["AvailablePages"];
                    string reqUrl = Request.RawUrl.Substring(1);
                    reqUrl += ".aspx";
                    bool flag = !AvailablePages.Contains(reqUrl);
                    if (flag && Request.RawUrl != "/About")
                    {
                        Response.Redirect("About.aspx");
                    }
                }
            }
        }

        private void RenderMenue(int ParentMenuId, System.Web.UI.WebControls.MenuItem NewMenuItem = null)
        {
            List<Models.MenuItem> menuItems = (List<Models.MenuItem>)Session["MenuItems"];
            List<System.Web.UI.WebControls.MenuItem> menus = new List<System.Web.UI.WebControls.MenuItem>();
            foreach(Models.MenuItem item in menuItems)
            {
                if (item.ParentId == ParentMenuId)
                {
                    System.Web.UI.WebControls.MenuItem menuItem = new System.Web.UI.WebControls.MenuItem
                    {
                        Text = item.MenuName,
                        NavigateUrl = item.PageUrl,
                        Value = item.MenuId.ToString()
                    };
                    if (NewMenuItem != null)
                    {
                        NewMenuItem.ChildItems.Add(menuItem);
                    }
                    RenderMenue(item.MenuId, menuItem);
                    while (menuItem.Parent != null)
                    {
                        menuItem = menuItem.Parent;
                    }
                    if (!MasterMenu.Items.Contains(menuItem)) { 
                        MasterMenu.Items.Add(menuItem);
                    }
                }
            }
            MasterNavBar.Visible = true;
        }

        private void FillOutASession()
        {
            List<Models.MenuItem> items = MenuItemDataBase.GetMenuItemsByUsername(HttpContext.Current.User.Identity.Name);
            List<Models.MenuItem> menuItems = new List<Models.MenuItem>();
            List<string> pageUrls = new List<string>();
            foreach (Models.MenuItem mi in items)
            {
                pageUrls.Add(mi.PageUrl);
                if (mi.MenuName != null)
                {
                    menuItems.Add(mi);
                }
            }
            Session["MenuItems"] = menuItems;
            Session["AvailablePages"] = pageUrls;
        }
        protected void SignOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}