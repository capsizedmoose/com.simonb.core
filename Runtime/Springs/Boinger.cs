using UnityEngine;

namespace SimonB.Core.Springs
{
    public class Boinger : MonoBehaviour
    {
        public FloatSpring spring;
        public float scaleCap = 0.9f;

        public void Boing(float boing)
        {
            spring.SetCurrentValue(boing);
        }
        void Update()
        {
            spring.Update(Time.unscaledDeltaTime);
            float scaler = spring.CurrentValue;
            scaler = Mathf.Clamp(scaler, -scaleCap, scaleCap);
            Vector3 newScale = new Vector3(1 + scaler, 1 - scaler, 1);
            transform.localScale = newScale; //wow
        }
    }
}
