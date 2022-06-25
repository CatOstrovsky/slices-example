using Core;
using DG.Tweening;
using Model.Slices;
using View;
using Random = UnityEngine.Random;

namespace Controller
{
    public class SliceController : ControllerBase
    {
        private SliceModel model = new SliceModel();
        private SlicePlaceController attachedPlace;

        public SliceView view;
        public override ViewBase GetView => view;

        private void Awake()
        {
            view.ShowUp();
        }

        public void SetKind(SliceKind sliceKind)
        {
            model.kind = sliceKind;
            view.ApplyKind(model.kind);
        }

        public void RandomizeScore()
        {
            model.score = Random.Range(1, 20);
        }

        public SliceKind GetKind()
        {
            return model.kind;
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
    }
}