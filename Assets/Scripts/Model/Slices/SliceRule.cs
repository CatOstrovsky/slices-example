using System;

[Serializable]
public class SliceRule
{
    public SliceKind kind;
    public int rotation;

    public SliceRule(SliceKind kind, int rotation)
    {
        this.kind = kind;
        this.rotation = rotation;
    }
}
