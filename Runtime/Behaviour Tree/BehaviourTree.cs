using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class BehaviourTree : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            Transform target = GameObject.Find("Target").transform;
            Vector3 rotation = new Vector3(0, 0, 0);

            _root = new Sequence(new List<Node>
            {
                new GoTo(transform, target, 10.0f),
                new Rotate(transform, rotation, 10.0f)
            });

        }

        protected void Update()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }

        public void SetRoot(Node root)
        {
            _root = root;
        }
    }
}