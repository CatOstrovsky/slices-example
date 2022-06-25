using Core;
using DG.Tweening;
using Model.Slices;
using View;
using Random = UnityEngine.Random;

namespace Controller
{
    public class SliceController : ControllerBase
    {
        protected virtual SliceModel model { get; } = new SliceModel { Type = SliceType.Regular };
        private SlicePlaceController attachedPlace;

        public SliceView view;
        public override ViewBase GetView => view;
        public SliceType SliceType => model.Type;
        public SliceKind Kind => model.kind;

        private void Awake()
        {
            view.ShowUp();
        }

        public void SetKind(SliceKind sliceKind)
        {
            model.kind = sliceKind;
            view.ApplyKind(model.kind);
        }
        
        public virtual void RandomizeScore()
        {
            model.score = Random.Range(1, 20);
        }

        public int GetScore()
        {
            return model.score;
        }

        public Sequence MoveToPlace(SlicePlaceController slicePlaceController)
        {
            attachedPlace = slicePlaceController;
            return view.MoveToPlaceAnimation(slicePlaceController);
        }

        public override void Dispose()
        {
            view.OnBeforeHide()
                .OnComplete(() =>
                {
                    base.Dispose();
                });
        }
    }
}