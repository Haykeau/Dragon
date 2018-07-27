using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;

    private bool triggered = false;

    void Update()
    {
        if (this.triggered && this.CheckAction())
        {
            this.Interact();
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Try to interract");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.triggered = true;
            Debug.Log("Player in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            this.triggered = false;
            Debug.Log("Player out");
        }
    }

    private bool CheckAction()
    {
        if (Input.GetKeyDown("e"))
        {
            return true;
        }
        return false;
    }
}
