using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM.Temp
{
    class Phone
    {

        Battery b;

        public void call()
        {
            Console.WriteLine("Call someone");
        }

        public void text()
        {
            Console.WriteLine("Text someone");
        }

        public Battery getBattery()
        {
            return new Battery();
        }
    }
}
