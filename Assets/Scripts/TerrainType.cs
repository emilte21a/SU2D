using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TerrainManagerScriptableObject", order = 1)]
public class TerrainType : ScriptableObject
{
    [Header("RuleTile that the specific world should be made out of")]
    public RuleTile ruleTile;

    [Header("How 'weird' the terrain should be")]
    public float craziness;

    [Header("The players mass is divided by this number")]
    public float gravityDivider;

    [Header("A The Specific Seed of One Planet")]
    public int seed;
}
