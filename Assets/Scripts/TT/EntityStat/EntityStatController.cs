using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TT
{
    public class EntityStatController : BaseEntity, IInfo
    {
        protected Dictionary<string, Stat> _stats = new Dictionary<string, Stat>();

        protected virtual void Update()
        {
            foreach (KeyValuePair<string, Stat> kvp in _stats)
            {
                kvp.Value.Update(Time.deltaTime);
            }
        }

        public void SetInfo(object data)
        {
            if (data is EntityInfo)
            {
                Info = (EntityInfo)data;
                this.Level = Info.Level;
            }
        }

        public virtual void AddBonus(Bonus bonus)
        {
            if (!_stats.ContainsKey(bonus.Info.StatIDBonus))
            {
                Debug.Log(string.Format("{0} does not exists!", bonus.Info.StatIDBonus));
                return;
            }
            _stats[bonus.Info.StatIDBonus].AddBonus(bonus);
        }

        public virtual void SetStatInfos(StatInfo[] statInfos)
        {
            for (int i = 0; i < statInfos.Length; i++)
            {
                SetStatInfo(statInfos[i]);
            }
        }

        public virtual void SetStatInfo(StatInfo info)
        {
            if (!_stats.ContainsKey(info.StatID))
            {
                _stats.Add(info.StatID, new Stat());
            }

            _stats[info.StatID].Info = info;
        }

        public virtual Stat GetStatByID(string statID)
        {
            if (!_stats.ContainsKey(statID)) return null;
            return _stats[statID];
        }

        public virtual Stat[] GetStats()
        {
            return _stats.Values.ToArray();
        }

        protected override void OnLevelUp(int level)
        {
            JSONNode statData = ServiceLocator.Current.Get<DataService>().GetByEntityInfo(Info);

            StatInfo[] stats = new StatInfo[statData.AsArray.Count];

            for (int i = 0; i < statData.AsArray.Count; i++)
            {
                stats[i] = JsonUtility.FromJson<StatInfo>(statData.AsArray[i].ToString());
            }

            this.SetStatInfos(stats);
        }
    }
}
