using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TT
{
    public class TargetController : MonoBehaviour
    {
        protected static List<TargetController> _targets = new List<TargetController>();

        protected virtual void Awake()
        {
            if (_targets == null) _targets = new List<TargetController>();
            if (_targets.Contains(this)) return;
            _targets.Add(this);
        }

        protected virtual void OnDestroy()
        {
            if (_targets != null) _targets.Remove(this);
        }

        protected virtual void OnDisable()
        {
            if (_targets != null) _targets.Remove(this);
        }

        protected virtual void OnEnable()
        {
            if (_targets.Contains(this)) return;
            _targets.Add(this);
        }

        public static Transform FindTarget(FilterTargetController filter)
        {
            if (filter == null) return null;
            TargetController bestTarget = null;

            foreach (TargetController target in _targets)
            {
                if (target.transform.Equals(filter.transform)) continue;
                if (!filter.CheckTarget(target.transform)) continue;

                if (bestTarget == null || filter.CheckBestTarget(bestTarget.transform, target.transform))
                {
                    bestTarget = target;
                }
            }

            return bestTarget ? bestTarget.transform : null;
        }

        public static Transform FindBestTarget(FilterTargetController filter)
        {
            if (filter == null) return null;
            TargetController bestTarget = null;

            foreach (TargetController target in _targets)
            {
                if (target.transform.Equals(filter.transform)) continue;

                if (bestTarget == null || filter.CheckBestTarget(bestTarget.transform, target.transform))
                {
                    bestTarget = target;
                }
            }

            return bestTarget ? bestTarget.transform : null;
        }

        public static List<Transform> FindTargets(FilterTargetController filter, int numRequire)
        {
            if (_targets == null || _targets.Count == 0 || filter == null || numRequire == 0)
                return new List<Transform>();

            List<Transform> result = new List<Transform>();
            if (numRequire == 1)
            {
                Transform target = FindTarget(filter);

                if (target != null) result.Add(target);
                return result;
            }

            foreach (TargetController target in _targets)
            {
                if (!filter.CheckTarget(target.transform)) continue;
            }

            return result;
        }
    }
}
