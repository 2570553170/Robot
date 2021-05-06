using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 步骤的管理类
/// </summary>
public class StepManager
{
    /// <summary>
    /// 步骤的总数量
    /// </summary>
    public int TaskCount;
    /// <summary>
    /// 当前步骤的标号，即第几步
    /// </summary>
    public int CurrentMessageSequence = 0;
    /// <summary>
    /// 按顺序存储所有的步骤
    /// </summary>
    private List<GameObject> steps;
    /// <summary>
    /// 已经保存了的，可待选择的JointMessage
    /// </summary>
    private Dictionary<string, JointsMessage> savedPointMessage;

    /// <summary>
    /// 类自己的实例，管理类需用单例模式，设置成单例，整个场景中只能存在一个
    /// </summary>
    private static StepManager instance;

    public List<GameObject> Steps { get {
            if (steps == null)
            {
                steps = new List<GameObject>();
            }
            return steps;
        } 
        set => steps = value; }

    /// <summary>
    /// 简单的单例模式
    /// </summary>
    public static StepManager Instance { get
        {
            if (instance == null)
            {
                instance = new StepManager();
            }
            return instance;
        }
        set => instance = value; }

    
    public Dictionary<string, JointsMessage> SavedPointMessage { get
        {
            if (savedPointMessage == null)
            {
                savedPointMessage = new Dictionary<string, JointsMessage>();
            }
            return savedPointMessage;
        } set => savedPointMessage = value; }

    /// <summary>
    /// 开始自动执行步骤相应步骤
    /// </summary>
    /// <param name="Index">步骤的序号，从0开始，和存储步骤的steps的序号对应</param>
    public void SatrtStep(int Index = -1) {
        if (Index == -1)
        {
            Index = CurrentMessageSequence;
        }
        if (Index<steps.Count)
        {
            steps[Index].GetComponent<StepBase>().StartStep();
        }
        else
        {
            Debug.Log("所有步骤已经运行完成，或者所给步骤序号有误，超出最大值");
            CurrentMessageSequence = 0;
        }
        
    }
    /// <summary>
    /// 当前步骤已完成，执行下一步步骤
    /// </summary>
    public void NextStep() 
    {
        
        CurrentMessageSequence++;
        Debug.Log("上一步骤已经完成，下一步是================>" + CurrentMessageSequence);
        SatrtStep(CurrentMessageSequence);


    }
    
}
