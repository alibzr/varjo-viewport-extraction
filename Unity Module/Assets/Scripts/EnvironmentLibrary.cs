using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentLibrary : MonoBehaviour
{
    // number of contents
    private static int contentCount = 10;

    // number of distortions
    private static int distortionCount = 5;

    // Initialize main environments - they are assigned in Awake function automatically. Ranodmized to mitigate order bias.
    public List<Environment> m_MainEnvironments = new List<Environment>();

    // just a content id to keep track of the test images
    public int[] MainContentIndex = Enumerable.Range(1, contentCount).ToArray<int>();

    public UnityEngine.Object[] textures = null;

    // a list of tuples that contains scaling factors
    public List<Tuple<float, float>> scalingFactors = new List<Tuple<float, float>>();

    // a list of tuples that contains longitude and latitude
    private int session = 2;



    private void Awake()
    {
        if (session == 1)
        {
            scalingFactors.Add(new Tuple<float, float>(1.0f, 1.0f));
            scalingFactors.Add(new Tuple<float, float>(1.0f, 0.8f));
            scalingFactors.Add(new Tuple<float, float>(1.0f, 0.7f));
            scalingFactors.Add(new Tuple<float, float>(1.0f, 0.6f));
            scalingFactors.Add(new Tuple<float, float>(1.0f, 0.5f));
        }
        else
        {
            scalingFactors.Add(new Tuple<float, float>(1.0f, 1.0f));
            scalingFactors.Add(new Tuple<float, float>(1.0f, 0.9f));
            scalingFactors.Add(new Tuple<float, float>(1.0f, 0.8f));
            scalingFactors.Add(new Tuple<float, float>(1.0f, 0.7f));
            scalingFactors.Add(new Tuple<float, float>(1.0f, 0.6f));
        }
        // loads all the omnidirectional images from the folder named Resournce
        textures = Resources.LoadAll("Textures");

        // generated a list of environments
        for (int i = 0; i < contentCount; ++i)
        {
            for (int j = 0; j < distortionCount; ++j)
            {
                m_MainEnvironments.Add(new Environment
                {
                    m_WorldRotation = 270,
                    m_ContentIndex = MainContentIndex[i],
                    m_FocusScalingFactor = scalingFactors[j].Item1,
                    m_ContextScalingFactor = scalingFactors[j].Item2,
                    m_Background = (Texture2D)textures[i]
                });
            }
        }
    }
}

[Serializable]
public class Environment
{
    // world rotation determines the rotation of the 360-image
    public int m_WorldRotation = 0;
    public int m_ContentIndex = 0;
    public float m_FocusScalingFactor = 1;
    public float m_ContextScalingFactor = 1;
    public Texture m_Background = null;
}