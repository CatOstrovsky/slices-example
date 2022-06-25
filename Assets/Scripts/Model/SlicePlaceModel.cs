using System.Collections.Generic;
using Controller;
using Model.Slices;

namespace Model
{
    public class SlicePlaceModel
    {
        public readonly Dictionary<SliceKind, SliceController> Slices = new Dictionary<SliceKind, SliceController>();

    }
}
