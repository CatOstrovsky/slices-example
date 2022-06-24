using System;
using System.Collections.Generic;
using UnityEngine;

public class SlicePlace : MonoBehaviour
{
    public readonly Dictionary<SliceKind, Slice> Slices = new Dictionary<SliceKind, Slice>();

    private void Start()
    {
        SetUp();
    }

    public bool IsWin()
    {
        foreach (var sliceKvp in Slices)
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
        Slices.Clear();
        foreach (SliceKind kind in Enum.GetValues(typeof(SliceKind)))
        {
            Slices.Add(kind, null);
        }
    }

    public void Clear()
    {
        foreach (var sliceKvp in Slices)
        {
            if (sliceKvp.Value != null)
            {
                Destroy(sliceKvp.Value.gameObject);
            }
        }

        SetUp();
    }

    public int GetTotalScore()
    {
        var total = 0;
        foreach (var slice in Slices)
        {
            total += slice.Value.score;
        }

        return total;
    }
}
