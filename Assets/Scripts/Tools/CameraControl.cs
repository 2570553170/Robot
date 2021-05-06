using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 相机控制脚本
/// </summary>
public class CameraControl : MonoBehaviour
{
    //目标物体 主相机要围绕其旋转的物体
    public Transform target;

    //主相机初始化时与目标物体之间的距离
    public float distance = 7.0f;


    //[HideInInspector]
    public float eulerAngles_x;
    //[HideInInspector]
    public float eulerAngles_y;


    //水平滚动相关

    //主相机与目标物体之间的最大缩放距离
    public float distanceMax = 10;

    //主相机与目标物体之间的最小缩放距离
    public float distanceMin = 3;
    
    //最大x（单位是角度）
    public int xMaxLimit = 45;

    //最小x（单位是角度）
    public int xMinLimit = -45;

    //主相机水平方向旋转速度
    public float xSpeed = 70.0f;


    //垂直滚动相关

    //最大y（单位是角度）
    public int yMaxLimit = 360;

    //最小y（单位是角度）
    public int yMinLimit = -360;

    //主相机纵向旋转速度
    public float ySpeed = 70.0f;


    Vector3 Teme;
    float XX, YY;

    //Vector3 screenPoint, offset;
    Vector3  OldPoint;


    //滚轮相关
    //鼠标滚轮灵敏度（备注：鼠标滚轮滚动后将调整相机与目标物体之间的间隔）
    public float MouseScrollWheelSensitivity = 1.0f;


    public LayerMask CollisionLayerMask;


    void Start()
    {
        Vector3 eulerAngles = this.transform.eulerAngles;//当前物体的欧拉角
        this.eulerAngles_x = eulerAngles.y;
        this.eulerAngles_y = eulerAngles.x;
        //设置鼠标贴图 / 恢复默认贴图
        //Cursor.SetCursor(Resources.Load<Texture2D>("Mouse/shouzhi"), Vector2.zero, CursorMode.ForceSoftware);
    }


    void Update()
    {


        ////中键目标左右移动
        //if (Input.GetMouseButtonDown(2))
        //{
        //    OldPoint = Input.mousePosition;
        //    //Cursor.SetCursor(Resources.Load<Texture2D>("Mouse/xiaoshou"), Vector2.zero, CursorMode.ForceSoftware);
        //    //设置鼠标贴图/恢复默认贴图
        //}
        //else if (Input.GetMouseButton(2))
        //{
        //    Vector3 Tagetpos = target.transform.localPosition;
        //    Vector3 MousPOS = Input.mousePosition;


        //    if (Input.mousePosition == OldPoint) return;


        //    Vector3 curPosition = (OldPoint - MousPOS) + Camera.main.WorldToScreenPoint(target.transform.localPosition);
        //    Vector3 pos = Camera.main.ScreenToWorldPoint(curPosition);


        //    target.transform.localPosition = new Vector3(Mathf.Clamp(pos.x, 0.4f, 2.3f), Mathf.Clamp(pos.y, 0.6f, 1.84f), Mathf.Clamp(pos.z, -2f, 0));


        //    // target.transform.position = pos;
        //    OldPoint = Input.mousePosition;
        //}
        //else if (Input.GetMouseButtonUp(2))
        //{
        //    //Cursor.SetCursor(Resources.Load<Texture2D>("Mouse/shouzhi"), Vector2.zero, CursorMode.ForceSoftware);
        //}
        //右键目标旋转
        if (Input.GetMouseButtonDown(1))
        {
            XX = Input.mousePosition.x;
            YY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButton(1))
        {
            this.eulerAngles_x += (Input.mousePosition.x - XX) * Time.deltaTime * this.xSpeed;
            this.eulerAngles_x = ClampAngle(this.eulerAngles_x, (float)this.xMinLimit, (float)this.xMaxLimit);
            this.eulerAngles_y -= (Input.mousePosition.y - YY) * Time.deltaTime * this.ySpeed;
            this.eulerAngles_y = ClampAngle(this.eulerAngles_y, (float)this.yMinLimit, (float)this.yMaxLimit);


            XX = Input.mousePosition.x;
            YY = Input.mousePosition.y;
        }

        //限制摄像机与目标物体间的最大与最小距离
        this.distance = Mathf.Clamp(this.distance - (Input.GetAxis("Mouse ScrollWheel") * MouseScrollWheelSensitivity), (float)this.distanceMin, (float)this.distanceMax);


        Quaternion quaternion = Quaternion.Euler(this.eulerAngles_y, this.eulerAngles_x, (float)0);


        //从目标物体处，到当前脚本所依附的对象（主相机）发射一个射线，如果中间有物体阻隔，则更改this.distance（这样做的目的是为了不被挡住）


        RaycastHit hitInfo = new RaycastHit();


        if (Physics.Linecast(this.target.position, this.transform.position, out hitInfo, this.CollisionLayerMask))
        {
            this.distance = hitInfo.distance - 0.05f;
        }


        Vector3 vector = ((Vector3)(quaternion * new Vector3(0, 0, -this.distance))) + this.target.position;


        //更改主相机的旋转角度和位置
        this.transform.rotation = quaternion;
        this.transform.position = vector;
    }




    //把角度限制到给定范围内
    public float ClampAngle(float angle, float min, float max)


    {
        while (angle < -360)
        {
            angle += 360;
        }


        while (angle > 360)
        {
            angle -= 360;
        }


        return Mathf.Clamp(angle, min, max);
    }
}
