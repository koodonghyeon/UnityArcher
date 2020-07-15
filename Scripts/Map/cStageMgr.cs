using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cStageMgr : MonoBehaviour
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
    public static cStageMgr GetInstance
    {
        get
        {
            //싱글톤이 없다. 
            if (_instacne == null)
            {
                //그러면 씬에서 찾아온다.
                _instacne = GameObject.FindObjectOfType(typeof(cStageMgr)) as cStageMgr;
            }
            //싱글톤이 없다.
            if (_instacne == null)
            {
                //새로운 오브젝트를 만들어서 거기다 싱글톤을 넣어서 만든다.
                var gameObject = new GameObject(typeof(cStageMgr).ToString());
                _instacne = gameObject.AddComponent<cStageMgr>();
       
            }
            //그리고 반환한다.
            return _instacne;
        }
    }
    //싱글톤변수
    private static cStageMgr _instacne;

    public GameObject Player;

    [System.Serializable]
    public class StartPositionArray
    {
        public List<Transform> StartPosition = new List<Transform> ( );
    }

    public StartPositionArray[] startPositionArrays;    // 0 1 2


    public List<Transform> StartPositionAngel = new List<Transform> ( );
    // 천사방 3개 
    public List<Transform> StartPositionBoss = new List<Transform> ( );
    // 보스 3개
    public Transform StartPositionLastBoss;
    // 막보 하나

    public int currentStage = 0;  //현재 방위치
    int LastStage = 20; // 막보방


    void Start ( )
    {
        Player = GameObject.FindGameObjectWithTag ( "Player" );
    }

    public void NextStage ( )
    {
        currentStage++;
        if(currentStage > LastStage)
        { return; }

        if ( currentStage % 5 != 0 )  //Normal Stage
        {
            int arrayIndex = currentStage / 10;
            int randomIndex = Random.Range ( 0, startPositionArrays[arrayIndex].StartPosition.Count );
            Player.transform.position = startPositionArrays[arrayIndex].StartPosition[randomIndex].position;
            startPositionArrays[arrayIndex].StartPosition.RemoveAt ( randomIndex );
        }
        else    //BossRoom or Angel
        {
            if ( currentStage % 10 == 5 )   // Angel
            {
                int randomIndex = Random.Range ( 0, StartPositionAngel.Count );
                Player.transform.position = StartPositionAngel[randomIndex].position;
            }
            else    //Boss
            {
                if ( currentStage == LastStage )  //LastBoss
                {
                    Player.transform.position = StartPositionLastBoss.position;
                }
                else    //Mid Boss
                {
                    int randomIndex = Random.Range ( 0, StartPositionBoss.Count );
                    Player.transform.position = StartPositionBoss[randomIndex].position;
                    StartPositionBoss.RemoveAt ( currentStage / 10 );
                }
            }
        }
        
    }//NextStage
}
