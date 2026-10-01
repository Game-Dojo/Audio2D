using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Entro");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        print("Salio");
    }
}
