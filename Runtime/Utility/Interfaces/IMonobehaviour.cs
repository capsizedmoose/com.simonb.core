using UnityEngine;

namespace SimonB.Core.Utility.Interfaces
{
	public interface IMonoBehavior{}
	public static class IMonoBehaviorExtensions
	{
		public static Transform GetTransform(this IMonoBehavior user) {
			if (user is MonoBehaviour mono)
				return mono.transform;
			return null;
		}
		public static GameObject GetGameObject(this IMonoBehavior user) {
			if (user is MonoBehaviour mono)
				return mono.gameObject;
			return null;
		}
		public static MonoBehaviour GetMonoBehaviour(this IMonoBehavior user) {
			if (user is MonoBehaviour mono)
				return mono;
			return null;
		}
	}
}