/*************************************************************************
 *  File                        :  BuildCollider.cs
 *  Author                 :  DavidLiu
 *  Date                     :  2020-11-06
 *  Description        :  
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 场景内建筑的碰撞器脚本
/// </summary>
[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class BuildColliderController : MonoBehaviour
{
    /// <summary>
    /// 建筑
    /// </summary>
    [HideInInspector]
    public BuildController buildController = null;
    public BoxCollider[] boxColliders = null;
    public Rigidbody m_rigibody = null;
    [HideInInspector]
    public float volume;
    public void Awake()
    {
        foreach(BoxCollider boxCollider in boxColliders)
        {
            Vector3 _size = boxCollider.size;
            volume += _size.x * _size.y * _size.z;
        }
    }
}
