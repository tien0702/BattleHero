using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class ObserverEvents<EventType, DataType>
    {
        Dictionary<EventType, HashSet<Action<DataType>>> _events;

        public ObserverEvents()
        {
            _events = new Dictionary<EventType, HashSet<Action<DataType>>>();
        }

        public virtual void Clear()
        {
            _events.Clear();
        }

        public virtual void ClearType(EventType type)
        {
            if (_events.ContainsKey(type)) _events[type].Clear();
        }

        public virtual void RegisterEvent(EventType eventType, Action<DataType> observer)
        {
            if (!_events.ContainsKey(eventType))
            {
                _events.Add(eventType, new HashSet<Action<DataType>>());
            }
            _events[eventType].Add(observer);
        }

        public virtual void UnRegisterEvent(EventType eventType, Action<DataType> observer)
        {
            if (!_events.ContainsKey(eventType)) return;

            _events[eventType].Remove(observer);
        }

        public virtual void Notify(EventType eventType, DataType data)
        {
            if (!_events.ContainsKey(eventType)) return;

            HashSet<Action<DataType>> observers = _events[eventType];
            foreach (Action<DataType> observer in observers)
            {
                if(observer != null) 
                    observer(data);
            }
        }
    }
}
