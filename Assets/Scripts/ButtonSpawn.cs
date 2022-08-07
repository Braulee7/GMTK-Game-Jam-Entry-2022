using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawn : MonoBehaviour
{
    public SpriteRenderer sr {get; private set;}
    public Sprite[] sprites;

    public bool buttonPressed;

    private List<int> diceNums;
    private Transform spawn;

    public GameObject[] enemies;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        spawn = transform.Find("SpawnPoint");
        buttonPressed = false;

        diceNums = new List<int>();
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log(collider.gameObject.name);
        

        if (collider.gameObject.name == "Hero" && !buttonPressed)
        {
            //change the sprite
            sr.sprite = sprites[1];

            //make the button be pressed down
            buttonPressed = true;

            //roll the two dice from the 
            foreach (Transform child in transform)
            {
                if (child.name == "Dice")
                {
                    child.gameObject.GetComponent<Dice>().Roll();

                    diceNums.Add(child.gameObject.GetComponent<Dice>().num);
                }
            }

            //set hero stats
            FindObjectOfType<Player>().SetStats(diceNums[0] * 2);

            //spawn in the enemies
            Invoke(nameof(Spawn), 2.5f);

        }
    }

    private void Spawn()
    {
        //set enemy damage
        float damage = diceNums[1] * 2;

        //be able to spawn multiple enemies slowly
        StartCoroutine(multiSpawn(damage));
        
    }

    private IEnumerator multiSpawn(float damage)
    {
        //offset so not all enemies spawn in one spot
        float xOffset = 0;

        for (int i = 0; i < diceNums[1]; ++i)
        {
            //if roll even then a bomber is spawned
            if (diceNums[1] % 2 == 0)
            {
                GameObject enemy = Instantiate(enemies[1], new Vector2(spawn.position.x + xOffset, spawn.position.y), Quaternion.identity);
                enemy.GetComponent<EnemyBase>().SetStats(damage);
                enemy.GetComponent<EnemyBase>().target = FindObjectOfType<Player>().transform;
            } else
            {
                //a goblin is spawned
                GameObject enemy = Instantiate(enemies[0], new Vector2(spawn.position.x + xOffset, spawn.position.y), Quaternion.identity);
                enemy.GetComponent<EnemyBase>().SetStats(damage);
                enemy.GetComponent<EnemyBase>().target = FindObjectOfType<Player>().transform;
            }
                


            //offset the spawn so they don't all spawn ontop of eachother
            xOffset += 1.0f;
            xOffset *= -1;

            //don't spawn all at once
            yield return new WaitForSeconds(0.5f);
        }
    }
}
