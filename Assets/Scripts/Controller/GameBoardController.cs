using Core;
using DG.Tweening;
using Plugins.Common;
using Factories;
using Random = UnityEngine.Random;

using Log = Plugins.Common.Log<GameBoardController>;

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
        GenerateNewSlice();
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

    private void GenerateNewSlice()
    {
        model.CurrentSlice = CreateSlice();
    }

    private SliceController CreateSlice()
    {
        var slice = sliceFactory.GetUnit();
        var sliceKind = GetRandomSliceKind();
        slice.SetKind(sliceKind);
        slice.RandomizeScore();

        return slice;
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
        var currentSliceKind = model.CurrentSlice.GetKind();
        if(!slicePlaceController.HasSliceWithKind(currentSliceKind))
        {
            slicePlaceController.AddSlice(model.CurrentSlice)
                .OnComplete(() =>
                {
                    GenerateNewSlice();
                    CheckIfWin();
                });
            ;
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
