using UnityEngine;
using TT;
using TMPro;

public enum CountTimeType
{
    Cooldown, Counting
}

public class TimerController : MonoBehaviour, IGameService
{
    #region Events
    public enum TimerEvent { OnStart, OnStop, OnComplete }
    public ObserverEvents<TimerEvent, float> Events { protected set; get; }
    = new ObserverEvents<TimerEvent, float>();
    #endregion

    #region References
    [Header("References")]
    [SerializeField] protected TextMeshProUGUI _timeTxt;
    #endregion

    #region Informations
    [SerializeField] protected CountTimeType _countType;
    [SerializeField] protected float _duration;
    #endregion

    #region Get Method
    public CountTimeType CountType => _countType;
    public float Duation => _duration;
    #endregion

    float _elapsedTime;

    private void Awake()
    {
        this.SetTimer(120, CountTimeType.Cooldown);
        this.StartCounting();
    }

    protected virtual void Update()
    {
        switch (_countType)
        {
            case CountTimeType.Counting:
                _elapsedTime += Time.deltaTime;
                break;
            case CountTimeType.Cooldown:
                _elapsedTime -= Time.deltaTime;
                break;
        }
        ShowOnUI();
    }

    public virtual void SetTimer(float duration, CountTimeType countType)
    {
        this._countType = countType;
        this._duration = duration;
        _timeTxt.text = (_countType == CountTimeType.Cooldown) ? Utilities.ConvertToMM_DD(_duration) : "00:00";
    }

    public virtual void StartCounting()
    {
        Events.Notify(TimerEvent.OnStart, this._duration);
        _elapsedTime = (_countType == CountTimeType.Cooldown) ? _duration : 0;
        this.enabled = true;
    }

    public virtual void StopCounting()
    {
        Events.Notify(TimerEvent.OnStop, this._duration);
    }

    protected virtual void OnCountingCompele()
    {
        Events.Notify(TimerEvent.OnComplete, this._duration);
        this.enabled = false;
    }

    protected virtual void ShowOnUI()
    {
        _timeTxt.text = Utilities.ConvertToMM_DD(_elapsedTime);
    }
}
