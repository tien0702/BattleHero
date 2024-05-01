using SimpleJSON;
using System;
using TT;
using System.Collections.Generic;

public class BattleEffectUtils
{
    public static IBattleEffect[] GetEffectsText(string data)
    {
        JSONNode json = JSONObject.Parse(data);
        if (json == null || !json.IsArray) return null;

        JSONArray array = json.AsArray;
        var nodes = NodeUtils.GetNodeTypes(array);
        List<IBattleEffect> result = new List<IBattleEffect>();
        for(int i = 0; i < nodes.Length; ++i)
        {
            object obj = (object)Activator.CreateInstance(nodes[i].ClassType);

            if (nodes[i].ClassInfoData != null)
            {
                IInfo info = (IInfo)obj;
                if (info != null)
                {
                    info.SetInfo(nodes[i].ClassInfoData);
                }
            }

            if (obj is IBattleEffect)
            {
                result.Add(obj as IBattleEffect);
            }
        }

        return result.ToArray();
    }
}
