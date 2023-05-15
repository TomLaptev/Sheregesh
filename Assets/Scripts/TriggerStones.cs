using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStones : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<PlayerBehaviour>().StartFatalCollision(); 
        StartCoroutine(StopColligenCoroutine());
    }

    IEnumerator StopColligenCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
