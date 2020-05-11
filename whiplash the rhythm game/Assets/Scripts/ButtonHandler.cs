using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    Vector3 inc = new Vector3(-8.0f / 200, 12.0f / 200, 0);
    Vector3 defaultPos;

    private void Start()
    {
        defaultPos = transform.position;
    }
    public void OnEnter()
    {
        if (defaultPos + inc != transform.position)
            transform.position += inc;
    }
    public void OnExit()
    {
        transform.position -= inc;
    }

}
