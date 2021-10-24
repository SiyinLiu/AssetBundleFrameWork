
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    /// <summary>
    /// AssetBundle关系类
    ///     存储指定的AB包的所有依赖关系包
    ///     存储指定的AB包所有的引用关系包
    /// </summary>
    public class ABRelation
    {
        //当前AssetBundle名称
        private string _ABName;
        //本包所有的依赖包集合
        private List<string> _ListAllDependenceAB;
        //本包所有的引用包集合
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
        /// 移除依赖关系
        /// </summary>
        /// <param name="abName">移除的包名称</param>
        /// <returns>
        ///     true:   此AssetBundle没有依赖项
        ///     false:  此AssetBundle没有其他依赖项
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
        /// 获取所有引用项
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDependence()
        {
            return _ListAllDependenceAB;
        }
        /// <summary>
        /// 添加一个引用包
        /// </summary>
        /// <param name="abName">包名称</param>
        public void AddReference(string abName)
        {
            if (!_ListAllReferenceAB.Contains(abName))
            {
                _ListAllReferenceAB.Add(abName);
            }   
        }
        /// <summary>
        /// 删除一个引用包
        /// </summary>
        /// <param name="abName">引用包名称</param>
        /// <returns>
        ///     true:   此AssetBundle没有引用项
        ///     false:  此AssetBundle没有其他引用项
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
        /// 获取所有引用
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllReference()
        {
            return _ListAllReferenceAB;
        }
    }
}

