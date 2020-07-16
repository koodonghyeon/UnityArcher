using System.Collections;
using UnityEngine;

public class cEnemyStageBoss: cBossFSM
{

  
    public GameObject _BossBullet;
    public Transform _AttackPoint;
    public AudioSource _Audio;
    //public AudioClip _Move;
    public AudioClip _Attack;
    public GameObject _BossAttack;
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
        _AttackCoolTime = 2f;
        _AttackCoolTimeCacl = _AttackCoolTime;

        _PlayerRealizeRange = 13;
        _MoveSpeed = 1;
        _AttackRange = 30f;
        _NvAgent.stoppingDistance = 2f;

        _Audio = GetComponent<AudioSource>();

    }
    protected override void Start()
    {
        base.Start();

    }

 
    protected override void InitMonster()
    {
        _MaxHp = 10000f;
        _CurrentHp = _MaxHp;
        _Damage = 150f;

    }

    protected override void AtkEffect()
    {
        Instantiate(cEffect.GetInstance._BossEffect, transform.position, Quaternion.Euler(90, 0, 0));
    }

    void Update()
    {
        if (_CurrentHp <= 0)

        {
            _NvAgent.isStopped = true;
            _RigidBody.gameObject.SetActive(false);
            cPlayerTargeting.GetInstance.MonsterList.Remove(transform.gameObject);
            cPlayerTargeting.GetInstance.TargetIndex = -1;
            cUIController.GetInstance.CheckBossRoom(false);
            Destroy(transform.gameObject);
            return;
        }
        else
        {
            cUIController.GetInstance._BossCurrentHP = _CurrentHp;
            cUIController.GetInstance._BossMaxHP = _MaxHp;
        }
    }

  
    protected override IEnumerator Attack()
    {
        yield return null;
        
       // _NvAgent.isStopped = true;
        transform.LookAt(_Player.transform.position);

        if (Random.value < 0.6)
        {
            if (Random.value < 0.5)
            {
                if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
                {
                    _Anim.SetTrigger("Attack1");
                }
            }
           else
            {
                if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack02"))
                {
                    _Anim.SetTrigger("Attack2");
                }
            }
        yield return new WaitForSeconds(0.5f);
        }
        else
        {
            if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
            {
                _Anim.SetTrigger("GetHit");
            }
            yield return _Delay500;
            _Audio.clip= _Attack;
            _Audio.Play();
            _NvAgent.stoppingDistance = 0f;
            _NvAgent.SetDestination(_Player.transform.position);
            _NvAgent.isStopped = false;
            _NvAgent.speed = 300f;
            while (Vector3.Distance(_NvAgent.destination, transform.position) > 1f)
            {
                if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                {
                    _Anim.SetTrigger("Walk");
                }
                yield return null;
            }
            _NvAgent.speed = _MoveSpeed;
            _NvAgent.stoppingDistance = _AttackRange;
        }
        _CanAtk = false;
        _CurrentState = State.Idle;

        _NvAgent.isStopped = false;
        //_NvAgent.speed = 100f;
        
        if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("stun"))
        {
            _Anim.SetTrigger("Attack");
        }
        AtkEffect();
        yield return _Delay500;

        _NvAgent.speed = _MoveSpeed;
        _isMove = false;
        _NvAgent.stoppingDistance = _AttackRange;
        _CurrentState = State.Idle;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            cPlayer.GetInstance.HIT(_Damage, 1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            float Dam;
            Dam = other.gameObject.GetComponent<cPlayerWeaPon>()._Damage;
           // cUIController.GetInstance.
    
            //_CurrentHp -= Dam;
            Instantiate(cEffect.GetInstance._DuckDmgEffect, other.transform.position, Quaternion.Euler(90, 0, 0));
            float Critical;
            Critical = Random.Range(0.0f, 100.0f);
            GameObject DamText =
                Instantiate(cEffect.GetInstance._MonsterDamText, transform.position, Quaternion.identity);

            if (Critical > cPlayerData.GetInstance._Critical)
            {
                _CurrentHp -= Dam;
                DamText.GetComponent<cDamText>().DisplayDamage(Dam, false);
            }
            else
            {
                _CurrentHp -= Dam * 2;
                DamText.GetComponent<cDamText>().DisplayDamage(Dam * 2, true);
            }
        }
    }
    public void Attack01()
    {
        Instantiate(_BossBullet, _AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, -45,0)));
        Instantiate(_BossBullet, _AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 0)));
        Instantiate(_BossBullet, _AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45, 0)));
    }
    public void Attack02()
    {
        Instantiate(_BossBullet, _AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, -35, 0)));
        Instantiate(_BossBullet, _AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, -10, 0)));
        Instantiate(_BossBullet, _AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 10, 0)));
        Instantiate(_BossBullet, _AttackPoint.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 35, 0)));
    }
}

