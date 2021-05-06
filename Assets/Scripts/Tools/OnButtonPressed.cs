using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

/// <summary>
/// 按钮的长按检查，OnButtonUp，OnButtonDown，OnButtonStay，三个委托时间分别会在按钮抬起时，按下时，一直按着时调用
/// </summary>
public class OnButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    
    // 延迟时间
    private float delay = 0.2f;

    // 按钮是否是按下状态
    private bool isDown = false;

    // 按钮最后一次是被按住状态时候的时间
    private float lastIsDownTime;

    public Action OnButtonUp;
    public Action OnButtonDown;
    public Action OnButtonStay;
    
    private void FixedUpdate()
    {
        
        // 如果按钮是被按下状态
        if (isDown)
        {
            // 当前时间 -  按钮最后一次被按下的时间 > 延迟时间0.2秒
            if (Time.time - lastIsDownTime > delay)
            {
                // 触发长按方法
                Debug.Log("长按");
                // 记录按钮最后一次被按下的时间
                if (OnButtonStay != null)
                {
                    OnButtonStay();
                }
                

            }
        }
    }
    void Update()
    {
        
    }

    // 当按钮被按下后系统自动调用此方法
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        lastIsDownTime = Time.time;
        if (OnButtonDown != null)
        {
            OnButtonDown();
        }
    }

    // 当按钮抬起的时候自动调用此方法
    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        if (OnButtonUp != null)
        {
            OnButtonUp();
            lastIsDownTime = Time.time;
        }
        
    }

    // 当鼠标从按钮上离开的时候自动调用此方法
    public void OnPointerExit(PointerEventData eventData)
    {
        isDown = false;
        
    }
}
