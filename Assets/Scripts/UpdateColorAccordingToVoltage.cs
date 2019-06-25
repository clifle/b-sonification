using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UpdateColorAccordingToVoltage : MonoBehaviour
{
    // Fade the color from red to green
    // back and forth over the defined duration

    Color colorStart = Color.blue;
    Color colorEnd = Color.green;
    int count = 0;
    
    Renderer rend;
    List<float> voltages = new List<float>();
    List<float> reds = new List<float>();
    List<float> greens = new List<float>();
    List<float> blues = new List<float>();

    void Start()
    {
        rend = GetComponent<Renderer> ();
        string componentName = this.gameObject.transform.parent.gameObject.name;  

	    // Loading voltages
        string resourceName = componentName;
        TextAsset contents = (TextAsset)Resources.Load(resourceName);
        string[] lines = contents.text.Split('\n');
        foreach (string line in lines)
        {
            if( !string.IsNullOrEmpty(line))
            {
                voltages.Add(float.Parse(line));
            }
        }

	    // Loading colormap
        TextAsset colormap = (TextAsset)Resources.Load("coolwarm");
        string[] colormapLines = colormap.text.Split('\n');
        foreach (string line in colormapLines)
        {
	        string[] values = line.Split(' ');

            // ignore first line
            if( values.Length == 1)
                continue;

            // Load colors
            reds.Add(float.Parse(values[0]));
            greens.Add(float.Parse(values[1]));
            blues.Add(float.Parse(values[2]));
        }
    }

    void Update()
    {
        /*
        float lerp = (voltages[count % voltages.Count] + 100) / 100;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        */

        float voltage = voltages[count % voltages.Count];
        int index = (int)(voltage + 100.0);
        rend.material.color = new Color(reds[index], greens[index], blues[index]);

        count++;
    }
}
