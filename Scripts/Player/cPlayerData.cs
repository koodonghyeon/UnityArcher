using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayerData : MonoBehaviour
{
    public static cPlayerData GetInstance
    {
        get
        {
            //싱글톤이 없다. 
            if (_instacne == null)
            {
                //그러면 씬에서 찾아온다.
                _instacne = GameObject.FindObjectOfType(typeof(cPlayerData)) as cPlayerData;
            }
            //싱글톤이 없다.
            if (_instacne == null)
            {
                //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                var gameObject = new GameObject(typeof(cPlayerData).ToString());
                _instacne = gameObject.AddComponent<cPlayerData>();

            }
            //그리고 반환한다.
            return _instacne;
        }
    }
    //싱글톤변수
    private static cPlayerData _instacne;
    public float _Dam=100;
    public float atkSpd = 2f;
    public float _CriticalDam=1.5f;
    public float _Critical=20;
    public GameObject Player;
    public float _MaxHP;
    public float _CurrnetHP;


    public GameObject[] _Bullet;
    public GameObject _EXP;
    public int _PlayerLV=1;
    public int _MaxLV=5;
    public float _PlayerCurrentExp = 0f;
    public float _PlayerLVUPexp = 500f;

   public AudioSource _Audio;
    public AudioClip _Dead;
    public bool _PlayerDead = false;
    private void Start()
    {
        _Audio = GetComponent<AudioSource>();
        _Audio.clip = _Dead;
    }
    private void Update()
    {
        if(!_PlayerDead && _CurrnetHP <= 0)
        {
            _Audio.Play();
            _CurrnetHP = 0;
            _PlayerDead = true;
            cPlayer.GetInstance._Anim.SetTrigger("Dead");
            cUIController.GetInstance.EndGame();
          
        }
    }
    public void PlayerExpCalc(float exp)
    {
        if (_PlayerLV <= _MaxLV)
        {
            _PlayerCurrentExp += exp;
            if (_PlayerCurrentExp >= _PlayerLVUPexp)
            {
                cPlayer.GetInstance.LVUPSoundPlay();
                ++_PlayerLV;
                _PlayerCurrentExp -= _PlayerLVUPexp;
                _PlayerLVUPexp *= 1.5f;
                StartCoroutine(PlayerLevelUp());
            }
        }
    }
    IEnumerator PlayerLevelUp()
    {
        yield return null;
        cPlayer.GetInstance._isLVUP = true;
        cEffect.GetInstance._PlayerLVUP.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        cUIController.GetInstance.PlayerLvUp(true);
        yield return new WaitForSeconds(1.5f);
        cEffect.GetInstance._PlayerLVUP.SetActive(false);



    }

    public List<int> PlayerSkill = new List<int>();
    // 1번 팅기는 스킬
    // 2번 더블샷
    //3번 멀티샷
    //4번 사선 쏘기
    //5번 벽에 팅기기
    //6번 공격력 업
    //7번 공격속도업
    //8번 최대 체력업
    //9번 크리티컬 확률업
    public void SetSkill(int index)
    {

        PlayerSkill[index] = PlayerSkill[index] + 1;
        if (index == 5)
        {
            PowerUp();
        }
        if (index == 6)
        {
            SetAttackSpeed();
        }
        if (index == 7)
        {
            SetAttackSpeed();
        }
        if (index == 8)
        {
            
            SetHP();
        }
        if(index == 9)
        {
            SetCritical();
        }
    }

    public void PowerUp()
    {
        _Dam += _Dam / 10;
    }
    public void SetAttackSpeed()
    {
        float _Speed = 0.5f;
        atkSpd += _Speed;
        cPlayer.GetInstance._Anim.SetFloat("AttackSpeed", atkSpd);
    }
    private void SetCritical()
    {
        _Critical += 10;
        _CriticalDam += 0.2f;
    }
    void SetHP()
    {
        _MaxHP += 200;
        _CurrnetHP += 200;
    }
}
