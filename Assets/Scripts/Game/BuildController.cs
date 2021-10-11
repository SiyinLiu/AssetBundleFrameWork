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
    //ľͷ
    WOOD,
    //ʯͷ
    STONE,
    //ը��
    BOMB
}
/// <summary>
/// ����������
/// </summary>
public class BuildController : MonoBehaviour
{
    [Header("��������")]
    public BuildType buildType;
    [Header("����")]
    [SerializeField]
    protected int score = 1;
    /// <summary>
    /// ��ը����
    /// </summary>
    [SerializeField]
    protected float explosionSize= 0.3f;
    [Header("�Ƿ���Ա��ƶ�")]
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
