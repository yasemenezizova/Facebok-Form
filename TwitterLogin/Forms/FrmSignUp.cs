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
    public partial class FrmSignUp : Form
    {
        public FrmSignUp()
        {
            InitializeComponent();
        }

       public bool EmptyControl()
        {
            bool control = true;
            if (txtName.Text==null)
            {
                txtName.ErrorText = "Enter your name!";
                control = false;
            }
            if (txtSurname.Text== null)
            {
                txtSurname.ErrorText = "Enter your Surname!";
                control = false;
            }
            if (txtEmailOrPhone== null)
            {
                txtEmailOrPhone.ErrorText = "Enter your phone Number or email!";
                control = false;
            }
            if (txtPassword.Text == null)
            {
                txtPassword.ErrorText = "Enter your passwor!";
                control = false;
            }
            if (dtEdtBirthday.EditValue == null)
            {
                dtEdtBirthday.ErrorText = "Enter your birthday!";
                control = false;
            }
            if (rdGrpGender.EditValue == null)
            {
                rdGrpGender.ErrorText = "Enter your gender!";
                control = false;
            }

            return control;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        public void InsertInfo()
        {
           
            SqlConnection sqlConnection = new SqlConnection(SqlUtils.conString);
            sqlConnection.Open();
            string query = @"INSERT INTO [dbo].[UserInfo]
                            ([Name]
                            ,[Surname]
                            ,[EmailOrPhone]
                            ,[Password]
                            ,[Birthday]
                            ,[Gender])
                        VALUES
                            (@Name
                            , @Surname
                            , @EmailOrPhone
                            , @Password
                            , @Birthday
                            , @Gender);";

            SqlCommand sqlCommand = new SqlCommand(query,sqlConnection);
            sqlCommand.Parameters.Add("Name", SqlDbType.NVarChar).Value = txtName.Text;
            sqlCommand.Parameters.Add("Surname", SqlDbType.NVarChar).Value = txtSurname.Text;
            sqlCommand.Parameters.Add("EmailOrPhone", SqlDbType.NVarChar).Value = txtEmailOrPhone.Text;
            sqlCommand.Parameters.Add("Password", SqlDbType.NVarChar).Value = txtPassword.Text;
            sqlCommand.Parameters.Add("Birthday", SqlDbType.Date).Value = dtEdtBirthday.Text;
            sqlCommand.Parameters.Add("Gender", SqlDbType.Bit).Value = rdGrpGender.Text;
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (EmptyControl() == true)
            {
                InsertInfo();
                MessageBox.Show("Information saved!!!");
                this.Close();
            }
        }

        
    }
}
