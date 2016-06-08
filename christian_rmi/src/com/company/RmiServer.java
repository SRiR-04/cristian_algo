package com.company;

import java.rmi.*;
import java.rmi.registry.*;
import java.util.Date;

public class RmiServer extends java.rmi.server.UnicastRemoteObject
        implements ReceiveMessageInterface
{
    int thisPort;
    String thisAddress;
    Registry registry;    // rmi registry for lookup the remote objects.

    // This method is called from the remote client by the RMI.
    // This is the implementation of the �gReceiveMessageInterface�h.
    public Date receiveMessage(String x) throws RemoteException
    {
        return new Date();
    }

    public RmiServer() throws RemoteException
    {
        thisAddress= "0.0.0.0";
        thisPort=3232;  // this port(registry�fs port)
        System.out.println("this address=" + thisAddress + ",port=" + thisPort);
        try {
            // create the registry and bind the name and object.
            registry = LocateRegistry.createRegistry(thisPort);
            registry.rebind("rmiServer", this);
        }
        catch(RemoteException e){
            throw e;
        }
    }

    static public void main(String args[])
    {
        try {
            new RmiServer();
        }
        catch (Exception e) {
            e.printStackTrace();
            System.exit(1);
        }
    }
}

