using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

[System.Serializable]
public class DelayHandleInfo
{
    public float DelayTime;
}

public class DelayHandle : BaseHandle, IInfo
{
    public DelayHandleInfo Info {  get; private set; }

    public override void Handle()
    {
        LeanTween.delayedCall(Info.DelayTime, () => { EndHandle(); });
    }

    public override void ResetHandle()
    {

    }

    public void SetInfo(object info)
    {
        if(info is  DelayHandleInfo)
        {
            Info = (DelayHandleInfo)info;
        }
    }
}
