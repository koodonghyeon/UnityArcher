using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cEnemyHpBar : MonoBehaviour
{
    public Slider _HPBar;
    public Slider _BackHPBar;
    public bool _BackHpHit = false;
    
    public Transform _Enemy;
    public float _MaxHp = 1000f;
    public float _CurrentHP = 1000f;

    void Update()
    {
        transform.position = _Enemy.position;
        _HPBar.value = Mathf.Lerp(_HPBar.value,_CurrentHP / _MaxHp,Time.deltaTime*5);
        if (_BackHpHit)
        {
            _BackHPBar.value= Mathf.Lerp(_BackHPBar.value, _CurrentHP / _MaxHp, Time.deltaTime * 10);
            if (_HPBar.value >= _BackHPBar.value - 0.01f)
            {
                _BackHpHit = false;
                _BackHPBar.value = _HPBar.value;
            }
        }
    }
    public void Dmg(float Damage)
    {
        _CurrentHP -= Damage;
        Invoke("BackHpStart", 0.5f);
    }
    void BackHpStart()
    {
        _BackHpHit = true;
    }
}
