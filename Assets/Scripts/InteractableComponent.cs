using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent _action;
    private int count = 0;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && count == 0)
        {
            collision.GetComponent<Player>().OnChanged += ActionInvoke;
            count += 1;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnChanged -= ActionInvoke;
            count = 0;
        }
    }

    public void ActionInvoke()
    {
        _action?.Invoke();
    }
}
