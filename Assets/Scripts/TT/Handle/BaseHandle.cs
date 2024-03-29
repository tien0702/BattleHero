using System;

namespace TT
{
    public abstract class BaseHandle : IHandle
    {
        protected Action<IHandle> _onEndHandle;
        public Action<IHandle> OnEndHandle { set => _onEndHandle = value; }

        public abstract void Handle();

        public abstract void ResetHandle();

        protected virtual void EndHandle()
        {
            if (_onEndHandle != null)
            {
                _onEndHandle(this);
            }
        }
    }
}