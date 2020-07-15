using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cEffect : MonoBehaviour
{
    public static cEffect GetInstance
    {
        get
        {
            //싱글톤이 없다. 
            if (_instacne == null)
            {
                //그러면 씬에서 찾아온다.
                _instacne = GameObject.FindObjectOfType(typeof(cEffect)) as cEffect;
            }
            //싱글톤이 없다.
            if (_instacne == null)
            {
                //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                var gameObject = new GameObject(typeof(cEffect).ToString());
                _instacne = gameObject.AddComponent<cEffect>();
                
            }
            //그리고 반환한다.
            return _instacne;
        }
    }
    //싱글톤변수
    private static cEffect _instacne;

    [Header("Monster")]
    public GameObject _DuckAtkEffect;
    public GameObject _DuckDmgEffect;
    public GameObject _LaserEffet;
    [Header("Player")]
    public GameObject _PlayerAtkEffect;
    public GameObject _PlayerDmgEffect;
    [Header("Item")]
    public GameObject _BombEffect;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
