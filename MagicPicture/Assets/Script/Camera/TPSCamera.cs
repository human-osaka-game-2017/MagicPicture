using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TPSCamera : MonoBehaviour {

    //障害物があるとplayerが見えないため
    //その障害物のメッシュを透過する
    //playerが見えるようになったら
    //メッシュを元に戻す
    private class Obstacle : IEquatable<Obstacle>
    {
        public Obstacle(GameObject gameObject)
        {
            this.gameObj = gameObject;
            this.material = new Material(this.gameObj.GetComponent<Renderer>().material);
        }
        public GameObject gameObj { get; set; }
        public Material material { get; set; }

        public bool Equals(Obstacle obj) { return gameObj == obj.gameObj; }

        public override bool Equals(object obj) { return this.Equals(obj as Obstacle); }
    }

    private GameObject player = null;
    private List<Obstacle> prevObstacleList = new List<Obstacle>();

    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    void Update()
    {
        GameObject obj = GameObject.Find("Player");

        RaycastHit[] hitColliders = null;
        Vector3 playerPos = new Vector3(
            this.player.transform.position.x,
            this.player.transform.position.y + 0.5f,
            this.player.transform.position.z);
        Vector3 vec = (playerPos - this.transform.position);
        Debug.DrawRay(this.transform.position, vec, Color.red);
        hitColliders = Physics.BoxCastAll(
            this.transform.position,
            new Vector3(0.5f,0.01f,0.01f),
            vec.normalized,
            this.transform.rotation,
            vec.magnitude,
            ~LayerMask.GetMask("player"));

        //このフレームの障害物List作成
        List<Obstacle> obstacleList = new List<Obstacle>();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.collider.gameObject.GetComponent<Renderer>() == null) continue;

            Obstacle hitObj = this.prevObstacleList.Find(i => i.gameObj == hitCollider.collider.gameObject);
            
            if(hitObj == null)
            {
                hitObj = new Obstacle(hitCollider.collider.gameObject);
            }

            obstacleList.Add(hitObj);
        }

        //新たに追加された障害物(meshを透過するobj)
        var newObstacleList = obstacleList.Except<Obstacle>(this.prevObstacleList);
        newObstacleList.ToList<Obstacle>().ForEach(i =>
        {
            Material material = i.gameObj.GetComponent<Renderer>().material;
            BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Fade);
            material.SetColor("_Color", new Color(1, 1, 1, 0));
        });

        //障害物じゃなくなったobject
        var exceptList = this.prevObstacleList.Except<Obstacle>(obstacleList);
        exceptList.ToList<Obstacle>().ForEach(i =>
        {
            i.gameObj.GetComponent<Renderer>().material = i.material;
        });

        this.prevObstacleList = obstacleList;
    }
}