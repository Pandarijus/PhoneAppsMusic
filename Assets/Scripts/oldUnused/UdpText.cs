using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UdpText : MonoBehaviour
{
    private static UdpClient client;
    
    IEnumerator Start()
    {
        Debug.Log("Connecting");
        client = new UdpClient(0);
        client.Connect("127.0.0.1", 6969);
        var textToSend = "Hello can you here me?";
       
        var wait = new WaitForSeconds(1);
        int c = -1;
        while (true)
        {
            c++;
            var bytesToSend = Encoding.ASCII.GetBytes(textToSend+c);
            client.Send(bytesToSend, bytesToSend.Length);
            yield return wait;
        }
    }

    private void OnDisable()
    {
        client.Close();
    }

}