using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TT;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string EffectaName;
    public BonusInfo[] BonusInfos;
}

public class ItemController : MonoBehaviour
{
    public GameObject ItemEffect;
    public BonusInfo BonusInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<EntityStatController>(out var entityStat))
        {
            entityStat.AddBonus(new Bonus(BonusInfo));
            float time = BonusInfo.Duration;
            var effect = Instantiate(ItemEffect, other.transform.parent);
            LeanTween.delayedCall(time, () =>
            {
                Destroy(effect.gameObject);
            });
            Destroy(gameObject);
        }
    }
}
