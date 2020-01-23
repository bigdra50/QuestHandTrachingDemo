using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    public Subject<KeyCode> GetKeyStream = new Subject<KeyCode>();

    void Start()
    {
        var updateStream = this.UpdateAsObservable();
        
        var wKeyStream = updateStream
            .Where(_ => Input.GetKey(KeyCode.W))
            .Do(_ => print("w"));
        var qKeyStream = updateStream
            .Where(_ => Input.GetKey(KeyCode.Q))
            .Do(_ => print("q"));

        wKeyStream.Subscribe(_ => GetKeyStream.OnNext(KeyCode.W));
        qKeyStream.Subscribe(_ => GetKeyStream.OnNext(KeyCode.Q));

        GetKeyStream.TakeUntilDestroy(this)
            .DistinctUntilChanged()
            .Buffer(2, 1)
            .Where(key => key[0] == KeyCode.W && key[1] == KeyCode.Q)
            .Subscribe(_ => { print("wq"); });

//        wKeyStream.Merge(qKeyStream, (w, q) => "")
//            .First()
//            .RepeatUntilDestroy(this.gameObject)
//            .Subscribe(_ => print("wq"));
    }
}