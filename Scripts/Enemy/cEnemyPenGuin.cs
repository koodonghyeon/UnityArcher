using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cEnemyPenGuin : cENemyLongFSM
{
    public GameObject _DangerMarker;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _PlayerRealizeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _AttackRange);
    }
    protected override void Awake()
    {
        base.Awake();


    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void InitMonster()
    {
        _MaxHp += (cStageMgr.GetInstance.currentStage + 1) * 100f;
        _CurrentHp = _MaxHp;
        _Damage += (cStageMgr.GetInstance.currentStage + 1) * 10f;
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

  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            _EnemyHP.GetComponent<cEnemyHpBar>().Dmg(collision.gameObject.GetComponent<cPlayerWeaPon>()._Damage);
            _CurrentHp -= collision.gameObject.GetComponent<cPlayerWeaPon>()._Damage;
            Instantiate(cEffect.GetInstance._DuckDmgEffect, collision.contacts[0].point, Quaternion.Euler(90, 0, 0));
        }
    }
    protected override void DangerMarkerShoot()
    {
        Vector3 NewPosition = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        Physics.Raycast(NewPosition, transform.forward, out RaycastHit hit, 30f, _layerMask);

        if (hit.transform.CompareTag("Wall"))
        {
            GameObject DangerMarkerClone = Instantiate(_DangerMarker, NewPosition, transform.rotation);
            DangerMarkerClone.GetComponent<cDangerLine>()._EndPosition = hit.point;
        }
    }
    protected override void Shoot()
    {
        //Vector3 CurrentRotation = transform.eulerAngles + new Vector3(-90, 0, 0);
        Instantiate(_EnemyBullet, _BoltGenPosition.position, transform.rotation);
    }
}
