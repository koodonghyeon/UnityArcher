using System.Collections;
using UnityEngine;

public class cENemyLongFSM : EnemyBase
{


   

    public GameObject _EnemyBullet;

    public Transform _BoltGenPosition;
    public bool _isMove=true;

    protected override void Awake ( )
    {
        base.Awake( );
       
        _AttackRange = 10f;
        _NvAgent.stoppingDistance = 7f;

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
   protected virtual void DangerMarkerShoot()
    {
       
    }

   protected virtual void Shoot()
    {
  
    }
    protected virtual IEnumerator Idle ( )
    {
        yield return null;
        if(!_Anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            _Anim.SetTrigger ( "Idle" );
        }

        if (_isMove) { 
            if ( _CanAtk )
            {
                _CurrentState = State.Attack;
            }
            else
            {
                transform.LookAt ( _Player.transform.position );
                _isMove = false;
                _CurrentState = State.Idle;
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

        
        _NvAgent.isStopped = true;
        _NvAgent.SetDestination ( _Player.transform.position );
        yield return _Delay500;
        transform.LookAt(_Player.transform.position);
        DangerMarkerShoot();
        yield return new WaitForSeconds(2f);
        Shoot();
        _CanAtk = false;
        _NvAgent.isStopped =false;

        if ( !_Anim.GetCurrentAnimatorStateInfo ( 0 ).IsName ( "stun" ) )
        {
            _Anim.SetTrigger ( "Attack" );
        }
        AtkEffect ( );
        yield return _Delay500;

        _NvAgent.speed = _MoveSpeed;
        _isMove = false;
        //_NvAgent.stoppingDistance = _AttackRange;
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
        if (_CanAtk)
        {
            _isMove = true;
            _CurrentState = State.Idle;
        }
        else
        {
           
            _NvAgent.SetDestination(_Player.transform.position);
            //_NvAgent.SetDestination(transform.position - Vector3.forward * 5f);
        }
    }
}
