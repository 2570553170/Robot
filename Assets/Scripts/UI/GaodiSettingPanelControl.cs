using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaodiSettingPanelControl : MonoBehaviour
{
    

    [HideInInspector]
    public GameObject TargetTask;
    [SerializeField]
    float maxHeight = 0.478f;
    [SerializeField]
    float minHeight = 0.426f;
    Transform zhen;
    private void Start()
    {
        
        zhen = GameObject.Find("zhen").transform;
        transform.Find("SaveBtn").GetComponent<Button>().onClick.AddListener(() => { OnSaveButtonClick(); });
        ClickButton(transform.Find("Moveup_btn").gameObject, true);
        ClickButton(transform.Find("Movedown_btn").gameObject, false);
        
    }

    void OnSaveButtonClick()
    {
        TargetTask.GetComponent<ShengjiangStep>().goHeight = zhen.position.y;
        gameObject.SetActive(false);


    }
    protected void ClickButton(GameObject button, bool isUp)
    {
        button.GetComponent<OnButtonPressed>().OnButtonStay += () =>
        {
            ScaraRobotControl.Instance.HeightAdjust(isUp);

        };

        //button.GetComponent<OnButtonPressed>().OnButtonDown += () =>
        //{
        //    isUpTotle = isUp;
        //    isCanMove = true;

        //}; 
        //button.GetComponent<OnButtonPressed>().OnButtonUp += () =>
        //{

        //    isCanMove = false;

        //};
    }
    //bool isCanMove;
    //bool isUpTotle;
    //private void Update()
    //{
    //    if (isCanMove)
    //    {
    //        if (isUpTotle)
    //        {
    //            zhen.localPosition += new Vector3(0, 0.0001f, 0);
    //        }
    //        else
    //        {
    //            zhen.localPosition -= new Vector3(0, 0.0001f, 0);
    //        }

    //        if (zhen.localPosition.y > maxHeight)
    //        {
    //            zhen.localPosition = new Vector3(zhen.localPosition.x, maxHeight, zhen.localPosition.z);
    //        }
    //        if (zhen.localPosition.y < minHeight)
    //        {
    //            zhen.localPosition = new Vector3(zhen.localPosition.x, minHeight, zhen.localPosition.z);
    //        }
    //    }
    //}
    public void InitThis() { 
    
    }






    

}
