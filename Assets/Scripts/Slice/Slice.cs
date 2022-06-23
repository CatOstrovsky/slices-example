using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    public SliceKind kind;
    public SliceState state;
    public int score;

    public List<SliceRule> rotationOnKindConfig = new List<SliceRule>
    {
        new SliceRule(SliceKind.TypeA, 0),
        new SliceRule(SliceKind.TypeB, 60),
        new SliceRule(SliceKind.TypeC, 120),
        new SliceRule(SliceKind.TypeD, 180),
        new SliceRule(SliceKind.TypeE, 240)
    };
}