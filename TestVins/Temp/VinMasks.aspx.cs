using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VinsUncoderLibrary.DataBase;
using VinsUncoderLibrary.Models;

namespace Temp
{
    public partial class VinMasks : System.Web.UI.Page
    {

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }


                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadUsersComponents();
            }
        }

        protected void GridViewOpenButton_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32((((Button)sender).CommandArgument).ToString());

            Button gridViewOpenButton = (Button)MarksGrid.Rows[rowIndex].FindControl("GridViewOpenButton");
            GridView innerGridView = (GridView)MarksGrid.Rows[rowIndex].FindControl("MasksGridView");

            if (gridViewOpenButton.Text == ">")
            {
                innerGridView.Visible = true;
                gridViewOpenButton.Text = "<";
            }
            else
            {
                innerGridView.Visible = false;
                gridViewOpenButton.Text = ">";
            }
        }

        protected void ButtondoubleArrowRight_Click(object sender, EventArgs e)
        {
            if (MarksGrid.PageIndex < MarksGrid.PageCount - 5)
            {
                MarksGrid.PageIndex += 5;
                BindMarksGridDataSource();
            }
            else
            {
                MarksGrid.PageIndex = MarksGrid.PageCount - 1;
                BindMarksGridDataSource();
            }
        }

        protected void ButtonArrowLeft_Click(object sender, EventArgs e)
        {
            if (MarksGrid.PageIndex > 0)
            {
                MarksGrid.PageIndex -= 1;
                BindMarksGridDataSource();
            }
        }

        protected void ButtonRightArrow_Click(object sender, EventArgs e)
        {
            if (MarksGrid.PageIndex < MarksGrid.PageCount - 1)
            {
                MarksGrid.PageIndex += 1;
                BindMarksGridDataSource();
            }
        }

        protected void ResetFiltersButton_Click(object sender, EventArgs e)
        {

        }

        protected void InputPager_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (Convert.ToInt32(tb.Text) <= MarksGrid.PageCount
                && Convert.ToInt32(tb.Text) > 0)
            {
                MarksGrid.PageIndex = Convert.ToInt32(tb.Text) - 1;
                BindMarksGridDataSource();
            }
        }

        protected void ButtonDoubleArrowLeft_Click(object sender, EventArgs e)
        {
            if (MarksGrid.PageIndex >= 5)
            {
                MarksGrid.PageIndex -= 5;
                BindMarksGridDataSource();
            }
            else
            {
                MarksGrid.PageIndex = 0;
                BindMarksGridDataSource();
            }
        }
        protected void MarksGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = (GridView)e.Row.FindControl("MasksGridView");
                GridView fgv = (GridView)e.Row.FindControl("FakeMasksGridView");

                string Mark = e.Row.Cells[1].Text;

                gv.DataSource = GetMasksDataTable(Mark);
                gv.DataBind();

                fgv.DataSource = new List<MaskModel>();
                fgv.DataBind();

            }
        }

        protected void ButtonPlusMark_Click(object sender, EventArgs e)
        {
            MarkError.Visible = false;
            AddMarkPopUpExtender.Show();
        }

        protected void ButtonAddMark_Click(object sender, EventArgs e)
        {
            AddMarkPopUpExtender.Hide();

            if (!MarksDataBase.CheckMarkByMarkName(TextBoxMark.Text))
            {
                MarksDataBase.AddNewMark(TextBoxMark.Text);
                MarksDataBase.AddMeaningfulMask(TextBoxMeaningfulMask.Text, TextBoxMark.Text);
                LoadUserMarksGrid();
            }
            else
            {
                AddMarkPopUpExtender.Show();
                MarkError.Visible = true;
            }
        }

        protected void ButtonCloseAddMarkPopUp_Click(object sender, EventArgs e)
        {
            AddMarkPopUpExtender.Hide();
        }

        protected void ButtonPlusMask_Click(object sender, EventArgs e)
        {
            AddMaskOnMarkPopUpPopupExtender.Show();
        }

        protected void DescriptionsInfo_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            DescriptionsGridView.DataSource = VinDescriptionsDataBase
                .GetVinsDescriptionsByMaskId(int.Parse(button.CommandArgument));
            DescriptionsGridView.DataBind();

            DesctptionOnMaskPopUpExtender.Show();
        }

         protected void ButtonCloseAddMaskOnMarkPopUp_Click(object sender, EventArgs e)
        {
            AddMaskOnMarkPopUpPopupExtender.Hide();
        }

        protected void ButtonCloseDesctptionOnMaskPopUp_Click(object sender, EventArgs e)
        {
            DesctptionOnMaskPopUpExtender.Hide();
        }

        protected void ButtonAddMaskOnMark_Click(object sender, EventArgs e)
        {
            string Mask = GenerateMaskByCheckBoxes();
            MaskDataBase.AddMask(new VinsUncoderLibrary.Models.Mask
            {
                MaskView = Mask,
                IdOfMark = hfMarkId.Value
            });
            LoadUsersComponents();
        }

        private string GenerateMaskByCheckBoxes()
        {
            string Mask = "";
            if (checkBoxPosition1.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition2.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition3.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition4.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition5.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition6.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition7.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition8.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition9.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition10.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition11.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition12.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition13.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition14.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition15.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition16.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }
            if (checkBoxPosition17.Checked)
            {
                Mask += "X";
            }
            else
            {
                Mask += "0";
            }

            return Mask;

        }

        private void LoadUsersComponents()
        {
            Session["CountOfFilters"] = 0;

            LoadUserMarksGrid();
        }

        private void LoadUserMarksGrid()
        {
            var marks = MarksDataBase.GetMarksViews();
            FillingMarksDataTable(marks);

            BindMarksGridDataSource();
        }
        private void FillingMarksDataTable(List<MarksView> marks)
        {
            DataTable taskTable = new DataTable("MarksList");

            taskTable.Columns.Add("Mark", typeof(string));
            taskTable.Columns.Add("MeaningfulMask", typeof(string));
            taskTable.Columns.Add("CountOfVinDescriptions", typeof(int));
            taskTable.Columns.Add("MarkId", typeof(int));

            foreach (var markRow in marks)
            {
                DataRow tableRow = taskTable.NewRow();

                tableRow["Mark"] = markRow.Mark;
                tableRow["MeaningfulMask"] = markRow.MeaningfulMask;
                tableRow["CountOfVinDescriptions"] = markRow.CountOfVinDescriptions;
                tableRow["MarkId"] = markRow.MarkId;

                taskTable.Rows.Add(tableRow);
            }

            Session["TaskTable"] = taskTable;
        }

        
        private DataTable GetMasksDataTable(string Mark)
        {
            DataTable taskTable = new DataTable("MasksList");

            taskTable.Columns.Add("XPosition", typeof(string));
            taskTable.Columns.Add("Mask", typeof(string));
            taskTable.Columns.Add("CountOfDescriptions", typeof(int));
            taskTable.Columns.Add("MaskId", typeof(int));
            taskTable.Columns.Add("MarkId", typeof(string));

            var masks = MaskDataBase.GetMaskModelsByMark(Mark);
            masks.Add(MaskDataBase.GetMaskModelByIdOfMask(12));
            foreach (var maskRow in masks)
            {
                DataRow tableRow = taskTable.NewRow();

                tableRow["XPosition"] = GetMasksXPosition(maskRow.MaskView);
                tableRow["Mask"] = maskRow.MaskView;
                tableRow["CountOfDescriptions"] = maskRow.CountOfDescriptions;
                tableRow["MaskId"] = maskRow.IdOfMask;
                tableRow["MarkId"] = maskRow.IdOfMark;

                taskTable.Rows.Add(tableRow);
            }
            return taskTable;
        }


        private string GetMasksXPosition(string Mask)
        {
            string xPositions = "";
            for (int i = 0; i < Mask.Length; i++)
            {
                if(Mask.ElementAt(i) == 'X')
                {
                    xPositions += i + " ";
                }
            }
            return xPositions;
        }
        private void UpdatePageCount()
        {
            Label label = PageCount;
            label.Text = MarksGrid.PageCount.ToString();
        }

        private void BindMarksGridDataSource()
        {
            MarksGrid.DataSource = Session["TaskTable"];
            MarksGrid.DataBind();

            FakeMarksGrid.DataSource = new List<MarksView>();
            FakeMarksGrid.DataBind();

            TextBox textBox = InputPager;
            textBox.Text = (MarksGrid.PageIndex + 1).ToString();

            Label label = CountRowsSize;
            label.Text = (Session["TaskTable"] as DataTable).Rows.Count.ToString();

            label = CurrentPageSize;
            label.Text = MarksGrid.Rows.Count.ToString();

            LabelCountOfFilters.Text = (Session["CountOfFilters"] as int?).ToString();

            UpdatePageCount();
        }

        protected void ButtonPlusDescription_Click(object sender, EventArgs e)
        {
            Button buttonPlusDescription = (Button)sender;
            DropDownOfEnumsInAddNewDescriptionOnMaskPopUp.DataSource = Enum
                .GetValues(typeof(TypeOfVinPartMeaning))
                .Cast<TypeOfVinPartMeaning>().ToList();
            DropDownOfEnumsInAddNewDescriptionOnMaskPopUp.DataBind();
            MaskInAddNewDescriptionOnMaskPopUpLabel.Text = MaskDataBase
                .GetMaskModelByIdOfMask(int.Parse(buttonPlusDescription.CommandArgument))
                .MaskView;
            string MarkId = hfMarkId.Value;
            if (buttonPlusDescription.CommandArgument.Equals("12"))
            {
                TrOfPossibleDependencies.Visible = false;
            }
            else
            {
                TrOfPossibleDependencies.Visible = true;
                DropDownOfPossibleDependencies.DataSource = VinDescriptionsDataBase
               .GetVinsDescriptionsByMarkId(MarkId)
               .Select(vd => vd.VinPart);
                DropDownOfPossibleDependencies.DataBind();
            }
           
            AddNewDescriptionOnMaskPopupExtender.Show();
            
        }

        protected void ButtonCloseAddNewDescriptionOnMask_Click(object sender, EventArgs e)
        {
            AddNewDescriptionOnMaskPopupExtender.Hide();
        }

        protected void ButtonAddNewVindescriptionOnMask_Click(object sender, EventArgs e)
        {

        }
    }
}