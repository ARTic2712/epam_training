using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSLib.Classes;
namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal t = new Terminal("Xiaomi redmi note 4", 1111111);
            Terminal t2 = new Terminal("Xiaomi redmi 5", 2442425);

            ATS.GetStation().AddPort(new Port(t));

            t.TurnOn();
            t.Call(2442425);
            t.TurnOff();
            t.Call(242);
            ATS.GetStation().AddPort(new Port(t2));
            t2.TurnOn();
            t.TurnOn();
            t.Call(2442425);

        }
    }
}
