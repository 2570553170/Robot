using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    /// <summary>
    /// 机器人开始自动运动
    /// </summary>
    public void StartTest() {

        StepManager.Instance.SatrtStep();
    }
    //bool isPause;
    
    //public void Pause() {

    //    Time.timeScale = isPause ? 1 : 0;
    //    isPause = !isPause;
    //}
}
