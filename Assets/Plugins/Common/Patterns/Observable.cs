using System;

namespace Plugins.Common
{
    public class Observable<T>
    {
        private T _oldValue;
        private T _value;

        public T Value
        {
            set
            {
                _oldValue = _value;
                onBeforeChange?.Invoke(_oldValue, value);
                _value = value;
                onChange?.Invoke(_oldValue, _value);
            }
            get { return _value; }
        }

        public Action<T, T> onBeforeChange;
        public Action<T, T> onChange;
    }
}