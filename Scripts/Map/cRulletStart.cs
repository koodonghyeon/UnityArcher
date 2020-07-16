using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cRulletStart : MonoBehaviour
{
    AudioSource _Audio;
    public AudioClip _Clip;

    private void Awake()
    {
        _Audio =GetComponent<AudioSource>();
        _Audio.clip = _Clip;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _Audio.Play();
            cUIController.GetInstance._RouletteGo.SetActive(true);
        }
    }
}
