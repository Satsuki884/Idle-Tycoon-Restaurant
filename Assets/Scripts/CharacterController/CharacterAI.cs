using System.Collections;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector3[] path;
    private int currentPointIndex = 0;
    private Tray assignedTray;
    private Vector3 trayPosition;

    public void SetPath(Vector3[] newPath, Tray tray, Vector3 trayPos)
    {
        path = newPath;
        assignedTray = tray;
        trayPosition = trayPos;
        StartCoroutine(MoveToTray());
    }

    IEnumerator MoveToTray()
    {
        foreach (Vector3 point in path)
        {
            while (Vector3.Distance(transform.position, point) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, point, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        while (Vector3.Distance(transform.position, trayPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, trayPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(ProcessOrder());
    }

    IEnumerator ProcessOrder()
    {
        yield return new WaitForSeconds(1f);
        // assignedTray.CompleteOrder();
        Destroy(gameObject);
    }
}
