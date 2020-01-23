using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    public List<GameObject> _creatures = new List<GameObject>();
    [SerializeField] private Material _mat;
    private float _startTime = 0;

    void Start()
    {
        _startTime = Time.timeSinceLevelLoad;
        //Destruction();
    }

    void Destruction()
    {
       // this.UpdateAsObservable()
       //     .Subscribe(_ =>
       //     {
       //         var diff = Time.timeSinceLevelLoad - _startTime;
       //         float rate = diff / 2f;

       //         _mat.SetFloat("_Distruction", Mathf.Lerp(0.25f, 4f, rate));
       //     });
        foreach (var c in _creatures)
        {
            var mats = c.GetComponentsInChildren<Renderer>();
            foreach (var mat in mats)
            {
                this.UpdateAsObservable().Subscribe(_ =>
                {

                    var diff = Time.timeSinceLevelLoad - _startTime;
                    var rate = diff / 2f;
                    mat.material.SetFloat("_Destruction", Mathf.Lerp(0f, 1f, rate));
                });
            }
        }
        
    }
}