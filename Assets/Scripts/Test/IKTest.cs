using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MechanicalControl.Instance.MechanicalCCDIK.solver.GetIKPosition());
        if (Input.GetMouseButtonDown(0))
        {
            
            MechanicalControl.Instance.MechanicalCCDIK.solver.SetIKPosition(new Vector3(-0.4f,-0.5f,0));
            Debug.Log(MechanicalControl.Instance.MechanicalCCDIK.solver.GetIKPosition());
        }
    }
}
