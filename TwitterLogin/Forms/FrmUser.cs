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
    public partial class FrmUser : Form
    {
        public string password { get; set; }
        public string username { get; set; }
        public FrmUser(string Username="", string Password="")
        {
            InitializeComponent();
            this.username = Username;
            this.password = Password;
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            SetUserInfo();
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void SetUserInfo()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlUtils.conString);

            sqlConnection.Open();
            string query = $@"select Name+' '+Surname as Info, Name, Surname, EmailOrPhone, Birthday, Gender
                            from UserInfo where EmailOrPhone = '{username}' and Password = '{password}'";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                lblMainName.Text = reader["Info"].ToString();
                txtName.Text = reader["Name"].ToString();
                txtSurname.Text = reader["Surname"].ToString();
                txtUserName.Text = reader["EmailOrPhone"].ToString();
                dtEdtBirthday.EditValue = reader["Birthday"].ToString();
                rdGrpGender.EditValue = reader["Gender"];
            }
            reader.Close();
            sqlConnection.Close();
        }
        public void DeleteUser()
        {
            string query = $"delete from UserInfo where EmailOrPhone='{username}' and Password='{password}'";
            SqlConnection sqlConnection = new SqlConnection(SqlUtils.conString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the account?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                DeleteUser();
                MessageBox.Show("Your account has been successfully deleted", "Information", MessageBoxButtons.OK);
                Close();
            }
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = $"update UserInfo set Name='{txtName.Text}', Surname='{txtSurname.Text}', EmailOrPhone='{txtUserName.Text}', Gender='{rdGrpGender.EditValue}', Birthday='{dtEdtBirthday.EditValue}' where EmailOrPhone='{username}' and Password='{password}'";
            SqlConnection sqlConnection = new SqlConnection(SqlUtils.conString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Information Updated");
            sqlConnection.Close();
            Close();
        }
    }
}
