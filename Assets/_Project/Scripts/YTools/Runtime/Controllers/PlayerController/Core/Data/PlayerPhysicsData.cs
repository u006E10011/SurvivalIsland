using UnityEngine;

namespace YTools
{
    [CreateAssetMenu(fileName = nameof(PlayerPhysicsData), menuName = "YTools/PlayerController/" + nameof(PlayerPhysicsData))]
    public class PlayerPhysicsData : ScriptableObject
    {
        public LayerMask Ground;
        public bool VisibleGizmos;
        public float Radius = 0.5f;
        public float Gravity = -9.8f;
    }
}