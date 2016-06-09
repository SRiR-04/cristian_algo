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
    /*! \class Client
 *  \brief Klasa zawiera obsługę algorytmu Cristiana ze strony klienta. 
 *  
 *  Zadania klienta: 
 *   - nawiązanie połączenia, (stworzenie i rejestracja kanału)
 *   - połączenie się poprzez kanał z serwerem
 *   - żądanego wywołania odpowiedniej funkcji
 *   - pobranie wyników serwera
 */
    class Client
    {
        static void Main(string[] args)
        {
            TcpChannel tcpChannel = new TcpChannel(); ///stworzenia kanału komunikajcyjnego w oparciu o protokuł TCP
            ChannelServices.RegisterChannel(tcpChannel, true); ///zarejstrowanie kanału

            Type requiredType = typeof(CristianInterface);  ///pobranie typu wymanaganego przez wspólny interfejs
                     

            CristianInterface remoteObject = (CristianInterface)Activator.GetObject(requiredType, "tcp://localhost:5511/GetTime");
            ///zlecenie wykonanie zadania do serwera poprzez połączenie utworzonym kanałem z podaniem adresem i portem na którym pracuje serwer

            DateTime clientTime1 = DateTime.Now; ///czas klienta przed wysłaniem wysłaniem żadania
            DateTime serverTime = remoteObject.GetServerTime(); ///czas odebrany od serwera 
            DateTime clientTime2 = DateTime.Now;           ///czas klienta poo wykonaniu połączenia z serwerem

            TimeSpan diff = serverTime - clientTime1; ///zmienna przechowująca pierwszą różnice czasu
            TimeSpan diff2 = clientTime2 - serverTime; ///zmienna przechowująca drugą różnice czasu

            int ping = (diff.Milliseconds + diff2.Milliseconds) / 2;  ///opóźnienie zegarów wyliczone zgodnie z algorytmem Crystiana

            DateTime newTime = serverTime.AddMilliseconds(ping);  ///wyznaczony nowy czas po zsynchronizowaniu zegarów
            Console.WriteLine("Server time: " + serverTime + ":" + serverTime.Millisecond); ///wypisanie komuniaktów na okienko
            Console.WriteLine("Time of connection (in miliseconds) " + ping);
            Console.WriteLine("New client time base on Cristian's Alghoritm: " + newTime + ":" + newTime.Millisecond);
            Console.ReadKey();  ///wymuszenie nie zamykania konsoli
        }
    } 
}

/*!
 * \interface CristianInterface
 *  \brief Wspólny interfejs
 *  */
public interface CristianInterface
{
    DateTime GetServerTime(); ///funkcja zlecana
}
