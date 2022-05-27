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
    public partial class Form3 : Form
    {
        private Form2 _form2;
        private string _strid;
        private string _sem;
        public Form3(Form2 f2, string str, string sem)
        {
            _form2 = f2;
            _strid = str;
            _sem = sem;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetGroupList();
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Семестр";
            dataGridView1.Columns[3].HeaderText = "Название";
            dataGridView1.Columns[4].HeaderText = "Численность";
        }
        private DataTable GetGroupList()
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Groups.* FROM Groups JOIN Streams ON Groups.stream = Streams.id WHERE Streams.id = " + _strid, con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form2.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value + string.Empty;
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value + string.Empty;
            button3.Enabled = true;
            button5.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out _)) {
                label1.Visible = false;
                string editId = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Groups SET name = \'" + textBox1.Text + "\', size = " + textBox2.Text + " WHERE id = " + editId, con))
                    {
                        con.Open();
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            dataGridView1.DataSource = GetGroupList();
                        }
                        catch
                        {
                            label1.Visible = true;
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
            string editId = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Groups WHERE id = " + editId, con))
                {
                    con.Open();
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        dataGridView1.DataSource = GetGroupList();
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
            label8.Visible = false;
            if (int.TryParse(textBox4.Text, out _))
            {
                if (textBox3.Text != "")
                {
                    string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Groups VALUES (" + _strid + ", " + _sem + ", \'" + textBox3.Text + "\', \'" + textBox4.Text + "\')", con))
                        {
                            con.Open();
                            try
                            {
                                int rows = cmd.ExecuteNonQuery();
                                dataGridView1.DataSource = GetGroupList();
                            }
                            catch
                            {
                                label8.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
