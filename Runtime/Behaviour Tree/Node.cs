using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        SUCCESS,
        FAILURE,
        RUNNING
    }

    public class Node
    {
        protected NodeState state;

        public Node Parent;
        protected List<Node> Children = new List<Node>();

        public Node()
        {
            state = NodeState.FAILURE;
            Parent = null;
        }

        public Node(List<Node> children)
        {
            state = NodeState.FAILURE;
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        #region Node Linking

        public void Attach(Node child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public void Detach(Node child)
        {
            child.Parent = null;
            Children.Remove(child);
        }

        #endregion

        public virtual void Reset()
        {
            state = NodeState.FAILURE;
            foreach (Node child in Children)
            {
                child.Reset();
            }
        }

        public virtual NodeState Evaluate()
        {
            return NodeState.FAILURE;
        }

        public NodeState State
        {
            get { return state; }
        }
    }
}
