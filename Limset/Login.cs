using Limset.Helper;

namespace Limset
{
    public partial class Login : Form
    {
        private bool _dragging;
        private Point _offset;
                
        login_service _service = new login_service();

        public Login()
        {
            InitializeComponent();                
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _offset = e.Location;
            }
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - _offset.X, currentScreenPos.Y - _offset.Y);
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
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
                    MessageBox.Show("400: invalid username or password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    var user = await _service.is_user_available(txtUsername.Text);
                    if (user == null)
                    {
                        MessageBox.Show("401: user not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        var is_active = await _service.is_user_active(txtUsername.Text);
                        if(!is_active)
                        {
                            MessageBox.Show("402: Error, user is not active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            var is_true = _service.verify_password_hash(txtPassword.Text, user.password_hash, user.password_salt);
                            if (!is_true)
                            {
                                MessageBox.Show("403: incorrect username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                var role = await _service.user_role(txtUsername.Text);
                                if (role == "Admin")
                                {
                                    Hide();
                                    Admin admin = new Admin();
                                    admin.Show();
                                }
                                else if (role == "User")
                                {
                                    Hide();
                                    User user_ = new User();
                                    user_.Show();
                                }
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }
    }
}