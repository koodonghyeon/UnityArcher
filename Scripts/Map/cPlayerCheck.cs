using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayerCheck : MonoBehaviour
{
    List<GameObject> _MonsterList = new List<GameObject>();
    public bool _isPlayerInRoom = false;
    public bool _isClearRoom = false;
    
    void Update()
    {
        if (_isPlayerInRoom)
        {
            if (_MonsterList.Count <= 0 && !_isClearRoom)
            {
                _isClearRoom = true;

            }
    }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRoom = true;
            cCamera.GetInstance.SetBound(this.GetComponent<Collider>());
            cPlayerTargeting.GetInstance.MonsterList = new List<GameObject>(_MonsterList);
        }
        if (other.CompareTag("Monster"))
        {
            _MonsterList.Add(other.gameObject);
        }
     

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isPlayerInRoom = false;
            cPlayerTargeting.GetInstance.MonsterList.Clear();
        }
        if (other.CompareTag("Monster"))
        {
            _MonsterList.Remove(other.gameObject);
        }
    }
}
