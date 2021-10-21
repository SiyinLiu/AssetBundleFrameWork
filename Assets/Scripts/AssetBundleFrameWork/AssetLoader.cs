/*************************************************************************
 *  File                        :  AssetLoader.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-20
 *  Description        :  ��������̣���һ�㣺AB��Դ������
 *      ���ܣ�
 *      1�����������ָ��AB����Դ
 *      2�����ؾ���"���湦��"����Դ����ѡ�ò���
 *      3��ж�ء��ͷ�AB��Դ
 *      4���鿴��ǰAB��Դ
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABFW
{
    public class AssetLoader : System.IDisposable
    {
        //��ǰAssetBundle
        private AssetBundle _CurrentAssetBundle;
        //������������
        private Hashtable _Ht;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="abObj">��WWW���ص�AssetBundleʵ��</param>
        public AssetLoader(AssetBundle abObj)
        {
            if(abObj != null)
            {
                _CurrentAssetBundle = abObj;
                _Ht = new Hashtable();
            }
            else
            {
                Debug.LogError(GetType() + "/ ���캯�� AssetBundle()/ ����abObj == null !����");
            }
        }
        /// <summary>
        /// ���ص�ǰ����ָ������Դ
        /// </summary>
        /// <param name="assetName">��Դ������</param>
        /// <param name="isCache">�Ƿ�������</param>
        /// <returns></returns>
        public Object LoadAsset(string assetName, bool isCache = false)
        {
            return LoadResource<Object>(assetName, isCache);
        }

        /// <summary>
        /// ���ص�ǰAB������Դ��������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName">��Դ������</param>
        /// <param name="isCache">�Ƿ�������</param>
        /// <returns></returns>
        private T LoadResource<T>(string assetName, bool isCache) where T : UnityEngine.Object
        {
            if (_Ht.Contains(assetName))
            {
                return _Ht[assetName] as T;
            }
            //��ʽ����
            T tmpTResource =  _CurrentAssetBundle.LoadAsset<T>(assetName);
            if(tmpTResource != null && isCache)
            {
                _Ht.Add(assetName, tmpTResource);
            }
            else if(tmpTResource == null)
            {
                Debug.LogError(GetType() + "/LoadResource<T>()/���� tmpTResource==null, ���飡");
            }
            return tmpTResource;
        }
        /// <summary>
        /// ж��ָ������Դ
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public bool UnLoadAsset(UnityEngine.Object asset)
        {
            if(asset != null)
            {
                Resources.UnloadAsset(asset);
            }
            Debug.LogError(GetType() + "/UnLoadAsset()/���� asset == null, ����");
            return false;
        }
        /// <summary>
        /// �ͷŵ�ǰAssetBundle�ڴ澵����Դ
        /// </summary>
        public void Dispose()
        {
            //�����ͷ�AssetBundle�ڴ澵����Դ
            _CurrentAssetBundle.Unload(false);
        }
        /// <summary>
        /// �ͷŵ�ǰAssetBundle�ڴ���Դ�����ͷ��ڴ���Դ
        /// </summary>
        public void DisposeAll()
        {
            _CurrentAssetBundle.Unload(true);
        }

        /// <summary>
        /// ��ѯ��ǰAssetBundle������������Դ����
        /// </summary>
        /// <returns></returns>
        public string[] RetriveAllAssetName()
        {
            return _CurrentAssetBundle.GetAllAssetNames();
        }
    }
}

