using Limset.Helper;

namespace Limset
{
    public partial class Admin : Form
    {
        private readonly Iadmin_service _service;
        public Admin(Iadmin_service service)
        {
            InitializeComponent();
            _service = service;
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Add_User add = new Add_User(_service);
            add.ShowDialog();
        }

        private void btnDisableUser_Click(object sender, EventArgs e)
        {
            Disable_User du = new Disable_User(_service);
            du.ShowDialog();
        }
    }
}
