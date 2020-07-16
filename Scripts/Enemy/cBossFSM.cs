using System.Collections;
using UnityEngine;

public class cBossFSM : EnemyBase
{

    protected bool _isMove=true;
    public GameObject _PlayerCheck;
    protected override void Awake()
    {
        base.Awake();



    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(FSM());
        _NvAgent.enabled = true;
    }
    protected virtual void InitMonster() { }

    protected virtual IEnumerator FSM()
    {
        yield return null;

        while (!_PlayerCheck.GetComponent<cPlayerCheck>()._isPlayerInRoom)
        {
            yield return _Delay500;
        }

        InitMonster();

        while (true)
        {
            yield return StartCoroutine(_CurrentState.ToString());
        }
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;
        if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            _Anim.SetTrigger("Idle");
        }
        transform.LookAt(_Player.transform.position);

        if (_CanAtk)
            {
                _CurrentState = State.Attack;
            }
            else
            {
                _CurrentState = State.Idle;
                _isMove = false;
                transform.LookAt(_Player.transform.position);
            }
     
    }

    protected virtual void AtkEffect() { }

    protected virtual IEnumerator Attack()
    {
        yield return null;

    }

    //protected virtual IEnumerator Move()
    //{
    //    yield return null;
    //    //Move
    //    if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
    //    {
    //        _Anim.SetTrigger("Walk");
    //    }
    //    if (_CanAtk)
    //    {
    //        _isMove = true;
    //        _CurrentState = State.Idle;
    //    }
    //    else
    //    {

    //        //_NvAgent.SetDestination(_Player.transform.position);
    //        _NvAgent.SetDestination(transform.position - Vector3.forward * 5f);
    //    }
    //}
}

