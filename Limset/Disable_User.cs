using Limset.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Limset
{
    public partial class Disable_User : Form
    {
        private static readonly LimSet_DbContext? _context;
        admin_service _service = new admin_service(_context);
        public Disable_User()
        {
            InitializeComponent();            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfirstName.Text == "" || txtLastName.Text == "")
                {
                    MessageBox.Show("provide all details to proceed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    await _service.disable_user(txtfirstName.Text, txtLastName.Text);
                    MessageBox.Show("operation successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }
    }
}
