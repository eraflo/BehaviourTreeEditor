using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class GoTo : Node
    {
        private Transform _transform;
        private Transform _target;

        private float _speed;

        public GoTo(Transform transform, Transform target, float speed)
        {
            _transform = transform;
            _target = target;
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            if (Vector3.Distance(_transform.position, _target.position) > 0.1f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
                _transform.LookAt(_target.position);
                state = NodeState.RUNNING;
            }
            else
            {
                state = NodeState.SUCCESS;
            }           
            
            return state;
        }

    }
}