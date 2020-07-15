using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Boom : cEnemyBullet
{
    Rigidbody _Rb;
  
    Collider _Ex;
    GameObject _Boom;
    float Time=3;
    Text _Timer;
    bool _isStart = false;

   protected override void Start ( )
    {
        _Damage = 100;
        _Boom = transform.GetChild(0).gameObject;
        _Rb = transform.parent.GetComponent<Rigidbody> ( );
        _Timer = transform.parent.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        _Timer.transform.parent.transform.rotation= Quaternion.Euler(90, 0, 90);
        _Ex = transform.GetChild(1).GetComponent<Collider>();
        _Rb.velocity = transform.forward * 10;
        Time = 3;
        _Ex.gameObject.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Wall") || other.transform.CompareTag("Player"))
        {
            if (!_isStart)
            {
                _isStart = true;
                _Timer.text = Time.ToString();
                _Rb.velocity = Vector3.zero;
                Destroy(this.transform.parent.gameObject, 6.0f);
                StartCoroutine(StartBomb());
            }
        }

        if (_Ex.gameObject.activeSelf)
        {
            if (other.transform.CompareTag("Player"))
            {
                cPlayer.GetInstance.HIT(_Damage);
       

            }
        }
        }
    IEnumerator StartBomb()
    {
        yield return new WaitForSeconds(1.0f);
        Time -= 1;
        _Timer.text = Time.ToString();
        yield return new WaitForSeconds(1.0f);
        Time -= 1;
        _Timer.text = Time.ToString();
        yield return new WaitForSeconds(1.0f);
        _Boom.SetActive(false);
        _Ex.gameObject.SetActive(true);
        _Timer.text = " ";
        Instantiate(cEffect.GetInstance._BombEffect, transform.position, Quaternion.Euler(90, 0, 0));


    }
}