/*************************************************************************
 *  File                        :  AssetBundleLoadDemo.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-11
 *  Description        :  
 *                              功能演示AssetBundle基本加载分为两种
 *                              1：加载非“对象预设”方式。（例如贴图、材质、音频...)
 *                              2:  加载"对象预设"（*.prefab）
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleLoadDemo : MonoBehaviour
{
    //测试对象，改变贴图
    public GameObject goCubeChangeTextur;
    //定义URL与资源名称
    private string _URL1;
    private string _AssetName1;
    public Transform TraShowPos;
    private string _URL2;
    private string _AssetName2;

    private void Awake()
    {
        //AB包下载地址
        //_URL1 = "file://" + Application.streamingAssetsPath + "/texture1";
        //_AssetName1 = "conga2";
        _AssetName1 = "mouse_background";
        _URL2 = "file://" + Application.streamingAssetsPath + "/prefabs1"; 
        _AssetName2 = "Floor";
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadNonObjectFromAB(_URL1, goCubeChangeTextur, _AssetName1));
        StartCoroutine(LoadPrefabFromAB(_URL2, _AssetName2, TraShowPos));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 加载"非GameObject"资源
    /// </summary>
    /// <param name="ABURL">AB包URL</param>
    /// <param name="goShowObj">操作且显示的对象</param>
    /// <param name="assetName">加载资源的名声</param>
    /// <returns></returns>
    IEnumerator LoadNonObjectFromAB(string ABURL,GameObject goShowObj,string assetName)
    {
        if(string.IsNullOrEmpty(ABURL) || goShowObj == null)
        {
            Debug.LogError(GetType() + "/LoadNonObjectFromAB()/输入的参数不合法请检查");
        }
        using(WWW www = new WWW(ABURL))
        {
            yield return www;
            //下载AB包
            AssetBundle ab = www.assetBundle;
            if (ab != null)
            {
                if(assetName == "")
                {
                    goShowObj.GetComponent<Renderer>().material.mainTexture = ab.mainAsset as Texture;
                }
                else
                {
                    //加载AB包
                    goShowObj.GetComponent<Renderer>().material.mainTexture = ab.LoadAsset(assetName) as Texture;
                }
                //卸载资源（只卸载AB包本身）
                ab.Unload(false);
            }
            else
            {
                Debug.LogError(GetType() + "/LoadNonObjectFromAB()/WWW 下载错误，请检查 URL： " + ABURL + "错误信息:" + www.error);
            }
        }
    }
    /// <summary>
    /// 加载预设资源
    /// </summary>
    /// <param name="ABURL">AB包URL</param>
    /// <param name="assetName">加载资源名称</param>
    /// <returns></returns>
    IEnumerator LoadPrefabFromAB(string ABURL,string assetName,Transform showPosition)
    {
        if (string.IsNullOrEmpty(ABURL))
        {
            Debug.LogError(GetType() + "/LoadNonObjectFromAB()/输入的参数不合法请检查");
        }
        using(WWW www = new WWW(ABURL))
        {
            yield return www;
            AssetBundle ab = www.assetBundle;
            if(ab != null)
            {
                if(assetName == "")
                {
                    if(showPosition != null)
                    {
                        GameObject goCloneObj = (GameObject)Instantiate(ab.mainAsset);
                        //克隆的对象显示位置
                        goCloneObj.transform.position = showPosition.position;
                    }
                    else
                    {
                        //克隆加载的预设对象
                        Instantiate(ab.mainAsset);
                    }
                }
                else
                {
                    if (showPosition != null)
                    {
                        GameObject goCloneObj = (GameObject)Instantiate(ab.LoadAsset(assetName));
                        goCloneObj.transform.position = showPosition.position;
                    }
                    else
                    {
                        //克隆加载的预设对象
                        Instantiate(ab.LoadAsset(assetName));
                    }
                    
                }
                //卸载资源（只卸载AB包本身）
                ab.Unload(false);
            }
            else
            {
                Debug.LogError(GetType() + "/LoadNonObjectFromAB()/WWW 下载错误，请检查 URL： " + ABURL + "错误信息:" + www.error);
            }
        }
    }
}
