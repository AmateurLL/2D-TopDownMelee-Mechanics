using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_ParEnemy : MonoBehaviour
{

    [Header("Component Links")]
    Rigidbody2D m_RigBody;
    Transform m_DamagePopUp;

    [Space]
    [Header("Enemy Body Attributes")]
    [SerializeField] float enemyHealth = 10.0f;
    [SerializeField] float enemySpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_RigBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();    
    }

    void CheckHealth()
    {
        if (enemyHealth <= 0)
        {
            //Death animation and process win stats
            Destroy(gameObject);
        }
    }

    public void TakeDmg(float _dmg, Vector3 _position)
    {
        enemyHealth -= _dmg;

        // Damage Pop text
        VFX_DmgPopUp.Create(_position, _dmg);
    }

}
