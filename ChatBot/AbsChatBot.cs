using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public abstract class AbsChatBot//Абстрактный класс для метода command
    {
        public abstract string Reply(string UserCommand);
    }
}
