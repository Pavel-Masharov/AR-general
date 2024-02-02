using UnityEngine;

namespace AR.Birds
{
    public class AreaTap : MonoBehaviour
    {
        [SerializeField] private TypeArea _typeArea;    
        [SerializeField] private Transform _targetPosition;

        public TypeArea GetTypeArea()
        {
            return _typeArea;
        }
        public Vector3 GetTargetPosition()
        {
            return _targetPosition.position;
        }
    }
}
