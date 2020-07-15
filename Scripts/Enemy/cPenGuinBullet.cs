using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPenGuinBullet : cEnemyBullet
{

   protected override void Start()
    {
       
       GetComponent<Rigidbody>().AddForce(transform.forward*5f,ForceMode.Impulse);
        _Damage = 200f;
        Destroy(gameObject,3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            cPlayer.GetInstance.HIT(_Damage);
            Instantiate(cEffect.GetInstance._DuckAtkEffect, transform.position, Quaternion.Euler(90, 0, 0));
            //Destroy(gameObject);
        }
        if (collision.transform.CompareTag("Wall"))
        {
            Instantiate(cEffect.GetInstance._DuckAtkEffect, transform.position, Quaternion.Euler(90, 0, 0));
           // Destroy(gameObject);
        }
    }
}
