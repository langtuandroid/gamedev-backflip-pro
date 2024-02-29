using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private Transform playerStartPositionTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetPlayerStartPosition()
    {
        return playerStartPositionTransform.position;
    }
}
