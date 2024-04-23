using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    [field: SerializeField] public Arena Info { private set; get; }

    #region References
    Transform[] _itemPositions;
    #endregion

    private void Awake()
    {
        var itemPositions = transform.Find("ItemPositions");
        _itemPositions = new Transform[itemPositions.childCount];
        for (int i = 0; i < itemPositions.childCount; i++)
        {
            _itemPositions[i] = itemPositions.GetChild(i).transform;
        }

        for(int i = 0; i < itemPositions.childCount;i++)
        {
            int index = UnityEngine.Random.Range(0, Info.ItemNames.Length);
            var item = Resources.Load<ItemController>("Prefabs/Items/"+Info.ItemNames[index]);
            Instantiate(item, _itemPositions[i]);
        }
    }

    void SpawnItem(int index)
    {

    }
}
