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
    /*! \class Server
     *  \brief Klasa zawiera obsługę zadań wykonywanych przez serwer w algorytmie Cristiana. 
     *  
     *  Zadania serwera: 
     *   - tworzenie połączenia, (ustawienie protokołu i rejestracja kanału)
     *   - oczekiwanie na połączenie klienta
     *   - wykonanie żądanego zadania
     *   - odesłanie otrzymanych wyników do klienta
     */ 
    class Server
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server started..."); /*! komuniakt rozpoczęcia pracy przez serwer */

            TcpChannel tcpChannel = new TcpChannel(5511); /*!< stworzenie kanału komunikacji z uwzględnieniem protokołu TCP oraz numerem portu */
            ChannelServices.RegisterChannel(tcpChannel, true); //! zarejstrowanie kanału

            Type commonInterfaceType = Type.GetType("Cristian");   //! zmienna zawierająca typu interfejsu łączącego klienta z serwerem

            RemotingConfiguration.RegisterWellKnownServiceType(commonInterfaceType, "GetTime", WellKnownObjectMode.SingleCall);
            //! rejestracja do wykonania zadania zadeklarowanego w inferfejsie

            System.Console.WriteLine("Press ENTER to quit");  //! aktywne oczekiwanie        
            System.Console.ReadKey();  //! wymuszenie nie zamykania okienka                                                     
        }
    }
}

/*!
 * \interface CristianInterface
 *  \brief Interfejs zawierający deklarakcje zlecanej metody
 *  */
public interface CristianInterface
{
    DateTime GetServerTime();  /*! deklaracja funkcji zwracające czas serwera */
}

/*!
 * \class Cristian
 *  \brief Klasa dziedzicząca po interfejsie i klasie MarshalByRefObject, która pozwala na dostęp do klasy z poza domeny danej aplikacji
 *  Klasa zawiera implementacje funkcji zlecanej do wykonania przez serwer.
 *  */
public class Cristian : MarshalByRefObject, CristianInterface
{
    public DateTime GetServerTime()  //! metoda wysyłająca do klienta akualny czas serwera
    {
        Console.WriteLine("Enquiry for {0} to get server time.");
        return DateTime.Now; //! aktualny czas serwera
    }
}
