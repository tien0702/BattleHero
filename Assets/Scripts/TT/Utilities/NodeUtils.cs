using SimpleJSON;
using System;
using System.Reflection;
using UnityEngine;

namespace TT
{
    [System.Serializable]
    public class NodeInfo
    {
        public string ClassName;
        public string ClassInfoName;
    }

    public class NodeType
    {
        public Type ClassType;
        public object ClassInfoData;
    }

    public class NodeUtils
    {
        public static NodeType[] GetNodeTypes(JSONArray array)
        {
            NodeType[] result = new NodeType[array.Count];
            for (int i = 0; i < array.Count; ++i)
            {
                result[i] = GetNodeType(array[i]);
            }
            return result;
        }

        public static NodeType GetNodeType(JSONNode json)
        {
            NodeInfo nodeInfo = GetNodeInfo(json);

            if (nodeInfo == null)
            {
                Debug.LogError("NodeInfo is null");
                return null;
            }

            NodeType nodeType = new NodeType();
            Assembly assembly = Assembly.GetExecutingAssembly();
            nodeType.ClassType = assembly.GetType(nodeInfo.ClassName);
            if (nodeType.ClassType == null || nodeType.ClassType.Equals(string.Empty))
            {
                Debug.LogError($"Can't find ClassName: {nodeInfo.ClassName}!");
                return null;
            }

            if (nodeInfo.ClassInfoName != null && !nodeInfo.ClassInfoName.Equals(string.Empty))
            {
                nodeType.ClassInfoData = JsonUtility.FromJson(json["data"].ToString()
                    , assembly.GetType(nodeInfo.ClassInfoName));
            }

            return nodeType;
        }

        private static NodeInfo GetNodeInfo(JSONNode json)
        {
            NodeInfo info = JsonUtility.FromJson<NodeInfo>(json.ToString());
            if (info == null)
            {
                Debug.LogWarning("Data is null");
                return null;
            }

            return info;
        }
    }
}
