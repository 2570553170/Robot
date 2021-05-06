using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class MoveSettingPanelControl : MonoBehaviour
{
    protected MechanicalControl mechanicalControl;
    public Dropdown dropdown;
    public Button yingyongbtn;
    public Button savePointBtn;
    [HideInInspector]
    public GameObject TargetTask;
    int pointNumber = 0;

    public GameObject left;
    public GameObject right;
    public GameObject up;
    public GameObject down;

    
    protected virtual void Start()
    {
       
        
        mechanicalControl = MechanicalControl.Instance;

        ClickButton(left, MoveDir.Left);
        ClickButton(right, MoveDir.Right);
        ClickButton(up, MoveDir.Up);
        ClickButton(down, MoveDir.Down);

        yingyongbtn.onClick.AddListener(() =>
        {
            OnYingyongBtnClick();
        });
        savePointBtn.onClick.AddListener(() =>
        {
            OnSavePointBtnClick();
        });

    }
    protected virtual void  OnSavePointBtnClick() {
        string key = string.Format("{0:d3}", (StepManager.Instance.SavedPointMessage.Count + 1)); 
        StepManager.Instance.SavedPointMessage.Add(key, mechanicalControl.SavePoint());
        TargetTask.GetComponent<MovingStep>().selectedPoint = key;
        InitDropdown();
        mechanicalControl.ShowTag(key);



        GameObject.Find("MovePanel").SetActive(false);
    }
    //protected string ResourcesPath = "Prefabs/PointTagCell";
    //public void ShowTag(string name)
    //{
    //    //在3D场景中生成一个位置展示点
    //    GameObject go = Instantiate(Resources.Load<GameObject>(ResourcesPath), GetGoInScene.GetInstance().PointTag);
    //    go.transform.parent = GameObject.Find("PointTags").transform;
        
    //    go.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = name;

    //}
    protected virtual void OnYingyongBtnClick()
    {
        if (dropdown.captionText.text !=null)
        {
            TargetTask.GetComponent<MovingStep>().targetPoint = StepManager.Instance.SavedPointMessage[dropdown.captionText.text];
        }

        TargetTask.GetComponent<MovingStep>().selectedPoint = dropdown.captionText.text;
        gameObject.SetActive(false);
    }
    protected virtual void ClickButton(GameObject button,MoveDir moveDir) {
        button.GetComponent<OnButtonPressed>().OnButtonDown += () =>
        {
            mechanicalControl.isMove = true;
            mechanicalControl.moveDir = moveDir;
            mechanicalControl.MechanicalCCDIK.solver.SetIKPosition(GameObject.Find("zhen").transform.position);
            mechanicalControl.MechanicalCCDIK.enabled = true;
            
        };
        button.GetComponent<OnButtonPressed>().OnButtonUp += () =>
        {
            mechanicalControl.isMove = false;
            mechanicalControl.moveDir = MoveDir.None;
            mechanicalControl.MechanicalCCDIK.enabled = false;
        };
    }

    public void InitDropdown() 
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        foreach (var item in StepManager.Instance.SavedPointMessage)
        {
            Dropdown.OptionData drop = new Dropdown.OptionData();
            drop.text = item.Key;
            optionDatas.Add(drop);
        }
        
        dropdown.options = optionDatas;
        dropdown.captionText.text = TargetTask.GetComponent<MovingStep>().selectedPoint;
        
    }




   


    
   

}
