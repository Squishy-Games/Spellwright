using UnityEngine;
using UnityEngine.Events;

public class Deadzone : MonoBehaviour
{
    [SerializeField] private UnityEvent takeDamage;

    private void OnCollisionEnter(Collision collision)
    {
        takeDamage.Invoke();
    }
}
