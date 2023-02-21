using UnityEngine;

namespace SimonB.Core.Utility
{
    public static class TransformExtensions
    {
        public static void Move2D(this Transform transform, Vector2 vector2) {
            transform.position += vector2.ToVector3(0);
        }
        
        
        public static void DeleteChildren(this Transform t)
        {
            foreach (Transform child in t)Object.Destroy(child.gameObject);
        }
    }
}