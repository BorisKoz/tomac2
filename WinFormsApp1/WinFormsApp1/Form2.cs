using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private Form1 _form1;
        private Form3 form3;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 MainMenu)
        {
            InitializeComponent();
            _form1 = MainMenu;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            streamData.DataSource = GetStreamList();
            streamData.Columns[1].HeaderText = "Поток";
            streamData.ReadOnly = true;
        }

        private DataTable GetStreamList()
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Streams", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }

        private void streamData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = streamData.SelectedRows[0].Cells[1].Value + string.Empty;
            button2.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            string editId = streamData.SelectedRows[0].Cells[0].Value + string.Empty;
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Streams SET Name = \'"+textBox1.Text+"\' WHERE id = " + editId, con))
                {
                    con.Open();
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        streamData.DataSource = GetStreamList();
                    }
                    catch
                    {
                        label1.Visible = true;
                    }
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            if (textBox2.Text != "")
            {
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Streams VALUES (\'"+textBox2.Text+"\')", con))
                    {
                        con.Open();
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            streamData.DataSource = GetStreamList();
                        }
                        catch
                        {
                            label2.Visible = true;
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            label3.Visible = true;
            button5.Visible = false;
            button6.Visible = true;
            button7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            button5.Visible = true;
            button6.Visible = false;
            button7.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string editId = streamData.SelectedRows[0].Cells[0].Value + string.Empty;
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Streams WHERE id = " + editId, con))
                {
                    con.Open();
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        streamData.DataSource = GetStreamList();
                    }
                    catch
                    {
                        label4.Visible = true;
                    }
                }
            }
            label3.Visible = false;
            button5.Visible = true;
            button6.Visible = false;
            button7.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] to_split = textBox1.Text.Split('-');
            string editId = streamData.SelectedRows[0].Cells[0].Value + string.Empty;
            form3 = new Form3(this, editId, to_split[1]) { Visible = false };
            form3.Show();
            this.Hide();
        }
    }
}
