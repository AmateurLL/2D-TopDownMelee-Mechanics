using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VFX_DmgPopUp : MonoBehaviour
{
    [Header("Component Links")]
    private TextMeshPro m_textMesh;
    private Color m_textColor;

    [Space]
    [Header("Popup Text Settings")]
    [SerializeField] float floatSpeed = 2.0f;
    [SerializeField] float fadeSpeed = 2.0f;
    [SerializeField] float fadeTimer = 1.2f;
    [SerializeField] float scaleRate = 1.2f;

    float maxFadeTimer;

    public static VFX_DmgPopUp Create(Vector3 _position, float _dmg)
    {
        GameObject dmgPopUpTran = Instantiate(SYS_GameAssets.instance.m_DmgPopup, _position, Quaternion.identity);
        VFX_DmgPopUp dmgPopUp = dmgPopUpTran.GetComponent<VFX_DmgPopUp>();
        dmgPopUp.Setup(_dmg);

        return dmgPopUp;
    }

    private void Awake()
    {
        m_textMesh = transform.GetComponent<TextMeshPro>();
        m_textColor = m_textMesh.color;
        maxFadeTimer = fadeTimer;
    }

    private void Update()
    {
        // Floating up
        transform.position += new Vector3(0, floatSpeed) * Time.deltaTime;

        // Scale effect
        if(fadeTimer > maxFadeTimer * 0.5f)
        {
            // increase scale in first half of life
            transform.localScale += Vector3.one * scaleRate * Time.deltaTime;
        }
        else
        {
            // decrease scale in second half of life
            transform.localScale -= Vector3.one * scaleRate * Time.deltaTime;
        }

        // Fading
        fadeTimer -= Time.deltaTime;
        
        if(fadeTimer < 0)
        {
            m_textColor.a -= fadeSpeed;
            m_textMesh.color = m_textColor;

            if(m_textMesh.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    public void Setup(float _dmg)
    {
        m_textMesh.SetText(_dmg.ToString());
        
    }

}
