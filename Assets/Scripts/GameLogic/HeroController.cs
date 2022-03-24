using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UIFrame;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HeroController : MonoBehaviourPunCallbacks
{
    [Header("攻击范围")]
    public float attackRange = 2f;

    [Header("转身速度")] 
    public float turnSpeed = 10f;

    [Header("移动速度")] 
    public float moveSpeed = 3.5f;
    
    private RaycastHit hit;
    private NavMeshAgent nav;
    private Animator ani;

    private PhotonView targetHero;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        //设置制动距离
        nav.stoppingDistance = attackRange;
        nav.speed = moveSpeed;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (photonView.Owner == null)
        {
            return;
            
        }
       
        CheckArrive(); 
        HeroInput();
        CheckTargetHeroInRange();
    }

    /// <summary>
    /// 英雄输入操作检测
    /// </summary>
    private void HeroInput()
    {
        if (Input.GetButtonDown("HeroMove"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, 1 << 8 | 1 << 9))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    return;
                }
                //设置位置
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    targetHero = null;
                    GameObject tempParticle = Instantiate(AssetsManager.Instance.GetAsset(SystemDefine.ClickParticlePath)) as GameObject;
                    //设置位置
                    tempParticle.transform.position = hit.point;
                    Destroy(tempParticle,1f);
                }
                else
                {
                    targetHero = hit.collider.GetComponent<PhotonView>();
                }

                SetHeroDestination(hit.point);
            }
        }
    }

    /// <summary>
    /// 判断目标是否在攻击范围
    /// </summary>
    private void CheckTargetHeroInRange()
    {
        if (targetHero == null)
        {
            ani.SetBool(GameConst.PLAYERATTACK_PARAM, false);
            return;
        }

        if (Vector3.Distance(targetHero.transform.position, transform.position) > attackRange)
        {
            ani.SetBool(GameConst.PLAYERATTACK_PARAM, false);
            SetHeroDestination(targetHero.transform.position);
        }
    }
    
    
    /// <summary>
    /// 是否到达目标
    /// </summary>
    private void CheckArrive()
    {
        if (nav.remainingDistance - nav.stoppingDistance <= 0.05f)
        {
            ani.SetFloat(GameConst.SPEED_PARAM, 0);
            if (targetHero != null)
            {
                //转向目标
                RotateTo(targetHero.transform.position);
                //攻击
                ani.SetBool(GameConst.PLAYERATTACK_PARAM,true);
            }
            else
            {
                ani.SetBool(GameConst.PLAYERATTACK_PARAM, false);
            }
        }
    }

    private void RotateTo(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        Quaternion targetQua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQua, Time.deltaTime * turnSpeed);
    }

    private void SetHeroDestination(Vector3 target)
    {
        //设置动画参数
        ani.SetFloat(GameConst.SPEED_PARAM, 1);
        //玩家指向目标的方向向量
        Vector3 dir = target - transform.position;
        float angle = Vector3.Angle(dir, transform.forward);
        if (angle > 90)
        {
            nav.velocity = Vector3.zero;
        }
        //设置导航目标
        nav.SetDestination(target);
    }
}
