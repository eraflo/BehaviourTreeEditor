using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Rotate : Node
    {
        private Transform _transform;
        private Vector3 _direction;

        private float _speed;

        public Rotate(Transform transform, Vector3 direction, float speed)
        {
            _transform = transform;
            _direction = direction;
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            Vector3 targetDir = _direction - _transform.position;
            float step = _speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(_transform.forward, targetDir, step, 0.0f);
            _transform.rotation = Quaternion.LookRotation(newDir);

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
