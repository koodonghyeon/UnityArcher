using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class cUIController : MonoBehaviour
{

    public static cUIController GetInstance
    {
        get
        {
        
             if (_instacne == null)
            {
          
                _instacne = GameObject.FindObjectOfType(typeof(cUIController)) as cUIController;
            }
        
            if (_instacne == null)
            {
        
                var gameObject = new GameObject(typeof(cUIController).ToString());
                _instacne = gameObject.AddComponent<cUIController>();
          
            }
       
            return _instacne;
        }
    }
    public cAdmobManager _Ad;
    //싱글톤변수
    private static cUIController _instacne;
    public GameObject _JoystickGo;
    public GameObject _JoystickPaneGo;
    public GameObject _SlotMachine;
    public GameObject _RouletteGo;
    public GameObject _EndGame;
    public GameObject _Clear;
    public Text ClearRoomCount;
    bool _isDie=false;
    public Slider _PlayerExpBar;
    public Slider _BossHpBar;
    public Slider _BossBackHPSlider;
    public bool _BackHpHit = false;
    public bool _BossRoom = false;

    public Text _PlayerLVText;
    public float _BossCurrentHP;
    public float _BossMaxHP;
    private int _MaxLV=5;

    AudioSource _AudioSource;
   public AudioClip _ButtonClip;
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _PlayerExpBar.value = cPlayerData.GetInstance._PlayerCurrentExp / cPlayerData.GetInstance._PlayerLVUPexp;
        _PlayerExpBar.gameObject.SetActive(true);
        _BossHpBar.gameObject.SetActive(false);
        _BossBackHPSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!_BossRoom)
        {
            if (cPlayerData.GetInstance._PlayerLV <= _MaxLV)
            {
                _PlayerExpBar.value = Mathf.Lerp(_PlayerExpBar.value, cPlayerData.GetInstance._PlayerCurrentExp / cPlayerData.GetInstance._PlayerLVUPexp, 0.75f);
                _PlayerLVText.text = "LV." + cPlayerData.GetInstance._PlayerLV;
                if (cPlayerData.GetInstance._PlayerLV == _MaxLV)
                {
                    _PlayerLVText.text = "LV.MAX";
                    _PlayerExpBar.value = 1;
                }
            }
        }
        else
        {
            _BossHpBar.value = Mathf.Lerp(_BossHpBar.value, _BossCurrentHP / _BossMaxHP, Time.deltaTime * 5);
            if(_BackHpHit)
            {
                _BossBackHPSlider.value = Mathf.Lerp(_BossBackHPSlider.value, _BossHpBar.value, Time.deltaTime * 10);
                if (_BossHpBar.value >=_BossBackHPSlider.value-0.01f)
                {
                    _BackHpHit = false;
                    _BossBackHPSlider.value = _BossHpBar.value;
                }

            }
        }
    }
    public void CheckBossRoom(bool isBossRoom)
    {
        _BossRoom = isBossRoom;
        if (isBossRoom)
        {
            _PlayerExpBar.gameObject.SetActive(false);
            _BossHpBar.gameObject.SetActive(true);
            _BossBackHPSlider.gameObject.SetActive(true);
        }
        else
        {
            _PlayerExpBar.gameObject.SetActive(true);
            _BossHpBar.gameObject.SetActive(false);
            _BossBackHPSlider.gameObject.SetActive(false);
        }
    }
    public void PlayerLvUp(bool isSlotMachineOn)
    {
        if (isSlotMachineOn)
        {
            _JoystickGo.SetActive(false);
            _JoystickPaneGo.SetActive(false);
            _SlotMachine.SetActive(true);
        }
        else
        {
            _JoystickGo.SetActive(true);
            _JoystickPaneGo.SetActive(true);
            _SlotMachine.SetActive(false);
        }
    }
    public void EndGame()
    {
        _JoystickGo.gameObject.SetActive(false);
        _JoystickPaneGo.gameObject.SetActive(false);
        StartCoroutine(EndGamePopUp());
    }
    IEnumerator EndGamePopUp()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        if (!_isDie)
        {
            _EndGame.SetActive(true);
        }
        else
        {
            _Clear.SetActive(true);
        }
        ClearRoomCount.text = cStageMgr.GetInstance.currentStage.ToString();
        
   
    }
    public void Restrat()
    {
       Time.timeScale = 1f;
        _AudioSource.clip = _ButtonClip;
        _AudioSource.Play();
        SceneManager.LoadScene(0);
    }
    public void Admob()
    {
        _AudioSource.clip = _ButtonClip;
        _AudioSource.Play();
        _Ad.ShowRewardedAd();

       
     
    }
}
