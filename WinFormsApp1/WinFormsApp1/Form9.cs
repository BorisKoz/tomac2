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
    public partial class Form9 : Form
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
        public Form9(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        private void Form9_Load(object sender, EventArgs e)
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
            dataGridView1.Columns[1].HeaderText = "Поток";
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Teachers WHERE surname like '%" + surname + "%'", con))
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

        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = true;
            selected4 = false;
            button3.Enabled = false;
            dataGridView4.DataSource = GetGroupList();
            tidyGrid4();
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

        private void button3_Click(object sender, EventArgs e)
        {
            string groupid = dataGridView4.SelectedRows[0].Cells[0].Value + string.Empty;
            string teachid = dataGridView2.SelectedRows[0].Cells[0].Value + string.Empty;
            string load = dataGridView3.SelectedRows[0].Cells[2].Value + string.Empty;
            string semester = dataGridView4.SelectedRows[0].Cells[2].Value + string.Empty;
            string day1 = dataGridView2.SelectedRows[0].Cells[4].Value + string.Empty;
            string day2 = dataGridView2.SelectedRows[0].Cells[5].Value + string.Empty;
            int year = (int.Parse(semester)+1)/2;
            int[,] studentTable = new int[12,8];
            int[,] teachTable = new int[12, 8];
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT l.day, l.daymode, l.time FROM Lesson as l WHERE teacher_id = " + teachid, con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            foreach (DataRow row in list.Rows)
            {
                int day = int.Parse(row[0].ToString());
                int mode = int.Parse(row[1].ToString());
                int time = int.Parse(row[2].ToString());
                switch (mode)
                {
                    case 1:
                        teachTable[day, time] = 1;
                        break;
                    case 2:
                        teachTable[day + 6, time] = 1;
                        break;
                    case 3:
                        teachTable[day, time] = 1;
                        teachTable[day + 6, time] = 1;
                        break;
                }
            }
            DataTable list1 = new DataTable();
            string connString1 = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT l.day, l.daymode, l.time FROM Lesson as l WHERE group_id = " + groupid, con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list1.Load(rd);
                }
            }
            foreach (DataRow row in list1.Rows)
            {
                int day = int.Parse(row[0].ToString());
                int mode = int.Parse(row[1].ToString());
                int time = int.Parse(row[2].ToString());
                switch (mode)
                {
                    case 1:
                        studentTable[day, time-1] = 1;
                        break;
                    case 2:
                        studentTable[day + 6, time-1] = 1;
                        break;
                    case 3:
                        studentTable[day, time-1] = 1;
                        studentTable[day + 6, time-1] = 1;
                        break;
                }

            }


            string xmlString = System.IO.File.ReadAllText("C:\\0.3inserts.xml");
            StringBuilder sb = new StringBuilder(xmlString);
            sb.Replace("@year", year.ToString());
            sb.Replace("@hours", load.ToString());
            sb.Replace("@day1", day1);
            sb.Replace("@day2", day2);
            if (checkBox1.Checked)
            {
                sb.Replace("@og0", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og0", "");
            }
            if (checkBox2.Checked)
            {
                sb.Replace("@og1", "defaultValue = \"0.00\"");
            } 
            else
            {
                sb.Replace("@og1", "");
            }
            if (checkBox3.Checked)
            {
                sb.Replace("@og2", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og2", "");
            }
            if (checkBox4.Checked)
            {
                sb.Replace("@og3", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og3", "");
            }
            if (checkBox5.Checked)
            {
                sb.Replace("@og4", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og4", "");
            }
            if (checkBox6.Checked)
            {
                sb.Replace("@og5", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og5", "");
            }
            if (checkBox7.Checked)
            {
                sb.Replace("@og6", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og6", "");
            }
            if (checkBox8.Checked)
            {
                sb.Replace("@og7", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og7", "");
            }
            if (checkBox9.Checked)
            {
                sb.Replace("@og8", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og8", "");
            }
            if (checkBox10.Checked)
            {
                sb.Replace("@og9", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og9", "");
            }
            if (checkBox11.Checked)
            {
                sb.Replace("@og.10", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og.10", "");
            }
            if (checkBox12.Checked)
            {
                sb.Replace("@og.11", "defaultValue = \"0.00\"");
            }
            else
            {
                sb.Replace("@og.11", "");
            }
            
            for (int i =0; i < 12; i++)
            {
                for (int j =1; j < 9; j ++)
                {
                    string name1 = "@u" + i.ToString() + "." + j.ToString();
                    string name2 = "@p" + i.ToString() + "." + j.ToString();
                    sb.Replace(name1, (studentTable[i, j - 1]).ToString());
                    sb.Replace(name2, (teachTable[i, j - 1]).ToString());
                }
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "файлы xml (*.xml)|*.xml";
            sfd.DefaultExt = "xml";
            string path = "C:\\test.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                path = System.IO.Path.GetFullPath(sfd.FileName);
            }
            System.IO.File.WriteAllText(path, sb.ToString());
        }

        private DataTable GetLoadSearch(string name)
        {
            DataTable list = new DataTable();

                button2.Enabled = true;
                string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT dl.id, d.discipline_name, dl.load_hours, dl.load_type FROM DisciplineLoads as dl JOIN Discipline as d on dl.discipline_id = d.id WHERE d.discipline_name like '%" + name + "%'", con))
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Streams WHERE Name like '%" + name + "%'", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }
    }
}
