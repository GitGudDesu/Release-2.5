using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour {
    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles;
    private int amtTileOnScreen = 10;
    private float safeZone = 15.0f;
    private int lastPrefabIndex = 0;

    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLength = 10.0f;

    // testing
    public GameObject[] collectibles;
    public List<GameObject> activeTokens;
    public List<GameObject> tokensToRemove;
    private const int TOKENS_ON_SCREEN = 5;
    private const float TOKEN_OFFSET = 2f;
    private const float TILE_OFFSET = 1f;
    private float tokenZ = 10.0f;

    
    private void Start () {
        activeTiles = new List<GameObject>();
        activeTokens = new List<GameObject>();
        tokensToRemove = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amtTileOnScreen; ++i)
        {
            if (i < 4)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }
            
        }
        
    }

    private void Update () {
		if (playerTransform.position.z - safeZone > (spawnZ - amtTileOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }

        foreach (GameObject go in activeTokens)
        {
            if (playerTransform.position.z > go.transform.position.z)
            {
                tokensToRemove.Add(go);
                activeTokens.Remove(go);
            }
        }

        while (tokensToRemove.Count > 0)
        {
            Destroy(tokensToRemove[0]);
            tokensToRemove.RemoveAt(0);
        }


    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }
        
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);

        if (go.transform.position.z > safeZone)
        {
            if (Random.Range(1, 6) > 1)
            {
                tokenZ = go.transform.position.z + TILE_OFFSET;

                float xPos = Random.Range(1, 3);
                xPos = (xPos == 1) ? -1.5f : (xPos == 2) ? 0f : 1.5f;

                for (int i = 0; i < TOKENS_ON_SCREEN; ++i)
                {
                    SpawnToken(xPos);
                }
            }
        }

    }

    private void SpawnToken(float xPos)
    {
        GameObject go;
        go = Instantiate(collectibles[0]) as GameObject;

        // not sure about this
        //go.transform.SetParent(transform);
        go.transform.position = new Vector3 (xPos, 1, tokenZ);
        tokenZ += TOKEN_OFFSET;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
