using TT;
using UnityEngine;

public class AutoRegisterService : MonoBehaviour
{
    private void Awake()
    {
        IGameService[] services = GetComponents<IGameService>();
        for(int i = 0; i < services.Length; i++)
        {
            ServiceLocator.Current.Register(services[i]);
        }
        Destroy(this);
    }
}
