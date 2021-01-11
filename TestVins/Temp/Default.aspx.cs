using System;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VinsUncoderLibrary.DataBase;
using VinsUncoderLibrary.Models;

namespace VinApp
{
    public partial class _Default : Page
    {

        private static readonly string _sortDirection = "sortDirection";
        private static readonly string _taskTable = "TaskTable";
        private static readonly string _pageIndex = "PageIndex";
        private static readonly string _countOfFilters = "CountOfFilters";
        private static readonly string _markFilter = "MarkFilter";
        private static readonly string _modelFilter = "ModelFilter";
        private static readonly string _sortFilter = "Sorting";
        private static readonly string _sortCssClass = "SortCssClass";
        private static readonly string _vinsGridPageSize = "VinsGridPageSize";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState[_sortDirection] == null)
                {
                    ViewState[_sortDirection] = SortDirection.Ascending;
                }


                return (SortDirection)ViewState[_sortDirection];
            }
            set { ViewState[_sortDirection] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                LoadUsersComponents();
                
            }
        }       

        protected void VinsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            VinsGrid.PageIndex = e.NewPageIndex;
            BindVinsGridDataSource();
        }

        protected void VinsGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = Session[_taskTable] as DataTable;
            
            if (dt != null)
            {

                string sortDirection = GetSortDirection(e.SortExpression);
                dt.DefaultView.Sort = e.SortExpression + " " + sortDirection;
                BindVinsGridDataSource();
                for (int i = 0; i < VinsGrid.HeaderRow.Cells.Count; i++)
                {
                    VinsGrid.HeaderRow.Cells[i].CssClass = "";
                }

                int index = 0;
                foreach (DataControlField controlField in fakeGridView.Columns)
                {
                    if (controlField.SortExpression == e.SortExpression)
                        break;
                    index++;
                }
                
                if (!((Session[_sortCssClass] as string) == "sortdesc"))
                {
                    if (sortDirection == "ASC")
                    {
                        fakeGridView.HeaderRow.Cells[index].CssClass = "sortasc";
                        Session[_sortCssClass] = "sortasc";
                    }
                    else
                    {
                        fakeGridView.HeaderRow.Cells[index].CssClass = "sortdesc";
                        Session[_sortCssClass] = "sortdesc";
                    }
                }
                else
                {
                    fakeGridView.HeaderRow.Cells[index].CssClass = null;
                    Session[_sortCssClass] = "sortasc";
                }

            }
        }

        protected void PopUpButton_Click(object sender, EventArgs e)
        {
            OpenPopUp((Button)sender);
        }

        protected void ButtonArrowLeft_Click(object sender, EventArgs e)
        {
            if ((Session[_pageIndex] as int?) > 0)
            {
                Session[_pageIndex] = (Session[_pageIndex] as int?) - 1;
                LoadUserVinsGrid();
            }
        }

        protected void ButtonRightArrow_Click(object sender, EventArgs e)
        {
            if ((Session[_pageIndex] as int?) < GetPageCount() - 1)
            {
                Session[_pageIndex] = (Session[_pageIndex] as int?) + 1;
                LoadUserVinsGrid();
            }
        }

        protected void InputPager_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (Convert.ToInt32(tb.Text) <= GetPageCount() && Convert.ToInt32(tb.Text) > 0)
            {
                Session[_pageIndex] = Convert.ToInt32(tb.Text) - 1;
                LoadUserVinsGrid();
            }
        }

        protected void ButtonDoubleArrowLeft_Click(object sender, EventArgs e)
        {
            if ((Session[_pageIndex] as int?) >= 5)
            {
                Session[_pageIndex] = (Session[_pageIndex] as int?) - 5;
                LoadUserVinsGrid();
            }
            else
            {
                Session[_pageIndex] = 0;
                LoadUserVinsGrid();
            }
        }

        protected void ButtondoubleArrowRight_Click(object sender, EventArgs e)
        {
            if ((Session[_pageIndex] as int?) < GetPageCount() - 5)
            {
                Session[_pageIndex] = 5 + (Session[_pageIndex] as int?);
                LoadUserVinsGrid();
            }
            else
            {
                Session[_pageIndex] = GetPageCount() - 1;
                LoadUserVinsGrid();
            }
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            string modelFilter = Session[_modelFilter] as string;
            string markFilter = Session[_markFilter] as string;
            if (modelFilter == null && hfModels.Value!= null)
            {
                modelFilter = hfModels.Value;
                Session[_countOfFilters] = (Session[_countOfFilters] as int?) + 1;
            }
            else if (modelFilter != null && hfModels.Value!= null &&
                    !(modelFilter as string).Equals(hfModels.Value))
            {
                modelFilter = hfModels.Value;
            }
            else if (modelFilter != null && hfModels.Value == null)
            {
                
            }
            else if (modelFilter != null)
            {
                modelFilter = null;
                Session[_countOfFilters] = (Session[_countOfFilters] as int?) - 1;
            }

            if (markFilter == null && hfMarks.Value != null)
            {
                markFilter = hfMarks.Value;
                Session[_countOfFilters] = (Session[_countOfFilters] as int?) + 1;
            }
            else if (markFilter != null && hfMarks.Value != null &&
                   !(markFilter as string).Equals(hfMarks.Value))
            {
                markFilter = hfMarks.Value;
            }
            else if (markFilter != null && hfMarks.Value == null)
            {
            }
            else if (markFilter != null)
            {
                markFilter = null;
                Session[_countOfFilters] = (Session[_countOfFilters] as int?) - 1;
            }
            Session[_modelFilter] = string.IsNullOrEmpty(modelFilter) ? null : modelFilter;
            Session[_markFilter] = string.IsNullOrEmpty(markFilter) ? null : markFilter;
            LoadUserVinsGrid();
        }

        protected void ResetFiltersButton_Click(object sender, EventArgs e)
        {
            ResetFilters();
            LoadUserVinsGrid();

        }

        protected void FakeButton_Click(object sender, EventArgs e)
        {

        }

        protected void VinsGridPageSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session[_vinsGridPageSize] = VinsGridPageSizeDropDown.SelectedValue;
            VinsGrid.PageSize = int.Parse(VinsGridPageSizeDropDown.SelectedValue);
            LoadUsersComponents();
        }



        private void OpenPopUp(Button button)
        {
            int intId = 100;
            string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('PopUpVinFullInfo.aspx?data=" + HttpUtility.UrlEncode(intId.ToString())

            + "','new window', 'top=250, left=500, width=500, height=520, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            Session["VinValue"] = button.CommandArgument;
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }

        private void ResetFilters()
        {
            Session[_countOfFilters] = 0;
            Session[_markFilter] = null;
            Session[_modelFilter] = null;
            hfMarks.Value = null;
            hfModels.Value= null;
            Session[_sortCssClass] = "sortasc";
            LoadUsersComponents();
        }

        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";

            if (ViewState["SortExpression"] is string sortExpression)
            {
                if (sortExpression == column)
                {
                    if ((ViewState["SortDirection"] is string lastDirection) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        private void LoadUsersComponents()
        {
            Session[_vinsGridPageSize] = VinsGrid.PageSize;
            Session[_pageIndex] = 0;
            Session[_countOfFilters] = 0;
            Session[_markFilter] = null;
            Session[_modelFilter] = null;
            Session[_sortCssClass] = "sortasc";
            LoadUserVinsGrid();
        }
        private void LoadUserVinsGrid()
        {
            string markFilter = Session[_markFilter] == null ? null : Session[_markFilter].ToString();
            string modelFilter = Session[_modelFilter] == null ? null : Session[_modelFilter].ToString();
            string sorting = Session[_sortFilter] == null ? null : Session[_sortFilter].ToString();
            var vins = VinDataBase.PrGetVinsWhithSortingAndFilters(
                (int)Session[_pageIndex],(int)Session[_vinsGridPageSize], markFilter, modelFilter, sorting);
            FillingVinDataTable(vins);
            BindVinsGridDataSource();
        }

        private void FillingVinDataTable(List<VinRowToView> vins)
        {
            DataTable taskTable = new DataTable("VinsList");
            taskTable.Columns.Add("VinValue", typeof(string));
            taskTable.Columns.Add("Mark", typeof(string));
            taskTable.Columns.Add("Model", typeof(string));
            taskTable.Columns.Add("Year", typeof(string));
            taskTable.Columns.Add("SerialNumber", typeof(string));
            foreach (var vinRow in vins)
            {
                DataRow tableRow = taskTable.NewRow();
                tableRow["VinValue"] = vinRow.VinValue;
                tableRow["Mark"] = vinRow.Mark;
                tableRow["Model"] = vinRow.Model;
                tableRow["Year"] = vinRow.Year;
                tableRow["SerialNumber"] = vinRow.SerialNumber;
                taskTable.Rows.Add(tableRow);
            }
            Session[_taskTable] = taskTable;
        }

        private void BindVinsGridDataSource()
        {
            VinsGrid.DataSource = Session[_taskTable];
            VinsGrid.DataBind();
            fakeGridView.DataSource = new List<VinRowToView>();
            fakeGridView.DataBind();
            TextBox textBox = InputPager;
            textBox.Text = ((int)Session[_pageIndex] + 1).ToString();
            Label label = CountRowsSize;
            label.Text = VinDataBase.PrGetCountOfRows(Session[_markFilter] as string,
                Session[_modelFilter] as string).ToString();
            label = CurrentPageSize;
            label.Text = VinsGrid.Rows.Count.ToString();
            LabelCountOfFilters.Text = (Session[_countOfFilters] as int?).ToString();
            UpdatePageCount();
        }
        private void UpdatePageCount()
        {
            Label label = PageCount;
            label.Text = GetPageCount().ToString();
        }

        private int GetPageCount()
        {
            int countOfRows = VinDataBase.PrGetCountOfRows(Session[_markFilter] as string, Session[_modelFilter] as string);
            if (countOfRows % (int)Session[_vinsGridPageSize] == 0)
            {
                return countOfRows / (int)Session[_vinsGridPageSize];
            }
            else
            {
                return countOfRows / (int)Session[_vinsGridPageSize] + 1;
            }
        }

    }
}