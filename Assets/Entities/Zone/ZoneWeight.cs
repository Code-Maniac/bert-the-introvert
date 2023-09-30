using System;
using UnityEngine;

[Serializable]
public class ZoneWeight
{
    [SerializeField] public BoxCollider2D zone;
    [SerializeField] public float weight = 1.0f;
}