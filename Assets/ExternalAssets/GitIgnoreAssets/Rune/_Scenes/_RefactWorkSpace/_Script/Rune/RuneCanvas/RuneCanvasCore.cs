using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using NoonNight.Battle.Inputs;
using NoonNight.Battle.Managers;
using NoonNight.Battle.Players;
using NoonNight.Battle.Players.InputImpls;
using NoonNight.Battle.Skill.Magics;
using NoonNight.Utilities;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NoonNight.Battle.RuneCanvas
{
    // TODO 魔法の合成の実装
    [RequireComponent(typeof(AudioSource))]
    public class RuneCanvasCore : Singleton<RuneCanvasCore>
    {
         private IInputEventProvider _input;
         private MagicGenerator _magicGenerator;
        private Queue<RuneCanvasVertex> _touchingVertices;
        private Queue<MagicType> _chargingMagics;
        private Subject<Unit> _resetCanvasStream = new Subject<Unit>();

        /// <summary>
        /// 直近2回までに触れた頂点のキュー 
        /// </summary>
        public Queue<RuneCanvasVertex> TouchingVertices => _touchingVertices;
        
        /// <summary>
        /// 表示中の辺
        /// </summary>
        public ReactiveCollection<EdgeType> EdgeInDrawing = new ReactiveCollection<EdgeType>();

        /// <summary>
        /// 魔法をそのまま使ったり合成するためキューに
        /// </summary>
        public Queue<MagicType> ChargingMagics => _chargingMagics;

        /// <summary>
        /// 魔法を生成してキャンバスをリセットするときに通知
        /// </summary>
        public IObservable<Unit> OnResetCanvas => _resetCanvasStream;


        public RuneCanvasCore()
        {
            _touchingVertices = _touchingVertices ?? new Queue<RuneCanvasVertex>(2);
            EdgeInDrawing = EdgeInDrawing ?? new ReactiveCollection<EdgeType>();
            _chargingMagics = _chargingMagics ?? new Queue<MagicType>();
        }

        private void Start()
        {
            #region 魔法出す処理
//            _input.OnRuneButtonPushed
//                .SkipLatestValueOnSubscribe()
 //               .Where(x => !x)
//                .Subscribe(x =>
//                {
//                    var rune = Edge2Rune(EdgeInDrawing);
//                    print($"Rune: {rune}");
//                    // Rune2Magic
//                    var magic = Rune2Magic(rune);
//                    print($"Magic: {magic}");
//                    _chargingMagics.Enqueue(magic);
//                    // TryDoMagic
//                    _magicGenerator.TryGenerateMagic(_chargingMagics, PlayerCore.Instance.CurrentParams.Value);
//                    _resetCanvasStream.OnNext(Unit.Default);
//                });
            #endregion

            OnResetCanvas.Subscribe(_ =>
            {
                TouchingVertices.Clear();
                EdgeInDrawing.Clear();
            });
        }

        public void UpdateTouchingVertices(RuneCanvasVertex vert)
        {
            // 同じの連続でなぞったらスキップ
            if (_touchingVertices.Count > 0 && _touchingVertices.Last() == vert)
            {
                return;
            }

            // 辺ができてるかできてないか
            if (_touchingVertices.Count < 1)
            {
                _touchingVertices.Enqueue(vert);
                return;
            }else if (_touchingVertices.Count == 1)
            {
                _touchingVertices.Enqueue(vert);
            }
            
            EdgeInDrawing.Add(Vertex2Edge(_touchingVertices.First().Vert, _touchingVertices.Last().Vert));
            _touchingVertices.Dequeue();
        }

        EdgeType Vertex2Edge(VertexType v1, VertexType v2)
        {
            var flag = v1 | v2;
            return (flag == (VertexType.A | VertexType.B)) ? EdgeType.AB :
                (flag == (VertexType.A | VertexType.C)) ? EdgeType.AC :
                (flag == (VertexType.A | VertexType.D)) ? EdgeType.AD :
                (flag == (VertexType.A | VertexType.E)) ? EdgeType.AE :
                (flag == (VertexType.A | VertexType.F)) ? EdgeType.AF :
                (flag == (VertexType.A | VertexType.G)) ? EdgeType.AG :
                (flag == (VertexType.B | VertexType.C)) ? EdgeType.BC :
                (flag == (VertexType.B | VertexType.D)) ? EdgeType.BD :
                (flag == (VertexType.B | VertexType.E)) ? EdgeType.BE :
                (flag == (VertexType.B | VertexType.F)) ? EdgeType.BF :
                (flag == (VertexType.B | VertexType.G)) ? EdgeType.BG :
                (flag == (VertexType.C | VertexType.D)) ? EdgeType.CD :
                (flag == (VertexType.C | VertexType.E)) ? EdgeType.CE :
                (flag == (VertexType.C | VertexType.F)) ? EdgeType.CF :
                (flag == (VertexType.C | VertexType.G)) ? EdgeType.CG :
                (flag == (VertexType.D | VertexType.E)) ? EdgeType.DE :
                (flag == (VertexType.D | VertexType.F)) ? EdgeType.DF :
                (flag == (VertexType.D | VertexType.G)) ? EdgeType.DG :
                (flag == (VertexType.E | VertexType.F)) ? EdgeType.EF :
                (flag == (VertexType.E | VertexType.G)) ? EdgeType.EG :
                (flag == (VertexType.F | VertexType.G)) ? EdgeType.FG :
                (EdgeType)0b0;
        }

        RuneType Edge2Rune(IEnumerable<EdgeType> edges)
        {
            EdgeType flag = 0b0;
            //edges.ForEach(e => { flag |= e; });
            foreach (var e in edges)
            {
                flag |= e;
            }

            return (flag == (EdgeType.AB | EdgeType.BD)
                    || flag == (EdgeType.CD | EdgeType.DF)
                    || flag == (EdgeType.DE | EdgeType.EG)) ? RuneType.Ken :
                (flag == (EdgeType.AD | EdgeType.DG)) ? RuneType.Is :
                (flag == (EdgeType.AG)) ? RuneType.Is :
                (flag == (EdgeType.AC | EdgeType.AD | EdgeType.DG | EdgeType.CD | EdgeType.DF | EdgeType.FG)) ? RuneType.Beorc :
                (flag == ((EdgeType.BD | EdgeType.BE | EdgeType.CD | EdgeType.CF))) ? RuneType.Eoh :
                (flag == (EdgeType.BD | EdgeType.DF | EdgeType.CD | EdgeType.CF | EdgeType.DE | EdgeType.BE)) ? RuneType.Daeg :
                (flag == (EdgeType.BF | EdgeType.CF | EdgeType.BE | EdgeType.CE)) ? RuneType.Daeg :
                (flag == (EdgeType.BF | EdgeType.CF | EdgeType.CD | EdgeType.BE | EdgeType.DE)) ? RuneType.Daeg :
                (flag == (EdgeType.BD | EdgeType.CF | EdgeType.DF | EdgeType.BE | EdgeType.CE)) ? RuneType.Daeg :
                (flag == (EdgeType.AC | EdgeType.AD | EdgeType.DG | EdgeType.EG)) ? RuneType.Yr :
                (flag == (EdgeType.AC | EdgeType.AD | EdgeType.CD | EdgeType.DG)) ? RuneType.Wynn :
                (flag == (EdgeType.AC | EdgeType.AD | EdgeType.CD | EdgeType.DG | EdgeType.DF)) ? RuneType.Rad :
                (flag == (EdgeType.AD | EdgeType.CD | EdgeType.CF)
                 || (flag) == (EdgeType.BE | EdgeType.DE | EdgeType.DG)) ? RuneType.Sigel :
                (flag == (EdgeType.AC | EdgeType.AD | EdgeType.DG)) ? RuneType.Lagu :
                RuneType.NONE;
        }

        MagicType Rune2Magic(RuneType rune)
        {
            return rune == RuneType.Ken ? MagicType.Fire :
                rune == RuneType.Is ? MagicType.Ice :
                rune == RuneType.Beorc ? MagicType.Heal :
                rune == RuneType.Eoh ? MagicType.Horse :
                rune == RuneType.Daeg ? MagicType.Sword :
                rune == RuneType.Yr ? MagicType.Shield :
                rune == RuneType.Sigel ? MagicType.FireTornado :
                rune == RuneType.Lagu ? MagicType.WaterLaser :
                rune == RuneType.Rad ? MagicType.Gear :
                rune == RuneType.Wynn ? MagicType.Card :
                MagicType.NONE;
        }
    }
}
