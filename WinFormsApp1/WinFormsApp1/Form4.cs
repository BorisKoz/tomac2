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
    public partial class Form4 : Form
    {
        private Form1 _form1;
        public Form4(Form1 MainMenu)
        {
            InitializeComponent();
            _form1 = MainMenu;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetTeachList();
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Фамилия";
            dataGridView1.Columns[2].HeaderText = "Имя";
            dataGridView1.Columns[3].HeaderText = "Отчество";
            dataGridView1.Columns[4].HeaderText = "День1";
            dataGridView1.Columns[5].HeaderText = "День2";
            dataGridView1.Columns[6].HeaderText = "ID";
        }

        private DataTable GetTeachList()
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Teachers", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string daychanger(string day)
        {
            switch (day)
            {
                case "Понедельник":
                    {
                        return "0";
                    }
                case "Вторник":
                    {
                        return "1";
                    }
                case "Среда":
                    {
                        return "2";
                    }
                case "Четверг":
                    {
                        return "3";
                    }
                case "Пятница":
                    {
                        return "4";
                    }
                case "Суббота":
                    {
                        return "5";
                    }
                default:
                    {
                        return "-1";
                    }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox5.Text != "" && textBox7.Text != "" && comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                label1.Visible = false;
                string editId = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Teachers SET Surname = \'" + textBox1.Text + "\', Name = \'"
                        + textBox2.Text + "\', LastName = \'" + textBox5.Text + "\', FavDay1 = " + daychanger(comboBox1.SelectedItem.ToString()) +
                        " , FavDay2 = " + daychanger(comboBox2.SelectedItem.ToString()) + ", Campid = \'" + textBox7.Text + "\' WHERE id = " + editId, con))
                    {
                        con.Open();
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            dataGridView1.DataSource = GetTeachList();
                        }
                        catch
                        {
                            label1.Visible = true;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "" && comboBox3.SelectedItem != null && comboBox4.SelectedItem != null)
            {
                label1.Visible = false;
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Teachers VALUES( \'" + textBox8.Text + "\',\'"
                        + textBox3.Text + "\',\'" + textBox4.Text + "\', " + daychanger(comboBox3.SelectedItem.ToString()) +
                        " , " + daychanger(comboBox4.SelectedItem.ToString()) + ", \'" + textBox6.Text + "\' )", con))
                    {
                        con.Open();
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            dataGridView1.DataSource = GetTeachList();
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
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Teachers WHERE id = " + editId, con))
                {
                    con.Open();
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        dataGridView1.DataSource = GetTeachList();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value + string.Empty;
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value + string.Empty;
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[3].Value + string.Empty;
            int.TryParse(dataGridView1.SelectedRows[0].Cells[4].Value + string.Empty, out int n);
            int.TryParse(dataGridView1.SelectedRows[0].Cells[5].Value + string.Empty, out int m);
            comboBox1.SelectedIndex = n+1;
            comboBox2.SelectedIndex = m + 1;
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value + string.Empty;
            button3.Enabled = true;
            button5.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
