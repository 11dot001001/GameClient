using ClientModel.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendScrollView : MonoBehaviour
{
    public Text friendName;
    public OtherAccount friend;

    void Start()
    {
        if (friend == null)
            throw new ArgumentNullException();
        friendName.text = friend.Nickname;
    }
}
