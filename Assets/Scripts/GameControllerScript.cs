using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI kill_count_text;
    public int kill_count = 0;

    public GameObject player;

    public GameObject LVLpanel;
    public int lvl_points;
    public TextMeshProUGUI lvl_points_text;

    public GameObject melee;
    public GameObject ranged;

    bool finish = false;
    Vector2 target;
    void Start()
    {
        
    }
    public void Finish()
    {
        finish = true;
        target = player.transform.position + new Vector3(1.5f, 0f);
        Animator animator = player.GetComponent<Animator>();
        animator.SetTrigger("Finish");
        Invoke("LvlUp", 2f);
    }
    void LvlUp()
    {
        lvl_points += 1;
        LVLpanel.SetActive(true);
        lvl_points_text.text = "Level Points : " + lvl_points.ToString();
        Debug.Log("vll");
    }

    public void NextDay()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    public void MeleeUpgrade()
    {
        lvl_points -= 1;
        lvl_points_text.text = "Level Points : " + lvl_points.ToString();
        melee.GetComponent<PlayerMeleeScript>().dmg += 25f;

    }
    public void RangedUpgrade()
    {
        lvl_points -= 1;
        lvl_points_text.text = "Level Points : " + lvl_points.ToString();
        ranged.GetComponent<ProjectileScript>().dmg += 20f;

    }
    public void SpeedUpgrade()
    {
        lvl_points -= 1;
        lvl_points_text.text = "Level Points : " + lvl_points.ToString();
        player.GetComponent<PlayerMovementScript>().runSpeed += 5f;


    }
    public void JumpUpgrade()
    {
        lvl_points -= 1;
        lvl_points_text.text = "Level Points : " + lvl_points.ToString();
        player.GetComponent<CharacterController2D>().m_JumpForce += 100f;

    }
    // Update is called once per frame
    void Update()
    {
        kill_count_text.text = kill_count.ToString();
        if (finish)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, target, 0.5f*Time.deltaTime);
        }
    }
}
