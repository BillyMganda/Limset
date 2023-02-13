using ExcelDataReader;
using Limset.Models;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace Limset
{
    public partial class Admin_Data_Online : Form
    {
        public Admin_Data_Online()
        {
            InitializeComponent();
        }

        DataTableCollection? tables;
        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        txtFilePath.Text = ofd.FileName;
                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                tables = result.Tables;
                                cboSheet.Items.Clear();
                                foreach (DataTable table in tables)
                                    cboSheet.Items.Add(table.TableName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } 
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = tables![cboSheet.SelectedItem.ToString()]!;
                if (dt != null)
                {
                    List<post_data_online> list = new List<post_data_online>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        post_data_online obj = new post_data_online();
                        obj.reference_range_id = Convert.ToInt32(dt.Rows[i]["CustomerID"]);
                        obj.test_format_id = Convert.ToInt32(dt.Rows[i]["CompanyName"]);
                        obj.code = dt.Rows[i]["ContactTitle"].ToString()!;
                        obj.clinical_field_id = Convert.ToInt32(dt.Rows[i]["City"]);
                        obj.clinical_condition = dt.Rows[i]["Address"].ToString()!;
                        obj.sex = dt.Rows[i]["Country"].ToString()!;
                        obj.age = dt.Rows[i]["Fax"].ToString()!;
                        obj.text_description = dt.Rows[i]["Phone"].ToString()!;
                        obj.phone_lo = dt.Rows[i]["PostalCode"].ToString()!;
                        obj.phone_hi = dt.Rows[i]["Region"].ToString()!;
                        obj.flag_lo = dt.Rows[i]["Region"].ToString()!;
                        obj.flag_hi = dt.Rows[i]["Region"].ToString()!;
                        obj.alert_lo = dt.Rows[i]["Region"].ToString()!;
                        obj.alert_hi = dt.Rows[i]["Region"].ToString()!;
                        obj.auth_lo = dt.Rows[i]["Region"].ToString()!;
                        obj.auth_hi = dt.Rows[i]["Region"].ToString()!;
                        obj.range_date = Convert.ToDateTime(dt.Rows[i]["Region"]);
                        obj.line_position = Convert.ToInt32(dt.Rows[i]["Region"]);
                        obj.is_child = Convert.ToBoolean(dt.Rows[i]["Region"]);
                        list.Add(obj);
                    }
                    dataGridView1.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
