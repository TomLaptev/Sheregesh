using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{   
    [SerializeField] GameObject _effectPrefab;    
    GameObject coinEffect;  
    void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<CoinManager>().AddOne();
        gameObject.SetActive(false); 
        coinEffect = Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Invoke("DestroyEffect", 0.5f);        
    }  
   void DestroyEffect()
   {
    Destroy(coinEffect);
   }
}
