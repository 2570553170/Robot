using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 机器人的接口类，暂时没用到，以后项目复杂后，让所有机器人类继承持接口，用来规范函数属性等
/// </summary>
interface IMechanical 
{
    void PointMove(MoveDir moveDir);


}
