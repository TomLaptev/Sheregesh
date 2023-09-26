using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStones : MonoBehaviour
{
    bool flagCollision;
    void OnTriggerEnter(Collider other)
    {
        if (!flagCollision)
        {
          flagCollision = true;  
          FindObjectOfType<PlayerBehaviour>().StartFatalCollision(); 
        StartCoroutine(StopColligenCoroutine());  
        }
        
    }

    IEnumerator StopColligenCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        flagCollision = false;
    }
}
