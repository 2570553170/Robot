using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 升降步骤相关的类
/// </summary>
public class ShengjiangStep : StepBase
{
    /// <summary>
    /// 现在指针所在的高度（即机器人末端点的高度）
    /// </summary>
    [HideInInspector]
    public float goHeight;
    /// <summary>
    /// 控制升降相关的UI面板
    /// </summary>
    GameObject shenjiangSettingPanel;
    
    /// <summary>
    /// 开始自动执行升降步骤
    /// </summary>
    public override void StartStep()
    {
        
        GameObject.Find("zhen").transform.DOMoveY(goHeight, 2f).OnComplete(()=> { StepManager.Instance.NextStep(); });
    }
    /// <summary>
    /// 当应用按钮被点击时，记录相关设置
    /// </summary>
    public override void OnButtonClicked()
    {
        base.OnButtonClicked();
        if (shenjiangSettingPanel == null)
        {
            shenjiangSettingPanel = GetGoInScene.GetInstance().GetGO("GaodiSettingPanel");

        }
        shenjiangSettingPanel.GetComponent<GaodiSettingPanelControl>().TargetTask = gameObject;
        shenjiangSettingPanel.GetComponent<GaodiSettingPanelControl>().InitThis();
        shenjiangSettingPanel.SetActive(true);

    }
}
