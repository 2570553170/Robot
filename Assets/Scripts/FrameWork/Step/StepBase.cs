using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 步骤的基类，移动，升降，等待等其他步骤皆继承于它
/// </summary>
public class StepBase : MonoBehaviour
{
    /// <summary>
    /// 步骤编号，第几步
    /// </summary>
    public int stepIndex;
    
    public CommandType taskType;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=> { OnButtonClicked(); });
    }
    public virtual void StartStep() { }
    

    public virtual void DeleteStep() {
    
        
    }
    public virtual void OnButtonClicked() 
    { 
        
    

    }


    
}
