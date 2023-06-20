using UnityEngine;

namespace SimonB.Core.Utility
{
    public static class TransformExtensions
    {
        public static void Move2D(this Transform transform, Vector2 vector2) {
            transform.position += vector2.ToVector3(0);
        }

        public static void SetRotationToAngle(this Transform transform, float Angle)
        {
            transform.rotation = Angle.ToEulerAngle();
        }
        
        public static void DeleteChildren(this Transform t)
        {
            foreach (Transform child in t)Object.Destroy(child.gameObject);
        }

        public static void SetLocalPosition(this Transform t, float? x = null, float? y = null, float? z = null)
        {
            Vector3 pos = t.localPosition;
            pos.Set(x,y,z);
            t.localPosition = pos;
        }
        public static void SetPosition(this Transform t, float? x = null, float? y = null, float? z = null)
        {
            Vector3 pos = t.position;
            pos.Set(x,y,z);
            t.position = pos;
        }

        public static void SetLocalXPosition(this Transform t, float x) => t.SetLocalPosition(x: x);
        public static void SetLocalYPosition(this Transform t, float y) => t.SetLocalPosition(y: y);
        public static void SetLocalZPosition(this Transform t, float z) => t.SetLocalPosition(z: z);
        public static void SetXPosition(this Transform t, float x) => t.SetPosition(x: x);
        public static void SetYPosition(this Transform t, float y) => t.SetPosition(y: y);
        public static void SetZPosition(this Transform t, float z) => t.SetPosition(z: z);

    }
}