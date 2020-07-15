using System.Collections;
using UnityEngine;

public class EnemyDuck : EnemyShortFSM
{
 
    public GameObject _MeleeAtkArea;

    private void OnDrawGizmosSelected ( )
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere ( transform.position, _PlayerRealizeRange );
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere ( transform.position, _AttackRange );
    }

    protected override void Awake ( )
    {
        base.Awake( );
        _AttackCoolTime = 2f;
        _AttackCoolTimeCacl = _AttackCoolTime;

        _AttackRange = 5f;
        _NvAgent.stoppingDistance = 2f;


    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ResetAtkArea());
    }
    IEnumerator ResetAtkArea ( )
    {
        while ( true )
        {
            yield return null;
            if ( !_MeleeAtkArea.activeInHierarchy && _CurrentState == State.Attack )
            {
                yield return new WaitForSeconds ( _AttackCoolTime );
                _MeleeAtkArea.SetActive ( true );
            }
        }
    }

    protected override void InitMonster ( )
    {
        _MaxHp += ( cStageMgr.GetInstance.currentStage + 1 ) * 100f;
        _CurrentHp = _MaxHp;
        _Damage += ( cStageMgr.GetInstance.currentStage + 1 ) * 10f;
    }

    protected override void AtkEffect()
    {
        Instantiate(cEffect.GetInstance._DuckAtkEffect, transform.position, Quaternion.Euler(90, 0, 0));
    }

    void Update()
    {
        if (_CurrentHp <= 0)

        {
            _NvAgent.isStopped = true;

            _RigidBody.gameObject.SetActive(false);
            cPlayerTargeting.GetInstance.MonsterList.Remove(transform.gameObject);
            cPlayerTargeting.GetInstance.TargetIndex = -1;
            Destroy(transform.parent.gameObject);
            return;
        }
    }

    private void OnCollisionEnter ( Collision collision )
    {
        if ( collision.transform.CompareTag ( "Bullet" ) )
        {
            _EnemyHP.GetComponent<cEnemyHpBar> ( ).Dmg (collision.gameObject.GetComponent<cPlayerWeaPon>()._Damage);
            _CurrentHp -= collision.gameObject.GetComponent<cPlayerWeaPon>()._Damage;
            Instantiate(cEffect.GetInstance._DuckDmgEffect, collision.contacts[0].point, Quaternion.Euler(90, 0, 0));
        }
        if (collision.transform.CompareTag("Player"))
        {
            cPlayer.GetInstance.HIT(_Damage);
        }
    }
}
