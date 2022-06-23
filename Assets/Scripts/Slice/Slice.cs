using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    [Header("Slice state and stats")]
    public SliceKind kind ;
    public SliceState state;
    public int score;

    public SlicesConfiguration slicesConfiguration;
}