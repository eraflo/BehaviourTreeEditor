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

        private Dictionary<string, object> _data = new Dictionary<string, object>();

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

        #region Data

        public void SetData(string key, object value)
        {
            _data[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if(_data.TryGetValue(key, out value))
            {
                return value;
            }

            Node node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.Parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_data.ContainsKey(key))
            {
                _data.Remove(key);
                return true;
            }

            Node node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.Parent;
            }
            return false;
        }

        #endregion

        public NodeState State
        {
            get { return state; }
        }
    }
}
