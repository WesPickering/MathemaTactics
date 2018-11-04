using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {
    public int total_accrued_points;
    public int block;
    public int attack;
    public int points;
    public int enemyAttack;
    public bool playerTurn;
    public GameObject enemyType;
    private GameObject enemy;
    public GameObject player;
    public EnemyController enemyScript;
    public PlayerController playerScript;
    public GameObject attack_text;
    public GameObject block_text;

    public GameObject healthText;
    public Slider healthSlider;
    public GameObject enemyHealthText;
    public GameObject enemyHealthSlider;
    public GameObject enemyAttackText;

    public GameObject game;
    public GameManager gameScript;

    public GameObject total_points_text;

    public GameObject canvas;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    private object timer_text;


    private GameObject enemyhealthCanvas;
    GameObject enemyHPSlider;
    GameObject enemyIntention;

    public GameObject playerHPSlider;

    public AudioButtonController sfx; 


    // Use this for initialization
    void Start () {
        playerTurn = true;
        enemy = Instantiate(enemyType);
        enemyScript = enemy.GetComponent<EnemyController>();
        points = total_accrued_points;

        enemyAttack = enemyScript.enemyAttack();
        enemyhealthCanvas = enemy.transform.Find("healthCanvas").gameObject;
        if (enemyhealthCanvas != null) {
            enemyHPSlider = enemyhealthCanvas.transform.Find("Slider").gameObject;
            enemyIntention = enemyhealthCanvas.transform.Find("Intention").gameObject;
            if (enemyHPSlider != null) {
                enemyHealthText = enemyHPSlider.transform.Find("Text").gameObject;
            }
            if (enemyIntention != null) {
                enemyAttackText = enemyIntention.transform.Find("damage").gameObject;
            }
        }
        //enemyHealthText = enemy.GetComponent<UnityEngine.UI.Slider>().text;

        playerScript = player.GetComponent<PlayerController>();

        attack_text.GetComponent<UnityEngine.UI.Text>().text = attack.ToString();
        block_text.GetComponent<UnityEngine.UI.Text>().text = block.ToString();
        healthText.GetComponent<UnityEngine.UI.Text>().text = playerScript.health.ToString() + "/" + playerScript.maxHealth.ToString();
        enemyHealthText.GetComponent<UnityEngine.UI.Text>().text = enemyScript.health.ToString() + "/" + enemyScript.maxHealth.ToString();

        enemyAttackText.GetComponent<UnityEngine.UI.Text>().text = enemyAttack.ToString();
        total_points_text.GetComponent<UnityEngine.UI.Text>().text = points.ToString();

        gameScript = game.GetComponent<GameManager>();

        sfx = GameObject.Find("SFXManager").GetComponent<AudioButtonController>();
    }

    public void setPoints(int point) {
        points = point;
        total_points_text.GetComponent<UnityEngine.UI.Text>().text = points.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerTurn != true) {

            playerScript.gainBlock(block);



            enemyAttackText.GetComponent<UnityEngine.UI.Text>().text = enemyAttack.ToString();


            StartCoroutine(wait());



            playerTurn = true;
        }
    }

    private IEnumerator wait () {
        float totalTime = 2f;
        float timer = .5f;

        if (attack > 0) {
            playerScript.anim.SetTrigger("Attack");
            while (timer >= 0) {
                timer -= Time.deltaTime;
                yield return null;
            }
            enemyScript.takeDamage(attack);
            enemyHealthText.GetComponent<UnityEngine.UI.Text>().text = enemyScript.health.ToString() + "/" + enemyScript.maxHealth.ToString();
            enemyHPSlider.GetComponent<Slider>().value = (float)enemyScript.health / (float)enemyScript.maxHealth;
            //Debug.Log("Enemy HP Fraction: " + (enemyScript.health / enemyScript.maxHealth));
        }

        if (attack > 0) {
            if (enemyScript.health <= 0) {
                enemyScript.anim.SetTrigger("Dead");
            } else {

                enemyScript.anim.SetTrigger("Hurt");
            }
            timer = totalTime;
            while (timer >= 0) {
                timer -= Time.deltaTime;
                yield return null;
            }
        }

        if(enemyScript.health <= 0) {
            winCanvas.SetActive(true);
            sfx.Play("Win");
        }

        if (enemyScript.health > 0) {
            enemyScript.anim.SetTrigger("Attack");
            timer = .5f;
            while (timer >= 0) {
                timer -= Time.deltaTime;
                yield return null;
            }
        }


        if (enemyScript.health > 0) {
            enemyScript.anim.SetTrigger("Attack");
            playerScript.takeDamage(enemyAttack);
            healthText.GetComponent<UnityEngine.UI.Text>().text = playerScript.health.ToString() + "/" + playerScript.maxHealth.ToString();
            playerHPSlider.GetComponent<Slider>().value = (float)playerScript.health / (float)playerScript.maxHealth;
            if (playerScript.health <= 0) {
                playerScript.anim.SetTrigger("Dead");
            } else {
                playerScript.anim.SetTrigger("Hurt");
            }
            timer = totalTime;

            while (timer >= 0) {
                timer -= Time.deltaTime;
                yield return null;
            }
        }

        if (playerScript.health <= 0){
            loseCanvas.SetActive(true);
            sfx.Play("Lose");
        }



        gameScript.Reset();
        canvas.SetActive(true);
        attack = 0;
        block = 0;
        attack_text.GetComponent<UnityEngine.UI.Text>().text = attack.ToString();
        block_text.GetComponent<UnityEngine.UI.Text>().text = block.ToString();
        enemyAttack = enemyScript.enemyAttack();
        enemyAttackText.GetComponent<UnityEngine.UI.Text>().text = enemyAttack.ToString();
    }

    public void onLockIn() {
        playerTurn = false;

    }

    public void incrementAttack() {
        if (block + attack < total_accrued_points) {
            attack++;
            points--;
            attack_text.GetComponent<UnityEngine.UI.Text>().text = attack.ToString();
            total_points_text.GetComponent<UnityEngine.UI.Text>().text = points.ToString();
        }
    }

    public void decrementAttack() {
        if (attack > 0) {
            attack--;
            points++;
            attack_text.GetComponent<UnityEngine.UI.Text>().text = attack.ToString();
            total_points_text.GetComponent<UnityEngine.UI.Text>().text = points.ToString();
        }
    }

    public void incrementDefense() {
        if (block + attack < total_accrued_points) {
            block++;
            points--;
            block_text.GetComponent<UnityEngine.UI.Text>().text = block.ToString();
            total_points_text.GetComponent<UnityEngine.UI.Text>().text = points.ToString();
        }
    }

    public void decrementDefense() {
        if (block > 0) {
            block--;
            points++;
            block_text.GetComponent<UnityEngine.UI.Text>().text = block.ToString();
            total_points_text.GetComponent<UnityEngine.UI.Text>().text = points.ToString();
        }
    }

}
