using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBossCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cUIController.GetInstance.CheckBossRoom(true);
            cUIController.GetInstance._BossRoom = true;
            cCamera.GetInstance.SetBackGround();
        }
    }
}
