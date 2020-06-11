using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ChatBot
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                MainForm frm = new MainForm();
                MainForm.bot.LHistory(textBox1.Text); //Загрузка истории
                frm.Show();
                frm.RecoveryChat(); //Восстановление
                this.Hide();
            }
            else MessageBox.Show("У вас нет логина?");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
