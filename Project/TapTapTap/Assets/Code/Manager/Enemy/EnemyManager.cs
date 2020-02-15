using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> Enemies;


    private void Start()
    {
        Enemies = new List<GameObject>();
    }
}
