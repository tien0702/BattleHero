using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamSlotController : MonoBehaviour
{
    [SerializeField] Image _heroAvt, _playerAvt, _selectImage;
    [SerializeField] TextMeshProUGUI _playerName, _heroLv;
    [SerializeField] GameObject _userLayout, _botLayout;

    public User UserInfo { private set; get; }

    private void OnEnable()
    {
        _botLayout.SetActive(true);
        _userLayout.SetActive(false);
    }

    private void OnDisable()
    {
        _botLayout.SetActive(true);
        _userLayout.SetActive(false);
        UserInfo = null;
    }

    public void SetPlayer(HeroInfo heroInfo, User user)
    {
        _playerName.text = user.NickName;
        _botLayout.SetActive(false);
        _userLayout.SetActive(true);
    }
}
