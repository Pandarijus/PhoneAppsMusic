using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UdpFile : MonoBehaviour
{
    private TcpClient client;

    private StreamReader streamReader;

    //   private string path;
    private int buffer = 1024;
/*
    void Start()
    {
        Debug.Log("Connecting");
        client = new TcpClient(0);
        client.Connect("127.0.0.1", 6969);
        ReadFile("");
        //client.Accept();
    }

    private void ReadFile(string path)
    {
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    private void SendFile(string path)
    {
        byte[] data;
        while (true)
        {
            data = sr.read(path, buffer);
            if (data == null)
            {
                break;
            }

            client.Send(data);
        }
    }


    private void OnDisable()
    {
        client.Close();
    }

// public void SetPath()
// {
//     this.path = path;
// }
*/
}