using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;
using RootMotion.FinalIK;
using TMPro;

/// <summary>
/// 移动方向
/// </summary>
public enum MoveDir { 
None,
Left,
Right,
Up,
Down

}
/// <summary>
/// 机器人的基本功能类，以后有偏差可修改，将独特的特性，独立到特定机器人的脚本里
/// </summary>
public class MechanicalControl : MonoBehaviour
{
    /// <summary>
    /// 存储关节点的实例对象的字典
    /// </summary>
    public Dictionary<string,Transform> jointsDic = new Dictionary<string,Transform>();
    /// <summary>
    /// 这个类的静态实例对象方便外部调用持类
    /// </summary>
    public static MechanicalControl Instance;
    /// <summary>
    /// 机器人上所挂载的控制运动的插件脚本CCDIK
    /// </summary>
    [Tooltip("控制机械臂的CCDIK,不拖拽就查找此脚本所挂载物体上的CCDIK")]
    public CCDIK MechanicalCCDIK;
    /// <summary>
    /// 老的移动方法的运动速度（各个关节单独运动的运动方法）
    /// </summary>
    public float MoveSpeedOld=1;
    private void Awake()
    {

        Instance = this;
    }

    // Start is called before the first frame update
    protected virtual void  Start()
    {
        if (MechanicalCCDIK == null)
        {
            MechanicalCCDIK = GetComponent<CCDIK>();
        }
        
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Joint");
        for (int i = 0; i < gos.Length; i++)
        {
            jointsDic.Add(gos[i].name, gos[i].transform);
        }
        MoveSpeed = MoveSpeed * Time.deltaTime;

        
    }
    /// <summary>
    /// 机器人是否在被按钮控制着移动
    /// </summary>
    public bool isMove;
    public MoveDir moveDir = MoveDir.None;
    /// <summary>
    /// 新的移动方法，末端点移动方法的移动速度（用插件FanilIK实现）
    /// </summary>
    protected float MoveSpeed = 10 ;
    /// <summary>
    /// 机械臂的关节终端位置，父类中只申明了变量，在子类中赋值
    /// </summary>
    public Transform zhen;
    //int a;
    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateMove();
        //a++;
        //Vector3 v = GetIkpositionOffset(new Vector3(0.4f,0.1f,0),new Vector3(0.2f, 0.1f, 0), a);
        //Debug.Log(v);
        //MechanicalCCDIK.solver.SetIKPosition(v);
    }

    /// <summary>
    /// 每帧判断相关事件
    /// </summary>
    protected virtual void UpdateMove() 
    {
        //判断手动调整机器人末端点位置
        PointMove(moveDir);
        //老的运动方法，判断机器人自动运动时，运动是否结束
        //if (CompletePoint > 0 && allowIn)
        //{
        //    if (OnRotateComplete != null)
        //    {
        //        OnRotateComplete();
        //    }

        //    CompletePoint = 0;
        //    allowIn = false;
        //    StepManager.Instance.NextStep();

        //}

    }

    //public Action OnRotateComplete;
    //protected bool allowIn = true;
    /// <summary>
    /// 保存末端点和各关节信息
    /// </summary>
    /// <returns>存储末端点和各关节信息的类</returns>
    public JointsMessage SavePoint()
    {
        JointsMessage jointsMessage = new JointsMessage();
        for (int i = 0; i < jointsDic.Count; i++)
        {
            jointsMessage.jointRotation.Add(jointsDic["Joint" + i].localEulerAngles) ;
            jointsMessage.IKPoint = GetGoInScene.GetInstance().PointTag.position;
        }

        return jointsMessage;

    }

    #region 用dotween优化机械臂自动运动方式

    /// <summary>
    /// 机器人开始自由运动
    /// </summary>
    /// <param name="jointsMessage"></param>
    public void StartRotate(JointsMessage jointsMessage)
    {
        //老方法的运动，用Dowteem实现的各个关节配合移动的方式
        //List<Tweener> tweeners = new List<Tweener>();
        //if (jointsMessage != null)
        //{
        //    tweeners = RotateTo(jointsMessage);
        //}
        //allowIn = true;
        StartCoroutine(SimpleMoveP(jointsMessage.IKPoint));
        


    }
    /// <summary>
    /// 末端点直线运动的实现代码
    /// </summary>
    /// <param name="Target">运动的终点</param>
    /// <returns></returns>
    IEnumerator SimpleMoveP(Vector3 Target) 
    {
        MechanicalCCDIK.enabled = true;
        int i = 0;
        Vector3 StartPoint = GetGoInScene.GetInstance().PointTag.position;
        while (true)
        {
            i++;
            Vector3 nextPoint = GetIkpositionOffset(StartPoint, Target, i);
            MechanicalCCDIK.solver.SetIKPosition(nextPoint);
            //if (zhen.localPosition.y <= 0.146f && zhen.localPosition.y >= 0.093f)
            //{
            //    zhen.localPosition += new Vector3(0, 0.0001f, 0) * (Target.y - StartPoint.y);
            //}
            if (nextPoint == Target)
            {
                
                Debug.Log(nextPoint + "==========>" + Target);
                MechanicalCCDIK.enabled = false;
                
                StepManager.Instance.NextStep();
                
                break;
            }
            yield return null;
            
        }
        
    
    
    }
    /// <summary>
    /// 通过开始点，结束点运动速度，计算获得每帧相应的位置点
    /// </summary>
    /// <param name="start">开始运动的点</param>
    /// <param name="end">运动终点</param>
    /// <param name="index">从开始运动后的帧数，即开始运动后的第几帧</param>
    /// <returns></returns>
    Vector3 GetIkpositionOffset(Vector3 start,Vector3 end,int index) 
    {
        
        Vector3 rtn = start + (end - start).normalized *index*MoveSpeed*0.1f*0.02f;
        if (Vector3.Distance(start, end) <= Vector3.Distance(start, rtn))
        {
            rtn = end;
        }
        //Debug.Log(rtn.x +"                  "+ rtn.z);
        return rtn;
   
    
    }
    /// <summary>
    /// 老方法的运动，用DoTween控制关节点旋转，现在已弃用，以后有需要可参考使用
    /// </summary>
    /// <param name="pointMessage"></param>
    /// <returns></returns>
    List<Tweener> RotateTo(JointsMessage pointMessage) 
    {
        List<Tweener> tweeners = new List<Tweener>();
        for (int i = 0; i < jointsDic.Count; i++)
        {
            tweeners.Add( RotateToSingle(jointsDic["Joint" + i], pointMessage.jointRotation[i]));
        }

        return tweeners;
    
    }
    Tweener RotateToSingle(Transform tf, Vector3 targetRotation)
    {
        return tf.DOLocalRotate(targetRotation, 2f).OnComplete(CompleteAction);
        
    }
    protected int CompletePoint = 0;
    void CompleteAction()
    {
        CompletePoint++;
        Debug.Log("有一个任务完成");
    }
    /// <summary>
    /// 用按钮控制机器人运动
    /// </summary>
    /// <param name="moveDir">运动方向</param>
    public virtual void PointMove(MoveDir moveDir){}
    /// <summary>
    /// 用按钮控制机器人Y轴方向高低调整
    /// </summary>
    /// <param name="isUpTotle"></param>
    public virtual void HeightAdjust(bool isUpTotle) { }
    /// <summary>
    /// 标记已保存位置点的指示物的预制体在Resources文件夹下的路径
    /// </summary>
    protected string ResourcesPath = "Prefabs/PointTagCell";
    /// <summary>
    /// 在末端点现在所在位置生成已保存位置点的标记
    /// </summary>
    /// <param name="name">标记显示的名字</param>
    public void ShowTag(string name)
    {
        //在3D场景中生成一个位置展示点
        GameObject go = Instantiate(Resources.Load<GameObject>(ResourcesPath), GetGoInScene.GetInstance().PointTag);
        go.transform.parent = GameObject.Find("PointTags").transform;
        go.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        go.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = name;

    }
    /// <summary>
    /// 在指定位置生成已保存位置点的标记
    /// </summary>
    /// <param name="target">标记的位置</param>
    /// <param name="name">标记显示的名字</param>
    public void ShowTag(Vector3 target, string name)
    {
        //在3D场景中生成一个位置展示点
        GameObject go = Instantiate(Resources.Load<GameObject>(ResourcesPath));
        go.transform.parent = GameObject.Find("PointTags").transform;
        go.transform.position = target;
        go.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        go.transform.Find("Text (TMP)").GetComponent<TextMeshPro>().text = name;

    }
    #endregion
}

/// <summary>
/// 存储机器人末端点和关节点信息的类
/// </summary>
public class JointsMessage 
{
    public List<Vector3> jointRotation  = new List<Vector3>();

    public Vector3 IKPoint = new Vector3();
   

}
