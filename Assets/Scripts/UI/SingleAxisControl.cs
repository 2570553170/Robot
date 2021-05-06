using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MoveMode { 
X,
Y,
Z,
MoveUpDown


}
public class SingleAxisControl : MonoBehaviour
{
    public MoveMode moveMode;
    public float speed=1;
    public int jointIndex;
    public GameObject add;
    public GameObject sub;
    public Text show;

    MechanicalControl mechanicalControl;

    private void Start()
    {
        mechanicalControl = MechanicalControl.Instance;
        if (moveMode == MoveMode.Y)
        {
            add.GetComponent<OnButtonPressed>().OnButtonStay += () => {

                mechanicalControl.jointsDic["Joint" + jointIndex].Rotate(Vector3.up, speed);
                
            };
            sub.GetComponent<OnButtonPressed>().OnButtonStay += () => {

                mechanicalControl.jointsDic["Joint" + jointIndex].Rotate(Vector3.up, -speed);
                
            };
        }
        else if(moveMode == MoveMode.MoveUpDown)
        {
            add.GetComponent<OnButtonPressed>().OnButtonStay += () => {

                //mechanicalControl.jointsDic["Joint" + jointIndex].Rotate(Vector3.up, speed);


            };
            sub.GetComponent<OnButtonPressed>().OnButtonStay += () => {

                //mechanicalControl.jointsDic["Joint" + jointIndex].Rotate(Vector3.up, -speed);


            };
        }
        

        
    }
    private void Update()
    {
        if (moveMode ==MoveMode.Y)
        {
            show.text = "Y轴旋转:" + mechanicalControl.jointsDic["Joint" + jointIndex].localEulerAngles.y.ToString("0");
        }
        else if (moveMode == MoveMode.MoveUpDown)
        {
            show.text = "Y轴旋转:" + mechanicalControl.jointsDic["Joint" + jointIndex].localPosition.y.ToString("0");
        }
        
    }
}
