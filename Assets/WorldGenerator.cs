using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class WorldGenerator : MonoBehaviour
{
    // Start is called before the first frame update
   
   void Start()
    {
        Transform tile_pool = GameObject.Find("TilePool").transform;

        for(int x = 0;x<10;x++)
        {
            for(int y = 0;y<10;y++)
            {
                tile_pool.SendMessage("Remove");
                var new_tile = tile_pool.GetChild(0);
                new_tile.parent = transform;

                var height = Random.Range(0.0f,1.0f);
                new_tile.localPosition = new Vector3(x,height,y);

                var tilescript = new_tile.GetComponent<RandomTile>();

                tilescript.type = (TileType)Random.Range(0,3);
                while(tilescript.type==TileType.Sand)
                 tilescript.type = (TileType)Random.Range(0,3);

                Vector3 origin = new Vector3(0,0,0);
                RaycastHit[] hits;
                hits = Physics.SphereCastAll(origin, 10, -transform.up, 10);

                foreach(RaycastHit h in hits)
                {
                //    h.collider.GetComponent<RandomTile>().type = TileType.Sand;
                }
                    
                    

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
