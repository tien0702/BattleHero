using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TeamSlotInfomation
{
    public string PlayerName;
    public byte AvatarID;
    public string HeroName;
    public byte HeroLevel;
}

public class TeamSlotController : MonoBehaviour
{
    public string PlayerName { get; private set; }
    [SerializeField] Image _heroAvt, _playerAvt, _selectImage;
    [SerializeField] TextMeshProUGUI _playerName, _heroLv;
    [SerializeField] GameObject _userLayout, _botLayout;

    public void OnEnable()
    {
        _botLayout.SetActive(true);
        _userLayout.SetActive(false);
    }

    public void OnDisable()
    {
        _botLayout.SetActive(true);
        _userLayout.SetActive(false);
    }

    public void UpdateTeamSlotInfomation(string playerName, string avtName, string heroName, byte heroLevel)
    {
        if (playerName == null || playerName.Equals(string.Empty))
        {
            this.CleanInfomation();
        }
        else
        {
            this.PlayerName = playerName;
            _playerName.text = playerName;
            _heroLv.text = heroLevel.ToString();
            _botLayout.SetActive(false);
            _userLayout.SetActive(true);
        }
    }

    public void CleanInfomation()
    {
        PlayerName = null;
        _botLayout.SetActive(true);
        _userLayout.SetActive(false);
    }
}
