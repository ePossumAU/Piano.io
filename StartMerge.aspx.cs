using System;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Web.UI.WebControls;

namespace PianoFileMerge2
{

    public partial class StartMerge : System.Web.UI.Page
    {
        private enum Transform { FileA, FileB, Merge };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Create blank tables, visualisation of file's structure.
                CreateEmptyGrids();
            }
        }

        private void CreateEmptyGrids()
        {
            CreateAndDisplayBlank((int)Transform.FileA);
            CreateAndDisplayBlank((int)Transform.FileB);
            CreateAndDisplayBlank((int)Transform.Merge);
        }

        private void CreateViewColumnHeaders(DataTable table, int selectedfile)
        {
            // Declare DataColumn and DataRow variables.
            DataColumn column;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.
            column = new DataColumn("user_id");
            column.DataType = Type.GetType("System.String");
            table.Columns.Add(column);

            // Create second column.
            if (selectedfile == (int)Transform.FileA || selectedfile == (int)Transform.Merge)
            {
                column = new DataColumn("email");
                column.DataType = Type.GetType("System.String");
                table.Columns.Add(column);
            }

            // Create third column.
            if (selectedfile == (int)Transform.FileB || selectedfile == (int)Transform.Merge)
            {
                column = new DataColumn("first_name");
                column.DataType = Type.GetType("System.String");
                table.Columns.Add(column);
            }

            // Create fourth column.
            if (selectedfile == (int)Transform.FileB || selectedfile == (int)Transform.Merge)
            {
                column = new DataColumn("last_name");
                column.DataType = Type.GetType("System.String");
                table.Columns.Add(column);
            }

            // Create fourth column.
            if (selectedfile == (int)Transform.Merge)
            {
                column = new DataColumn("Marker");
                column.DataType = Type.GetType("System.String");
                table.Columns.Add(column);
            }
        }

        private void CreateAndDisplayBlank(int selectedfile)
        {
            //Create three new DataTables and DataSource objects for FileA, FileB and Merge.
            DataTable table = new DataTable();

            CreateViewColumnHeaders(table, selectedfile);

            // Create new DataRow objects and add to DataTable.
            DataRow row;

            // Create new DataRow objects and add to DataTable.
            for (int i = 0; i < 10; i++)
            {
                row = table.NewRow();
                table.Rows.Add(row);
            }

            // Create a DataView using the DataTable in gridview.
            DataView view = new DataView(table);

            // Set a DataGrid control's DataSource to the DataView.
            if (selectedfile == (int)Transform.FileA)
            {
                GrvDataFileA.DataSource = view;
                GrvDataFileA.DataBind();
            }
            else if (selectedfile == (int)Transform.FileB)
            {
                GrvDataFileB.DataSource = view;
                GrvDataFileB.DataBind();
            }
            else if (selectedfile == (int)Transform.Merge)
            {
                GrvDataMerge.DataSource = view;
                GrvDataMerge.DataBind();
            }
        }


        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            CreateEmptyGrids();
            LblStatus.Text = "Message: Select file CSV FileA.";
            LblStatus.ForeColor = System.Drawing.Color.Black; ;
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            //Create control objects for both Upload Files and 
            FileUpload uploadControl = new FileUpload();
            GridView grvDataFiles = new GridView();

            //Perform file upload iteration twice, first for FileA, then for FileB.
            for (int csvFile = 1; csvFile <= 2; csvFile++)
            {
                switch (csvFile)
                {
                    case 1:
                        uploadControl = FileUpload1;
                        grvDataFiles = GrvDataFileA;
                        break;
                    case 2:
                        uploadControl = FileUpload2;
                        grvDataFiles = GrvDataFileB;
                        break;
                }

                if (uploadControl.HasFile)
                {
                    try
                    {
                        string currentPath = Server.MapPath("~/") +
                                      Path.GetFileName(uploadControl.FileName);
                        uploadControl.SaveAs(currentPath);

                        grvDataFiles.DataSource = GetDataTableFromCSVFile(currentPath);
                        grvDataFiles.DataBind();
                        LblStatus.Text = (csvFile == 1) ? "CSV Upload status: <b>File A</b> uploaded." : "CSV Upload status: <b>File A and File B</b> uploaded.";
                        LblStatus.ForeColor = System.Drawing.Color.Green ;
                        File.Delete(currentPath);

                    }
                    catch (Exception ex)
                    {
                        LblStatus.Text = @"CSV Upload status: The file could not be uploaded. 
                    The following error has occured: " + ex.Message;
                        LblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {

                    if (FileUpload1.HasFile && !FileUpload2.HasFile)
                        LblStatus.Text = "Message: Select file CSV FileB.";
                    else if (!FileUpload1.HasFile && FileUpload2.HasFile)
                        LblStatus.Text = "Message: Select file CSV FileA.";
                    LblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private static DataTable GetDataTableFromCSVFile(string csvfilePath)
        {
            DataTable csvData = new DataTable();
            using (TextFieldParser csvReader = new TextFieldParser(csvfilePath))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                //Read columns from CSV file
                string[] colFields = csvReader.ReadFields();

                foreach (string column in colFields)
                {
                    DataColumn datacolumn = new DataColumn(column);
                    datacolumn.AllowDBNull = true;
                    csvData.Columns.Add(datacolumn);
                }

                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }
            }
            return csvData;
        }


        protected void BtnMerge_Click(object sender, EventArgs e)
        {
            CreateAndDisplayMerge();
        }

        private void CreateAndDisplayMerge()
        {
            //Create three new DataTables and DataSource objects for FileA, FileB and Merge.
            DataTable mergeTable = new DataTable();

            CreateViewColumnHeaders(mergeTable, (int)Transform.Merge);

            // Create new DataRow objects and add to merge DataTable.
            DataRow targetFileRow;

            foreach (GridViewRow sourceFileOneRow in GrvDataFileA.Rows)
            {
                targetFileRow = mergeTable.NewRow();

                //Get email component only from email address in FileA, exclude host component.
                //Apri search/user does not search @string

                string csvFileEmail = Server.HtmlEncode(sourceFileOneRow.Cells[1].Text);
                string[] wordSplit = csvFileEmail.Split('@');
                //First [0] part is email and second [1] is host. 
                var searchEmail = wordSplit[0];

                var systemUID = string.Empty;
                //Use API to check email exists
                if (ExistsUser(searchEmail, ref systemUID))
                {
                    targetFileRow["user_id"] = systemUID;
                    targetFileRow["Marker"] = "Found";
                }
                else
                    targetFileRow["user_id"] = sourceFileOneRow.Cells[0].Text;

                targetFileRow["email"] = sourceFileOneRow.Cells[1].Text;

                //Merge with file B
                foreach (GridViewRow sourceFileTwoRow in GrvDataFileB.Rows)
                {
                    if (sourceFileOneRow.Cells[0].Text == sourceFileTwoRow.Cells[0].Text) //UserId from A = B
                    {
                        targetFileRow["first_name"] = sourceFileTwoRow.Cells[1].Text;
                        targetFileRow["last_name"] = sourceFileTwoRow.Cells[2].Text;
                    }
                }

                mergeTable.Rows.Add(targetFileRow);
            }

            // Create a DataView using the DataTable in gridview.
            DataView view = new DataView(mergeTable);

            // Sort by email, last name in descending order
            view.Sort = "email ASC, last_name ASC";

            // Set a DataGrid control's DataSource to the DataView.
            GrvDataMerge.DataSource = view;
            GrvDataMerge.DataBind();

            //Highlight changes made for inconsistent Uids.
            foreach (GridViewRow dataRow in GrvDataMerge.Rows)
            {
                if (dataRow.Cells[4].Text == "Found")
                {
                    dataRow.Cells[0].BackColor = System.Drawing.Color.LightGreen;
                    dataRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
                else
                    dataRow.HorizontalAlign = HorizontalAlign.Left;
            }

        }

        private bool ExistsUser(string searchEmail, ref string systemUserID)
        {
            var emailFound = false;

            string[] systemRecord = new string[2] { string.Empty, string.Empty };
            using (PianoAPIs apiManager = new PianoAPIs())
                systemRecord = apiManager.GetUserDetails(searchEmail);

            if (searchEmail == systemRecord[0])
            {
                systemUserID = systemRecord[1];
                emailFound = true;
            }

            return emailFound;
        }

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            //Finally create CSV file output.
            CreateMergedFile();
        }

        private void CreateMergedFile()
        {
            TxtApp.Text = GrvDataMerge.Columns.Count.ToString();
            TxtAppPublisher.Text = GrvDataMerge.Rows.Count.ToString();

            //return;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=GridViewExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();
            //Add header, override grid header names wiith software header names.
            sb.Append("user_id,email,firstname,lastname");

            //append new line
            sb.Append("\r\n");
            for (int row = 0; row < GrvDataMerge.Rows.Count; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    //add separator
                    sb.Append(GrvDataMerge.Rows[row].Cells[col].Text + ',');
                }
                //append new line
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("PianoLogin.aspx");
        }
    }
}