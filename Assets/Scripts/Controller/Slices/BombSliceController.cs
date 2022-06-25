using Model.Slices;
using UnityEngine;

namespace Controller
{
    public class BombSliceController : SliceController
    {
        protected override SliceModel model { get; } = new SliceModel { Type = SliceType.Bomb };
        
        public override void RandomizeScore()
        {
            model.score = Random.Range(20, 100);
        }
    }
}