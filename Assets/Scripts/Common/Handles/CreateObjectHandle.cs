using TT;

[System.Serializable]
public class CreateObjectInfo
{
    public string Path;
}

public class CreateObjectHandle : BaseHandle, IInfo
{
    public CreateObjectInfo Info { private set; get; }
    public override void Handle()
    {

    }

    public override void ResetHandle()
    {

    }

    public void SetInfo(object info)
    {
        if(info is CreateObjectInfo)
        {
            Info = (CreateObjectInfo)info;
        }
    }
}
