using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//todo rename Searcher
static class RouteSearch
{
    //行きたい座標への最短ルートに障害物がないか判定し
    //障害物がある場合それをよけるための座標を返す。
    //障害物がない場合行きたい座標を返す。
    //@param targetPos  行きたい座標
    //@param myPos      現在の自分の座標
    public static Vector3 ObtainNextPos(Vector3 targetPos, Vector3 myPos)
    {
        Vector3 ret = targetPos;

        Vector3 direction = targetPos - myPos;
        RaycastHit onHitRay;
        if (Physics.Raycast(
            myPos,
            direction.normalized,
            out onHitRay,
            direction.magnitude,
            ~(LayerMask.NameToLayer("player") | LayerMask.NameToLayer("Ignore Raycast"))))
        {
            //四隅との距離を算出
            //todo 関数化
            BoxCollider collidedObj = onHitRay.collider.gameObject.GetComponent<BoxCollider>();
            Vector3 size = collidedObj.transform.rotation * collidedObj.size;
            Vector3 collidedPos = collidedObj.transform.position;

            List<Node> points = new List<Node>();
            points.Add(new Node(new Vector3(collidedPos.x + size.x / 2, collidedPos.y, collidedPos.z + size.z / 2)));
            points.Add(new Node(new Vector3(collidedPos.x + size.x / 2, collidedPos.y, collidedPos.z - size.z / 2)));
            points.Add(new Node(new Vector3(collidedPos.x - size.x / 2, collidedPos.y, collidedPos.z - size.z / 2)));
            points.Add(new Node(new Vector3(collidedPos.x - size.x / 2, collidedPos.y, collidedPos.z + size.z / 2)));

            foreach (var node in points)
            {
                node.distance = (node.pos - myPos).magnitude;
            }

            //1番近いものは真正面のnodeのため
            //2番目に近いnodeに移動
            points = points.OrderByDescending((Node a) => a.distance).ToList();
            ret = points[1].pos;
        }

        return ret;
    }

    private class Node
    {
        public Node(Vector3 argPos)
        {
            pos = argPos;
            distance = 0;
        }
        public Vector3 pos { get; set; }
        public float distance { get; set; }
    }
}