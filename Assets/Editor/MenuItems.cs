using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuItems
{
    [MenuItem("Tools/ClearPlayerPrefs")]
    private static void NewMenuOption()
    {
        //PlayerPrefs.DeleteAll();
        Debug.Log("执行了");
    }
    static string Name;

    
    //[ContextMenuItem("Randomize Name", "Randomize")]
    //private void Randomize()
    //{
    //    Name = "Some Random Name";
    //}
}
