/*************************************************************************
 *  File                        :  SingleABLoader.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-21
 *  Description        :  
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class SingleABLoader : System.IDisposable
    {
        //�����ࣺ��Դ������
        private AssetLoader _AssetLoader;
        //ί��
        private DeLoadComplete _LoadCompleteHandler;
        //AssetBundle ����
        private string _ABName;
        //AssetBundle ����·��
        private string _ABDownLoadPath;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="abName"></param>
        public SingleABLoader(string abName,DeLoadComplete loadComplete)
        {
            _ABName = abName;
            _ABDownLoadPath = PathTool.GetWWWPath() + "/" + abName;
            _LoadCompleteHandler = loadComplete;
        }
        /// <summary>
        /// ʹ��WWW����AssetBundle��Դ��
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadAssetBundle()
        {
            using(WWW www = new WWW(_ABDownLoadPath))
            {
                //����
                yield return www;
                //WWW����AB�����
                if(www.progress >= 1)
                {
                    //��ȡAssetBundle��ʵ��
                    AssetBundle abObj = www.assetBundle;
                    if(abObj != null)
                    {
                        //ʵ����������
                        _AssetLoader = new AssetLoader(abObj);
                        //AssetBundle ������ϣ�����ί��
                       if (_LoadCompleteHandler != null)
                        {
                            _LoadCompleteHandler(_ABName);
                        }
                    }
                    else
                    {
                        Debug.LogError(GetType() + "/LoadAssetBundle()/WWW ���س������飡 AssetBundle URL:" + _ABDownLoadPath + " ������Ϣ��" + www.error);
                    }
                }
            }//using end
        }
        /// <summary>
        /// ����AB���ڵ���Դ
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string assetName,bool isCache)
        {
            if(_AssetLoader != null)
            {
                return _AssetLoader.LoadAsset(assetName, isCache);
            }
            Debug.LogError(GetType() + "/LoadAsset()/���� _AssetLoader==null����! ");
            return null;
        }

        /// <summary>
        /// ж�أ�AB������Դ��
        /// </summary>
        public void UnLoadAsset(UnityEngine.Object asset)
        {
            if(_AssetLoader != null)
            {
                _AssetLoader.UnLoadAsset(asset);
            }else
            {
                Debug.LogError(GetType() + "/UnLoadAsset()/����_AssetLoader == null, ���飡");
            }
        }
        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {
            if (_AssetLoader != null)
            {
                _AssetLoader.Dispose();
                _AssetLoader = null;
            }
            else
            {
                Debug.LogError(GetType() + "/UnLoadAsset()/����_AssetLoader == null, ���飡");
            }
        }
        /// <summary>
        /// �ͷŵ�ǰAssetBundle��Դ������ж��������Դ
        /// </summary>
        public void DisposeAll()
        {
            if (_AssetLoader != null)
            {
                _AssetLoader.DisposeAll();
                _AssetLoader = null;
            }
            else
            {
                Debug.LogError(GetType() + "/UnLoadAsset()/����_AssetLoader == null, ���飡");
            }
        }

        //��ѯ��ǰAssetBundle�������е���Դ
        public string[] RetrivalAllAssetName()
        {
            if(_AssetLoader != null)
            {
                return _AssetLoader.RetriveAllAssetName();
            }
            Debug.LogError(GetType() + "/RetrivalAllAssetName()/����_AssetLoader == null, ���飡");
            return null;
        }
    }
}

