using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    // Skript p� fienden som h�ller data om fienden: HP, maxHP och de tv� olika sprites som anv�nds
    // Skriptet i sig k�r ingen kod utan inneh�ller enbart fiendens data; all faktisk kod som �ndrar spriten,
    // redigerar HP osv. hanteras i ButtonScript.

    public float health;
    public float maxHealth;

    public Sprite healthySprite;
    public Sprite damagedSprite;

}
