using UnityEngine;

namespace TT
{
    public interface ICheckBestTarget
    {
        public bool CheckBestTarget(Transform current, Transform next);
    }
}