using UnityEngine;

public class ChildCollisionDetector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log('1'); 
        transform.parent.SendMessage("OnChildCollisionEnter", other, SendMessageOptions.DontRequireReceiver);
    }
}
