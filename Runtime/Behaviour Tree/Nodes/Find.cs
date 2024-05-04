using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Find : Node
    {
        private Transform _transform;
        private List<Transform> _targets;

        public Find(Transform transform, List<Transform> targets)
        {
            _transform = transform;
            _targets = targets;
        }

        public override NodeState Evaluate()
        {
            if (_targets == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            foreach (Transform target in _targets)
            {
                if (Vector3.Distance(_transform.position, target.position) < 1)
                {
                    state = NodeState.SUCCESS;
                    return state;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}