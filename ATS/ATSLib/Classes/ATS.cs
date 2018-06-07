using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSLib.Classes
{
    public class ATS
    {
        private static ATS _station;
        public ICollection<Port> Ports {get; set; }
        private IDictionary<int,int> ActiveConnections { get; set; }
        private ATS()
        {
            Ports = new List<Port>();
            ActiveConnections = new Dictionary<int, int>();
        }
        public static ATS GetStation()
        {
            if(_station ==null) _station = new ATS();
            return _station;
        }
        public void AddPort(Port port)
        {
            port.SetConnection += SetConnection;
            Ports.Add(port);
        }
        public Enums.Mode SetConnection(CallEventArgs call)
        {
            Port portOut=  Ports.FirstOrDefault(x => x.PhoneNumber == call.OutPhoneNumber);
            if (portOut == null)
            {
                return Enums.Mode.NotExist;
            }
            Port portIn = Ports.First(x => x.PhoneNumber == call.InPhoneNumber );
            Enums.Mode modePort = portOut.Mode;
            if (modePort ==Enums.Mode.Free )
            {
                if (!ActiveConnections.ContainsKey(call.InPhoneNumber))
                {
                    ActiveConnections.Add(call.InPhoneNumber, call.OutPhoneNumber);
                    portOut.Mode = Enums.Mode.Ringing;
                    portIn.Mode = Enums.Mode.Ringing;
                    if( portOut.IncomingCall(call.InPhoneNumber)==Enums.Answer.Answered )
                    {
                        //////Создать звонок
                        Console.WriteLine("Звонок!!!");
                    }
                }
            }
            return modePort;
        }
    }
}
