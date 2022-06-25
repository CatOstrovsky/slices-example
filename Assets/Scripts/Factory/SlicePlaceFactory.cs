using Controller;
using Core;
using Plugins.Common;
using UnityEngine;

namespace Factory
{
    using Log = Log<SlicePlaceFactory>;
        
    public class SlicePlaceFactory : FactoryBase<SlicePlaceController>
    {
        private int _unitIndex;
        public Vector2[] placeOptions;
        public int GetMaxPlacesCount => placeOptions?.Length ?? 0;
            
        public override SlicePlaceController GetUnit()
        {
            var unit = base.GetUnit();
            if (_unitIndex <= placeOptions.Length)
            {
                unit.transform.localPosition = placeOptions[_unitIndex];
            }
            else
            {
                Log.Error($"Can not place the unit! index={_unitIndex}");
            }
            _unitIndex++;
            return unit;
        }
    }
}