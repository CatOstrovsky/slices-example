using System.Threading.Tasks;
using Core;
using Newtonsoft.Json;
using UnityEngine;

using Log = Plugins.Common.Log<PlayerPrefsStorageService>;
public class PlayerPrefsStorageService :
    ServiceBase,
    IStorageService
{
    public override async Task Init()
    {
        Log.Info($"Fake {nameof(PlayerPrefsStorageService)} initialization delay");
        await Task.Delay(2000);
        
        await base.Init();
    }

    public bool Set<T>(string key, T value)
    {
        if(value is ModelBase)
        {
            var data = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, data);
        }
        else
        {
            Log.Error($"Looks like something wrong! Unable to save {key} setting value with type {value.GetType()}");
            return false;
        }
        
        return true;
    }

    public bool Get<T>(string key, ref T output) where T : new()
    {
        if(output is ModelBase)
        {
            var value = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                output = JsonConvert.DeserializeObject<T>(value);
            }
            else
            {
                output = new T();
            }
            
            return true;
        }
        else
        {
            Log.Error($"Looks like something wrong! Unable to save {key} setting value with type {typeof(T)}");
            return false;
        }
    }
}