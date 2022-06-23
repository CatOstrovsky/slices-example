using System;
using System.Collections.Generic;
using UnityEngine;

public class SlicePlace : MonoBehaviour
{
    public Dictionary<SliceKind, Slice> slices = new Dictionary<SliceKind, Slice>();

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        slices.Clear();
        foreach (SliceKind kind in Enum.GetValues(typeof(SliceKind)))
        {
            slices.Add(kind, null);
        }
    }

    public void Clear()
    {
        foreach (var sliceKvp in slices)
        {
            if (sliceKvp.Value != null)
            {
                Destroy(sliceKvp.Value.gameObject);
            }
        }

        SetUp();
    }
}
