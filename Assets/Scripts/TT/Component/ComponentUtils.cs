using System;
using UnityEngine;

namespace TT
{
    public class ComponentUtils
    {
        public static object HandleNodeType(GameObject gameObject, NodeType nodeType, Action<object> callback = null)
        {
            object obj = null;
            if (nodeType.ClassType.IsSubclassOf(typeof(Component)))
            {
                obj = gameObject.AddComponent(nodeType.ClassType);
            }
            else
            {
                obj = (object)Activator.CreateInstance(nodeType.ClassType);
            }

            if (nodeType.ClassInfoData != null)
            {
                IInfo info = (IInfo)obj;
                if (info != null)
                {
                    info.SetInfo(nodeType.ClassInfoData);
                }
            }

            if (callback != null)
            {
                callback(obj);
            }
            return obj;
        }

        public static void HandleNodes(GameObject gameObject, NodeType[] nodes, Action<object> callback = null)
        {
            foreach (NodeType node in nodes)
            {
                ComponentUtils.HandleNodeType(gameObject, node, callback);
            }
        }
    }
}
