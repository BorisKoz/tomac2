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
    public partial class Form5 : Form
    {
        private Form1 _form1;
        public Form5(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDisList();
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Дисциплина";
        }
        private DataTable GetDisList()
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Discipline", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value + string.Empty;
            button5.Enabled = true;
            button4.Enabled = true;
            dataGridView2.DataSource = GetLoadList();
            dataGridView2.ReadOnly = true;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].HeaderText = "Количество часов";
            dataGridView2.Columns[2].HeaderText = "Тип нагрузки";
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[3].Visible = false;
        }
        private DataTable GetLoadList()
        {
            DataTable list = new DataTable();
            string id = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
            if (id != "")
            {
                button2.Enabled = true;
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM DisciplineLoads WHERE discipline_id = " + id, con))
                    {
                        con.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        list.Load(rd);
                    }
                }
            }
            return list;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView2.SelectedRows[0].Cells[1].Value + string.Empty;
            comboBox2.SelectedItem = dataGridView2.SelectedRows[0].Cells[2].Value + string.Empty;
            button3.Enabled = true;
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
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Discipline WHERE id = " + editId, con))
                {
                    con.Open();
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        dataGridView1.DataSource = GetDisList();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out _))
            {
                if (comboBox1.SelectedItem != null)
                {
                    string editId = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                    string loadId = dataGridView2.SelectedRows[0].Cells[0].Value + string.Empty;
                    string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE DisciplineLoads SET load_hours =" + textBox2.Text + ", load_type = \'" + comboBox1.SelectedItem.ToString() + "\', discipline_id = " + editId + " WHERE id = " +loadId, con))
                        {
                            con.Open();
                            try
                            {
                                int rows = cmd.ExecuteNonQuery();
                                dataGridView2.DataSource = GetLoadList();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out _) && comboBox2.SelectedItem != null)
            {
                string editId = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO DisciplineLoads VALUES (" + textBox4.Text + ", \'" + comboBox2.SelectedItem.ToString() + "\', " + editId + ")", con))
                    {
                        con.Open();
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            dataGridView2.DataSource = GetLoadList();
                        }
                        catch
                        {
                            label8.Visible = true;
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            if (textBox1.Text != "")
            {
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Discipline VALUES (\'" + textBox1.Text + "\')", con))
                    {
                        con.Open();
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            dataGridView1.DataSource = GetDisList();
                        }
                        catch
                        {
                            label1.Visible = true;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form1.Show();
        }
    }
}
