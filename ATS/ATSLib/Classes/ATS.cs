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
        private IDictionary<int,int> Connections { get; set; }
        private ATS()
        {
            Ports = new List<Port>();
            Connections = new Dictionary<int, int>();
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
        // Дописать SetConnection
        public Enums.Mode SetConnection(CallEventArgs call)
        {
            return Enums.Mode.Busy;
        }
    }
}
