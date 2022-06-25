using System;
using System.Threading.Tasks;
using Core;
using DG.Tweening;

public class SlicePlaceController : ControllerBase
{
    private SlicePlaceModel model = new SlicePlaceModel();
    public Action<SlicePlaceController> onClickAction;
    public SlicePlaceView view;
    public override ViewBase GetView => view;
    
    public bool HasSliceWithKind(SliceKind modelKind)
    {
        return model.Slices[modelKind] != null;
    }

    public Sequence AddSlice(SliceController sliceController)
    {
        model.Slices[sliceController.GetKind()] = sliceController;
        sliceController.transform.SetParent(transform);
        return sliceController.MoveToPlace(this);
    }
    
    private void Start()
    {
        SetUp();
    }

    public bool IsWin()
    {
        foreach (var sliceKvp in model.Slices)
        {
            if (sliceKvp.Value == null)
            {
                return false;
            }
        }

        return true;
    }

    private void SetUp()
    {
        model.Slices.Clear();
        foreach (SliceKind kind in Enum.GetValues(typeof(SliceKind)))
        {
            model.Slices.Add(kind, null);
        }
    }

    public void Win()
    {
        foreach (var sliceKvp in model.Slices)
        {
            if (sliceKvp.Value != null)
            {
                sliceKvp.Value.Dispose();
            }
        }

        SetUp();
    }

    public int GetTotalScore()
    {
        var total = 0;
        foreach (var slice in model.Slices)
        {
            total += slice.Value.GetScore();
        }

        return total;
    }

    public void OnClick()
    {
        onClickAction?.Invoke(this);
    }

    public bool HasPlaceFor(SliceKind sliceKind)
    {
        if (!model.Slices.ContainsKey(sliceKind))
        {
            return true;
        }
        return model.Slices[sliceKind] == null;
    }
}
