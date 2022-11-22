using UnityEngine;

public class DangerArea : MonoBehaviour {

    [SerializeField] private protected GameObject bombPreFab;
    [SerializeField] private protected Transform[] areaLimit;

    private protected GameObject playerObject;
    private protected float playerPostionX;

    [SerializeField] private protected float timeToSpawn;
    private protected float timer = 0;

    [SerializeField] private protected float maxSpawnBombDistance;

    void Start() {
        playerObject = GameObject.Find("Player");
    }

    void Update() {
        playerPostionX = playerObject.transform.position.x;

        if (playerPostionX > areaLimit[0].position.x && playerPostionX < areaLimit[1].position.x) {
            if (timer >= timeToSpawn) {
                for (int i = 0; i < 1; i++)
                    Instantiate(bombPreFab, new Vector2(Random.Range(playerPostionX - maxSpawnBombDistance, playerPostionX + maxSpawnBombDistance), 10 + Mathf.Pow(i, i)), transform.rotation);

                timer = 0;
            }
            else timer += Time.deltaTime;
        }
        else timer = 0;
    }
}