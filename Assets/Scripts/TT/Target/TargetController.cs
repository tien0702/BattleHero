using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class TargetController : MonoBehaviour
    {
        protected static List<TargetController> _targets = new List<TargetController>();

        #region Static Method

        public static Transform FindTarget(AutoFilterTargetController filter)
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

        public static Transform FindTarget(ICheckTarget[] checkTargets, ICheckBestTarget[] checkBestTargets)
        {
            LinkedList<TargetController> targets = new LinkedList<TargetController>();
            foreach (TargetController target in _targets)
            {
                for (int i = 0; i < checkTargets.Length; i++)
                {
                    if (!checkTargets[i].CheckTarget(target.transform)) continue;
                    targets.AddLast(target);
                }
            }

            TargetController result = null;
            foreach (TargetController target in targets)
            {
                if (result == null) result = target;
                for (int i = 0; i < checkBestTargets.Length; i++)
                {
                    if (!checkBestTargets[i].CheckBestTarget(result.transform, target.transform)) continue;
                    Debug.Log($"Change {result.gameObject.name} to {target.gameObject.name}");
                    result = target;
                }
            }
            return result?.transform;
        }

        public static Transform FindBestTarget(AutoFilterTargetController filter)
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

        public static List<Transform> FindTargets(AutoFilterTargetController filter, int numRequire)
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
        #endregion
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
    }
}
