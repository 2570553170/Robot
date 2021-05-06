using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    /// <summary>
    /// 用于存储点表文件中读取出来的位置点信息
    /// </summary>
    public Dictionary<string, CsvPoint> cps = new Dictionary<string, CsvPoint>();
    /// <summary>
    /// 用于存储点表文件中读取出来的指令信息
    /// </summary>
    public List<Command> commands = new List<Command>();

    /// <summary>
    /// 批量生成TaskCell到场景中，并进行初始化赋值
    /// </summary>
    public void GenerateTaskCell() {
        for (int i = 0; i < commands.Count; i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("UI/TaskCell"), GameObject.Find("TaskShowPanel").transform);
            if (commands[i].commandType == CommandType.Delay)
            {
                WaitingStep step = go.AddComponent<WaitingStep>();
                step.delayTime = float.Parse(commands[i].Param)/1000f;
                go.transform.Find("Text").GetComponent<Text>().text = "待机指令";
            }
            else 
            {
                MovingStep step = go.AddComponent<MovingStep>();
                JointsMessage jm = new JointsMessage();
                jm.IKPoint = cps[commands[i].Param].point * 0.001f;
                string key = string.Format("{0:d3}", (StepManager.Instance.SavedPointMessage.Count + 1));
                StepManager.Instance.SavedPointMessage.Add(key, jm);
                MechanicalControl.Instance.ShowTag(jm.IKPoint, key);

                step.targetPoint.IKPoint = jm.IKPoint;
                go.transform.Find("Text").GetComponent<Text>().text = "移动指令";
            }
            
            StepManager.Instance.Steps.Add(go);
        }

    }
    /// <summary>
    /// 选择指令文件，并进行每行解析
    /// </summary>
    public void GetCommand() {
        string path = SaveDialog.SelectPath();
        
        StreamReader sr = new StreamReader(path, Encoding.Default);
        while (true)
        {
            string fileDataLine;            
            fileDataLine = sr.ReadLine();
            if (fileDataLine == null)
            {
                break;
            }
            ReadCommand(fileDataLine);
            
        }
        sr.Close();


    }
    /// <summary>
    /// 解读每一行指令
    /// </summary>
    /// <param name="command">字符串格式的指令</param>
    private void ReadCommand(string command) {
        command.Replace(" ", "");
        if (command ==null&&command == "")
        {
            return;
        }
        Command cmd = new Command();
        if (command.Contains("Delay"))
        {
            cmd.commandType = CommandType.Delay;
            
            
        }
        else if (command.Contains("MoveY"))
        {
            cmd.commandType = CommandType.MoveY;
            
        }
        else if (command.Contains("MoveC"))
        {
            cmd.commandType = CommandType.MoveC;
            
        }
        else if (command.Contains("MoveP"))
        {
            cmd.commandType = CommandType.MoveP;
            
        }
        else
        {
            return;
        }
        cmd.Param = SubMyString(command);
        commands.Add(cmd);
        

    }

    //public void ShowWarningMessage(string message) 
    //{
    //    GameObject go = GameObject.Find("UIRoot").transform.Find("WarningMessagePanel").gameObject;
    //    if (go ==null)
    //    {
    //        go = Instantiate(Resources.Load("UI/WarningMessagePanel") as GameObject, GameObject.Find("UIRoot").transform,false);
    //    }
    //    go.transform.Find("Text").GetComponent<Text>().text = message;


    //}
    /// <summary>
    /// 字符串处理，截取一行指令，括号中的字符串，即为指令的参数（时间或者位置点名字）
    /// </summary>
    /// <param name="str">指令行</param>
    /// <returns>括号中的字符串</returns>
    private string SubMyString(string str) {
        
        int arr = str.IndexOf(")") - 1 - str.IndexOf("(");//结束位置 减 1 再减 开始位置 获取中间位置数
        String str2 = str.Substring(str.IndexOf("(") + 1, arr);//参数1：开始位置加1 参数2：长度：中间位置数
        return str2;

    }
    /// <summary>
    /// 打开路径选择界面，选择路径，将csv点表中的信息存储在cps中
    /// </summary>
    public void GetCps()
    {
        string path = SaveDialog.SelectPath();
        cps = GetPoint(path);
        Debug.Log(cps["P1"].name);
    }
    /// <summary>
    /// 从硬盘中读取.CSV文件,并进行解析，数据存入CSVPoint类中
    /// </summary>
    /// <param name="path">csv文件所在路径（完整路径）</param>
    /// <returns>字典 key为点的名字，值为存有数据的CSVPoint类</returns>
    private Dictionary<string, CsvPoint> GetPoint(string path)
    {
        CsvStreamReader csv = new CsvStreamReader(path);
        Dictionary<string, CsvPoint> csvDic = new Dictionary<string, CsvPoint>();
        for (int i = 1; i < csv.csvDT.Rows.Count; i++)
        {
            if (csv.csvDT.Rows[i][3].ToString() != "")
            {
                CsvPoint cp = new CsvPoint();
                cp.name = csv.csvDT.Rows[i][0].ToString();
                cp.MoveType = csv.csvDT.Rows[i][1].ToString();
                cp.point = new Vector3(float.Parse(csv.csvDT.Rows[i][2].ToString()), float.Parse(csv.csvDT.Rows[i][3].ToString()), float.Parse(csv.csvDT.Rows[i][4].ToString()));
                cp.C = float.Parse(csv.csvDT.Rows[i][5].ToString());

                csvDic.Add(cp.name, cp);
            }
            else
            {
                break;
            }
        }
        return csvDic;
    }
}
/// <summary>
/// 读取进来的位置点数据格式类
/// </summary>
public class CsvPoint
{
    public string name;
    public string MoveType;
    public Vector3 point;
    public float C;


}
/// <summary>
/// 读取进来的指令的数据格式类
/// </summary>
public class Command
{
    
    public CommandType commandType;
    /// <summary>
    /// 指令的相关参数 Delay指令参数为等待时间，运动指令参数为运动目标点
    /// </summary>
    public string Param;



}
/// <summary>
/// 指令类型
/// </summary>
public enum CommandType
{
    Delay,
    MoveY,
    MoveC,
    MoveP
}
