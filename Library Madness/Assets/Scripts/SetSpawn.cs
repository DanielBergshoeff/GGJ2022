using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawn : MonoBehaviour
{
    public Vector3Variable SpawnPosition;
    public Transform Spawn;

    private void Awake() {
        SpawnPosition.Value = Spawn.position;
    }
}
