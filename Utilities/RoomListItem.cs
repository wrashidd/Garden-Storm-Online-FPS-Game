using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

/*
This class is responsible for displaying current room info
*/
public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;
    public RoomInfo _info;

    public void SetUp(RoomInfo info)
    {
        _info = info;
        text.text = info.Name;
    }

    public void OnCLick()
    {
        Launcher.Instance.JoinRoom(_info);
    }
}
