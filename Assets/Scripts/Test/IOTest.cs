using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class IOTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        string path = SaveDialog.SelectPath();
        StreamReader sr = new StreamReader(path, Encoding.Default);
        while (true)
        {
            string fileDataLine;

            fileDataLine = sr.ReadLine();
            if (fileDataLine == null)
            {
                break;
            }
            Debug.Log(fileDataLine);
        }
        sr.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
