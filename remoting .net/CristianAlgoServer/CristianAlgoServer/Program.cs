using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;


namespace CristianAlgoServer
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Console.WriteLine("Server started...");

            TcpChannel tcpChannel = new TcpChannel(9998);
            ChannelServices.RegisterChannel(tcpChannel, true);

            Type commonInterfaceType = Type.GetType("Cristian");
            

            RemotingConfiguration.RegisterWellKnownServiceType(commonInterfaceType,
            "GetTime", WellKnownObjectMode.SingleCall);

            System.Console.WriteLine("Press ENTER to quit");
            System.Console.ReadLine();


        }
    }
}

public interface CristianInterface
{
    DateTime GetServerTime();
}

public class Cristian : MarshalByRefObject, CristianInterface
{
    public DateTime GetServerTime()
    {
       // Console.WriteLine("Enquiry for {0} to get server time.");       
        return DateTime.Now;
    }
}
