using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///主流程（3层）  (一个场景中）多个AssetBundle管理。
///     1.获取AB之间依赖与引用关系
///     2.管理AssetBundle包之间自动连锁（递归）加载机制
/// </summary>
namespace ABFW
{
    public class MultiABMgr : MonoBehaviour
    {
        //(下层）引用类:“单个AB包加载实现类”
        private SingleABLoader _CurrentSingleABLoader;
        //AB包实现类缓存集合(作用：缓存AB包防止重复加载，即，"AB包缓存集合"）
        private Dictionary<string, SingleABLoader> _DicSingleABLoaderCache;
        //当前场景（调试使用）
        private string _CurrentScenesName;
        //当前AssetBundle名称
        private string _CurrentABName;
        //AB包名称与对应依赖关系集合
        private Dictionary<string, ABRelation> _DicABRelation;
        //委托：所有AB加载完成
        private DeLoadComplete _LoadAllABPackageCompleteHandle;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scenesName">场景的名称</param>
        /// <param name="abName">AB包的名称</param>
        /// <param name="loadAllABPackCompleteHandle">(委托)是否调用完成</param>
        public MultiABMgr(string scenesName,string abName,DeLoadComplete loadAllABPackCompleteHandle)
        {
            _CurrentScenesName = scenesName;
            _CurrentABName = abName;
            _DicSingleABLoaderCache = new Dictionary<string, SingleABLoader>();
            _DicABRelation = new Dictionary<string, ABRelation>();
            _LoadAllABPackageCompleteHandle = loadAllABPackCompleteHandle;
        }

        /// <summary>
        /// 完成指定AB包调用
        /// </summary>
        /// <param name="abName">AB包名称</param>
        private void CompleteLoadAB(string abName)
        {
            if (abName.Equals(_CurrentABName))
            {
                if(_LoadAllABPackageCompleteHandle != null)
                {
                    _LoadAllABPackageCompleteHandle(abName);
                }
            }
        }

        public IEnumerator LoadAssetBundle(string abName)
        {
            //AB关系的建立
            if (!_DicABRelation.ContainsKey(abName))
            {
                ABRelation aBRelation = new ABRelation(abName);
                _DicABRelation.Add(abName, aBRelation);
            }
            ABRelation tmpABRelationObj = _DicABRelation[abName];
            //得到AB包所有的依赖关系
            string[] strDependenceArray = ABManifestLoader.GetInstance().RetrivalDependence(abName);
           
            foreach(string item_Depence in strDependenceArray)
            {
                //添加依赖项
                tmpABRelationObj.AddDependence(item_Depence);
                //添加引用项(递归调用)
                yield return LoadReference(item_Depence, abName);

            }
            //真正加载AB包
            if (_DicSingleABLoaderCache.ContainsKey(abName))
            {
                yield return _DicSingleABLoaderCache[abName].LoadAssetBundle();
            }
            else
            {
                _CurrentSingleABLoader = new SingleABLoader(abName, CompleteLoadAB);
                _DicSingleABLoaderCache.Add(abName,_CurrentSingleABLoader);
                yield return _CurrentSingleABLoader.LoadAssetBundle(); 
            }
        }

        /// <summary>
        /// 加载引用AB包
        /// </summary>
        /// <param name="abName">ab包名称</param>
        /// <param name="refABName">被引用AB包名称</param>
        /// <returns></returns>
        private IEnumerator LoadReference(string abName,string refABName)
        {
            //AB包已经加载
            if (_DicABRelation.ContainsKey(abName))
            {
                ABRelation tmpAbRelationObj = _DicABRelation[abName];
                //添加AB包引用关系
                tmpAbRelationObj.AddReference(refABName);
            }
            //AB包没有被加载
            else
            {
                ABRelation tmpAbRelationObj = new ABRelation(abName);
                _DicABRelation.Add(abName,tmpAbRelationObj);
                //开始加载依赖的包（这是一个递归调用）
                yield return LoadAssetBundle(abName);

            }
            yield return null;
        }
    }
}

