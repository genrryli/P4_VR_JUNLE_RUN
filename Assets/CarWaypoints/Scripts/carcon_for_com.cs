using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class carcon_for_com : MonoBehaviour
{
    public int loop = 0;

    /// 路标点数据文件 <summary>
    /// 路标点数据文件
    /// </summary>
    public TextAsset waypointsData = null;

    /// 路标点专用XML操作 <summary>
    /// 路标点专用XML操作
    /// </summary>
    private WaypointsXML _WaypointsXML = new WaypointsXML();

    /// 所有路标点 <summary>
    /// 所有路标点
    /// </summary>
    public List<WaypointsModel> WaypointsModelAll = new List<WaypointsModel>();

    /// 检测路标点 <summary>
    /// 用来保存已通过的路标点
    /// 同样的路标点则不加入
    /// 冲过终点线时取数量
    /// 大于最少数量则算通过一圈
    /// </summary>
    public List<WaypointsModel> CheckPoints = new List<WaypointsModel>();

    void Start()
    {
        //获取路标点数据
        _WaypointsXML.GetXmlData(WaypointsModelAll, null, waypointsData.text);
    }

    void FixedUpdate()
    {
        CircleNumberCheck();//圈数检测     
    }

    /// 圈数检测 <summary>
    /// 圈数检测
    /// 思路如下：
    /// 每一帧计算距离最近的检查点
    /// 检查点存在则不添加，不存在则添加
    /// 冲过终点线时取数量
    /// 大于最少数量则算通过一圈
    /// 然后清零 CheckPoints
    /// </summary>
    private void CircleNumberCheck()
    {
        WaypointsModel ClosestWP = GetClosestWP(WaypointsModelAll, transform.position);//获取距离最近的路径点

        //判断当前最近路标点是否已存在
        for (int i = 0; i < CheckPoints.Count; i++)
        {
            //存在则返回
            if (ClosestWP.Position == CheckPoints[i].Position)
                return;
        }

        //不存在则添加
        CheckPoints.Add(ClosestWP);
    }

    /// 计算赛道长度 <summary>
    /// 计算赛道长度
    /// </summary>
    /// <returns>返回赛道长度</returns>
    public float CalcTotalDis()
    {
        //把所有点和点的距离相加而得出

        float temTotalDis = 0f;//临时总距离
        for (int i = 0; i < WaypointsModelAll.Count; i++)
        {
            if (i >= WaypointsModelAll.Count - 1)
            { temTotalDis += Vector3.Distance(WaypointsModelAll[i].Position, WaypointsModelAll[0].Position); }
            else
            { temTotalDis += Vector3.Distance(WaypointsModelAll[i].Position, WaypointsModelAll[i + 1].Position); }
        }

        return temTotalDis;
    }

    /// 获取距离最近的路径点 <summary>
    /// 获取距离最近的路径点
    /// </summary>
    /// <param name="DPs">路径点集合</param>
    /// <param name="myPosition">当前坐标</param>
    /// <returns>返回最近距离的路标点</returns>
    private WaypointsModel GetClosestWP(List<WaypointsModel> all, Vector3 myPosition)
    {
        WaypointsModel tMin = null;
        float minDist = Mathf.Infinity;//正无穷

        for (int i = 0; i < all.Count; i++)
        {
            float dist = Vector3.Distance(all[i].Position, myPosition);
            if (dist < minDist)
            {
                tMin = all[i];
                minDist = dist;
            }
        }
        return tMin;
    }

    /// 计算finished赛道长度 <summary>
    /// 计算finishes赛道长度
    /// </summary>
    /// <returns>返回finishrd赛道长度</returns>
    public float finished_dis()
    {
        //把所有点和点的距离相加而得出

        float temTotalDis = 0f;//临时总距离
        for (int i = 0; i < CheckPoints.Count; i++)
        {
            if (i >= CheckPoints.Count - 1)
            { temTotalDis += Vector3.Distance(CheckPoints[i].Position, CheckPoints[0].Position); }
            else
            { temTotalDis += Vector3.Distance(CheckPoints[i].Position, CheckPoints[i + 1].Position); }
        }

        return temTotalDis;
    }

    void OnTriggerEnter(Collider Trigger)
    {
        //检查点数量大于最小检查点数量则算一圈
        if (Trigger.tag == ("final_check"))
        {
            if (CheckPoints.Count >= WaypointsModelAll.Count / 3 * 2) { loop++; }
            CheckPoints.Clear();//清空检测点
        }
    }
}