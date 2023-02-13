using Limset.Helper;

namespace Limset
{
    public partial class Admin : Form
    {
        
        public Admin()
        {
            InitializeComponent();
            
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Add_User add = new Add_User();
            add.ShowDialog();
        }

        private void btnDisableUser_Click(object sender, EventArgs e)
        {
            Disable_User du = new Disable_User();
            du.ShowDialog();
        }

        private void btnDataOnline_Click(object sender, EventArgs e)
        {
            Admin_Data_Online ado = new Admin_Data_Online();
            ado.ShowDialog();
        }

        private void btnDataMachine_Click(object sender, EventArgs e)
        {

        }
    }
}
