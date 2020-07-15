using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack,
    };

    public State _CurrentState = State.Idle;

   protected WaitForSeconds _Delay500 = new WaitForSeconds(0.5f);

    public float _MaxHp = 1000f;
    public float _CurrentHp = 1000f;

    public float _Damage = 100f;

    protected float _PlayerRealizeRange = 15;
    protected float _AttackRange = 5f;
    protected float _AttackCoolTime = 5f;
    protected float _AttackCoolTimeCacl = 5f;
    protected bool _CanAtk = true;

    protected float _MoveSpeed = 2f;

    protected GameObject _Player;
    protected NavMeshAgent _NvAgent;
    protected float _Distance;

    protected GameObject _ParentRoom;

    protected Animator _Anim;
    protected Rigidbody _RigidBody;
 
    public LayerMask _layerMask;
    public GameObject _EnemyHP;

    // Use this for initialization
    protected virtual void Awake ( )
    {
        _Player = FindObjectOfType<cPlayer>().gameObject;

        _NvAgent = GetComponent<NavMeshAgent> ( );
        _RigidBody = GetComponent<Rigidbody> ( );
        _Anim = GetComponent<Animator> ( );

        _ParentRoom = transform.parent.transform.parent.transform.GetChild(0).gameObject;

  
    }
    protected virtual void Start()
    {
        StartCoroutine(CalcCoolTime());
    }

    protected bool CanAtkStateFun ( )
    {
        Vector3 targetDir = new Vector3 ( _Player.transform.position.x - transform.position.x, 0f, _Player.transform.position.z - transform.position.z );

        Physics.Raycast ( new Vector3 ( transform.position.x, 0.5f, transform.position.z ), targetDir, out RaycastHit hit, 30f, _layerMask );
        _Distance = Vector3.Distance (_Player.transform.position, transform.position );

        if ( hit.transform == null )
        {
       
            return false;
        }

        if ( hit.transform.CompareTag ( "Player" ) && _Distance <= _AttackRange )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual IEnumerator CalcCoolTime ( )
    {
        while ( true )
        {
            yield return null;
            if ( !_CanAtk )
            {
                _AttackCoolTimeCacl -= Time.deltaTime;
                if (_AttackCoolTimeCacl <= 0 )
                {
                    _AttackCoolTimeCacl = _AttackCoolTime;
                    _CanAtk = true;
                }
            }
        }
    }
}
