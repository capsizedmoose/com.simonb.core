using UnityEngine;

namespace SimonB.Core.Utility
{
    public static class VectorExtensions
    {
        public static Vector2 ToVector2(this Vector3 vector3, bool flipYZ = false)
        {
            return new Vector2(vector3.x, flipYZ ? vector3.z : vector3.y);
        }

        public static Vector3 ToVector3(this Vector2 vector2, float depth = 0, bool flipYZ = false)
        {
            return new Vector3(vector2.x, flipYZ ? depth : vector2.y, flipYZ ? vector2.y : depth);
        }

        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }

        public static Vector2 SetMagnitude(this Vector2 v, float newMagnitude) {
            v = v.normalized * newMagnitude;
            return v;
        }
        public static float ToAngle(this Vector2 vector2) {
            return Vector2.Angle(Vector2.right, vector2.normalized);
        }
        public static float ToSignedAngle(this Vector2 vector2)
        {
            return Vector2.SignedAngle(Vector2.right, vector2.normalized);
        }

        public static Quaternion ToEulerAngle(this Vector2 vec)
        {
            return Quaternion.Euler(0, 0, vec.ToSignedAngle());
        }
        
        public static float RandomBetween(this Vector2 vec)
        {
            return Random.Range(vec.x, vec.y);
        }

    }
}