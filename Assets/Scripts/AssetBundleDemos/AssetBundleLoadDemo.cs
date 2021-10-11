/*************************************************************************
 *  File                        :  AssetBundleLoadDemo.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-11
 *  Description        :  
 *                              ������ʾAssetBundle�������ط�Ϊ����
 *                              1�����طǡ�����Ԥ�衱��ʽ����������ͼ�����ʡ���Ƶ...)
 *                              2:  ����"����Ԥ��"��*.prefab��
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleLoadDemo : MonoBehaviour
{
    //���Զ��󣬸ı���ͼ
    public GameObject goCubeChangeTextur;
    //����URL����Դ����
    private string _URL1;
    private string _AssetName1;
    public Transform TraShowPos;
    private string _URL2;
    private string _AssetName2;

    private void Awake()
    {
        //AB�����ص�ַ
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
    /// ����"��GameObject"��Դ
    /// </summary>
    /// <param name="ABURL">AB��URL</param>
    /// <param name="goShowObj">��������ʾ�Ķ���</param>
    /// <param name="assetName">������Դ������</param>
    /// <returns></returns>
    IEnumerator LoadNonObjectFromAB(string ABURL,GameObject goShowObj,string assetName)
    {
        if(string.IsNullOrEmpty(ABURL) || goShowObj == null)
        {
            Debug.LogError(GetType() + "/LoadNonObjectFromAB()/����Ĳ������Ϸ�����");
        }
        using(WWW www = new WWW(ABURL))
        {
            yield return www;
            //����AB��
            AssetBundle ab = www.assetBundle;
            if (ab != null)
            {
                if(assetName == "")
                {
                    goShowObj.GetComponent<Renderer>().material.mainTexture = ab.mainAsset as Texture;
                }
                else
                {
                    //����AB��
                    goShowObj.GetComponent<Renderer>().material.mainTexture = ab.LoadAsset(assetName) as Texture;
                }
                //ж����Դ��ֻж��AB������
                ab.Unload(false);
            }
            else
            {
                Debug.LogError(GetType() + "/LoadNonObjectFromAB()/WWW ���ش������� URL�� " + ABURL + "������Ϣ:" + www.error);
            }
        }
    }
    /// <summary>
    /// ����Ԥ����Դ
    /// </summary>
    /// <param name="ABURL">AB��URL</param>
    /// <param name="assetName">������Դ����</param>
    /// <returns></returns>
    IEnumerator LoadPrefabFromAB(string ABURL,string assetName,Transform showPosition)
    {
        if (string.IsNullOrEmpty(ABURL))
        {
            Debug.LogError(GetType() + "/LoadNonObjectFromAB()/����Ĳ������Ϸ�����");
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
                        //��¡�Ķ�����ʾλ��
                        goCloneObj.transform.position = showPosition.position;
                    }
                    else
                    {
                        //��¡���ص�Ԥ�����
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
                        //��¡���ص�Ԥ�����
                        Instantiate(ab.LoadAsset(assetName));
                    }
                    
                }
                //ж����Դ��ֻж��AB������
                ab.Unload(false);
            }
            else
            {
                Debug.LogError(GetType() + "/LoadNonObjectFromAB()/WWW ���ش������� URL�� " + ABURL + "������Ϣ:" + www.error);
            }
        }
    }
}
