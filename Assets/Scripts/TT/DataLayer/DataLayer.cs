using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace TT
{
    public class DataLayer
    {
        /// <summary>
        /// All text files in a folder named 'path'
        /// </summary>
        Dictionary<string, JSONNode> _datas = new Dictionary<string, JSONNode>();
        protected string _resourcesName;

        public DataLayer(string name)
        {
            this._resourcesName = name;
            this.LoadFromResources();
        }

        protected virtual void LoadFromResources()
        {
            string dataPath = string.Format($"Data/{_resourcesName}/");
            TextAsset[] datas = Resources.LoadAll<TextAsset>(dataPath);
            foreach (var data in datas)
            {
                JSONNode node = JSONNode.Parse(data.text);
                _datas.Add(data.name, node);
            }
        }

        public virtual JSONNode Get(string name)
        {
            if (_datas.ContainsKey(name))
                return _datas[name];
            else
                return null;
        }

        public virtual JSONNode GetWithIndex(string name, int index)
        {
            JSONNode result = null;
            if (!_datas.ContainsKey(name))
            {
                Debug.Log($"Data of name {name} is not Exists!");
            }
            else if (!_datas[name].IsArray)
            {
                Debug.Log($"Data of name {name} is not Array!");
            }
            else
            {
                JSONArray array = _datas[name].AsArray;

                if (index < 0 || index >= array.Count)
                {
                    Debug.Log($"{name}, Invalid index!");
                }
                else
                {
                    result = array[index];
                }
            }

            return result;
        }
    }
}
