using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ChatBot
{
    public class ChatBot : AbsChatBot
    {
        static string name;
        string way;
        static string goblin;
        readonly List<string> history = new List<string>();

        readonly List<Regex> regices = new List<Regex> {
            new Regex(@"привет\s?\,?\s?бот"),
            new Regex(@"сколько времени\??"),
            new Regex(@"число\??"),
            new Regex(@"анекдот?"),
            new Regex(@"(\d+)(\s)?с(\s)?(\d+)"),
            new Regex(@"(\d+)(\s)?-(\s)?(\d+)"),
            new Regex(@"(\d+)(\s)?у(\s)?(\d+)"),
            new Regex(@"(\d+)(\s)?/(\s)?(\d+)")
        };

        Func<string, string> funcBuf;//Буфер

        readonly List<Func<string, string>> funcs = new List<Func<string, string>>
        {
            PrivBot,
            Time,
            Date,
            Goblin,
            Plus,
            Minus,
            Mul,
            Div
        };

        static string PrivBot(string command)//Приветствие
        {
            return "Привет " + name + ".";
        }

        static string Time(string command)//Время
        {
            return DateTime.Now.ToString("HH:mm");
        }

        static string Date(string command)//Дата
        {
            return DateTime.Now.ToString("dd.MM.yy");
        }

        static string Goblin(string command)
        {
            goblin = "Goblin.txt";
            
            if (File.Exists(goblin))
            {
                String[] str = File.ReadAllLines(goblin);

                string fline = "";
                var rand = new Random();
                int i = rand.Next(0,6);

                fline += str[i] + Environment.NewLine;
                return Environment.NewLine + fline;
            }
            else return "Без аникдотов сегодня...";
        }

        static string Plus(string command)//Сложение
        {
            command = command.Replace(" ", "");
            string[] words = command.Split(new char[] { 'с' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                int term1 = Convert.ToInt32(words[0]);
                int term2 = Convert.ToInt32(words[1]);
                return (term1 + term2).ToString();
            }
            catch
            {
                return "Что-то явно пошло не так :(";
            }
        }

        static string Minus(string command)//Вычетание
        {
            command = command.Replace(" ", "");
            string[] words = command.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                int term1 = Convert.ToInt32(words[0]);
                int term2 = Convert.ToInt32(words[1]);
                return (term1 - term2).ToString();
            }
            catch
            {
                return "Что-то явно пошло не так :(";
            }
        }

        static string Mul(string command)//Умножение
        {
            command = command.Replace(" ", "");
            string[] words = command.Split(new char[] { 'у' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                int term1 = Convert.ToInt32(words[0]);
                int term2 = Convert.ToInt32(words[1]);
                return (term1 * term2).ToString();
            }
            catch
            {
                return "Что-то явно пошло не так :(";
            }
        }

        static string Div(string command)//Деление
        {
            command = command.Replace(" ", "");
            string[] words = command.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                int term1 = Convert.ToInt32(words[0]);
                int term2 = Convert.ToInt32(words[1]);
                return (term1 / term2).ToString();
            }
            catch
            {
                return "Что-то явно пошло не так :(";
            }
        }

        public ChatBot() { }//Конструктор

        public ChatBot(string filename) { LHistory(filename); }//Конструктор с параметрами

        public List<string> history2 { get { return history; } }//Получение истории

        public string name2 { get { return name; } }//Получение имени челика

        public string way2 { get { return way; } }//Получение пути к файлу с историей

        public void AddHistory(string[] reply)// Добавляем переписку
        {
            history.AddRange(reply);
            File.WriteAllLines(way, history.ToArray(), Encoding.GetEncoding(1251));
        }

        public void LHistory(string User)//Загрузка истории
        {
            name = User;
            way = name + ".txt";
            try
            {
                history.AddRange(File.ReadAllLines(way, Encoding.GetEncoding(1251)));
                if (File.GetLastWriteTime(way).ToString("dd.MM.yy") !=
                    DateTime.Now.ToString("dd.MM.yy"))
                {
                    String[] date = new String[] {"\n\t\t\t" +
                        DateTime.Now.ToString("dd.MM.yy"+ "\n")};
                    AddHistory(date);
                }
            }
            catch
            {
               File.WriteAllLines(way, history.ToArray(), Encoding.GetEncoding(1251));
                String[] date = new String[] {"\t\t\t" +
                    DateTime.Now.ToString("dd.MM.yy"+"\n")};
                AddHistory(date);
            }
        }

        public override string Reply(string UserCommand)//Ответ челику
        {
            UserCommand = UserCommand.ToLower(); 
            for (int i = 0; i < regices.Count; i++)
            {
                if (regices[i].IsMatch(UserCommand))
                {
                    funcBuf = funcs[i];
                    return funcBuf(UserCommand);
                }
            }
            return "Извините, я вас не понимаю";
        }
    }   
}
