using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SimonB.Core.Utility
{ 
    public abstract class Singleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
    {
        private static T instance;
        private static readonly object instanceLock = new object();
        public static T Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null && !QuitHelper.IsQuitting)
                    {
                        instance = GameObject.FindObjectOfType<T>();
                        if (instance == null)
                        {
                            GameObject go = new GameObject(typeof(T).ToString());
                            instance = go.AddComponent<T>();
                            DontDestroyOnLoad(instance.gameObject);
                        }
                    }

                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            Init();
            if (instance == null) instance = gameObject.GetComponent<T>();
            else if (instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(gameObject);
                throw new SystemException(string.Format("Instance of {0} already exists, removing {1}", GetType().FullName, ToString()));
            }
        }
        
        protected virtual void Init() { }
    }
    
    
    
}