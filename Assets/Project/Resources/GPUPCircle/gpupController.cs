using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gpupController : MonoBehaviour
{
    private float startTime;
    private Material mat;
    
    void Start()
    {
        startTime = Time.timeSinceLevelLoad;
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        var diff = Time.timeSinceLevelLoad - startTime;
        float rate = diff / 2f;
        
        mat.SetFloat("_Radius", Mathf.Lerp(0.25f, 4f, rate));

        if (rate >= 1f)
        {
            //Destroy(this.gameObject);
        }
    }
}
