using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadable:MonoBehaviour
{

    [SerializeField] private GameObject _particle;
    [SerializeField] Renderer[] _rend;
    public bool _isDead = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (_isDead)
        {
            Dead();
        }
    }


    public void Dead()
    {
        foreach (var rend in _rend)
        {
            rend.enabled = false;
        }
        _particle.gameObject.SetActive(true);
        _isDead = true;
        Destroy(gameObject, 3f);
    }
    
}
