using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace CristianAlgoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            TcpChannel tcpChannel = new TcpChannel();
            ChannelServices.RegisterChannel(tcpChannel, true);

            Type requiredType = typeof(CristianInterface);

            CristianInterface remoteObject = (CristianInterface)Activator.GetObject(requiredType,
            "tcp://localhost:9998/GetTime");

            DateTime clientTime1 = DateTime.Now;
            DateTime serverTime = remoteObject.GetServerTime();
            DateTime clientTime2 = DateTime.Now;           
            
            TimeSpan diff = serverTime - clientTime1;
            TimeSpan diff2 = clientTime2 - serverTime;

            int ping = (diff.Milliseconds + diff2.Milliseconds) / 2;

            DateTime newTime = serverTime.AddMilliseconds(ping);
            Console.WriteLine("Server time: " + serverTime + ":" + serverTime.Millisecond);
            Console.WriteLine("Time of connection (in miliseconds) " + ping);
            Console.WriteLine("New client time base on Cristian's Alghoritm: " + newTime +":"+newTime.Millisecond);
            Console.ReadKey();
        }
    } 
}

public interface CristianInterface
{
    DateTime GetServerTime();
}
