using Sirenix.OdinInspector;
using UnityEngine;

namespace ARPG.Runtime.Utilities
{
    public abstract class StaticInstance<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
    {
        public static T Instance { get; private set;}
        protected virtual void Awake() {
            Instance = this as T;
        }
        protected virtual void OnApplicationQuit() {
            Instance = null;
            Destroy(gameObject);
        }
    }

    public abstract class Singleton<T> : StaticInstance<T> where T : SerializedMonoBehaviour
    {
        protected override void Awake() {
            if(Instance != null) Destroy(gameObject);
            else base.Awake();
        }
    }
    
    
    
}