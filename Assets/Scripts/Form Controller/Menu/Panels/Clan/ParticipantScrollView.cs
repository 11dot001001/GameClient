using ClientModel.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticipantScrollView : MonoBehaviour
{
    public Text participantName;
    public Text participantPoints;
    public OtherAccount participant;

    void Start()
    {
        if (participant == null)
            throw new ArgumentNullException();
        participantName.text = participant.Nickname;
        participantPoints.text = participant.Id.ToString();//поменять на клановые очки
    }
}
