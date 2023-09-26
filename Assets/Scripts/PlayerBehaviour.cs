using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public RoadGenerator RoadGenerator;
    [SerializeField] public PlayerController PlayerController;
    [SerializeField] public GameManager GameManager;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _effectPrefab;
    GameObject ShatterEffect;
    [SerializeField] AudioSource _soundCollision;


    public void StartFatalCollision()
    {
        RoadGenerator.currentSpeed = 0;
        _animator.SetTrigger("Drunk Idle");
        GameManager._soundTheme.Stop();
        _soundCollision.Play();
        ShatterEffect = Instantiate(_effectPrefab, _effectPrefab.transform.position, transform.rotation);
        StartCoroutine(StopFallingCoroutine());
    }

    IEnumerator StopFallingCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.DecrementLifesPlayer();
        Destroy(ShatterEffect);
    }
}
