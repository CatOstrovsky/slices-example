using DG.Tweening;
using UnityEngine;

public class Slice : MonoBehaviour
{
    [Header("Slice state and stats")] public SliceKind kind;
    public SliceState state;
    public int score;

    public SlicesConfiguration slicesConfiguration;

    public void ApplyKind()
    {
        var currentRotate = slicesConfiguration.rotationOnKindConfig.Find(item => item.kind == kind);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, currentRotate.rotation);
    }


    public Sequence MoveToSlicePlace(SlicePlace slicePlace)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(slicePlace.transform.position, .5f));
        return sequence;
    }
}