using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpTest : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        trans_1 = transform.position;
    }
     Vector3 trans_1;
    public Transform trans_2;

    public float speed;
    private float timer;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Slerp(trans_1, trans_2.position, 0.2f);
    }
}
