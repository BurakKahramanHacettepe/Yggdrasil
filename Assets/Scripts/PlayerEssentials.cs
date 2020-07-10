using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerEssentials : MonoBehaviour
{
    public GameObject restartPanel;

    public PostProcessProfile postFX;
    private Animator animator;
    public float hp = 100f;
    public float cd = 1f;
    public float melee_cd = 1f;


    public Image hp_bar;
    public Image cd_bar;

    Vignette v;
    ColorGrading c;
    void Start()
    {
        animator = GetComponent<Animator>();
        postFX.TryGetSettings(out v);
        postFX.TryGetSettings(out c);
        v.color.value = Color.gray;
        c.saturation.value = 20f;


    }


    // Update is called once per frame
    void Update()
    {
        hp_bar.fillAmount = hp / 100f;
        cd_bar.fillAmount = cd / 1f;

        cd += 0.4f*Time.deltaTime;
        cd = Mathf.Clamp(cd, 0f, 1f);

        melee_cd += 2f*Time.deltaTime;
        melee_cd = Mathf.Clamp(melee_cd, 0f, 1f);
    }


    public void Hurt(float dmg)
    {
        GameObject.FindGameObjectWithTag("PlayerPain").GetComponent<AudioSource>().Play();

        GameObject.FindGameObjectWithTag("MainCamera").
        GetComponent<CameraScript>().shakeDuration = 0.3f;

        

        hp -= dmg;

        animator.SetFloat("Hp", hp);
        animator.SetTrigger("Hurt");

        if (hp <= 0)
        {
            Dead();
        }
        else
        {
            StartCoroutine("FlashRed");
        }
    }
    void Dead()
    {
        hp_bar.fillAmount = hp / 100f;
        hp = Mathf.Clamp(hp, 0f, 100f);

        GetComponent<PlayerMovementScript>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponentInChildren<CircleCollider2D>().enabled = false;


        StartCoroutine("ToBW");//BW enumator yap direk geçiş olmasın
    }

    IEnumerator ToBW()
    {
        while(c.saturation.value > -100f)
        {
            c.saturation.value = Mathf.Clamp(c.saturation.value, -100f, 20f);
            c.saturation.value -= 10f;
            yield return new WaitForSeconds(0.05f);
        }
        restartPanel.SetActive(true);



    }


    IEnumerator FlashRed()
    {
        v.color.value = Color.red;
        yield return new WaitForSeconds(0.2f);
        v.color.value = Color.gray;


    }
}
