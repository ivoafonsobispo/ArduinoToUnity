using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class Controller : MonoBehaviour
{
    Thread IOThread = new Thread(DataThread);
    private static SerialPort sp;
    private static string incomingMsg = "";
    private static string outgoingMsg = "";

    private static void DataThread()
    {
        sp = new SerialPort("COM3", 9600);
        sp.Open();

        while (true)
        {
            if (sp.IsOpen)
            {
                if (outgoingMsg != "")
                {
                    sp.Write(outgoingMsg);
                }
            }

            incomingMsg = sp.ReadExisting();
            Thread.Sleep(200);
        }
    }

    private void OnDestroy()
    {
        IOThread.Abort();
        sp.Close();
    }

    void Start()
    {
        IOThread.Start();
    }

    void Update()
    {
        if (incomingMsg != "")
        {
            Debug.Log(incomingMsg);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            outgoingMsg = "1";
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            outgoingMsg = "0";
        }
    }
}
