using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayerWeaPon : MonoBehaviour
{
    public float _Damage;
    void Start()
    {
        _Damage = cPlayer.GetInstance._AttaackDamage;
        GetComponent<Rigidbody>().AddForce(transform.up *50, ForceMode.Impulse);

    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Wall") || other.transform.CompareTag("Monster"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(gameObject, 0.2f);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Monster"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(gameObject, 0.2f);
        }   
    }
}
