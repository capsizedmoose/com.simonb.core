using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimonB.Core.Utility
{
    public static class Helper
    {
        
        public static Camera _camera;

        public static Camera Camera
        {
            get
            {
                if(_camera==null)_camera = Camera.main;
                return _camera;
            }
        }

        public static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }

        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _results;

        public static bool IsOverUI()
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition,_results);
            return _results.Count > 0;
        }

        public static Bounds GetScreenBounds(float padding = 0)
        {
            var vertExtent = Camera.orthographicSize;    
            var horzExtent = vertExtent * Camera.aspect;
            
            var size = new Vector2(horzExtent * 2 - padding, vertExtent * 2 - padding);
            return new Bounds(Camera.transform.position.ToVector2(), size);
        }
        public static Bounds GetScrollBounds()
        {
            var vertExtent = Camera.orthographicSize;    
            var horzExtent = vertExtent * Camera.aspect;
            
            var size = new Vector2(horzExtent * 2 - 15, vertExtent * 2 - 15);
            return new Bounds(Camera.transform.position.ToVector2(), size);
        }
        
        public static Vector2 GetMousePos()
        {
            return Camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public static T GetRandom<T>(this List<T> list)
        {
            if (list.Count == 0) { throw new System.InvalidOperationException("List is empty"); }
            return list[Random.Range(0, list.Count)];
        }
        public static T GetRandom<T>(this T[] array)
        {
            if (array.Length == 0) { throw new System.InvalidOperationException("Array is empty"); }
            return array[Random.Range(0, array.Length)];
        }

        
        public static Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 end, Vector2 point)
        {
            //Get heading
            Vector2 heading = (end - origin);
            float magnitudeMax = heading.magnitude;
            heading.Normalize();

            //Do projection from the point but clamp it
            Vector2 lhs = point - origin;
            float dotP = Vector2.Dot(lhs, heading);
            dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
            return origin + heading * dotP;
        }
    }
}