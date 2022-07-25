using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDP : MonoBehaviour
{
    private static UdpClient client;
    private static int buffer = 1024;
    private void Start()
    {
        Debug.Log("Connecting");
        client = new UdpClient(0);
        client.Connect("127.0.0.1", 6969);
        //client.Connect("192.168.0.79", 6969);
    }
    
    public static void Send(byte[] sendBytes)
    {
        client.Send(sendBytes, 1024);
      //  client.Send(sendBytes, sendBytes.Length);
    }
    
    // public static void Send(string textToSend)
    // {
    //     byte[] sendBytes = Encoding.ASCII.GetBytes(textToSend);
    //     client.Send(sendBytes, sendBytes.Length);
    // }

    private void OnDisable()
    {
        client.Close();
    }
    /*
      Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
        ProtocolType.Udp);
        
        IPAddress serverAddr = IPAddress.Parse("192.168.2.255");
        
        IPEndPoint endPoint = new IPEndPoint(serverAddr, 11000);
        
        string text = "Hello";
        byte[] send_buffer = Encoding.ASCII.GetBytes(text );
        
        sock.SendTo(send_buffer , endPoint);
    */
}
