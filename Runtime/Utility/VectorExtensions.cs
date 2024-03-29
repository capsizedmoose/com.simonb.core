﻿using UnityEngine;

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
        
        public static Vector3 Flat(this Vector3 v, float depth = 0f) {
            return new Vector3(v.x, depth, v.z);
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

        
        public static Vector2 ToDirectionVector(this float degrees) {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
            return new Vector2(cos, sin);
        }
        
        public static Vector2 SetMagnitude(this Vector2 v, float newMagnitude) {
            v = v.normalized * newMagnitude;
            return v;
        }

        public static Vector3 SetMagnitude(this Vector3 v, float newMagnitude) {
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

        public static Quaternion ToEulerAngle(this Vector2 vec) {
            return ToEulerAngle(vec.ToSignedAngle());
        }
        public static Quaternion ToEulerAngle(this float angle)
        {
            return Quaternion.Euler(0, 0, angle);
        }
        public static float RandomBetween(this Vector2 vec)
        {
            return Random.Range(vec.x, vec.y);
        }

        public static Vector2 ToNormalVector(this float angle)
        {
            float radian = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        #region Vector2 SET
        public static void SetX(this ref Vector2 v, float x) => v.Set(x, v.y);
        public static void SetY(this ref Vector2 v, float y) => v.Set(v.x, y);
        public static void Set(this ref Vector2 v, float? x = null, float? y = null)
        {
            v.Set(
                x.GetValueOrDefault(v.x),
                y.GetValueOrDefault(v.y)
            );
        }
        #endregion
        
        
        #region Vector3 SET
        public static void Set(this ref Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            v.Set(
                x.GetValueOrDefault(v.x),
                y.GetValueOrDefault(v.y),
                z.GetValueOrDefault(v.z)
            );
        }
        public static void SetX(this ref Vector3 v, float x) => v.Set(x, v.y, v.z);
        public static void SetY(this ref Vector3 v, float y) => v.Set(v.x, y, v.z);
        public static void SetZ(this ref Vector3 v, float z) => v.Set(v.x, v.y, z);
        
        #endregion

        
        
        public static bool IsInside(this Vector2Int a, Vector2Int size)
        {
            return (a.x > 0 && a.x <= size.y && a.y > 0 && a.y < size.y);
        }

        public static bool IsOutside(this Vector2Int a, Vector2Int size)
        {
            return (a.x < 0 || a.x > size.y || a.y < 0 || a.y > size.y);
        }

        public static Vector2Int Expand(this Vector2Int a, int expandBy)
        {
            return a + new Vector2Int(Mathf.Max(0,a.x +expandBy), Mathf.Max(0,a.y +expandBy));
        }

        public static Vector2Int Shrink(this Vector2Int a, int shrinkBy)
        {
            return a.Expand(-shrinkBy);
        }
        
        
    }
}