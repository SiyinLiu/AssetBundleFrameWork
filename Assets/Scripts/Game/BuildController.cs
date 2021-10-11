/*************************************************************************
 *  File                        :  BuildController.cs
 *  Author                 :  DavidLiu
 *  Date                     :  2020-11-06
 *  Description        :  
 *************************************************************************/
using System.Collections;
using UnityEngine;
public enum BuildType
{
    //木头
    WOOD,
    //石头
    STONE,
    //炸弹
    BOMB
}
/// <summary>
/// 建筑控制器
/// </summary>
public class BuildController : MonoBehaviour
{
    [Header("建筑类型")]
    public BuildType buildType;
    [Header("分数")]
    [SerializeField]
    protected int score = 1;
    /// <summary>
    /// 爆炸粒子
    /// </summary>
    [SerializeField]
    protected float explosionSize= 0.3f;
    [Header("是否可以被推动")]
    public bool enablePush = false;
    [SerializeField]
    protected BuildColliderController buildCollider = null;
    protected Vector3 initPosition;
    [HideInInspector]
    public bool isActive;

    public void Awake()
    {
        if(buildCollider == null)
        {
            Debug.LogWarning("build name=" + gameObject.name);
        }
        else
        {
            buildCollider.buildController = this;
        }
        initPosition = buildCollider.transform.position;
        isActive = true;
    }
    public int Score
    {
        get { return score; }
    }
}
