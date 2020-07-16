using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDamText : MonoBehaviour
{
    public TextMesh _DamText;
    
    void Start()
    {
        Destroy(gameObject,2f);    
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 2);    
    }
    public void DisplayDamage(float PlayerDam,bool isCritical)
    {
        if (isCritical)
        {
            _DamText.text = "<color=#ff0000>" + "-" + PlayerDam + "</color>";
        }
        else
        {
            _DamText.text = "<color=#ffffff>" + "-" + PlayerDam + "</color>";
        }
        }
}
