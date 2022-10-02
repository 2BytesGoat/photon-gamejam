using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPunObservable {
    public float moveSpeed = 1.25f;

    public Rigidbody2D rb;
    public GameObject PlayerText;
    public Animator animator;

    private Vector2 movement;
    private bool flipped;

    private Weapon weapon;
    private PhotonView photonView;

    private Camera player_camera;

    public Camera camera {
        get { return player_camera; }
        set { player_camera = value; }
    }

    private void Awake() {
        weapon = GetComponentInChildren<Weapon>();
        photonView = GetComponent<PhotonView>();
    }
    private void Start() {
        if (PhotonNetwork.IsConnected) {
            PlayerText.GetComponent<TMPro.TextMeshPro>().text = photonView.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update() {
        if (PhotonNetwork.IsConnected && !photonView.IsMine) {
            return;
        }

        if (weapon.isAttacking) {
            movement = Vector2.zero;
            animator.SetFloat("Speed", movement.sqrMagnitude);
            return;
        }

        if (Input.GetButtonDown("Fire1")) {
            weapon.StartAttack();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        animator.SetFloat("Speed", movement.sqrMagnitude);

        flipped = movement.x < 0 || (movement.x == 0 && flipped);
    }

    private void LateUpdate() {
        if (flipped) {
            this.transform.localScale = new Vector3(-1, 1, 1);
            PlayerText.transform.localScale = new Vector3(-1, 1, 1);
        } else {
            this.transform.localScale = new Vector3(1, 1, 1);
            PlayerText.transform.localScale = new Vector3(1, 1, 1);
        }

        if (PhotonNetwork.IsConnected && !photonView.IsMine) {
            return;
        }

        Vector3 new_camera_pos = transform.position;
        new_camera_pos.z = -10;
        player_camera.transform.position = new_camera_pos;

    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(flipped);
        } else {
            flipped = (bool)stream.ReceiveNext();
        }
    }
}
