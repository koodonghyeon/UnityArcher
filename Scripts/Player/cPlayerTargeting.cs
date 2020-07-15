using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayerTargeting : MonoBehaviour
{
    public static cPlayerTargeting GetInstance
    {
        get
        {
            //싱글톤이 없다. 
            if (_instacne == null)
            {
                //그러면 씬에서 찾아온다.
                _instacne = GameObject.FindObjectOfType(typeof(cPlayerTargeting)) as cPlayerTargeting;
            }
            //싱글톤이 없다.
            if (_instacne == null)
            {
                //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                var gameObject = new GameObject(typeof(cPlayerTargeting).ToString());
                _instacne = gameObject.AddComponent<cPlayerTargeting>();
                //DontDestroyOnLoad(gameObject); -> 씬으로 넘어 갈 때 나는 파괘되지 않는다.
            }
            //그리고 반환한다.
            return _instacne;
        }
    }
    //싱글톤변수
    private static cPlayerTargeting _instacne;

    public bool getATarget = false;
    float currentDist = 0;      //현재 거리
    float closetDist = 100f;    //가까운 거리
    float TargetDist = 100f;   //타겟 거리
    int closeDistIndex = 0;    //가장 가까운 인덱스
    public int TargetIndex = -1;      //타겟팅 할 인덱스
    int prevTargetIndex = 0;
    public LayerMask layerMask;

    public float atkSpd = 1f;

    public List<GameObject> MonsterList = new List<GameObject> ( );
    //Monster를 담는 List 

    public GameObject PlayerBolt;  //발사체
    public Transform AttackPoint;

   
    void Update ( )
    {
        SetTarget();
        AtkTarget();
    }
    
    void Attack ( )
    {
        cPlayer.GetInstance._Anim.SetFloat ( "AttackSpeed", atkSpd );
        Quaternion Rotation = transform.rotation;
        Quaternion Dir = Quaternion.Euler(new Vector3(90,0,0));
        Instantiate(PlayerBolt, AttackPoint.position, Rotation* Dir);
    }

    void SetTarget ( )
    {
        if ( MonsterList.Count != 0 )
        {
            prevTargetIndex = TargetIndex;
            currentDist = 0f;
            closeDistIndex = 0;
            TargetIndex = -1;

            for ( int i = 0 ; i < MonsterList.Count ; i++ )
            {
                if ( MonsterList[i] == null ) { return; }   // 추가
                currentDist = Vector3.Distance ( transform.position, MonsterList[i].transform.GetChild ( 0 ).position );//변경 

                RaycastHit hit;
                bool isHit = Physics.Raycast ( transform.position, MonsterList[i].transform.GetChild ( 0 ).position - transform.position,//변경 
                                            out hit, 20f, layerMask);

                if ( isHit && hit.transform.CompareTag ( "Monster" ) )
                {
                    if ( TargetDist >= currentDist  )
                    {
                        TargetIndex = i;

                        TargetDist = currentDist;

                        if ( !cJoystick.GetInstance._isPlayerMoving && prevTargetIndex != TargetIndex )  // 추가
                        {
                            TargetIndex = prevTargetIndex;
                        }
                    }
                }

                if ( closetDist >= currentDist )
                {
                    closeDistIndex = i;
                    closetDist = currentDist;
                }
            }

            if ( TargetIndex == -1 )
            {
                TargetIndex = closeDistIndex;
            }
            closetDist = 100f;
            TargetDist = 100f;
            getATarget = true;
        }

    }

    void AtkTarget ( )
    {
        if ( TargetIndex == -1 || MonsterList.Count == 0 )  // 추가 
        {
            cPlayer.GetInstance._Anim.SetBool ( "Attack", false );
            return;
        }
        if ( getATarget && !cJoystick.GetInstance._isPlayerMoving && MonsterList.Count != 0 )
        {
            transform.LookAt ( MonsterList[TargetIndex].transform.GetChild ( 0 ) );     // 변경

            if (cPlayer.GetInstance._Anim.GetCurrentAnimatorStateInfo ( 0 ).IsName ( "Idle" ) )
            {
                cPlayer.GetInstance._Anim.SetBool ( "Idle", false );
                cPlayer.GetInstance._Anim.SetBool ( "Walk", false );
                cPlayer.GetInstance._Anim.SetBool ( "Attack", true );
            }

        }
        else if ( cJoystick.GetInstance._isPlayerMoving )
        {
            if (!cPlayer.GetInstance._Anim.GetCurrentAnimatorStateInfo ( 0 ).IsName ( "Run" ) )
            {
                 cPlayer.GetInstance._Anim.SetBool ( "Attack", false );
                 cPlayer.GetInstance._Anim.SetBool ( "Idle", false );
                cPlayer.GetInstance._Anim.SetBool ( "Walk", true );
            }
        }
        else
        {
           cPlayer.GetInstance._Anim.SetBool ( "Attack", false );
           cPlayer.GetInstance._Anim.SetBool ( "Idle", true );
           cPlayer.GetInstance._Anim.SetBool ( "Walk", false );
        }
    }
}
