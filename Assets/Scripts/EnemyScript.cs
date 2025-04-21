using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private CharacterController characterController;
    private Transform character;
    private float lastHitTime = 0f;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        character = GameObject.Find("Character").transform;
    }

    void Update()
    {
        if (Time.time - lastHitTime > 1.0)
        {
            Vector3 v = character.position - transform.position;
            v.Normalize();
            characterController.SimpleMove(v);
            v.y = 0;
            v.Normalize();
            transform.forward = v;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Character")
        {
            if (Time.time - lastHitTime > 1.0)
            {
                lastHitTime = Time.time;
                Debug.Log("Catch you!");
                Vector3 newPosition = RandomPosition(from: transform.position, distance: 10.0f);
                newPosition.y = 1.1f + Terrain.activeTerrain.SampleHeight(newPosition);
                transform.position = newPosition;

            }
            else
            {
                Debug.Log("Skipped Hit");
            }
        }
    }

    private Vector3 RandomPosition(Vector3 from, float distance)
    {
        Vector3 delta;
        int cnt = 0;
        do
        {
            delta = new Vector3(0.5f - Random.value, 0, 0.5f - Random.value);
            cnt += 1;
        } while (cnt < 100 && (delta == Vector3.zero));
        delta.Normalize();
        return delta * distance + from;
    }
}