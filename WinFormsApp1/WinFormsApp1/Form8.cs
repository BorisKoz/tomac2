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
    public partial class Form8 : Form
    {
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
        private bool selected1 = false, selected4 = false;
        private Form1 _form1;
        public Form8(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            dataGridView1.DataSource = GetStreamList();
            tidyGrid1();
        }

        private void tidyGrid1()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Поток";
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
        private void tidyGrid2()
        {
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].HeaderText = "День";
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].HeaderText = "Неделя";
            dataGridView2.Columns[5].HeaderText = "Пара";
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[9].HeaderText = "Поток";
            dataGridView2.Columns[10].HeaderText = "Группа";
            dataGridView2.Columns[11].HeaderText = "Фамилия";
            dataGridView2.Columns[12].HeaderText = "Имя";
            dataGridView2.Columns[13].HeaderText = "Отчество";
            dataGridView2.Columns[14].HeaderText = "Предмет";
            dataGridView2.Columns[15].HeaderText = "Тип пары";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[2].Value = inttoday(row.Cells[1].Value.ToString());
                    row.Cells[4].Value = modechanger(row.Cells[3].Value.ToString());
                }
            }
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected1 = true;
            selected4 = false;
            button3.Enabled = true;
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

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected4 = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = GetTimetable(selected1, selected4);
            tidyGrid2();
        }

        private DataTable GetTimetable(bool selected1, bool selected4)
        {
            string comstring = "select l.id, l.day, l.dayname, l.daymode, l.modename, l.time, l.group_id, l.teacher_id, l.discipline_id, st.Name as streamn, g.name, t.Surname, t.Name, t.LastName, d.discipline_name, dl.load_type from Lesson as l inner join (Groups as g inner join Streams as st on g.stream = st.id) on l.group_id = g.id inner join Teachers as t on l.teacher_id = t.id inner join (DisciplineLoads as dl inner join Discipline as d on d.id = dl.discipline_id) on dl.id = l.discipline_id";
            comstring += " WHERE st.id = " + dataGridView1.SelectedRows[0].Cells[0].Value;
            if (selected4) {
                comstring += " AND g.id = " + dataGridView4.SelectedRows[0].Cells[0].Value;
            }
            comstring += " ORDER BY day ASC, daymode ASC, time ASC";
            DataTable list = new DataTable();
            string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(comstring, con))
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
    }
}
