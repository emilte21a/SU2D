using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    //Skrev egentligen detta scriptet i 3an i gymnasiet, 
    //så det är menat att vara random seeds varje gång, 
    //men för enkelhet så har jag skippat det och hållt mig till ett fast seed för varje bana så att jag kan customisa själv

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
    [SerializeField] public short worldSize = 300;
    [SerializeField] int heightMultiplier = 15;
    [SerializeField] int seed;
    [SerializeField] Texture2D noiseTexture;

    [HideInInspector]
    private static List<RuleTile> tilesInWorld;

    public Vector2[] spawnPoints;

    void Start()
    {
        spawnPoints = new Vector2[worldSize];
        tilesInWorld = new List<RuleTile>();
        seed = terrainType.seed;
        tileToPlace = terrainType.ruleTile;
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
        tilesInWorld.Clear();
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

        Instantiate(antennae, new Vector3(spawnPoints[worldSize / 2].x + 0.5f, spawnPoints[worldSize / 2].y + 2.5f), quaternion.identity);
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
