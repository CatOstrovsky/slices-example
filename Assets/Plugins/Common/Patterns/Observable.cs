using System;
using System.Runtime.Serialization;

namespace Plugins.Common
{
    [DataContract]
    public class Observable<T>
    {
        private T _oldValue;
        private T _value;

        [DataMember]
        public T Value
        {
            set
            {
                _oldValue = _value;
                onBeforeChange?.Invoke(_oldValue, value);
                _value = value;
                onChange?.Invoke(_oldValue, _value);
                onValueChanged?.Invoke(_value);
            }
            get { return _value; }
        }

        public Action<T, T> onBeforeChange;
        public Action<T, T> onChange;
        public Action<T> onValueChanged;
    }
}