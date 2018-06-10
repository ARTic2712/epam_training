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
        public ICollection<Port> Ports { get; set; }
        public event EventHandler<CallEventArgs> TerminalAnswered;
        public event EventHandler<CallEventArgs> TerminalNoAnswered;
        public event EventHandler<CallEventArgs> TerminalRejected;
        public event EventHandler<CallInfo> EndCallEvent;

        private IDictionary<int, int> ActiveConnections { get; set; }
        private IDictionary<KeyValuePair<int,int>, CallInfo> Calls { get; set; }
        private ATS()
        {
            Ports = new List<Port>();
            ActiveConnections = new Dictionary<int, int>();
            Calls = new Dictionary<KeyValuePair<int, int>, CallInfo>();
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
        private Enums.Mode SetConnection(CallEventArgs call)
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
                    GetStation().TerminalAnswered += portIn. OutCallAnswered;
                   GetStation().TerminalNoAnswered += portIn.OutCallNoAnswered;
                    GetStation().TerminalRejected += portIn.OutCallRejected;
                    ActiveConnections.Add(call.InPhoneNumber, call.OutPhoneNumber);
                    portOut.Mode = Enums.Mode.Ringing;
                    portIn.Mode = Enums.Mode.Ringing;
                    portOut.AnswerEvent  += AnsweredCall;
                    portOut.RejectEvent += RejectedCall;
                    portOut.NoAnswerEvent  += NoAnsweredCall;
                    portOut.IncomingCall(call.InPhoneNumber);
                }
            }
            if (modePort == Enums.Mode.Ringing) return Enums.Mode.Busy;
            return modePort;
        }
        private void AnsweredCall(object o, EventArgs e)
        {
            Port port = (o as Port);
            port.AnswerEvent -= AnsweredCall;
            port.RejectEvent -= RejectedCall;
            port.NoAnswerEvent -= NoAnsweredCall;
            port.EndCallEventOnPort += EndCall;
            var connection = ActiveConnections.FirstOrDefault(x => x.Value == port.PhoneNumber);
            Ports.FirstOrDefault(x => x.PhoneNumber == connection.Key).EndCallEventOnPort += EndCall;
            TerminalAnswered (this, new CallEventArgs(connection.Value, connection.Key));
            Calls.Add(connection, new CallInfo(connection.Key, connection.Value, DateTime.Now, Enums.AnswerType.Answered));
        }
        private void RejectedCall(object o, EventArgs e)
        {
            Port port = (o as Port);
            port.AnswerEvent -= AnsweredCall;
            port.RejectEvent -= RejectedCall;
            port.NoAnswerEvent -= NoAnsweredCall;
            var connection = ActiveConnections.FirstOrDefault(x => x.Value == port.PhoneNumber);
            ActiveConnections.Remove(connection.Key );
            TerminalRejected (this, new CallEventArgs(connection.Value, connection.Key));
            Calls.Add(connection, new CallInfo(connection.Key, connection.Value, DateTime.Now, Enums.AnswerType.Canceled ));


        }
        private void NoAnsweredCall(object o, EventArgs e)
        {
            Port port = (o as Port);
            port.AnswerEvent -= AnsweredCall;
            port.RejectEvent -= RejectedCall;
            port.NoAnswerEvent -= NoAnsweredCall;
            var connection = ActiveConnections.FirstOrDefault(x => x.Value == port.PhoneNumber);
            ActiveConnections.Remove(connection.Key);
            TerminalNoAnswered(this, new CallEventArgs(connection.Value, connection.Key));
            Calls.Add(connection, new CallInfo(connection.Key, connection.Value, DateTime.Now, Enums.AnswerType.NotResponding ));
            
        }
        private void EndCall(object o,EventArgs e)
        {
            Port port = (o as Port);
            Port secondPort;
            port.EndCallEventOnPort -= EndCall;
            var connection = ActiveConnections.FirstOrDefault(x => (x.Value == (o as Port).PhoneNumber) || (x.Key == (o as Port).PhoneNumber));
            if (port.PhoneNumber == connection.Key)
            {
                secondPort = Ports.First(x => x.PhoneNumber == connection.Value);

            }
            else
            {
                secondPort = Ports.First(x => x.PhoneNumber == connection.Key);
            }
            CallInfo call = Calls.FirstOrDefault(x => (x.Key.Key == port.PhoneNumber && x.Key.Value == secondPort.PhoneNumber) || (x.Key.Value == port.PhoneNumber && x.Key.Key == secondPort.PhoneNumber)).Value;
            call.FinishCall(DateTime.Now);
            EndCallEvent(this, call);
            secondPort.EndCallEventOnPort -= EndCall;

        }
    }
}
