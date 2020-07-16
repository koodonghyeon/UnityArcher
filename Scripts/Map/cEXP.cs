using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cEXP : MonoBehaviour
{
    GameObject _Player;
    
    void Start()
    {
        _Player = cPlayerData.GetInstance.Player;
        StartCoroutine(WaitClear());
    }

    IEnumerator WaitClear()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            while (transform.parent.GetChild(0).GetComponent<cPlayerCheck>()._isClearRoom)
            {
               
             transform.position = Vector3.Lerp(transform.position, _Player.transform.position, 0.07f);
                yield return null;
            }
        }
    }
}
