using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class EntityController : BaseEntity
{
    protected override void OnLevelUp(int level)
    {
        var levelData = ServiceLocator.Current.Get<DataService>().GetByEntityInfo(this.Info);
        NodeType[] nodes = NodeUtils.GetNodeTypes(levelData.AsArray);
        ComponentUtils.HandleNodes(gameObject, nodes, OnAddChild);
    }
}
