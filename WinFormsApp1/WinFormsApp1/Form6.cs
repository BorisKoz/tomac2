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
    public partial class Form6 : Form
    {
        private Form1 _form1;
        private int chosen = 0;
        public Form6(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
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
        private string inttoday(string day)
        {
            switch (day)
            {
                case "0":
                    {
                        return "Понедельник";
                    }
                case "1":
                    {
                        return "Вторник";
                    }
                case "2":
                    {
                        return "Среда";
                    }
                case "3":
                    {
                        return "Четверг";
                    }
                case "4":
                    {
                        return "Пятница";
                    }
                case "5":
                    {
                        return "Суббота";
                    }
                default:
                    {
                        return "-1";
                    }
            }
        }
        private string modechanger(string mode)
        {
            switch (mode)
            {
                case "1":
                    {
                        return "НЕЧ";
                    }
                case "2":
                    {
                        return "ЧЕТ";
                    }
                case "3":
                    {
                        return "ОБЩ";
                    }
                default:
                    return "-1";
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
        private void gridPrep()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Пара";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "День";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "Неделя";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].HeaderText = "Поток";
            dataGridView1.Columns[10].HeaderText = "Группа";
            dataGridView1.Columns[11].HeaderText = "Фамилия";
            dataGridView1.Columns[12].HeaderText = "Имя";
            dataGridView1.Columns[13].HeaderText = "Отчество";
            dataGridView1.Columns[14].HeaderText = "Предмет";
            dataGridView1.Columns[15].HeaderText = "Тип пары";
            dataGridView1.Columns[16].Visible = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[3].Value = inttoday(row.Cells[2].Value.ToString());
                    row.Cells[5].Value = modechanger(row.Cells[4].Value.ToString());
                }
            }
            chosen = 0;
            button3.Enabled = false;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetLessonList();
            dataGridView1.ReadOnly = true;
            gridPrep();
        }

        private DataTable GetLessonList()
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.*, st.Name as streamn, g.name, t.Surname, t.Name, t.LastName, d.discipline_name, dl.load_type, d.id from Lesson as l inner join (Groups as g inner join Streams as st on g.stream = st.id) on l.group_id = g.id inner join Teachers as t on l.teacher_id = t.id inner join (DisciplineLoads as dl inner join Discipline as d on d.id = dl.discipline_id) on dl.id = l.discipline_id", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }
        private DataTable GetLessonStreamList(string streamString)
        {
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.*, st.Name as streamn, g.name, t.Surname, t.Name, t.LastName, d.discipline_name, dl.load_type from Lesson as l inner join (Groups as g inner join Streams as st on g.stream = st.id) on l.group_id = g.id inner join Teachers as t on l.teacher_id = t.id inner join (DisciplineLoads as dl inner join Discipline as d on d.id = dl.discipline_id) on dl.id = l.discipline_id WHERE st.Name LIKE '%" + streamString+"%'", con))
                {
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    list.Load(rd);
                }
            }
            return list;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button5.Enabled = true;
            dataGridView2.DataSource = GetTeachList();
            dataGridView3.DataSource = GetLoadList();
            tidyGrid2();
            tidyGrid3();
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
        private DataTable GetLoadList()
        {
            DataTable list = new DataTable();
            string id = dataGridView1.SelectedRows[0].Cells[16].Value + string.Empty;
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

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetLessonList();
            gridPrep();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetLessonStreamList(textBox1.Text);
            gridPrep();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form1.Show();
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
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Lesson WHERE id = " + editId, con))
                {
                    con.Open();
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        dataGridView1.DataSource = GetLessonList();
                        gridPrep();
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
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                string day = daychanger(comboBox2.SelectedItem.ToString());
                if (day != "-1")
                {
                    int week = modetoint(comboBox1.SelectedItem.ToString());
                    if (week != -1)
                    {
                        bool is_parseble = int.TryParse(textBox2.Text, out int pair);
                        if (is_parseble && pair >= 1 && pair <= 8)
                        {
                            label1.Visible = false;
                            string editId = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand("Update Lesson SET time = " + pair.ToString() + ", day = " + day + ", daymode = " + week + ", teacher_id = " +
                                    dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + ", discipline_id = " +
                                    dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + " WHERE id = " + editId, con))
                                {
                                    con.Open();
                                    try
                                    {
                                        int rows = cmd.ExecuteNonQuery();
                                        dataGridView1.DataSource = GetLessonList();
                                        gridPrep();
                                    }
                                    catch
                                    {
                                        label1.Visible = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            chosen += 1;
            if (chosen >=2)
            {
                button3.Enabled = true;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            chosen += 1;
            if (chosen >= 2)
            {
                button3.Enabled = true;
            }
        }
    }
}
