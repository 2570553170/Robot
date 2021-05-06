using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 查找场景中的物体，常用到的可以存储在这边，方便之后调用，减少性能消耗
/// </summary>
public class GetGoInScene : MonoBehaviour
{
    public Dictionary<string,GameObject> sceneGOs;

    Transform pointTag;
    
    private static GetGoInScene instance;
    private GetGoInScene()
    {
    }
    public static GetGoInScene GetInstance()
    {
        if (instance == null)
        {
            instance = new GetGoInScene();
        }
        return instance;
    }

   

    public Dictionary<string, GameObject> SceneGOs
    {
        get
        {
            if (sceneGOs  == null)
            {
                sceneGOs = new Dictionary<string, GameObject>();
            }
            return sceneGOs;
        } 
        set => sceneGOs = value; }

    public Transform PointTag {
        get
        {
            if (pointTag == null)
            {
                pointTag = GameObject.Find("PointTag").transform;
            }
            return pointTag;
        }
        set => pointTag = value; }

    GameObject go;
    public GameObject GetGO(string name) 
    {
        if (SceneGOs.ContainsKey(name))
        {
            return SceneGOs[name];
        }
        else
        {

            //GameObject go = Instantiate(Resources.Load<GameObject>("UI/MoveSettingPanel"), GameObject.Find("UIRoot").transform);
            GameObject go = GameObject.Find("UIRoot").transform.Find(name).gameObject;
            SceneGOs.Add(name, go);
            return go;
        }
    
    
    }
}
