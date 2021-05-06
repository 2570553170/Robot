using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 等待步骤相关的类
/// </summary>
public class WaitingStep : StepBase
{
    /// <summary>
    /// 等待的时间，单位秒
    /// </summary>
    public float delayTime;
    /// <summary>
    /// 等待步骤相关的UI面板
    /// </summary>
    GameObject waitSettingPanel;

    /// <summary>
    /// 自动开始执行这一步骤
    /// </summary>
    public override void StartStep()
    {
        StartCoroutine(WaitingToStart());
    }

    /// <summary>
    /// 等待时间，时间到后再执行下一步
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitingToStart() {

        yield return new WaitForSeconds(delayTime);
        StepManager.Instance.NextStep();
    }
    /// <summary>
    /// 保存等待步骤相关的设置
    /// </summary>
    public override void OnButtonClicked()
    {
        base.OnButtonClicked();
        if (waitSettingPanel == null)
        {
            waitSettingPanel = GetGoInScene.GetInstance().GetGO("WaitSettingPanel");


        }
        waitSettingPanel.GetComponent<WaitingSettingPanelControl>().TargetTask = gameObject;
        waitSettingPanel.GetComponent<WaitingSettingPanelControl>().InitInputField();
        waitSettingPanel.SetActive(true);

    }
}
