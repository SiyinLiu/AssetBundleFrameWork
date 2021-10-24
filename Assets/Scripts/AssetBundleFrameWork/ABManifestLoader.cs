/*************************************************************************
 *  File                        :  ABManifestLoader.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-21
 *  Description        :  �����ࣺ��ȡAssetBundle�����ļ�
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class ABManifestLoader : System.IDisposable
    {
        //����ʵ��
        private static ABManifestLoader _Instance;
        //AssetBundle(�嵥�ļ���ϵͳ��
        private AssetBundleManifest _ManifestObj;
        //AssetBundle �嵥�ļ���·��
        private string _StrManifestPath;
        //��ȡAB�嵥�ļ���AssetBundle��
        private AssetBundle _ABReadMainfest;
        //�嵥�ļ��Ƿ���أ�Mainfest)��ɡ�
        private bool _IsLoadFinish;

        public bool IsLoadFinish
        {
            get { return _IsLoadFinish; }
        }

        private ABManifestLoader()
        {
            //ȷ���嵥�ļ�WWW����·��������
            _StrManifestPath = PathTool.GetWWWPath() + "/" + PathTool.GetPlatfromPath();
            _ManifestObj = null;
            _ABReadMainfest = null;
            _IsLoadFinish = false;
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public static ABManifestLoader GetInstance() { 
            if(_Instance == null)
            {
                _Instance = new ABManifestLoader();
            }
            return _Instance;
        }

        /// <summary>
        /// ����Manifest�嵥�ļ�
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadMainfest()
        {
            using(WWW www = new WWW(_StrManifestPath))
            {
                yield return www;
                if(www.progress >= 1)
                {
                    AssetBundle abObj = www.assetBundle;
                    if(abObj != null)
                    {
                        _ABReadMainfest = abObj;
                        //��ȡ�嵥�ļ���Դ������ȡ��ϵͳ���ʵ����)
                        _ManifestObj = _ABReadMainfest.LoadAsset(ABDefine.ASSETBUNDLE_MANIFEST) as AssetBundleManifest;//AssetBundleManifest�ǹ̶�����
                        //���μ������ȡ�嵥�ļ����
                        _IsLoadFinish = true;
                    }
                    else
                    {
                        Debug.LogError(GetType() + "/LoadManifestFile()/WWW���س�������!,_StrManifestPath="+_StrManifestPath 
                            + " ������Ϣ��"+ www.error);
                    }
                }
            }
        }
        /// <summary>
        /// ��ȡAssetBundleManifestϵͳ��
        /// </summary>
        /// <returns></returns>
        public AssetBundleManifest GetABManifest()
        {
            if (_IsLoadFinish)
            {
                if(_ManifestObj != null)
                {
                    return _ManifestObj;
                }
                else
                {
                    Debug.LogError(GetType() + "/GetABManifest()/_ManifestObj==null ���飡");
                }
            }
            else
            {
                Debug.Log(GetType() + "/GetABManifest()/_IsLoadFinish== false,Manifestû�м������ ���飡");
            }
            return null;
        }

        /// <summary>
        /// ��ȡAssetBundleManifest��ϵͳ�ࣩ��ָ������������������
        /// </summary>
        /// <param name="abName">AB������</param>
        /// <returns></returns>
        public string[] RetrivalDependence(string abName) {
            if(_ManifestObj != null && !string.IsNullOrEmpty(abName))
            {
                return _ManifestObj.GetAllDependencies(abName);
            }
            return null;
        }

        /// <summary>
        /// �ͷű�����Դ
        /// </summary>
        public void Dispose()
        {
            if(_ABReadMainfest != null)
            {
                _ABReadMainfest.Unload(true);
            }
        }
    }
}

