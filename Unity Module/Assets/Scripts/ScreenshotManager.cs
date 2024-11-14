using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

public class ScreenshotManager : MonoBehaviour
{
    public EnvironmentLibrary m_EnvironmentLibrary = null;
    public GameObject XRRing = null;
    [Serializable] public class NewEnvironment : UnityEvent<Environment> { }
    public NewEnvironment OnNewEnvironment = null;
    private int environmentIndex = 0;
    private int rotationIndex = 0;
    private NamedPipeClientStream clientStream;
    private string message;
    // a list of tuples that contains longitude and latitude
    public List<(int,int)> longLat = new List<(int, int)>
        {
            /*(0,-45),(0,0),(0,45),
            (60,-45),(60,0),(60,45),
            (120,-45),(120,0),(120,45),
            (180,-45),(180,0),(180,45),
            (240,-45),(240,0),(240,45),
            (300,-45),(300,0),(300,45),*/
            (0  ,-60),(0  ,-30),(0  ,0),(0  ,30),(0  ,60),
            (30 ,-60),(30 ,-30),(30 ,0),(30 ,30),(30 ,60),
            (60 ,-60),(60 ,-30),(60 ,0),(60 ,30),(60 ,60),
            (90 ,-60),(90 ,-30),(90 ,0),(90 ,30),(90 ,60),
            (120,-60),(120,-30),(120,0),(120,30),(120,60),
            (150,-60),(150,-30),(150,0),(150,30),(150,60),
            (180,-60),(180,-30),(180,0),(180,30),(180,60),
            (210,-60),(210,-30),(210,0),(210,30),(210,60),
            (240,-60),(240,-30),(240,0),(240,30),(240,60),
            (270,-60),(270,-30),(270,0),(270,30),(270,60),
            (300,-60),(300,-30),(300,0),(300,30),(300,60),
            (330,-60),(330,-30),(330,0),(330,30),(330,60),
        };
    private async void Start()
    {
        try
        {
            clientStream = new NamedPipeClientStream(".", "myNamedPipe", PipeDirection.Out);
            await clientStream.ConnectAsync();

            Debug.Log("Connected to the named pipe.");

            // Send multiple messages asynchronously
            while (environmentIndex < m_EnvironmentLibrary.m_MainEnvironments.Count)
            {
                // Selects a new omnidirectional image
                Select();

                while (rotationIndex < longLat.Count)
                {
                    changeCameraDirection();

                    message = "2" + "_" + environmentIndex + "_" + rotationIndex;

                    await Task.Delay(500);

                    await SendMessageAsync(message);

                    await Task.Delay(500);

                    rotationIndex++;
                }

                environmentIndex++;
                rotationIndex = 0;
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
    private void Select()
    {
        // Send new environment - to be assingned to the skybox
        OnNewEnvironment.Invoke(m_EnvironmentLibrary.m_MainEnvironments[environmentIndex]);
    }
    private void changeCameraDirection()
    {
        // change direction of the camera to the latitude and longitude values
        XRRing.transform.rotation = Quaternion.Euler(longLat[rotationIndex].Item2, longLat[rotationIndex].Item1, 0);
    }
    private async Task SendMessageAsync(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        await clientStream.WriteAsync(buffer, 0, buffer.Length);
    }
}
