using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    public float viewRadius = 5;
    public int viewAngle = 135;
    Collider2D[] playerInRadius;
    public LayerMask obstacleMask, playerMask;
    public List<Transform> visiblePlayer = new List<Transform> ();

    void FixedUpdate () {
        FindVisiblePlayer ();
    }

    void FindVisiblePlayer () {
        playerInRadius = Physics2D.OverlapCircleAll (transform.position, viewRadius);

        visiblePlayer.Clear ();

        for (int i = 0; i < playerInRadius.Length; i++) {
            Transform player = playerInRadius[i].transform;
            Vector2 dirPlayer = new Vector2 (player.position.x - transform.position.x, player.position.y - transform.position.y);

            if (Vector2.Angle (dirPlayer, transform.right) < viewAngle / 2) {
                float distancePlayer = Vector2.Distance (transform.position, player.position);

                if (!Physics2D.Raycast (transform.position, dirPlayer, distancePlayer, obstacleMask)) {
                    if (player.name == "ToDetect") {
                        visiblePlayer.Add (player);
                    }
                }
            }
        }
    }
    public Vector2 DirFromAngle (float angle, bool isGlobal = false) {
        if (!isGlobal) {
            angle += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}