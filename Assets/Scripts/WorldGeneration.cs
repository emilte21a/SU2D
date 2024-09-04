using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] GameObject building;
    [SerializeField] GameObject antennae;

    [SerializeField]
    TerrainType terrainType;

    [SerializeField]
    RuleTile tileToPlace;

    [SerializeField]
    Tilemap grid;

    [Header("Generation controls")]
    [SerializeField] float surfaceThreshold = 0.9f;
    [SerializeField] float caveFrequency = 0.05f;
    [SerializeField] float terrainFrequency = 0.05f;
    [SerializeField] short worldSize = 300;
    [SerializeField] int heightMultiplier = 15;
    [SerializeField] int seed;
    [SerializeField] Texture2D noiseTexture;

    [HideInInspector]
    private List<RuleTile> tilesInWorld;

    [HideInInspector]
    public static Vector2[] spawnPoints;

    void Start()
    {
        tilesInWorld = new List<RuleTile>();
        seed = UnityEngine.Random.Range(-10000, 10000);
        tileToPlace = terrainType.ruleTile;
        spawnPoints = new Vector2[worldSize];
        GenerateNoiseTexture();
        GenerateWorld();
    }

    void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldSize, worldSize);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float p = Mathf.PerlinNoise((x + seed) * caveFrequency, (y + seed) * caveFrequency);

                noiseTexture.SetPixel(x, y, new Color(p, p, p));
            }
        }

        noiseTexture.Apply();
    }

    void GenerateWorld()
    {
        for (int x = 0; x < worldSize; x++)
        {
            int height = (int)(Mathf.PerlinNoise((x + seed) * terrainFrequency * terrainType.craziness, seed * terrainFrequency * terrainType.craziness) * heightMultiplier + 100);
            spawnPoints[x] = new Vector2(x, height);
            for (int y = 0; y < height; y++)
            {
                bool shouldPlaceTile = noiseTexture.GetPixel(x, y).r <= surfaceThreshold;

                if (shouldPlaceTile) PlaceTile(new Vector3Int(x, y), tileToPlace);
            }
        }

        Instantiate(building, new Vector3(spawnPoints[130].x, spawnPoints[130].y + 2.2f), quaternion.identity);
        Instantiate(antennae, new Vector3(spawnPoints[worldSize / 2].x, spawnPoints[worldSize / 2].y + 2.5f), quaternion.identity);
    }

    void PlaceTile(Vector3Int position, RuleTile tile)
    {
        grid.SetTile(position, tile);
        tilesInWorld.Add(tile);
    }

    public void DestroyTileAt(Vector3Int pos)
    {
        RuleTile ruleTileToRemove = grid.GetTile(pos) as RuleTile; //Hämtar en tile i griden med positionen pos och konverterar den till en ruletile

        if (ruleTileToRemove != null) //Om ruletilen som ska bort inte är null
        {
            grid.SetTile(pos, null); //Gör den till null (ta bort den)
            tilesInWorld.Remove(ruleTileToRemove); //Ta bort den från listan där alla tiles är lagrade

        }
    }
}
