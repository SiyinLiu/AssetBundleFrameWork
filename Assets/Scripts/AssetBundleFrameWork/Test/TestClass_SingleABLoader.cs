/*************************************************************************
 *  File                        :  TestClass_SingleABLoader.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-21
 *  Description        :  框架内部验证测试
 *                                专门验证"SingleABLoader"类  
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class TestClass_SingleABLoader : MonoBehaviour
    {
        private SingleABLoader m_LoadObj;
        private string m_ABDependName1 = "scence_1/textures.ab";
        private string m_ABDependName2 = "scence_1/materials.ab";
        private string m_ABName1 = "scence_1/prefabs.ab";
        private string m_AssetName1 = "TestCubePrefab.prefab";
        public void Start()
        {
            SingleABLoader m_LoadDependObj1 = new SingleABLoader(m_ABDependName1, LoadDependComplete1);
            StartCoroutine(m_LoadDependObj1.LoadAssetBundle());
        }

        private void LoadDependComplete1(string abName)
        {
            Debug.Log("依赖包1(贴图包)加载完毕，加载依赖包2(材质包)");
            SingleABLoader m_LoadDependObj2 = new SingleABLoader(m_ABDependName2, LoadDependComplete2);
            StartCoroutine(m_LoadDependObj2.LoadAssetBundle());
        }
        private void LoadDependComplete2(string abName)
        {
            Debug.Log("依赖包2（材质包）加载完毕，开始正式加载预设包");
            m_LoadObj = new SingleABLoader(m_ABName1, LoadComplete);
            StartCoroutine(m_LoadObj.LoadAssetBundle());
        }
        private void LoadComplete(string abName)
        {
            Object tmpObj = m_LoadObj.LoadAsset(m_AssetName1, false);
            Instantiate(tmpObj);
            string[] strArray = m_LoadObj.RetrivalAllAssetName();
            foreach(string str in strArray)
            {
                Debug.Log(str);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("释放静态内存资源");
                //m_LoadObj.Dispose();
                m_LoadObj.DisposeAll();
            }
        }
    }
}

