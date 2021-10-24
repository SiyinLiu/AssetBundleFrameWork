
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    /// <summary>
    /// AssetBundle��ϵ��
    ///     �洢ָ����AB��������������ϵ��
    ///     �洢ָ����AB�����е����ù�ϵ��
    /// </summary>
    public class ABRelation
    {
        //��ǰAssetBundle����
        private string _ABName;
        //�������е�����������
        private List<string> _ListAllDependenceAB;
        //�������е����ð�����
        private List<string> _ListAllReferenceAB;

        public ABRelation(string abName) {
            if (!string.IsNullOrEmpty(abName))
            {
                _ABName = abName;
            }
            _ListAllDependenceAB = new List<string>();
            _ListAllReferenceAB = new List<string>();
        }

        public void AddDependence(string abName)
        {
            if (!_ListAllReferenceAB.Contains(abName))
            {
                _ListAllDependenceAB.Add(abName);
            }
        }
        /// <summary>
        /// �Ƴ�������ϵ
        /// </summary>
        /// <param name="abName">�Ƴ��İ�����</param>
        /// <returns>
        ///     true:   ��AssetBundleû��������
        ///     false:  ��AssetBundleû������������
        /// </returns>
        public bool RemoveDependence(string abName)
        {
            if (_ListAllDependenceAB.Contains(abName))
            {
                _ListAllDependenceAB.Remove(abName);
            }
            if(_ListAllDependenceAB.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDependence()
        {
            return _ListAllDependenceAB;
        }
        /// <summary>
        /// ���һ�����ð�
        /// </summary>
        /// <param name="abName">������</param>
        public void AddReference(string abName)
        {
            if (!_ListAllReferenceAB.Contains(abName))
            {
                _ListAllReferenceAB.Add(abName);
            }   
        }
        /// <summary>
        /// ɾ��һ�����ð�
        /// </summary>
        /// <param name="abName">���ð�����</param>
        /// <returns>
        ///     true:   ��AssetBundleû��������
        ///     false:  ��AssetBundleû������������
        /// </returns>
        public bool RemoveReference(string abName)
        {
            if (_ListAllReferenceAB.Contains(abName))
            {
                _ListAllReferenceAB.Remove(abName);
            }
            if(_ListAllReferenceAB.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllReference()
        {
            return _ListAllReferenceAB;
        }
    }
}

