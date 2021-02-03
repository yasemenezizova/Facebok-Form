using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookLogin
{
    public partial class FrmLogIn : Form
    {

        public FrmLogIn()
        {
            InitializeComponent();
      
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSingUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSignUp frmSignUp = new FrmSignUp();
            frmSignUp.ShowDialog();

        }
       

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string query = $"SELECT count(*) FROM UserInfo WHERE EmailOrPhone=@EmailOrPhone AND Password=@Password";
            SqlConnection sqlConnection = new SqlConnection(SqlUtils.conString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add("@EmailOrPhone", SqlDbType.NVarChar).Value = txtUserName.Text;
            sqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = txtPassword.Text;
            int count = (int)sqlCommand.ExecuteScalar();
            if (count == 0)
            {
                MessageBox.Show("Istifadeci adi ve ya sifre sehvdir!");
            }
            else
            {
                FrmUser frmUser = new FrmUser(txtUserName.Text, txtPassword.Text);
                frmUser.Show();
                
            }
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

    }
}
