using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_PlayerChar : MonoBehaviour
{
    [Header("Component Links")]
    public Transform m_AttackPos;
    public LayerMask m_EnemyDetection;
    Rigidbody2D m_RigBody;

    [Space]
    [Header("Character Body Attributes")]
    [SerializeField] float playerHealth = 100.0f;
    [SerializeField] float playerSpeed = 1.0f;

    [Space]
    [Header("Character Body Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDamage = 8.0f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float attackAreaX = 1.0f;
    [SerializeField] float attackAreaY = 1.0f;

    private float attackSpeed = 0.4f;
    private float attackSpeedCoolDown;
    private float moveHori;
    private float moveVert;
    private Vector2 playerDirection;

    // Start is called before the first frame update
    void Start()
    {
        m_RigBody = GetComponent<Rigidbody2D>();
        //m_AttackPos = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovementControl();
        Attack();
        Aim();
    }

    void CharacterMovementControl()
    {
        moveHori = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(moveHori, moveVert);
        movementSpeed = Mathf.Clamp(playerDirection.magnitude, 0.0f, 1.0f);
        playerDirection.Normalize();

        m_RigBody.velocity = playerDirection * movementSpeed * playerSpeed;
    }

    void Attack()
    {
        if(attackSpeedCoolDown <= 0)
        {
            // Button 1 - Normal attack
            if (Input.GetMouseButtonDown(0))
            {
                // Run through objects in specific layer in collision 
                Collider2D[] detectedEnemies = Physics2D.OverlapBoxAll(m_AttackPos.position, new Vector2(attackAreaX, attackAreaY), 0.0f, m_EnemyDetection);
                for(int i = 0; i < detectedEnemies.Length; i++)
                {
                    detectedEnemies[i].GetComponent<CSS_ParEnemy>().TakeDmg(attackDamage, m_AttackPos.transform.position);
                }

                // Reset attack
                attackSpeedCoolDown = attackSpeed;
                //Debug.Log("Attack");
            }
        }
        else
        {
            // cant attack
            attackSpeedCoolDown -= Time.deltaTime;
        }
    }

    void Aim()
    {
        if (playerDirection != Vector2.zero)
        {
            m_AttackPos.transform.localPosition = playerDirection * attackRange;
        }
    }

    // Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_AttackPos.position, new Vector2(attackAreaX, attackAreaY));
    }


}
