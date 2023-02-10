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
        private readonly Iadmin_service _service;
        public Disable_User(Iadmin_service service)
        {
            InitializeComponent();
            _service = service;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfirstName.Text == "" || txtLastName.Text == "")
                {
                    MessageBox.Show("provide all details to proceed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    _service.disable_user(txtfirstName.Text, txtLastName.Text);
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
