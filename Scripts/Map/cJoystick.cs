using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class cJoystick : MonoBehaviour,IDragHandler,IPointerDownHandler,IEndDragHandler
{
    private void Awake()
    {
        if (_instacne == null)
        {

            _instacne = this;
            DontDestroyOnLoad(gameObject);

        }

        else if (_instacne != null)
        {
            Destroy(this.gameObject);
        }
    }
    public static cJoystick GetInstance
    {
        get
        {
            //싱글톤이 없다. 
            if (_instacne == null)
            {
                //그러면 씬에서 찾아온다.
                _instacne = GameObject.FindObjectOfType(typeof(cJoystick)) as cJoystick;
            }
            //싱글톤이 없다.
            if (_instacne == null)
            {
                //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                var gameObject = new GameObject(typeof(cPlayer).ToString());
                _instacne = gameObject.AddComponent<cJoystick>();
                //DontDestroyOnLoad(gameObject); -> 씬으로 넘어 갈 때 나는 파괘되지 않는다.
            }
            //그리고 반환한다.
            return _instacne;
        }
    }
    private static cJoystick _instacne;
    //작은스틱
    public GameObject _SmallStick;
    //조이스틱 
    public GameObject _BigStick;
    //스틱 처음지점
    private Vector3 _StickStartPosition;
    //스틱 처음지점 저장용변수 
    private Vector3 _JoyStickFirstPosition;
    //조이스틱 방향
    public Vector3 _joyVec;
    //조이스틱 반지름
    private float _StickRadius;
    //플레이어가 이동중인지
    public bool _isPlayerMoving = false;
    void Start()
    {
        _StickRadius = _BigStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        _JoyStickFirstPosition = _BigStick.transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        PointerEventData _PointerEventData = eventData as PointerEventData;
        Vector3 _DragPosition = _PointerEventData.position;
        _joyVec = (_DragPosition - _StickStartPosition).normalized;
        //작은 조이스틱이 큰반지름안에서만 움직이기
        float _StickDistance = Vector3.Distance(_DragPosition, _StickStartPosition);
        if (_StickDistance < _StickRadius)
        {
            _SmallStick.transform.position = _StickStartPosition + _joyVec * _StickDistance;
        }
        else
        {
            _SmallStick.transform.position = _StickStartPosition + _joyVec * _StickRadius;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _joyVec = Vector3.zero;
        cPlayer.GetInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _BigStick.transform.position = _JoyStickFirstPosition;
        _SmallStick.transform.position = _JoyStickFirstPosition;
        if (!cPlayer.GetInstance._Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {

            cPlayer.GetInstance._Anim.SetBool("Attack", false);
            cPlayer.GetInstance._Anim.SetBool("Walk", false);
            cPlayer.GetInstance._Anim.SetBool("Idle", true);
        }

        _isPlayerMoving = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _BigStick.transform.position = Input.mousePosition;
        _SmallStick.transform.position = Input.mousePosition;
        _StickStartPosition = Input.mousePosition;
        if (!cPlayer.GetInstance._Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            cPlayer.GetInstance._Anim.SetBool("Attack", false);
            cPlayer.GetInstance._Anim.SetBool("Idle", false);
            cPlayer.GetInstance._Anim.SetBool("Walk", true);
        }

        _isPlayerMoving = true;
        cPlayerTargeting.GetInstance.getATarget = false;
    }
}
