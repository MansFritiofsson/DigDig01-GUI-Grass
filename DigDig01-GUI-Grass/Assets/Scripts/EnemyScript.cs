using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    // Skript på fienden som håller data om fienden: HP, maxHP och de två olika sprites som används
    // Skriptet i sig kör ingen kod utan innehåller enbart fiendens data; all faktisk kod som ändrar spriten,
    // redigerar HP osv. hanteras i ButtonScript.

    public float health;
    public float maxHealth;

    public Sprite healthySprite;
    public Sprite damagedSprite;

}
