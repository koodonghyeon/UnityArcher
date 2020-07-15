using System.Collections;
using UnityEngine;

public class EnemyShortFSM : EnemyBase
{
 
    

    protected override void Awake ( )
    {
        base.Awake( );
       
  

    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(FSM());
    }
    protected virtual void InitMonster ( ) { }

    protected virtual IEnumerator FSM ( )
    {
        yield return null;

        while (!_ParentRoom.GetComponent<cPlayerCheck>()._isPlayerInRoom)
        {
            yield return _Delay500;
        }

        InitMonster ( );

        while ( true )
        {
            yield return StartCoroutine ( _CurrentState.ToString ( ) );
        }
    }

    protected virtual IEnumerator Idle ( )
    {
        yield return null;
        if(!_Anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            _Anim.SetTrigger ( "Idle" );
        }

        if ( CanAtkStateFun ( ) )
        {
            if ( _CanAtk )
            {
                _CurrentState = State.Attack;
            }
            else
            {
                _CurrentState = State.Idle;
                transform.LookAt ( _Player.transform.position );
            }
        }
        else
        {
            _CurrentState = State.Move;
        }
    }

    protected virtual void AtkEffect ( ) { }

    protected virtual IEnumerator Attack ( )
    {
        yield return null;
        //Atk

        _NvAgent.stoppingDistance = 0f;
        _NvAgent.isStopped = true;
        _NvAgent.SetDestination ( _Player.transform.position );
        yield return _Delay500;

        _NvAgent.isStopped = false;
        _NvAgent.speed = 100f;
        _CanAtk = false;

        if ( !_Anim.GetCurrentAnimatorStateInfo ( 0 ).IsName ( "stun" ) )
        {
            _Anim.SetTrigger ( "Attack" );
        }
        AtkEffect ( );
        yield return _Delay500;

        _NvAgent.speed = _MoveSpeed;
        _NvAgent.stoppingDistance = _AttackRange;
        _CurrentState = State.Idle;
    }

    protected virtual IEnumerator Move ( )
    {
        yield return null;
        //Move
        if ( !_Anim.GetCurrentAnimatorStateInfo ( 0 ).IsName ( "walk" ) )
        {
            _Anim.SetTrigger ( "Walk" );
        }
        if ( CanAtkStateFun ( ) && _CanAtk )
        {
            _CurrentState = State.Attack;
        }
        else if ( _Distance > _PlayerRealizeRange )
        {
            _NvAgent.SetDestination ( transform.parent.position - Vector3.forward *5);
        }
        else
        {
            _NvAgent.SetDestination ( _Player.transform.position );
        }
    }
}
