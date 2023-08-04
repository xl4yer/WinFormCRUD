using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WinformSample.Models;

namespace WinformSample
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;Database=webapi;Uid=root;Pwd=Admin1234;";
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var lst = new List<customer>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand($"SELECT * FROM customer", con);
                    com.CommandType = CommandType.Text;

                    com.Parameters.Clear();

                    MySqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new customer
                        {
                            customerid = Convert.ToInt32(rdr["customerid"]),
                            name = rdr["name"].ToString(),
                            address = rdr["address"].ToString(),
                        });
                    }

                    dataGridView1.DataSource = lst;
                    con.Close();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            var lst = new List<customer>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("INSERT INTO customer (name,address) VALUES (@name,@address)", con);
                    com.CommandType = CommandType.Text;

                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@name", txtname.Text);
                    com.Parameters.AddWithValue("@address", txtaddress.Text);
                    i = com.ExecuteNonQuery();
                    con.Close();

                    if (i >= 1)
                    {
                        MessageBox.Show("Successfully Save!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Transaction Cancelled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {

                    //
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            txtname.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            txtaddress.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = 0;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("UPDATE customer SET name=@name, address=@address WHERE customerid=@customerid", con);
                    com.CommandType = CommandType.Text;

                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@name", txtname.Text);
                    com.Parameters.AddWithValue("@address", txtaddress.Text);
                    com.Parameters.AddWithValue("@customerid", txtid.Text);

                    i = com.ExecuteNonQuery();
                    con.Close();

                    if (i >= 1)
                    {
                        MessageBox.Show("Successfully Updated!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button2.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Transaction Cancelled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Do You Want To Delete This Record?", "Warning", MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog == DialogResult.Yes)
            {
                int i = 0;
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        MySqlCommand com = new MySqlCommand("DELETE FROM customer WHERE customerid=@customerid", con);
                        com.CommandType = CommandType.Text;

                        com.Parameters.Clear();
                        com.Parameters.AddWithValue("@customerid", dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                        i = com.ExecuteNonQuery();
                        con.Close();

                        if (i >= 1)
                        {
                            MessageBox.Show("Successfully Deleted!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button2.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Transaction Cancelled", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtid.Clear();
            txtname.Clear();
            txtaddress.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var lst = new List<customer>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand($"SELECT * FROM customer WHERE customerid=@customerid OR name=@name OR address=@address", con);
                    com.CommandType = CommandType.Text;

                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@customerid", txtsearch.Text);
                    com.Parameters.AddWithValue("@name", txtsearch.Text);
                    com.Parameters.AddWithValue("@address", txtsearch.Text);

                    MySqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        lst.Add(new customer
                        {
                            customerid = Convert.ToInt32(rdr["customerid"]),
                            name = rdr["name"].ToString(),
                            address = rdr["address"].ToString(),
                        });
                    }

                    dataGridView1.DataSource = lst;
                    con.Close();
                }
                catch (Exception ex)
                {
                    // Handle the exception
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button2.PerformClick();
        }
    }
}

