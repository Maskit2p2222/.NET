using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VinsUncoderLibrary.DataBase;
using VinsUncoderLibrary.Models;

namespace Temp
{
    public partial class PopUpVinFullInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataToGridView(Session["VinValue"] as string);
        }

        private void BindDataToGridView(string Vinvalue)
        {
            List<VinPartDecodingResult> list = VinDecodingResultDataBase
                .GetResultsTableByVin(new Vin {
                    VinTextValue = Vinvalue
                });
            GridViewVinDecdingResultsTable.DataSource = list;
            GridViewVinDecdingResultsTable.DataBind();
        }

    }
}