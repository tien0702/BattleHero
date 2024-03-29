using SimpleJSON;
using System.Collections.Generic;
using TT;
using UnityEngine;

namespace TT
{
    public class DataService : IGameService
    {
        Dictionary<string, DataLayer> _services = new Dictionary<string, DataLayer>();

        public DataLayer GetDL(string type)
        {
            if (!_services.ContainsKey(type))
            {
                DataLayer data = new DataLayer(type);
                _services.Add(type, data);
            }
            return _services[type];
        }

        public JSONNode GetByEntityInfo(EntityInfo info)
        {
            DataLayer dl = GetDL(info.Type);

            JSONNode result = null;

            if (dl == null)
            {
                Debug.Log($"DataLayer of type {info.Type} is not exists!");
            }
            else
            {
                return dl.GetWithIndex(info.Name, info.Level - 1);
            }

            return result;
        }
    }
}
