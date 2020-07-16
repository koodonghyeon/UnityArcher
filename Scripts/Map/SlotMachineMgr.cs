using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineMgr : MonoBehaviour
{
    public GameObject[] SlotSkillObject;
    public Button[] Slot;

    public Sprite[] SkillSprite;
    public AudioSource _Source;
    public AudioSource _EndSource;

    public AudioClip _ShowClip;
    public AudioClip _RotatingClip;
    public AudioClip _EndClip;

    [System.Serializable]
    public class DisplayItemSlot
    {
        public List<Image> SlotSprite = new List<Image> ( );
    }
    public DisplayItemSlot[] DisplayItemSlots;


    public List<int> StartList = new List<int> ( );
    public List<int> ResultIndexList = new List<int> ( );
    int ItemCnt = 3;
    int[] answer = { 2, 3, 1 };



    private void Start()
    {
        
    }
    private void OnEnable()
    {
        StartList.Clear();
        ResultIndexList.Clear();
        SlotSkillObject[0].transform.localPosition = new Vector3(0, 300f, 0);
        SlotSkillObject[1].transform.localPosition = new Vector3(0, 300f, 0);
        SlotSkillObject[2].transform.localPosition = new Vector3(0, 300f, 0);
        for (int i = 0; i < ItemCnt * Slot.Length; i++)
        {
            StartList.Add(i);
        }

        for (int i = 0; i < Slot.Length; i++)
        {
            for (int j = 0; j < ItemCnt; j++)
            {
                Slot[i].interactable = false;

                int randomIndex = Random.Range(0, StartList.Count);
                if (i == 0 && j == 1 || i == 1 && j == 0 || i == 2 && j == 2)
                {
                    ResultIndexList.Add(StartList[randomIndex]);
                }
                DisplayItemSlots[i].SlotSprite[j].sprite = SkillSprite[StartList[randomIndex]];

                if (j == 0)
                {
                    DisplayItemSlots[i].SlotSprite[ItemCnt].sprite = SkillSprite[StartList[randomIndex]];
                }
                StartList.RemoveAt(randomIndex);
            }
        }
        _Source = GetComponent<AudioSource>();
        _Source.clip = _ShowClip;
        _Source.Play();

        for (int i = 0; i < Slot.Length; i++)
        {
            StartCoroutine(StartSlot(i));
        }
    }
    IEnumerator StartSlot ( int SlotIndex )
    {
        for ( int i = 0 ; i < ( ItemCnt * ( 6 + SlotIndex * 4 ) + answer[SlotIndex] ) * 2 ; i++ )
        {
            
                _Source.clip = _RotatingClip;
                _Source.Play();
         
            SlotSkillObject[SlotIndex].transform.localPosition -= new Vector3 ( 0, 50f, 0 );
            if ( SlotSkillObject[SlotIndex].transform.localPosition.y < 50f )
            {
              
                SlotSkillObject[SlotIndex].transform.localPosition += new Vector3 ( 0, 300f, 0 );
               



            }
            yield return new WaitForSeconds ( 0.02f );
        }
        for (int i = 0; i < ItemCnt; i++)
        {
            Slot[i].interactable = true;
            _EndSource.clip = _EndClip;
            _EndSource.Play();
        }
    }

    public void ClickBtn ( int index )
    {

        cUIController.GetInstance.PlayerLvUp(false);
        cPlayerData.GetInstance.SetSkill(ResultIndexList[index]);
        cPlayer.GetInstance._isLVUP = false;
    }

 
}