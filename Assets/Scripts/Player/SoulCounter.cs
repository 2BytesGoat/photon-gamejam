using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoulCounter : MonoBehaviour {
    [SerializeField]
    private string soulLootLayerName;

    [SerializeField]
    private GameObject soulSliderPrefab;

    private Slider soulSlider;
    private int souls = 0;
    private PhotonView photonView;

    // Use this for initialization
    void Start() {
        photonView = GetComponent<PhotonView>();

        if (PhotonNetwork.IsConnected && !photonView.IsMine) {
            return;
        }
        soulSlider = Instantiate(soulSliderPrefab, GameObject.Find("Canvas").transform).GetComponent<Slider>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (PhotonNetwork.IsConnected && !photonView.IsMine) {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer(soulLootLayerName)) {
            souls += collision.gameObject.GetComponent<SoulDrop>().SoulAmount;
            if (souls > Constants.MAX_SOULS) {
                souls = (int)Constants.MAX_SOULS;
            }
            soulSlider.value = souls / Constants.MAX_SOULS;

            if (PhotonNetwork.IsConnected) {
                photonView.RPC("DestroySoulDrop", RpcTarget.MasterClient, collision.gameObject.GetComponent<PhotonView>().ViewID);
            } else {
                Destroy(collision.gameObject);
            }
        }
    }

    [PunRPC]
    private void DestroySoulDrop(int viewId) {
        PhotonNetwork.Destroy(PhotonView.Find(viewId));
    }
}
