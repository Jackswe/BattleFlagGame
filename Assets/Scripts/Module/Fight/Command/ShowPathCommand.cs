using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


// 显示移动路径的指令
public class ShowPathCommand : BaseCommand
{
    Collider2D pre;   // 鼠标之间检测到2d碰撞盒
    Collider2D current;  // 鼠标当前检测的2d碰撞盒
    AStar astar;  // A星
    AStarPoint start;  // 开始点
    AStarPoint end;   // 终点
    List<AStarPoint> prePaths;  // 之前检测到的路径集合 用来清空用


    public ShowPathCommand(ModelBase model) : base(model)
    {
        prePaths = new List<AStarPoint>();
        start = new AStarPoint(model.RowIndex,model.ColIndex);
        astar = new AStar(GameApp.MapManager.RowCount,GameApp.MapManager.ColCount);
    }

    public override bool Update(float dt)
    {
        // 点击鼠标后 确定移动的位置
        if(Input.GetMouseButtonDown(0))
        {
            if (prePaths.Count != 0 && this.model.Step >= prePaths.Count - 1) {
                GameApp.CommandManager.AddCommand(new MoveCommand(this.model,prePaths));  // 移动
            }
            else
            {
                GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent); // 否则执行未选中方法

                // 不移动直接显示操作选项
                // 显示选项界面
                GameApp.ViewManager.Open(ViewType.SelectOptionView, this.model.data["Event"], (Vector2)this.model.transform.position);

            }

            return true;
        }

        current = Tools.ScreenPointToRay2D(Camera.main,Input.mousePosition);  // 检测当前鼠标位置是否有2d碰撞盒

        if (current != null) {
            // 之前的检测碰撞盒和当前的盒子不一致 才进行 路径检测
            if (current != pre) {
                pre = current;   // 记录之前检测的碰撞盒

                Block b = current.GetComponent<Block>();

                if (b != null)
                {
                    // 检测到block脚本的物体 进行寻路
                    end = new AStarPoint(b.RowIndex,b.ColIndex);
                    astar.FindPath(start, end, updatePath);
                }
                else {
                    // 没检测到 将之前的路径 清除
                    for (int i = 0; i < prePaths.Count; i++) {
                        GameApp.MapManager.mapArr[prePaths[i].RowIndex, prePaths[i].ColIndex].SetDirSp(null,Color.white);
                    }
                    prePaths.Clear();
                
                }
            }
        }

        return false;
    }


    private void updatePath(List<AStarPoint> paths) {
        // 如果之前已经有路径了 要先清除
        if (prePaths.Count != 0) {

            for (int i = 0; i < prePaths.Count; i++) {
                // 将路径上的图片设置为空
                GameApp.MapManager.mapArr[prePaths[i].RowIndex,prePaths[i].ColIndex].SetDirSp(null,Color.white);  
            }
        }

        if (paths.Count >= 2 && model.Step >= paths.Count - 1) {
            // 更新路径上的图片
            for (int i = 0; i < paths.Count; i++)
            {
                BlockDirection dir = BlockDirection.down;  // 向下的箭头对象
                                                           // 设置方块的样式
                if (i == 0)
                {
                    dir = GameApp.MapManager.GetDirection(paths[i], paths[i + 1]);
                }
                else if (i == paths.Count - 1)
                {
                    dir = GameApp.MapManager.GetDirection2(paths[i], paths[i - 1]);
                }
                else {
                    dir = GameApp.MapManager.GetDirection3(paths[i - 1], paths[i], paths[i + 1]);
                }
                GameApp.MapManager.SetBlockDir(paths[i].RowIndex, paths[i].ColIndex, dir, Color.yellow);
            }
 
        }

        prePaths = paths;  // 重置路径
    }


}
