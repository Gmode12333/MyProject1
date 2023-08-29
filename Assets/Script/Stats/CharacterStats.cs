using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [Header("Basic Stats")]
    public int maxHeatlh;
    public float moveSpeed;
    [Header("Enemy Level")]
    [Range(1, 100)]
    public int level;
    [Header("Enemy Exp Drop")]
    [Range(1, 10000)]
    public int ExpDrop;

}
