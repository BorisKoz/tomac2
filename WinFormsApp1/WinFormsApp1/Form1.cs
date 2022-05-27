using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Form2 GroupsForm;
        private Form4 TeachForm;
        private Form5 DisForm;
        private Form6 EditLessonForm;
        private Form7 AddLessonForm;
        private Form8 ShowTimetableForm;
        private Form9 MESForm;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            GroupsForm = new Form2(this) { Visible = false };
            GroupsForm.Show();
        }

        private void button_add_teacher_Click(object sender, EventArgs e)
        {
            this.Hide();
            TeachForm = new Form4(this) { Visible = false };
            TeachForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            DisForm = new Form5(this) { Visible = false };
            DisForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditLessonForm = new Form6(this) { Visible = false };
            EditLessonForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddLessonForm = new Form7(this) { Visible = false };
            AddLessonForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowTimetableForm = new Form8(this) { Visible = false };
            ShowTimetableForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MESForm = new Form9(this) { Visible = false };
            MESForm.Show();
        }
    }
}
