using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

/*
This class is responsible for players join/leave logic
*/
public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text text;
    private Player _player;

    public void SetUp(Player player)
    {
        _player = player;
        text.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (_player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
