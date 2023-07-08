using System;
using UnityEngine;

namespace SimonB.Core.Springs
{
	[Serializable]
	public class AngleSpring : GenericDampedSpring<float>
	{
		public AngleSpring(float startPos, float frequency, float damping) : base(startPos, frequency, damping) { }

		public override void Nudge(float nudgeValue)
		{
			velocity += nudgeValue;
		}

		public override void ResetVelocity()
		{
			velocity = 0;
		}

		public override void Update(float deltaTime)
		{
			currentValue = Mathf.DeltaAngle(0, currentValue);
			goalValue = currentValue + Mathf.DeltaAngle(currentValue, goalValue);
			
			SpringMotion.CalcDampedSimpleHarmonicMotion(
				ref currentValue, ref velocity,
				goalValue, deltaTime,
				frequency, damping);
			InvokeUpdate();
		}

		public override float GetClampedValue(float min, float max)
		{
			return Mathf.Clamp(currentValue, min, max);
		}
	}

    [Serializable]
    public class FloatSpring : GenericDampedSpring<float>
    {
        public void AddPosition(float value)
        {
            goalValue += value;
        }

        public override void Update(float deltaTime)
        {
            SpringMotion.CalcDampedSimpleHarmonicMotion(
                ref currentValue, ref velocity,
                goalValue, deltaTime,
                frequency, damping);
            InvokeUpdate();
        }

        public override float GetClampedValue(float min, float max)
        {
            
            return Mathf.Clamp(currentValue, min, max);
        }

        public override void Nudge(float nudgeValue)
        {
            velocity += nudgeValue;
        }

        public override void ResetVelocity()
        {
            velocity = 0;
        }

        
        public FloatSpring(float startPos, float frequency, float damping) : base(startPos, frequency, damping)
        {
        }
    }

    [Serializable]
    public class Vector2Spring : GenericDampedSpring<Vector2>
    {

        
        public override void Update(float deltaTime)
        {
            SpringMotion.CalcDampedSimpleHarmonicMotion(
                ref currentValue, ref velocity,
                goalValue, deltaTime,
                frequency, damping);
            InvokeUpdate();
        }

        public override Vector2 GetClampedValue(float min, float max)
        {
            
            return new Vector2(
                Mathf.Clamp(currentValue.x, min, max),
                Mathf.Clamp(currentValue.y, min, max)
            );
        }

        public override void Nudge(Vector2 nudgeValue)
        {
            velocity += nudgeValue;
        }

        public override void ResetVelocity()
        {
            velocity = Vector2.zero;
        }

        public Vector2Spring(Vector2 startPos, float frequency, float damping) : base(startPos, frequency, damping)
        {
        }
    }

    [Serializable]
    public class Vector3Spring : GenericDampedSpring<Vector3>
    {

        public override void Update(float deltaTime)
        {
            SpringMotion.CalcDampedSimpleHarmonicMotion(
                ref currentValue, ref velocity,
                goalValue, deltaTime,
                frequency, damping);
            InvokeUpdate();
        }

        public override Vector3 GetClampedValue(float min, float max)
        {
            return new Vector3(
                Mathf.Clamp(currentValue.x, min, max),
                Mathf.Clamp(currentValue.y, min, max),
                Mathf.Clamp(currentValue.z, min, max)
            );
        }

        public override void Nudge(Vector3 nudgeValue)
        {
            velocity += nudgeValue;
        }

        public override void ResetVelocity()
        {
            velocity = Vector2.zero;
        }

        public Vector3Spring(Vector3 startPos, float frequency, float damping) : base(startPos, frequency, damping)
        {
        }
    }

    public abstract class GenericDampedSpring<T>
    {
        
        public float? clamped = null;
        protected T currentValue;
        protected T goalValue;
        protected T velocity;
        [SerializeField] protected float frequency = 0;
        [SerializeField] protected float damping = 0;
        
        public event Action<T> OnSpringValueUpdated;
        
        public T CurrentValue => currentValue;
        public T TargetValue => goalValue;

        public T Velocity => velocity;

        
        
        protected void InvokeUpdate()
        {
            OnSpringValueUpdated?.Invoke(currentValue);
        }

        public void SetFrequency(float frequency)
        {
            this.frequency = frequency;
        }

        public void SetDamping(float damping)
        {
            this.damping = damping;
        }
        public GenericDampedSpring(T startPos, float frequency, float damping)
        {
            currentValue = goalValue = startPos;
            this.frequency = frequency;
            this.damping = damping;
        }

        public virtual void SetCurrentValue(T value)
        {
            currentValue = value;
        }

        public virtual void SetTargetValue(T value)
        {
            goalValue = value;
        }

        public virtual void SetValueInstantly(T value)
        {
            goalValue = currentValue = value;
            ResetVelocity();
        }

        public abstract void Nudge(T nudgeValue);
        public abstract void ResetVelocity();
        public abstract void Update(float deltaTime);

        public abstract T GetClampedValue(float min, float max);

        public virtual void SetVelocity(T newVelocity)
        {
            velocity = newVelocity;
        }


    }
}