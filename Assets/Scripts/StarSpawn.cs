using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawn : MonoBehaviour
{
    public GameObject star;
    public int spawnCount;

    void Start()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            Vector2 position = GetRandomPosition();
            AddStar(position);
        }
    }

    // Get random start position for the star
    Vector2 GetRandomPosition()
    {
        Vector2 position = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        return position;
    }

    // Add star to the scene
    void AddStar(Vector2 position)
    {
        GameObject new_star = Instantiate(
            star,
            position,
            Quaternion.identity,
            gameObject.transform
        );
    }
}
