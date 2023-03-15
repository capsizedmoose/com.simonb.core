using UnityEngine;

namespace SimonB.Core.Utility
{
    public static class MonoBehaviorExtensions
    {
        public static void DestroyGameObject(this MonoBehaviour monoBehaviour) {
            if(monoBehaviour == null) return;
            Object.Destroy(monoBehaviour.gameObject);
        }
    }
}