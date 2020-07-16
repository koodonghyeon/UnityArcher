using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    Rigidbody _RigidBody;
    public float Damage = 300f;
   public AudioSource _Audio;
   public AudioClip _Clip;
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
        _RigidBody.velocity = transform.forward * 10;
        _Audio = GetComponent<AudioSource>();
        _Audio.clip = _Clip;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            _Audio.Play();
            //if(!_Audio.isPlaying)
            Destroy(gameObject, 0.3f);
        }
        if (collision.transform.CompareTag("Player"))
        {
            _Audio.Play();
            cPlayer.GetInstance.HIT(Damage, 1);
          //  if (!_Audio.isPlaying)
                Destroy(gameObject, 0.3f);
        }
    }

}
