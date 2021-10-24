using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///�����̣�3�㣩  (һ�������У����AssetBundle����
///     1.��ȡAB֮�����������ù�ϵ
///     2.����AssetBundle��֮���Զ��������ݹ飩���ػ���
/// </summary>
namespace ABFW
{
    public class MultiABMgr : MonoBehaviour
    {
        //(�²㣩������:������AB������ʵ���ࡱ
        private SingleABLoader _CurrentSingleABLoader;
        //AB��ʵ���໺�漯��(���ã�����AB����ֹ�ظ����أ�����"AB�����漯��"��
        private Dictionary<string, SingleABLoader> _DicSingleABLoaderCache;
        //��ǰ����������ʹ�ã�
        private string _CurrentScenesName;
        //��ǰAssetBundle����
        private string _CurrentABName;
        //AB���������Ӧ������ϵ����
        private Dictionary<string, ABRelation> _DicABRelation;
        //ί�У�����AB�������
        private DeLoadComplete _LoadAllABPackageCompleteHandle;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="scenesName">����������</param>
        /// <param name="abName">AB��������</param>
        /// <param name="loadAllABPackCompleteHandle">(ί��)�Ƿ�������</param>
        public MultiABMgr(string scenesName,string abName,DeLoadComplete loadAllABPackCompleteHandle)
        {
            _CurrentScenesName = scenesName;
            _CurrentABName = abName;
            _DicSingleABLoaderCache = new Dictionary<string, SingleABLoader>();
            _DicABRelation = new Dictionary<string, ABRelation>();
            _LoadAllABPackageCompleteHandle = loadAllABPackCompleteHandle;
        }

        /// <summary>
        /// ���ָ��AB������
        /// </summary>
        /// <param name="abName">AB������</param>
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
            //AB��ϵ�Ľ���
            if (!_DicABRelation.ContainsKey(abName))
            {
                ABRelation aBRelation = new ABRelation(abName);
                _DicABRelation.Add(abName, aBRelation);
            }
            ABRelation tmpABRelationObj = _DicABRelation[abName];
            //�õ�AB�����е�������ϵ
            string[] strDependenceArray = ABManifestLoader.GetInstance().RetrivalDependence(abName);
           
            foreach(string item_Depence in strDependenceArray)
            {
                //���������
                tmpABRelationObj.AddDependence(item_Depence);
                //���������(�ݹ����)
                yield return LoadReference(item_Depence, abName);

            }
            //��������AB��
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
        /// ��������AB��
        /// </summary>
        /// <param name="abName">ab������</param>
        /// <param name="refABName">������AB������</param>
        /// <returns></returns>
        private IEnumerator LoadReference(string abName,string refABName)
        {
            //AB���Ѿ�����
            if (_DicABRelation.ContainsKey(abName))
            {
                ABRelation tmpAbRelationObj = _DicABRelation[abName];
                //���AB�����ù�ϵ
                tmpAbRelationObj.AddReference(refABName);
            }
            //AB��û�б�����
            else
            {
                ABRelation tmpAbRelationObj = new ABRelation(abName);
                _DicABRelation.Add(abName,tmpAbRelationObj);
                //��ʼ���������İ�������һ���ݹ���ã�
                yield return LoadAssetBundle(abName);

            }
            yield return null;
        }
    }
}

