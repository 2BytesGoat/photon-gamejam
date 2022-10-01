using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDrop : MonoBehaviour, IPunObservable {
    public int SoulAmount;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(SoulAmount);
        } else {
            SoulAmount = (int)stream.ReceiveNext();
        }
    }
}
