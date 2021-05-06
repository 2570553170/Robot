using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionShow : MonoBehaviour
{
    public Text x;
    public Text y;
    public Text z;




    Vector3 lastPosition;
    void Update()
    {
        if (lastPosition != GetGoInScene.GetInstance().PointTag.position )
        {
            lastPosition = GetGoInScene.GetInstance().PointTag.position;
            x.text = "X:   " + String.Format("{0:F}", GetGoInScene.GetInstance().PointTag.position.x);
            y.text = "Y:   " + String.Format("{0:F}", GetGoInScene.GetInstance().PointTag.position.y);
            z.text = "Z:   " + String.Format("{0:F}", GetGoInScene.GetInstance().PointTag.position.z);
        }
    }
}
