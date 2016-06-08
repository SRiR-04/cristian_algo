package com.company;
import java.rmi.*;
import java.util.Date;

public interface ReceiveMessageInterface extends Remote
{
    Date receiveMessage(String x) throws RemoteException;
}
