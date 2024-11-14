using UnityEngine;
using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

public class IPCClient : MonoBehaviour
{
    private NamedPipeClientStream clientStream;
    private const int messageCount = 5; // Number of messages to send

    private async void Start()
    {
        try
        {
            clientStream = new NamedPipeClientStream(".", "myNamedPipe", PipeDirection.Out);
            await clientStream.ConnectAsync();

            Debug.Log("Connected to the named pipe.");

            // Send multiple messages asynchronously
            for (int i = 0; i < messageCount; i++)
            {
                await SendMessageAsync($"Message {i + 1} from Unity!");
                await Task.Delay(5000);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            if (clientStream != null)
                clientStream.Close();
        }
    }

    private async Task SendMessageAsync(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await clientStream.WriteAsync(buffer, 0, buffer.Length);
    }
}

