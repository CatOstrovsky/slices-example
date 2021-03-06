using Core;
using DG.Tweening;
using Factory;
using Model;
using Model.Slices;
using Random = UnityEngine.Random;
using Log = Plugins.Common.Log<Controller.GameBoardController>;

namespace Controller
{
    public class GameBoardController : ControllerBase
    {
        private GameboardModel model = new GameboardModel();
        public override ViewBase GetView => null;

        public ScoreController _scoreController;
        public SliceFactory sliceFactory;
        public SlicePlaceFactory slicePlaceFactory;

        public void Start()
        {
            GenerateSlicePlaces();
            GenerateNewSlice(SliceType.Regular);
        }

        private void GenerateSlicePlaces()
        {
            for (int i = 0; i < slicePlaceFactory.GetMaxPlacesCount; i++)
            {
                var slicePlace = slicePlaceFactory.GetUnit();
                model.slicePlaces.Add(slicePlace);
                slicePlace.onClickAction += SetSlicePlace;
            }
        }

        private void GenerateNewSlice(SliceType? sliceType = null)
        {
            model.CurrentSlice = CreateSlice(sliceType);
        }

        private SliceController CreateSlice(SliceType? sliceType = null)
        {
            var randomizedType = sliceType ?? RandomizeSliceType();
            var slice = sliceFactory.GetUnit(randomizedType);
            var sliceKind = GetRandomSliceKind();
            slice.SetKind(sliceKind);
            slice.RandomizeScore();

            return slice;
        }

        private SliceType RandomizeSliceType()
        {
            if (Random.Range(0f, 1f) > .8f)
            {
                return SliceType.Bomb;
            }
            else
            {
                return SliceType.Regular;
            }
        }

        private SliceKind GetRandomSliceKind(int tryCount = 0)
        {
            var randomizedSliceKind = (SliceKind)Random.Range(0, 6);
            if (model.slicePlaces.Count == 0)
            {
                return randomizedSliceKind;
            }

            foreach (var slicePlace in model.slicePlaces)
            {
                if (slicePlace.HasPlaceFor(randomizedSliceKind))
                {
                    return randomizedSliceKind;
                }
            }

            tryCount++;
            if (tryCount >= 10)
            {
                Log.Error($"Unable to find a place for {randomizedSliceKind} slice kind!");
                return randomizedSliceKind;
            }
            else
            {
                return GetRandomSliceKind();
            }
        }

        public void SetSlicePlace(SlicePlaceController slicePlaceController)
        {
            var currentSliceKind = model.CurrentSlice.Kind;
            if (!slicePlaceController.HasSliceWithKind(currentSliceKind))
            {
                slicePlaceController.AddSlice(model.CurrentSlice)
                    .OnComplete(() =>
                    {
                        GenerateNewSlice();
                        CheckIfWin();
                    });
            }
        }

        private void CheckIfWin()
        {
            foreach (var slicePlace in model.slicePlaces)
            {
                if (slicePlace.IsWin())
                {
                    var totalScore = slicePlace.GetTotalScore();
                    _scoreController.AddScore(totalScore);
                    slicePlace.Win();
                }
            }
        }
    }
}