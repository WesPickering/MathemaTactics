using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int health;
    public int maxHealth;
    public int numOfTurns;
    public Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
    }

    public void takeDamage(int damage) {
        if (health - damage <= 0) {
            Debug.Log("Enemy died!");
            //TODO destroy the game object and move on to win state
        }
        health -= damage;
    }

    public void heal(int healing) {
        if (health + healing >= maxHealth) {
            health = maxHealth;
        }
        else {
            health += healing;
        }
    }

    public int enemyAttack() {
        int attack = Random.Range(1, 15);
        attack += numOfTurns;
        numOfTurns++;
        return attack;
    }
}
