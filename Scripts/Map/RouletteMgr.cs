using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteMgr : MonoBehaviour
{
    public GameObject RoulettePlate; 
    public GameObject RoulettePanel; 
    public Transform Needle;

    public AudioClip _StartEndClip;
    public AudioClip _RotatingClip;
    AudioSource _Source;
    public Sprite[] SkillSprite; 
    public Image[] DisplayItemSlot;

    List<int> StartList = new List<int> ( );
    List<int> ResultIndexList = new List<int> ( );
    int ItemCnt = 6;

    private void Start()
    {
        _Source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartList.Clear();
        ResultIndexList.Clear();
     
        for (int i = 0; i < ItemCnt; i++)
        {
            StartList.Add(i);
        }

        for (int i = 0; i < ItemCnt; i++)
        {
            int randomIndex = Random.Range(0, StartList.Count);
            ResultIndexList.Add(StartList[randomIndex]);
            DisplayItemSlot[i].sprite = SkillSprite[StartList[randomIndex]];
            StartList.RemoveAt(randomIndex);
        }
    }
    public void RouletteStart()
    {
        StartCoroutine(StartRoulette());
    }
    IEnumerator StartRoulette ( )
    {
        yield return new WaitForSeconds ( 2f );
        _Source.clip = _StartEndClip;
        _Source.Play();
        float randomSpd = Random.Range ( 3.0f, 10.0f );
        float rotateSpeed = 100f * randomSpd;
        _Source.clip = _RotatingClip;
        while ( true )
        {
            yield return null;
            if ( rotateSpeed <= 0.01f ) break;
                _Source.Play();
          
     
            rotateSpeed = Mathf.Lerp ( rotateSpeed, 0, Time.deltaTime * 2f );
            RoulettePlate.transform.Rotate ( 0, 0, rotateSpeed );
        }
        _Source.clip = _StartEndClip;
        _Source.Play();
        yield return new WaitForSeconds ( 1f );
        Result();

        yield return new WaitForSeconds ( 1f );
        StartCoroutine(RouletteEnd());
    }

    void Result ( )
    {
        int closetIndex = -1;
        float closetDis = 500f;
        float currentDis = 0f;

        for ( int i = 0 ; i < ItemCnt; i++ )
        {
            currentDis = Vector2.Distance ( DisplayItemSlot[i].transform.position, Needle.position );
            if ( closetDis > currentDis )
            {
                closetDis = currentDis;
                closetIndex = i;
              
            }
        }
    
        cPlayerData.GetInstance.SetSkill(ResultIndexList[closetIndex]);
      
    }
    IEnumerator RouletteEnd()
    {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
    }
}
