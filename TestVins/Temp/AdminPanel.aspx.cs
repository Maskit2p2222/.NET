
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VinsUncoderLibrary.DataBase;
using VinsUncoderLibrary.Extentions;
using VinsUncoderLibrary.Models;
using VinsUncoderLibrary.Services;

namespace VinApp
{
    public partial class AddVinDescription : Page
    {
        // private TreeView vinDescriptionsUpdatableTreeView;
        private const string _treeMainString = "Main";
        private const string _treeMasksString = "Masks";
        private const string _treeVinsString = "Vins";
        private const string _treeVinDescsString = "VinDescs";
        private const string _worldManufacturerIdentifierMask = "XXX00000000000000";
        private const int _worldManufacturerIdentifierMaskId = 12;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitializeTreeView();
            }
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            MainH.Visible = true;
            MainH.InnerText = TreeView1.SelectedNode.Text;
            ClearDataSourse();
            List<string> Marks = GetListOfMarks();
            switch (TreeView1.SelectedNode.Text)
            {
                case _treeMainString:
                    GridViewAll.Visible = false;
                    MainH.Visible = false;
                    break;
                case _treeMasksString:
                    MainH.Visible = true;
                    InitializeMasks();
                    break;
                case _treeVinsString:
                    MainH.Visible = true;
                    InitializeVins();
                    break;
                case _treeVinDescsString:
                    MainH.Visible = true;
                    InitializeVinsDesc();
                    break;
                default:
                    MainH.Visible = true;
                    string Mark = Marks.Where(m => m == TreeView1.SelectedNode.Text).FirstOrDefault();
                    InitializeMarkDataSourse(TreeView1.SelectedNode);
                    break;
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            List<VinPartDecodingResult> list = VinDecodingResultDataBase.GetResultsTableByVin(
                new Vin 
                { 
                    VinTextValue = btn.CommandArgument 
                }
            );
            GridViewVinsResult.Visible = true;
            GridViewVinsResult.DataSource = list;
            GridViewVinsResult.DataBind();
        }      
        protected void GridViewAll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewAll.PageIndex = e.NewPageIndex;
            if (TreeView1.SelectedNode.Text == _treeMasksString)
            {
                InitializeMasks();
            }
            else if (TreeView1.SelectedNode.Parent.Text == _treeMasksString)
            {
                var data = MaskDataBase.GetMasksByMark(TreeView1.SelectedNode.Text);
                GridViewAll.DataSource = data;
                GridViewAll.Visible = true;
                GridViewAll.DataBind();
            }
            else if (TreeView1.SelectedNode.Text == _treeVinDescsString)
            {
                InitializeVinsDesc();
            }
            else if (TreeView1.SelectedNode.Parent.Text == _treeVinDescsString)
            {
                var vinDescs = VinDescriptionsDataBase.GetVinDescroptionViewByMark(TreeView1.SelectedNode.Text);
                GridViewAll.DataSource = vinDescs;
                GridViewAll.Visible = true;
                GridViewAll.DataBind();
            }


        }
        protected void GridViewMasks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewMasks.PageIndex = e.NewPageIndex;
            if (TreeView1.SelectedNode.Parent.Text == _treeMasksString)
            {
                var data = MaskDataBase.GetMasksByMark(TreeView1.SelectedNode.Text);
                GridViewMasks.DataSource = data;
                GridViewMasks.Visible = true;
                GridViewMasks.DataBind();
            }


        }

        protected void GridViewVinDescs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewVinDescs.PageIndex = e.NewPageIndex;
            if (TreeView1.SelectedNode.Parent.Text == _treeVinDescsString)
            {
                var vinDescs = VinDescriptionsDataBase.GetVinDescroptionByMark(TreeView1.SelectedNode.Text);
                GridViewVinDescs.DataSource = vinDescs;
                GridViewVinDescs.Visible = true;
                GridViewVinDescs.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            AddNewVinDescription();
            //InitializeMarkDataSourse_VinDescs(TreeView1.SelectedNode);
            //UpdatePanel1.Update();
        }

        protected void ListBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox1.MaxLength = GetLenghtOfVinPart(ListBox2.SelectedValue);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            AddNewVinDescLinks.Visible = false;
            AddNewVinDescLinksH.Visible = false;
            AddVinsDescTable.Visible = true;
            List<Mask> masks = MaskDataBase.GetMasksByMark(TreeView1.SelectedNode.Text);
            masks.Add(MaskDataBase.GetMasksByIdOfMask(12));
            ListBox2.Visible = true;
            ListBox2.DataSource = masks.Select(mask => mask.MaskView).ToList();
            ListBox2.DataBind();
            List<TypeOfVinPartMeaning> allEnums = Enum.GetValues(typeof(TypeOfVinPartMeaning)).Cast<TypeOfVinPartMeaning>().ToList();
            ListBox1.Visible = true;
            ListBox1.DataSource = allEnums;
            ListBox1.DataBind();
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            Button1.Visible = true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            AddMaskTable.Visible = true;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            AddNewMask();

        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            ClearDataSourse();
            AddNewMarkDiv.Visible = true;
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            string Mark = TextBox3.Text;
            if (!MarksDataBase.CheckMarkByMarkName(Mark))
            {
                MarksDataBase.AddNewMark(Mark);
            }
            else
            {
                MarkError.Visible = true;
            }
        }
        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {
            var vins = VinDataBase.GetAllVins().Where(v => v.VinTextValue.Contains(TextBox7.Text.ToUpper()));
            GridViewVins.DataSource = vins;
            GridViewVins.Visible = true;
            GridViewVins.DataBind();
        }
        protected void TextBox8_TextChanged(object sender, EventArgs e)
        {
            InitializeTreeView(TextBox8.Text);
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            MeaningfulMaskTable.Visible = true;
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            MarksDataBase.AddMeaningfulMask(TextBox5.Text, TreeView1.SelectedNode.Text);
        }
        protected void Button10_Click(object sender, EventArgs e)
        {
            
            var btn = (Button)sender;
            int RowIndex = Convert.ToInt32((btn.CommandArgument).ToString());
            while (RowIndex >= GridViewMasks.PageSize)
            {
                RowIndex -= GridViewMasks.PageSize;
            }
            var treeViewWhithVinDescriptions = (TreeView)GridViewMasks.Rows[RowIndex].FindControl("VinDescTree");
            treeViewWhithVinDescriptions.Nodes.Clear();
            if (!treeViewWhithVinDescriptions.Visible) 
            {
                AddNewVinDescLinks.Visible = false;
                AddNewVinDescLinksH.Visible = false;
                AddVinsDescTable.Visible = false;
                AddMaskTable.Visible = false;
                Button13.Visible = false;
                var lable = (Label)GridViewMasks.Rows[RowIndex].FindControl("LabelMaskVisibleFalse");
                var vinDescriptions = VinDescriptionsDataBase
                    .GetVinsDescriptionsByMaskId(Int32.Parse(lable.Text))
                    .Where(v => v.IdOfMark == MarksDataBase.GetMarkIdByMark(TreeView1.SelectedNode.Text));
                TreeNode MainmenuItem = new TreeNode
                {
                    Text = TreeView1.SelectedNode.Text

                };
                foreach (VinDescription vinDescription in vinDescriptions)
                {
                    RenderDescTree(vinDescription, ref treeViewWhithVinDescriptions);
                }
                treeViewWhithVinDescriptions.Visible = true;

                Button3.Visible = true;
            }
            else
            {
                treeViewWhithVinDescriptions.Visible = false;
            }
            
        }
        private void RenderDescTree(VinDescription vinDescription, ref TreeView VinDescTree)
        {
            TreeNode MainmenuItem = new TreeNode
            {
                Text = vinDescription.VinPart + " ( " + vinDescription.EnumMeaningOfVinParts + " ) -  " + vinDescription.VinPartDecription,
                Value = vinDescription.IdOfDescription.ToString()
            };
            RenderTree(MainmenuItem);

            VinDescTree.Nodes.Add(MainmenuItem);
            //DescTree.Visible = true;
        }
        protected void Button12_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            AddNewVinDescriptionAndLink(btn.CommandArgument);
        }
        protected void VinDescTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            AddNewVinDescLinks.Visible = false;
            AddNewVinDescLinksH.Visible = false;
            var VinDescTree = (TreeView)sender;
            AddVinsDescTable.Visible = false;
            if (VinDescTree.SelectedNode.Text == "AddNew")
            {
                Button13.Visible = false;
                AddNewVinDescLinks.Visible = true;
                AddNewVinDescLinksH.Visible = true;
                AddNewVinDescLinksH.InnerText = "Add on description: " 
                                                + VinDescTree.SelectedNode.Parent.Text.SubstringUpToTheSpace()
                                                + " on Mark - "
                                                + TreeView1.SelectedNode.Text;
                Button12.CommandArgument = VinDescTree.SelectedNode.Parent.Value;
                List<Mask> masks = MaskDataBase.GetMasksByMark(TreeView1.SelectedNode.Text);
                masks.Add(MaskDataBase.GetMasksByIdOfMask(_worldManufacturerIdentifierMaskId));
                DropDownList1.DataSource = masks.Select(mask => mask.MaskView).ToList();
                DropDownList1.DataBind();
                List<TypeOfVinPartMeaning> allEnums = Enum.GetValues(typeof(TypeOfVinPartMeaning))
                                                          .Cast<TypeOfVinPartMeaning>().ToList();
                DropDownList2.DataSource = allEnums;
                DropDownList2.DataBind();
            }
            else
            {
                Button13.Visible = true;
                Button13.CommandArgument = VinDescTree.SelectedNode.Value;
            }
        }
        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            TextBox6.Text = "";
            TextBox6.MaxLength = GetLenghtOfVinPart(DropDownList1.SelectedValue);
        }
        protected void Button13_Click(object sender, EventArgs e)
        {
           VinDescriptionsDataBase.RemoveVinDescById(Int32.Parse(((Button)sender).CommandArgument));
           //DescTree.Visible = false;
        }
        protected void GridViewVins_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var vins = VinDataBase.GetAllVins();
            VinDataBase.RemoveVin(vins.ElementAt(e.RowIndex));
            vins.RemoveAt(e.RowIndex);
            GridViewVins.DataSource = vins;
            GridViewVins.DataBind();
        }
        protected void Button11_Click(object sender, EventArgs e)
        {

        }
        private void AddNewMask()
        {
            string Mark = TreeView1.SelectedNode.Text;
            string MarkId = MaskDataBase.GetMarkIdByMark(Mark);
            MaskDataBase.AddMask(new Mask
            {
                IdOfMark = MarkId ?? MaskDataBase.GenerateNewMarkId(),
                MaskView = TextBox4.Text
            });
        }
        private void RenderTree(TreeNode NewMenuItem)
        {
            List<VinDescription> vinDescriptions = VinDescriptionsDataBase.GetVinDescriptionLinksByDescId(Int32.Parse(NewMenuItem.Value));
            foreach (VinDescription vinDescription in vinDescriptions)
            {
                NewMenuItem.ChildNodes.Add(new TreeNode
                {
                    Text = vinDescription.VinPart + " ( " + vinDescription.EnumMeaningOfVinParts + " ) -  " + vinDescription.VinPartDecription,
                    Value = vinDescription.IdOfDescription.ToString()
                });
            }
            NewMenuItem.ChildNodes.Add(new TreeNode
            {
                Text = "AddNew",
                Value = "NONE"
            });
            foreach (TreeNode MenuItem in NewMenuItem.ChildNodes)
            {
                if (MenuItem.Text != "AddNew")
                {
                    if (VinDescriptionsDataBase.GetVinDescriptionLinksByDescId(Int32.Parse(MenuItem.Value)).Any())
                    {
                        RenderTree(MenuItem);
                    }
                }
            }
        }
        private int GetLenghtOfVinPart(string Mask)
        {
            int counter = 0;
            foreach (char symbol in Mask)
            {
                if (symbol == 'X')
                {
                    counter++;
                }
            }
            return counter;
        }
        private void AddNewVinDescriptionAndLink(string parentDescription)
        {
            //string MarkId = MaskDataBase.GetMarkIdByMark(TreeView1.SelectedNode.Text);

            string MarkId = MarksDataBase.GetMarkIdByMark(TreeView1.SelectedNode.Text);
            VinDescription description = new VinDescription
            {
                VinPartDecription = TextBox9.Text,
                EnumMeaningOfVinParts = EnumService.StringToEnum(DropDownList2.SelectedValue),
                VinPart = TextBox6.Text,
                IdOfMark = MarkId,
                IdOfMask = DropDownList1.SelectedValue == _worldManufacturerIdentifierMask
                ? _worldManufacturerIdentifierMaskId
                : MaskDataBase.GetMasksByIdOfMark(MarkId)
                .Where(m => m.MaskView == DropDownList1.SelectedValue)
                .FirstOrDefault().IdOfMask

            };
            int IdOfDescriptionSecond = VinDescriptionsDataBase.AddVinDescriptionWhithOutput(description);
            int IdOfDescriptionFirst = VinDescriptionsDataBase.GetVinsDescriptionsById(Int32.Parse(parentDescription)).IdOfDescription;
            VinDescriptionsLink vinDescriptionsLink = new VinDescriptionsLink
            {
                IdOfDescriptionSecond = IdOfDescriptionSecond,
                IdOfDescriptionFirst = IdOfDescriptionFirst
            };
            LinkDataBase.AddNewLink(vinDescriptionsLink);
        }
        private void InitializeTreeView()
        {
            TreeView1.Nodes.Clear();
            TreeNode node = new TreeNode
            {
                Value = _treeMainString
            };
            TreeNode masksNode = new TreeNode
            {
                Value = _treeMasksString
            };
            TreeNode VinsNode = new TreeNode
            {
                Value = _treeVinsString
            };
            List<string> marks = GetListOfMarks();
            foreach (string mark in marks)
            {
                masksNode.ChildNodes.Add(new TreeNode
                {
                    Value = mark
                });
            }

            node.ChildNodes.Add(masksNode);
            node.ChildNodes.Add(VinsNode);
            TreeView1.Nodes.Add(node);

        }
        private void InitializeTreeView(string FindMark)
        {
            TreeView1.Nodes.Clear();
            TreeNode node = new TreeNode
            {
                Value = _treeMainString
            };
            TreeNode masksNode = new TreeNode
            {
                Value = _treeMasksString
            };
            TreeNode VinsNode = new TreeNode
            {
                Value = _treeVinsString
            };
            List<string> marks = GetListOfMarks();
            foreach (string mark in marks.Where(m => m.ToLower().Contains(FindMark.ToLower())))
            {
                masksNode.ChildNodes.Add(new TreeNode
                {
                    Value = mark
                });
            }

            node.ChildNodes.Add(masksNode);
            node.ChildNodes.Add(VinsNode);
            TreeView1.Nodes.Add(node);

        }
        private List<string> GetListOfMarks()
        {
            return MarksDataBase.GetAllMarks();
        }
        private void AddNewVinDescription()
        {
            //string MarkId = MaskDataBase.GetMarkIdByMark(TreeView1.SelectedNode.Text);
            string MarkId = MarksDataBase.GetMarkIdByMark(TreeView1.SelectedNode.Text);
            VinDescription description = new VinDescription
            {
                VinPartDecription = TextBox2.Text,
                EnumMeaningOfVinParts = EnumService.StringToEnum(ListBox1.SelectedValue),
                VinPart = TextBox1.Text,
                IdOfMark = MarkId,
                IdOfMask = ListBox2.SelectedValue == _worldManufacturerIdentifierMask
                ? _worldManufacturerIdentifierMaskId 
                : MaskDataBase.GetMasksByIdOfMark(MarkId)
                .Where(m => m.MaskView == ListBox2.SelectedValue)
                .FirstOrDefault().IdOfMask

            };
            VinDescriptionsDataBase.AddVinDescription(description);
        }
        private void ClearDataSourse()
        {
            GridViewVinsResult.Visible = false;
            GridViewAll.Visible = false;
            GridViewMasks.Visible = false;
            GridViewVins.Visible = false;
            VinDescH.Visible = false;
            AddVinsDescTable.Visible = false;
            Button3.Visible = false;
            Button4.Visible = false;
            AddMaskTable.Visible = false;
            MainH.Visible = false;
            AddNewMarkDiv.Visible = false;
            TextBox7.Visible = false;
            MeaningfulMaskTable.Visible = false;
            Button8.Visible = false;
            MeaningfulMask.Visible = false;
            GridViewVinDescs.Visible = false;
            AddNewVinDescLinks.Visible = false;
            AddNewVinDescLinksH.Visible = false;
            //DescTree.Visible = false;
            Button13.Visible = false;
        }
        private void InitializeMarkDataSourse(TreeNode selectedNode)
        {
            switch (selectedNode.Parent.Text)
            {
                case _treeMasksString:
                    Button4.Visible = true;
                    InitializeMarkDataSourse_Masks(selectedNode);
                    break;
                case _treeVinDescsString:
                    InitializeMarkDataSourse_VinDescs(selectedNode);
                    break;
            }
        }
        private void InitializeMarkDataSourse_Masks(TreeNode selectedNode)
        {
            var Mask = MarksDataBase.GetMeaningfulMaskbyMark(selectedNode.Text);
            if (string.IsNullOrEmpty(Mask))
            {
                Button8.Visible = true;
            }
            else
            {
                MeaningfulMask.Style.Value = "color:red";
                MeaningfulMask.Visible = true;
                string NewMask = InitializeMeaningfulMask(Mask);
                if (Mask.Substring(0, NewMask.Length) == NewMask)
                {
                    MeaningfulMask.Style.Value = "color:Black";
                    MeaningfulMask.InnerText = NewMask;
                }
                else
                {
                    MeaningfulMask.InnerText = NewMask;
                }
            }
            var masks = MaskDataBase.GetMasksByMark(selectedNode.Text);
            masks.Add(MaskDataBase.GetMasksByIdOfMask(12));
            GridViewMasks.DataSource = masks;
            GridViewMasks.DataBind();
            GridViewMasks.Visible = true;
        }
        private void InitializeMarkDataSourse_VinDescs(TreeNode selectedNode)
        {
            var vinDescs = VinDescriptionsDataBase.GetVinDescroptionByMark(selectedNode.Text);
            GridViewVinDescs.DataSource = vinDescs;
            GridViewVinDescs.Visible = true;
            Button3.Visible = true;
            GridViewVinDescs.DataBind();
        }
        private void InitializeVinsDesc()
        {
            var vinDescs = VinDescriptionsDataBase.GetAllVinsDescriptionsView();
            VinDescH.Visible = true;
            GridViewAll.DataSource = vinDescs;
            GridViewAll.Visible = true;
            GridViewAll.DataBind();
        }
        private void InitializeVins()
        {
            var vins = VinDataBase.GetAllVins();
            TextBox7.Visible = true;
            GridViewVins.DataSource = vins;
            GridViewVins.Visible = true;
            GridViewVins.DataBind();
        }
        private void InitializeMasks()
        {
            var masks = MaskDataBase.GetAllMasksView();
            VinDescH.Visible = true;
            GridViewAll.DataSource = masks;
            GridViewAll.DataBind();
            GridViewAll.Visible = true;
        }
        private string InitializeMeaningfulMask(string Mask)
        {
            List<Mask> masks = MaskDataBase.GetMasksByMark(TreeView1.SelectedValue);
            int Lenght = GetLenghtOfVinPart(Mask);
            char[] CheckMask = new char[Lenght];
            foreach (Mask mask in masks)
            {
                for (int i = 0; i < Lenght; i++)
                {
                    if (mask.MaskView.ElementAt(i) == 'X')
                    {
                        CheckMask[i] = 'X';
                    }
                    else if (CheckMask[i] != 'X')
                    {
                        CheckMask[i] = '0';
                    }
                }
            }
            string StringMask = "";
            for (int i = 0; i < Lenght; i++)
            {
                if (CheckMask[i] == 'X')
                    StringMask += 'X';
                else
                    StringMask += '0';
            }
            return StringMask;
        }
    }
}
