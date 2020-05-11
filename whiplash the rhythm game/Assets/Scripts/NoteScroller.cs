using UnityEngine;

public class NoteScroller : MonoBehaviour
{
    public float bpm;
    public bool hasStarted;
    public Vector3 defaultPosition;

    void Start()
    {
        bpm = bpm / 60f * 3f;
        defaultPosition = transform.position;
        hasStarted = true;
    }
    void Update()
    {
        if (GameController.instance.gamePaused) hasStarted = false;
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, bpm * Time.deltaTime, 0f);
        }
    }
}
