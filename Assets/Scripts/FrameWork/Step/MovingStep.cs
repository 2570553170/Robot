using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 移动步骤相关的类
/// </summary>
public class MovingStep : StepBase
{
    /// <summary>
    /// 移动的目标点
    /// </summary>
    public JointsMessage targetPoint = new JointsMessage();
    /// <summary>
    /// 目标点在点表中的名字
    /// </summary>
    public string selectedPoint;
    /// <summary>
    /// 移动设置相关的Panel
    /// </summary>
    GameObject moveSettingPanel;
    /// <summary>
    /// 开始自动移动
    /// </summary>
    public override void StartStep()
    {
        
        if (targetPoint !=null)
        {
            MechanicalControl.Instance.StartRotate(targetPoint);
        }
    }
    /// <summary>
    /// 当应用按钮被按下是调用，记录移动指令相关设置
    /// </summary>
    
    public override void OnButtonClicked()
    {

        base.OnButtonClicked();
        if (moveSettingPanel == null)
        {
            moveSettingPanel = GetGoInScene.GetInstance().GetGO("MoveSettingPanel");

        }
        moveSettingPanel.GetComponent<MoveSettingPanelControl>().TargetTask = gameObject;
        moveSettingPanel.GetComponent<MoveSettingPanelControl>().InitDropdown();
        moveSettingPanel.SetActive(true);
        
    }
    
   

    
}
