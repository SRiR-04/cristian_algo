package com.company;

import java.rmi.*;
import java.rmi.registry.*;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

public class RmiClient
{
    static public void main(String args[])
    {
        DateFormat dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss.SSS");
        ReceiveMessageInterface rmiServer;
        Registry registry;
        String serverAddress = "0.0.0.0";
        int serverPort = 3232;
        System.out.println("sending to "+serverAddress+":"+serverPort);
        try {
            // get the registry
            registry = LocateRegistry.getRegistry(serverAddress, serverPort);
            // look up the remote object
            rmiServer = (ReceiveMessageInterface)(registry.lookup("rmiServer"));

            // christian algorytm:
            Date t1 = new Date();
            Date serverTime = rmiServer.receiveMessage("Time");
            Date t2 = new Date();

            Date calculatedTime = new Date(serverTime.getTime() + t2.getTime() - t1.getTime());

            System.out.println("server time " + dateFormat.format(serverTime));
            System.out.println("computed time " + dateFormat.format(calculatedTime));
        }
        catch(RemoteException e){
            e.printStackTrace();
        }
        catch(NotBoundException e){
            e.printStackTrace();
        }
    }
}
