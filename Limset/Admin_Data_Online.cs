using Limset.Models;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using OfficeOpenXml;
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
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    DataTable dataTable = new DataTable(worksheet.Name);
                    for (int row = worksheet.Dimension.Start.Row; row <= worksheet.Dimension.End.Row; row++)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                        {
                            if (row == worksheet.Dimension.Start.Row)
                            {
                                dataTable.Columns.Add(worksheet.Cells[row, col].Value.ToString());
                            }
                            else
                            {                                
                                dataRow[dataTable.Columns[col - 1].ColumnName] = worksheet.Cells[row, col].Value ?? DBNull.Value;
                            }
                        }
                        if (row != worksheet.Dimension.Start.Row)
                        {
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    return dataTable;
                }
            }
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
