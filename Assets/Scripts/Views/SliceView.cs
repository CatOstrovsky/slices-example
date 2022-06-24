using System.Threading.Tasks;
using Core;
using DG.Tweening;
using Plugins.Common;
using UnityEngine;

public class SliceView : ViewBase
{
    [SerializeField]
    public SlicesConfiguration slicesConfiguration;

    public void ApplyKind(SliceKind sliceKind)
    {
        var rotation = transform.rotation;
        var currentRotate = slicesConfiguration.rotationOnKindConfig.Find(item => item.kind == sliceKind);
        rotation = Quaternion.Euler(rotation.x, rotation.y, currentRotate.rotation);
        transform.rotation = rotation;
    }

    public Sequence MoveToPlaceAnimation(SlicePlaceController slicePlaceController)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(slicePlaceController.GetView.transform.position, .5f));
        return sequence;
    }

    public void ShowUp()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .25f);
    }
}