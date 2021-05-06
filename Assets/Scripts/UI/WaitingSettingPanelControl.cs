using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class WaitingSettingPanelControl : MonoBehaviour
{
    public InputField inputField;

    [HideInInspector]
    public GameObject TargetTask;
    private void Start()
    {
       
        //inputField = transform.Find("InputField_Waiting").GetComponent<InputField>();
        transform.Find("SaveBtn").GetComponent<Button>().onClick.AddListener(() => { OnSaveButtonClick(); });
    }

    void OnSaveButtonClick() {
        //GameObject go = Instantiate(Resources.Load<GameObject>("UI/TaskCell"), GameObject.Find("TaskShowPanel").transform);
        //WaitingStep step = go.AddComponent<WaitingStep>();
        //if (inputField.text != null)
        //{
        //    step.delayTime = float.Parse(inputField.text);
        //}
        //else
        //{
        //    Debug.Log("未填写延迟时间");
        //}
        //Debug.Log("现在的步骤是：============>" + StepManager.Instance.CurrentMessageSequence);
        //StepManager.Instance.Steps.Add(go);

        //if (inputField.text != null)
        //{
        //    step.delayTime = float.Parse(inputField.text);
        //}
        //else
        //{
        //    Debug.Log("未填写延迟时间");
        //}

        if (inputField.text != null)
        {
            TargetTask.GetComponent<WaitingStep>().delayTime = float.Parse(inputField.text);
        }
        else
        {
            Debug.Log("未填写延迟时间");
        }
        Debug.Log("现在的步骤是：============>" + StepManager.Instance.CurrentMessageSequence);
        //StepManager.Instance.Steps.Add(go);

        if (inputField.text != null)
        {
            TargetTask.GetComponent<WaitingStep>().delayTime = float.Parse(inputField.text);
        }
        else
        {
            Debug.Log("未填写延迟时间");
        }


        gameObject.SetActive(false);

    }


    /// <summary>
    /// 文本输入限制事件（输入触发，只能输入数字）     tips：只要不是正则规定的数就替换成空格
    /// </summary>
    public void InputLimt()
    {
        inputField.text = Regex.Replace(inputField.text, @"[^0-9]", "");

    }

    public void InitInputField() {
        if (inputField.text !=null)
        {
            inputField.text = TargetTask.GetComponent<WaitingStep>().delayTime.ToString();
        }
        else
        {
            inputField.text = "0";
        }
        
    
    }






}
