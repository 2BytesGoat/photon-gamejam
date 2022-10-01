using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable, ILootDroppable {
    [Header("Health related")]
    [SerializeField] float _health = 3;
    [SerializeField] bool _targetable = true;
    [SerializeField] bool _invincible = false;

    [SerializeField] bool isAlive = true;
    [SerializeField] bool disableSimulation = false;
    [SerializeField] bool canTurnInvincible = false;
    [SerializeField] float invincibilityTime = 0.25f;

    [Header("Loot related")]
    [SerializeField] int lootAmount = 5;
    [SerializeField] GameObject soulDrop;

    float invincibleTimeEmplased = 0f;

    Rigidbody2D rb;
    Collider2D physicsCollider;

    public float Health {
        set {
            _health = value;
            if (_health <= 0) {
                isAlive = false;
                Targetable = false;
                OnObjectDestroyed();
            }
        }
        get { return _health; }
    }

    public bool Targetable {
        get { return _targetable; }
        set {
            _targetable = value;

            if (disableSimulation) {
                rb.simulated = false;
            }

            physicsCollider.enabled = value;
        }
    }
    public bool Invincible {
        get { return _invincible; }
        set {
            _invincible = value;
            if (_invincible) {
                invincibleTimeEmplased = 0f;
            }
        }
    }

    public void OnHit(float damage, Vector2 knockback) {
        if (!Invincible) {
            Health -= damage;

            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (canTurnInvincible) {
                Invincible = true;
            }
        }
    }

    public void OnHit(float damage) {
        if (!Invincible) {
            Health -= damage;

            if (canTurnInvincible) {
                Invincible = true;
            }
        }
    }

    public void OnObjectDestroyed() {
        if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient) {
            return;
        }

        DropLoot();
        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void DropLoot() {
        GameObject loot = PhotonNetwork.IsConnected ?
            PhotonNetwork.Instantiate(soulDrop.name, transform.position, Quaternion.identity) :
            Instantiate(soulDrop, transform.position, Quaternion.identity);
        loot.GetComponent<SoulDrop>().SoulAmount = lootAmount;
    }

    public void Start() {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    public void FixedUpdate() {
        if (Invincible) {
            invincibleTimeEmplased += Time.deltaTime;
        }
        if (invincibleTimeEmplased >= invincibilityTime) {
            Invincible = false;
        }
    }

    // TODO: move this to separate class
    private void OnTriggerEnter2D(Collider2D collision) {
        Collider2D collider = collision.GetComponent<Collider2D>();
        if (_targetable) {
            if (collider.tag.ToLower() == "weapon") {
                Weapon weapon = collider.GetComponent<Weapon>();
                Vector2 direction = (transform.position - collider.transform.position).normalized;
                Debug.Log(direction * weapon.knockbackPower);
                OnHit(weapon.damage, direction * weapon.knockbackPower);
            }
        }

    }
}
