
using Limset.Helper;

namespace Limset
{
    public partial class Add_User : Form
    {
        private readonly Iadmin_service _service;
        public Add_User(Iadmin_service service)
        {
            InitializeComponent();
            _service = service;
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        string user_role = "";
        private void rdoAdmin_CheckedChanged(object sender, EventArgs e)
        {
            user_role = "Admin";
        }
        private void rdoUser_CheckedChanged(object sender, EventArgs e)
        {
            user_role = "User";
        }        
        private async void btnSave_Click(object sender, EventArgs e)
        {
            var available = _service.are_all_details_available(txtfirstName.Text, txtLastName.Text, txtUsername.Text, txtPassword.Text, user_role);
            if(!available)
            {
                MessageBox.Show("provide all details to proceed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var length = _service.credential_lengths(txtUsername.Text, txtPassword.Text);
                if(!length)
                {
                    MessageBox.Show("invalid lengths for username and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    var user_available = _service.is_user_available(txtUsername.Text);
                    if(user_available == true)
                    {
                        MessageBox.Show("username already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {                        
                        await _service.add_user(txtfirstName.Text, txtLastName.Text, txtUsername.Text, txtPassword.Text, user_role);
                        MessageBox.Show("user successfully added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
        }

        
    }
}
