using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class MyButtonExtrn
{
    static void ButtonPress(this Button button) 
    {
        Debug.Log("拓展强化Button");
    
    }
}
