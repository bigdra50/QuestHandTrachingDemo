using System.Collections;
using System.Collections.Generic;
using NoonNight.Battle.Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace NoonNight.Battle.RuneCanvas
{
        // TODO RuneCanvasCoreと相互依存してる
    public class RuneCanvasVertex : MonoBehaviour
    {
        public ReactiveProperty<bool> IsTouching = new ReactiveProperty<bool>(false);
        [SerializeField] private VertexType _vert;
        [SerializeField] private HaloWave _halo;
        [SerializeField] private AudioSource _audio;

         private IInputEventProvider _inputEvent;
        private float _basePitch = .74f;
        private float _addPitch = .1f;
        
        public VertexType Vert => _vert;
        //public HaloWave Halo => _halo;

        RuneCanvasVertex()
        {
            _audio = new AudioSource();
        }

        void Start()
        {
            var vert = gameObject.GetComponent<RuneCanvasVertex>();
//            IsTouching.SetValueAndForceNotify(false);

            // 頂点に手を触れた時に光らせたり音出したり
            this.OnTriggerEnterAsObservable()
                .Where(col => col.CompareTag("Finger"))
                .Subscribe(_ =>
                {
                    //IsTouching.SetValueAndForceNotify(true);
                    RuneCanvasCore.Instance.UpdateTouchingVertices(vert);
                    _halo.gameObject.SetActive(true);
                    _audio.pitch = _basePitch + _addPitch * RuneCanvasCore.Instance.EdgeInDrawing.Count;
                    _audio.Play();
                });

            // RuneCanvas非表示にした時
            //_inputEvent.OnRuneButtonPushed
            //    .SkipLatestValueOnSubscribe()
            //    .Where(x => !x)
            //    .Subscribe(_ => _halo.gameObject.SetActive(false));
        }
    }
}
