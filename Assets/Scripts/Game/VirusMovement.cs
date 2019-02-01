using UnityEngine;

namespace Assets.Scripts
{
    public class VirusMovement : MonoBehaviour
    {
        private const float _speedCorrector = Mathf.PI * 0.5F / 0.701983915256665F;
        private const float _maxScaleChangeFactor = 0.4F;
        private float _impulseSpeedChangeArgument;
        private float _impulseSpeedFactor;
        public float Speed;
        public float FrameSpeed => Speed * _impulseSpeedFactor * Time.fixedDeltaTime;
        private float MaxSpeed => 0.7176653F * _speedCorrector * Speed;

        public void Initialize(float impulseSpeedPhaze) => _impulseSpeedChangeArgument = impulseSpeedPhaze;

        private void FixedUpdate()
        {
            _impulseSpeedFactor = ImpulseSpeedChange() * Speed;
            transform.localScale = ScaleChange();
        }
        private float ImpulseSpeedChange()
        {
            int count = (int)(_impulseSpeedChangeArgument / (Mathf.PI * 0.5F));
            _impulseSpeedChangeArgument -= count * Mathf.PI * 0.5F;
            _impulseSpeedChangeArgument += Time.fixedDeltaTime;
            return Mathf.Sin(_impulseSpeedChangeArgument) * (Mathf.Cos(_impulseSpeedChangeArgument) / (_impulseSpeedChangeArgument + 0.1F)) * _speedCorrector;
        }
        private Vector2 ScaleChange() => new Vector2(1F + _maxScaleChangeFactor * _impulseSpeedFactor / MaxSpeed, 1F - _maxScaleChangeFactor * _impulseSpeedFactor / MaxSpeed);
    }
}