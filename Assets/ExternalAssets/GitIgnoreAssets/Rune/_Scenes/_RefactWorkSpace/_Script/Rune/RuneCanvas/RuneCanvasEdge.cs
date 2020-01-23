using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace NoonNight.Battle.RuneCanvas
{
    public class RuneCanvasEdge : MonoBehaviour
    {
        public List<LineRenderer> linesInDrawing;
        [SerializeField] private Material _lineMaterial;

        void Start()
        {
            RuneCanvasCore.Instance.EdgeInDrawing
                .ObserveAdd()
                .Subscribe(_ =>
                {
                    SetLinePosition();
                });

            RuneCanvasCore.Instance.OnResetCanvas
                .Subscribe(_ =>
                {
                    linesInDrawing.ForEach(l => Destroy(l.gameObject));
                    linesInDrawing.Clear();
                });

        }

        private void SetLinePosition()
        {
            var line = new GameObject("Line").AddComponent<LineRenderer>();
            line.transform.SetParent(this.transform);
            line.startWidth = .105f;
            line.endWidth = .105f;
            line.material = _lineMaterial;
            line.SetPosition(0, RuneCanvasCore.Instance.TouchingVertices.First().transform.position);
            line.SetPosition(1, RuneCanvasCore.Instance.TouchingVertices.Last().transform.position);
            linesInDrawing.Add(line);
            
        }
    }
}
