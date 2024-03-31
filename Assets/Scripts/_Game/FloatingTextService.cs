using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;
using DamageNumbersPro;

public class FloatingTextService : MonoBehaviour, IGameService
{
    [SerializeField] DamageNumberMesh[] _texts;

    Dictionary<string, DamageNumberMesh> _textDictionary = new Dictionary<string, DamageNumberMesh>();

    private void Awake()
    {
        if (_texts != null)
        {
            foreach (DamageNumberMesh text in _texts)
            {
                _textDictionary.Add(text.name, text);
            }
        }

        ServiceLocator.Current.Register(this);
    }

    public DamageNumber GetByName(string name)
    {
        if(_textDictionary.ContainsKey(name)) return _textDictionary[name];
        return null;
    }
}
