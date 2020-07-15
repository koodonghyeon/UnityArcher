using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayer : MonoBehaviour
{

    public static cPlayer GetInstance
    {
        get
        {
            //싱글톤이 없다. 
            if (_instacne == null)
            {
                //그러면 씬에서 찾아온다.
                _instacne = GameObject.FindObjectOfType(typeof(cPlayer)) as cPlayer;
            }
            //싱글톤이 없다.
            if (_instacne == null)
            {
                //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                var gameObject = new GameObject(typeof(cPlayer).ToString());
                _instacne = gameObject.AddComponent<cPlayer>();
                //DontDestroyOnLoad(gameObject); -> 씬으로 넘어 갈 때 나는 파괘되지 않는다.
            }
            //그리고 반환한다.
            return _instacne;
        }
    }
    //싱글톤변수
    private static cPlayer _instacne;
    private Rigidbody _RigidBody;
    public Animator _Anim;
    public float _moveSpeed=3f;
    public float _AttaackDamage;
    void Start()
    {
        _AttaackDamage = 900;
        _RigidBody = GetComponent<Rigidbody>();
        _Anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (cJoystick.GetInstance._joyVec.x != 0 || cJoystick.GetInstance._joyVec.y != 0)
        {
            _RigidBody.velocity = new Vector3(cJoystick.GetInstance._joyVec.x, _RigidBody.velocity.y, cJoystick.GetInstance._joyVec.y)*_moveSpeed;
            _RigidBody.rotation = Quaternion.LookRotation(new Vector3(cJoystick.GetInstance._joyVec.x,0, cJoystick.GetInstance._joyVec.y));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Portal"))
        {
            cStageMgr.GetInstance.NextStage();
        }
       
    }
    public void HIT(float Damage)
    {
        if (!_Anim.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
            _Anim.SetTrigger("Dmg");
        }
        cPlayerHPBar.GetInstance._CurrntHP -= Damage;
            Instantiate(cEffect.GetInstance._PlayerDmgEffect, cPlayerTargeting.GetInstance.AttackPoint.position, Quaternion.Euler(90, 0, 0));
        cPlayerHPBar.GetInstance.GetHPBoost();
    }
}
