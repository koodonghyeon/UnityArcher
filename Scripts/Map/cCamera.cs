using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class cCamera : MonoBehaviour
    {

        public static cCamera GetInstance
        {
            get
            {
                //싱글톤이 없다. 
                if (_instacne == null)
                {
                    //그러면 씬에서 찾아온다.
                    _instacne = GameObject.FindObjectOfType(typeof(cCamera)) as cCamera;
                }
                //싱글톤이 없다.
                if (_instacne == null)
                {
                    //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                    var gameObject = new GameObject(typeof(cCamera).ToString());
                    _instacne = gameObject.AddComponent<cCamera>();

                }
                //그리고 반환한다.
                return _instacne;
            }
        }
        //싱글톤변수
        private static cCamera _instacne;
    

        public GameObject _Target;


    //이동속도
    private float _MoveSpeed = 5;
    //타겟위치
    private Vector3 _TargetPosition;
    //카메라가 나가지못할 영역
    public Collider _Bound;
    //최소 영역
    private Vector3 _minBound;
    //최대 영역
    private Vector3 _maxBound;
    //카메라의 반너비,반높이 값을 지닐변수
    private float _halfWidth;
    private float _halfHeight;
    //카메라 의 반높이 값을 구할 속성을 이용하기 위한변수
    private Camera _theCamera;

    private void Awake()
    {
        _theCamera = GetComponent<Camera>();

        _minBound = _Bound.bounds.min;
        _maxBound = _Bound.bounds.max;
        _halfHeight = _theCamera.orthographicSize;
        _halfWidth = _halfHeight * Screen.width / Screen.height;

    }

    private void LateUpdate()
    {
        if (_Target.gameObject != null)
        {
            _TargetPosition.Set(_Target.transform.position.x, this.transform.position.y, _Target.transform.position.z );
            this.transform.position = Vector3.Lerp(this.transform.position, _TargetPosition, _MoveSpeed * Time.deltaTime);
            //카메라 영역조절
            float clampedX = Mathf.Clamp(this.transform.position.x, _minBound.x + _halfWidth, _maxBound.x - _halfWidth);
            float clampedZ = Mathf.Clamp(this.transform.position.z, _minBound.z + _halfHeight, _maxBound.z - _halfHeight);
            this.transform.position = new Vector3(clampedX, this.transform.position.y, clampedZ );
        }
    
    }
public void SetBound(Collider Bound)
    {
        _Bound = Bound;
        _minBound = _Bound.bounds.min;
        _maxBound = _Bound.bounds.max;
    }

    }
