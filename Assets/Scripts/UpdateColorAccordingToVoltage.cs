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
//    float duration = 1.0f;
    int count = 0;
    
    Renderer rend;
    List<float> voltages = new List<float>();

    void Start()
    {
        rend = GetComponent<Renderer> ();
        string componentName = this.gameObject.transform.parent.gameObject.name;  
//        StreamReader file = new StreamReader(Application.dataPath + "/Voltages/" + name + ".txt");
//        while(!file.EndOfStream)
//        {
//            string line = file.ReadLine();
//            voltages.Add(float.Parse(line)); 
//        }
//        file.Close();  

        string resourceName = componentName;
        TextAsset contents = (TextAsset)Resources.Load(resourceName);
        string[] lines = contents.text.Split('\n');
        foreach (string line in lines)
        {
            voltages.Add(float.Parse(line));
        }
    }

    void Update()
    {
//        float lerp = Mathf.PingPong(Time.time, duration) / duration;
//        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        float lerp = (voltages[count % voltages.Count] + 100) / 100;
        rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        count++;
    }
}
