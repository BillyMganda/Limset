using Limset.Models;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System.Data;
using System.Data.OleDb;
using System.Text;
using DataTable = System.Data.DataTable;

namespace Limset
{
    public partial class Admin_Data_Online : Form
    {
        public Admin_Data_Online()
        {
            InitializeComponent();
        }
                
        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|XLSX files (*.xlsx)|*.xlsx|XLS files (*.xls)|*.xls|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                    string filePath = openFileDialog.FileName;
                    string extension = Path.GetExtension(filePath);

                    switch (extension)
                    {
                        case ".csv":
                            DataTable csvData = ReadCSV(filePath);
                            dataGridView1.DataSource = csvData;
                            break;
                        case ".xlsx":
                        case ".xls":
                            DataTable excelData = ReadExcel(filePath);
                            dataGridView1.DataSource = excelData;
                            break;
                        default:
                            MessageBox.Show("Invalid file type selected. Please select a .csv, .xlsx, or .xls file.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } 
        }

        private DataTable ReadCSV(string filePath)
        {
            DataTable csvData = new DataTable();
            using (Microsoft.VisualBasic.FileIO.TextFieldParser parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(filePath))
            {
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields()!;
                    if (csvData.Columns.Count == 0)
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            csvData.Columns.Add();
                        }
                    }
                    csvData.Rows.Add(fields);
                }
            }
            return csvData;
        }

        private DataTable ReadExcel(string filePath)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = (_Worksheet)xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            DataTable excelData = new DataTable();
            for (int i = 1; i <= xlRange.Columns.Count; i++)
            {
                excelData.Columns.Add(i.ToString());
            }

            for (int i = 1; i <= xlRange.Rows.Count; i++)
            {
                DataRow row = excelData.NewRow();
                for (int j = 1; j <= xlRange.Columns.Count; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        row[j - 1] = xlRange.Cells[i, j].Value2.ToString();
                    }
                }
                excelData.Rows.Add(row);
            }

            xlWorkbook.Close();
            xlApp.Quit();

            return excelData;
        }

        private async void btnUpload_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                List<post_data_online> list = new List<post_data_online>();

                // Get data from DataGridView and add to list
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        post_data_online online = new post_data_online
                        {
                            reference_range_id = (int)row.Cells[0].Value,
                            test_format_id = (int)row.Cells[1].Value,
                            code = row.Cells[2].Value.ToString()!,
                            clinical_field_id = int.Parse(row.Cells[3].Value.ToString())
                        };

                        list.Add(online);
                    }
                }

                // Serialize list of post_data_online to JSON
                string json = JsonConvert.SerializeObject(list);

                // Make POST request to server with JSON data
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:44355/api/users", content);

                    if (response.IsSuccessStatusCode) 
                    {
                        MessageBox.Show("Data posted successfully");
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while posting data");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
