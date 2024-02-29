using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using JetSystems;

public class PlayerDetection : MonoBehaviour
{
    public static UnityAction OnFinishLineHit;
    public static UnityAction OnGameoverHit;

    public static UnityAction OnSomethingHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            OnFinishLineHit?.Invoke();
            other.GetComponent<FinishLine>().OnTriggerCallback();
        }
        else if (other.CompareTag("Gameover"))
        {
            OnGameoverHit?.Invoke();
        }
        else if (other.HasInterface<ICollectable>())
            other.GetComponent<ICollectable>().Collect(GetComponent<JetCharacter>());

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Gameover"))
        {
            OnGameoverHit?.Invoke();
        }
        else
        {
            OnSomethingHit?.Invoke();
        }
    }
}
