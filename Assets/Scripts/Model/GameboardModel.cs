using System.Collections.Generic;
using Controller;

namespace Model
{
    public class GameboardModel
    {
        public List<SlicePlaceController> slicePlaces = new List<SlicePlaceController>();
        public SliceController CurrentSlice;
    }
}
