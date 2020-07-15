using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cPlayerHPBar : MonoBehaviour
{
    public static cPlayerHPBar GetInstance
    {
        get
        {
         
            if (_instacne == null)
            {
            
                _instacne = GameObject.FindObjectOfType(typeof(cPlayerHPBar)) as cPlayerHPBar;
            }
         
            if (_instacne == null)
            {
                
                var gameObject = new GameObject(typeof(cPlayerHPBar).ToString());
                _instacne = gameObject.AddComponent<cPlayerHPBar>();
            }
            return _instacne;
        }
    }
    //싱글톤변수
    private static cPlayerHPBar _instacne;

    public Transform cPlayer;
    public Slider _HPBar;
    public float _maxHP=1000;
    public float _CurrntHP=1000;

    public GameObject _HpLineFolder;
    public Text _PlayerHpText;
    float _UintHP = 200.0f;

    
    void Update()
    {
        transform.position = cPlayer.position;
        _HPBar.value = _CurrntHP / _maxHP;
        _PlayerHpText.text = "" + _CurrntHP;
    }
    public void GetHPBoost()
    {
        float _ScaleX = (1000f / _UintHP) / (_maxHP / _UintHP);

        _HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        foreach(Transform child in _HpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(_ScaleX, 1, 1);
        }
        _HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}
