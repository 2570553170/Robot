using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaraRobotControl : MechanicalControl
{
    /// <summary>
    /// 重写父类中的PointMove，写入ScaraRobot特定的运动控制逻辑
    /// </summary>
    /// <param name="moveDir">运动方向</param>
    public override void PointMove(MoveDir moveDir)
    {
        

        if (isMove)
        {
            switch (moveDir)
            {
                case MoveDir.None:
                    break;
                case MoveDir.Left:
                    MechanicalCCDIK.solver.IKPosition.x += MoveSpeedOld * 0.1f * Time.deltaTime;
                    break;
                case MoveDir.Right:
                    MechanicalCCDIK.solver.IKPosition.x -= MoveSpeedOld * 0.1f * Time.deltaTime;
                    break;
                case MoveDir.Up:
                    MechanicalCCDIK.solver.IKPosition.z -= MoveSpeedOld * 0.1f * Time.deltaTime;
                    break;
                case MoveDir.Down:
                    MechanicalCCDIK.solver.IKPosition.z += MoveSpeedOld * 0.1f * Time.deltaTime;
                    break;
                default:
                    break;
            }
            
        }
    }
    protected override void Update()
    {
        base.Update();
        //HeightAdjust();
    }
    protected override void Start()
    {
        base.Start();
        zhen = GameObject.Find("zhen").transform;
    }

    
    //bool isUpTotle;
    /// <summary>
    /// 高低调整的最大值
    /// </summary>
    [SerializeField]
    float maxHeight = 0.146f;
    /// <summary>
    /// 高低调整的最小值
    /// </summary>
    [SerializeField]
    float minHeight = 0.093f;
    /// <summary>
    /// 重写父类中的HeightAdjust，写入ScaraRobot特定的高低控制逻辑
    /// </summary>
    /// <param name="isUpTotle">是否为向上运动</param>
    public override void HeightAdjust(bool isUpTotle) 
    {
        
            if (isUpTotle)
            {
                zhen.localPosition += new Vector3(0, 0.0005f, 0);
            }
            else
            {
                zhen.localPosition -= new Vector3(0, 0.0005f, 0);
            }

            if (zhen.localPosition.y > maxHeight)
            {
                zhen.localPosition = new Vector3(zhen.localPosition.x, maxHeight, zhen.localPosition.z);
            }
            if (zhen.localPosition.y < minHeight)
            {
                zhen.localPosition = new Vector3(zhen.localPosition.x, minHeight, zhen.localPosition.z);
            }
        }
    
    
}
