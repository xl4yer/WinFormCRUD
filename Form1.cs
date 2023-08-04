using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;
using System.Data;
using WinformSample.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinformSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;Database=webapi;Uid=root;Pwd=Admin1234;";
        private void button1_Click(object sender, EventArgs e)
        {
            var lst = new List<user>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT * FROM user WHERE Username = @user and Password = @pass", con);
                    com.CommandType = CommandType.Text;

                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@user", txtusername.Text);
                    com.Parameters.AddWithValue("@pass", txtpassword.Text);

                    MySqlDataReader rdr = com.ExecuteReader();
                    if (rdr.Read())
                    {
                        lst.Add(new user
                        {
                            userid = Convert.ToInt32(rdr["userid"]),
                            username = rdr["username"].ToString(),
                            password = rdr["password"].ToString(),
                        });

                        var Form = new Form2();
                        Form.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password!", "INVALID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}