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
    public GameObject[] to_disable;
    public int kill_count = 0;

    public GameObject player;
    public GameObject player_stats;





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
        player.GetComponent<Animator>().SetTrigger("Finish");
        foreach (GameObject item in to_disable)
        {
            item.SetActive(false);
        }
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
        if (lvl_points >= 1f)
        {
            lvl_points -= 1;
            lvl_points_text.text = "Level Points : " + lvl_points.ToString();
            melee.GetComponent<PlayerMeleeScript>().dmg += 25f;
        }
        

    }
    public void RangedUpgrade()
    {
        if (lvl_points >= 1f)
        {
            lvl_points -= 1;
            lvl_points_text.text = "Level Points : " + lvl_points.ToString();
            ranged.GetComponent<ProjectileScript>().dmg += 20f;
        }
    }
    public void SpeedUpgrade()
    {
        if (lvl_points >= 1f)
        {
            lvl_points -= 1;
            lvl_points_text.text = "Level Points : " + lvl_points.ToString();
            player_stats.GetComponent<PlayerStatsScript>().runSpeed += 5f;
        }

    }
    public void JumpUpgrade()
    {
        if (lvl_points >= 1f)
        {
            lvl_points -= 1;
            lvl_points_text.text = "Level Points : " + lvl_points.ToString();
            player_stats.GetComponent<PlayerStatsScript>().jumpForce += 100f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        kill_count_text.text = "Score: "+kill_count.ToString();
        if (finish)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, target, 0.5f*Time.deltaTime);
        }
    }
}
