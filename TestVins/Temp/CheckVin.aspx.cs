using System;
using System.Collections.Generic;
using System.Web.UI;
using VinsUncoderLibrary;
using VinsUncoderLibrary.DataBase;
using VinsUncoderLibrary.Models;

namespace VinApp
{
    public partial class CheckVin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            UncodeVin();
        }
        private void UncodeVin()
        {
            string Vin = TextBox1.Text;
            Label1.Text = "Вин расшифрован";
            Label1.Visible = true;
            if (VinDataBase.GetVinById(Vin).VinTextValue != Vin)
            {
                Uncoder uncoder = new Uncoder();
                uncoder.UncodeVin(new Vin { VinTextValue = Vin });
            }
            List<VinPartDecodingResult> results = VinDecodingResultDataBase.GetResultsTableByVin(new Vin { VinTextValue = Vin });

            GridView1.DataSource = results;
            GridView1.DataBind();
        }

    }
}
