using UnityEngine;
using UnityEngine.UI;
using System;

public class UIControl : MonoBehaviour
{
    MechanicalControl mechanicalControl;

    int pointNumber = 0;

    void Start()
    {
        
        
        mechanicalControl = MechanicalControl.Instance;

        ClickButton("left", MoveDir.Left);
        ClickButton("right", MoveDir.Right);
        ClickButton("up", MoveDir.Up);
        ClickButton("down", MoveDir.Down);

        
    }
    /// <summary>
    /// 点击上下左右四个调整按钮时的行为逻辑
    /// </summary>
    /// <param name="name">按钮的名字，用于查找场景中的Gameobject实例</param>
    /// <param name="moveDir">运动的方向</param>
    void ClickButton(string name,MoveDir moveDir) {
        transform.Find(name).GetComponent<OnButtonPressed>().OnButtonDown += () =>
        {
            mechanicalControl.isMove = true;
            mechanicalControl.moveDir = moveDir;

        };
        transform.Find(name).GetComponent<OnButtonPressed>().OnButtonUp += () =>
        {
            mechanicalControl.isMove = false;
            mechanicalControl.moveDir = MoveDir.None;
        };
    }
   

    
}
