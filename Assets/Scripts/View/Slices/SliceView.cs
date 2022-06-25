using Controller;
using Core;
using DG.Tweening;
using Model.Slices;
using UnityEngine;

namespace View
{
    public class SliceView : ViewBase
    {
        [SerializeField]
        public SlicesConfiguration slicesConfiguration;

        public virtual void ApplyKind(SliceKind sliceKind)
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

        public virtual void ShowUp()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, .25f);
        }

        public virtual Sequence OnBeforeHide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .25f));
            sequence.Append(transform.DOScale(Vector3.zero, .25f));
            return sequence;
        }
    }
}