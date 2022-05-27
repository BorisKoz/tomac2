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
    public partial class Form7 : Form
    {
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

        private int modetoint(string mode)
        {
            switch (mode)
            {
                case "НЕЧ":
                    {
                        return 1;
                    }
                case "ЧЕТ":
                    {
                        return 2;
                    }
                case "ОБЩ":
                    {
                        return 3;
                    }
                default:
                    return -1;
            }
        }

        private bool selected1 = false, selected2 = false, selected3 = false, selected4 = false;
        private Form1 _form1;
        public Form7(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = GetStreamList();
            dataGridView2.DataSource = GetTeachList();
            dataGridView3.DataSource = GetLoadList();
            tidyGrid1();
            tidyGrid2();
            tidyGrid3();
        }
        private void tidyGrid1()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[0].HeaderText = "Поток";
        }
        private void tidyGrid2()
        {
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].HeaderText = "Фамилия";
            dataGridView2.Columns[2].HeaderText = "Имя";
            dataGridView2.Columns[3].HeaderText = "Отчество";
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].HeaderText = "ID";
        }
        private void tidyGrid3()
        {
            dataGridView3.ReadOnly = true;
            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[1].HeaderText = "Предмет";
            dataGridView3.Columns[2].HeaderText = "Количество часов";
            dataGridView3.Columns[3].HeaderText = "Тип нагрузки";

        }

        private void tidyGrid4()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView4.ReadOnly = true;
            dataGridView4.Columns[0].Visible = false;
            dataGridView4.Columns[1].Visible = false;
            dataGridView4.Columns[2].HeaderText = "Семестр";
            dataGridView4.Columns[3].HeaderText = "Название";
            dataGridView4.Columns[4].HeaderText = "Численность";
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
        private DataTable GetTeachSearch(string surname)
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Teachers WHERE surname like '%"+surname+"%'", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }
        private DataTable GetLoadList()
        {
            DataTable list = new DataTable();

                button2.Enabled = true;
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT dl.id, d.discipline_name, dl.load_hours, dl.load_type FROM DisciplineLoads as dl JOIN Discipline as d on dl.discipline_id = d.id ", con))
                    {
                        con.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        list.Load(rd);
                    }
                }
            
            return list;
        }
        private DataTable GetLoadSearch(string name)
        {
            DataTable list = new DataTable();
                button2.Enabled = true;
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT dl.id, d.discipline_name, dl.load_hours, dl.load_type FROM DisciplineLoads as dl JOIN Discipline as d on dl.discipline_id = d.id WHERE d.discipline_name like '%"+name+"%'", con))
                    {
                        con.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        list.Load(rd);
                    }
                }
            return list;
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

        private DataTable GetStreamSearch(string name)
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Streams WHERE Name like '%"+name+"%'", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form1.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = true;
            selected4 = false;
            button3.Enabled = false;
            dataGridView4.DataSource = GetGroupList();
            tidyGrid4();
        }
        private DataTable GetGroupList()
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Groups.* FROM Groups JOIN Streams ON Groups.stream = Streams.id WHERE Streams.id = " + dataGridView1.SelectedRows[0].Cells[0].Value, con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                button3.Enabled = false;
                selected1 = false;
                selected4 = false;
                dataGridView1.DataSource = GetStreamSearch(textBox1.Text);
                tidyGrid1();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                button3.Enabled = false;
                selected2 = false;
                dataGridView2.DataSource = GetTeachSearch(textBox2.Text);
                tidyGrid2();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                button3.Enabled = false;
                selected3 = false;
                dataGridView3.DataSource = GetLoadSearch(textBox3.Text);
                tidyGrid3();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                string day = daychanger(comboBox2.SelectedItem.ToString());
                if (day != "-1")
                {
                    int week = modetoint(comboBox1.SelectedItem.ToString());
                    if (week != -1)
                    {
                        bool is_parseble = int.TryParse(textBox4.Text, out int pair);
                        if (is_parseble && pair >= 1 && pair <= 8)
                        {
                            label1.Visible = false;
                            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand("INSERT INTO Lesson VALUES ("+pair+","+day+", NULL,"+week+", NULL, "+
                                    dataGridView4.SelectedRows[0].Cells[0].Value+","+
                                    dataGridView2.SelectedRows[0].Cells[0].Value + ","+
                                    dataGridView3.SelectedRows[0].Cells[0].Value + ")", con))
                                {
                                    con.Open();
                                    try
                                    {
                                        int rows = cmd.ExecuteNonQuery();
                                        label4.Visible = true;
                                    }
                                    catch
                                    {
                                        label3.Visible = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected4 = true;
            if (selected1 && selected2 && selected3 && selected4)
            {
                button3.Enabled = true;
            } 
            else
            {
                button3.Enabled = false;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected2 = true;
            if (selected1 && selected2 && selected3 && selected4)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected3 = true;
            if (selected1 && selected2 && selected3 && selected4)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }
    }
}
