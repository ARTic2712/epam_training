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
            ATS.GetStation().AddPort(new Port(t));
            t.TurnOn();
            t.Call(2442425);
            t.TurnOff();
            t.Call(242);
            t.TurnOn();
            t.Call(2442425);

        }
    }
}
