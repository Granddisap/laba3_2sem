using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatBot
{
    public partial class MainForm : Form
    {
        static public ChatBot bot = new ChatBot();
        
        public MainForm()
        {
            InitializeComponent();
        }

        public void RecoveryChat()//Восстановление истории переписки
        {
            for (int i = 0; i < bot.history2.Count; i++)
                textBox1.Text += bot.history2[i] + Environment.NewLine;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)//Ввод
        {
            if (textBox2.Text.Trim() != "")
            {
                String[] UserCommand = textBox2.Text.Split(new String[] { "\r\n" },
                    StringSplitOptions.RemoveEmptyEntries);

                string Mes = UserCommand[0];

                UserCommand[0] = UserCommand[0].Insert(0, "[" + DateTime.Now.ToString("HH:mm") + "] " + bot.name2 + ": ");

                bot.AddHistory(UserCommand);

                textBox1.AppendText(UserCommand[0] + "\r\n");
                textBox2.Text = "";

                string[] botReply = new string[] { bot.Reply(Mes) };
                botReply[0] = botReply[0].Insert(0, "[" + DateTime.Now.ToString("HH:mm") + "] Бот: ");

                textBox1.AppendText(botReply[0] + Environment.NewLine);
                bot.AddHistory(botReply);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Обновление текста
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }

        private void label1_Click(object sender, EventArgs e)//Окно помощи
        {
            Help frm = new Help();
            frm.Show();
        }
    }
}
