using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int health;
    public int maxHealth;
    public int block;
    public Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void takeDamage(int damage) {

        if ((health + block) - damage <= 0) {
            health = 0;
            Debug.Log("You died");
            //TODO change to the death scene
        } else if (damage > block) {
            health -= damage - block;
        } else {
            block -= damage;
        }

    }

    public void heal(int healing) {
        if (health + healing >= maxHealth) {
            health = maxHealth;
        } else {
            health += healing;
        }
    }

    public void gainBlock(int blocking) {
        block = blocking;
    }
}
