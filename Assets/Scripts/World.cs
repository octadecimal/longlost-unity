using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    public Tilemap Ground;
    public GameObject WorkerType;
    public List<GameObject> Entities;

    private int entityCount = 0;

    public void Start()
    {
        Entities = new List<GameObject>();

        var worker = createEntity(EntityType.Worker);
        worker.transform.Translate(new Vector3(1, 1, 0));

        WorkerType.transform.position = new Vector3(-100, -100, 0);
    }

    public GameObject createEntity(EntityType type)
    {
        GameObject entity = null;

        switch (type)
        {
            case EntityType.Worker:
                entity = Instantiate(WorkerType, this.transform);
                break;
        }

        entityCount++;

        Entity controller = entity.GetComponent<Entity>();
        controller.Id = entityCount;
        Entities.Add(entity);

        return entity;
    }
}