using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BombSliceView : SliceView
    {
        public Image sprite;

        public override void ShowUp()
        {
            transform.DOShakeScale(.3f, 25);
        }

        public override Sequence OnBeforeHide()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOShakeScale(.5f, 2));
            sequence.Join(sprite.DOFade(0, .6f));
            return sequence;
        }
    }
}