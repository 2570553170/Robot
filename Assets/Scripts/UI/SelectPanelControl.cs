using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanelControl : MonoBehaviour
{
    int number = 0;
    private void Num()
    {
        number++;
    }
    private void Start()
    {
        transform.Find("Move").GetComponent<Button>().onClick.AddListener(() => { Num(); OnMoveBtnClick(); });
        transform.Find("Wait").GetComponent<Button>().onClick.AddListener(() => { Num(); OnWaitBtnClick(); });
        transform.Find("Gaodi").GetComponent<Button>().onClick.AddListener(() => { Num(); OnDaodiBtnClick(); });
        transform.Find("Reset").GetComponent<Button>().onClick.AddListener(() => { Num(); OnResetBtnClick(); });
        
    }

    GameObject MoveSettingPanel;

    void OnMoveBtnClick() {

        GameObject go = Instantiate(Resources.Load<GameObject>("UI/TaskCell"), GameObject.Find("TaskShowPanel").transform);
        
        MovingStep step = go.AddComponent<MovingStep>();
        //step.targetPoint = MechanicalControl.Instance.SavePoint();
        go.transform.Find("Text").GetComponent<Text>().text = "移动指令" + number;
        Debug.Log("现在的步骤是：============>" + StepManager.Instance.CurrentMessageSequence);

        StepManager.Instance.Steps.Add(go);

    }
    GameObject WaitSettingPanel;

    void OnWaitBtnClick() 
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("UI/TaskCell"), GameObject.Find("TaskShowPanel").transform);
        WaitingStep step = go.AddComponent<WaitingStep>();
        go.transform.Find("Text").GetComponent<Text>().text = "待机指令" + number;
        Debug.Log("现在的步骤是：============>" + StepManager.Instance.CurrentMessageSequence);
        StepManager.Instance.Steps.Add(go);

    }
    void OnDaodiBtnClick()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("UI/TaskCell"), GameObject.Find("TaskShowPanel").transform);
        ShengjiangStep step = go.AddComponent<ShengjiangStep>();
        go.transform.Find("Text").GetComponent<Text>().text = "升降指令"+number;
        Debug.Log("现在的步骤是：============>" + StepManager.Instance.CurrentMessageSequence);
        StepManager.Instance.Steps.Add(go);

    }

    void OnResetBtnClick()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("UI/TaskCell"), GameObject.Find("TaskShowPanel").transform);
        RestStep step = go.AddComponent<RestStep>();
        go.transform.Find("Text").GetComponent<Text>().text = "复位指令" + number;
        Debug.Log("现在的步骤是：============>" + StepManager.Instance.CurrentMessageSequence);
        StepManager.Instance.Steps.Add(go);
    }
    GameObject LoadPanelFromRes(string path) {

        return Instantiate(Resources.Load<GameObject>(path),transform.parent);

    }

    public void OnNext()
    {
        
    }
}
