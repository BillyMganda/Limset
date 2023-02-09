using Limset.Helper;

namespace Limset
{
    public partial class Login : Form
    {
        private readonly Ilogin_service _service;
        public Login(Ilogin_service service)
        {            
            InitializeComponent();
            _service = service;
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username_ok = _service.is_username_ok(txtUsername.Text);
                var password_ok = _service.is_password_ok(txtPassword.Text);
                if (!username_ok || !password_ok)
                {
                    MessageBox.Show("invalid username or password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    var user = await _service.is_user_available(txtUsername.Text);
                    if (!user)
                    {
                        MessageBox.Show("user not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}