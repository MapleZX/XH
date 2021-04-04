using Godot;
using System;
using XHSystem;
using _Deprecated_;
public class ChangeDemo : Button
{
    // [Export] string controller_name = "player";
    // // [Service] 
    // IMoveController moveController;
    // [Service("change_controller")] IXHEventHandler ChangeControllerHandler;

    // public override void _Ready()
    // {
    //     // this.RegisterNode(); // 注册节点
    //     this.SignalConnect(); // 连接信号
    //     var service = this.Context().GetXHCollection(); // 获取上下文

    //     moveController = service.XHResolve<IMoveController>(controller_name);
        
    // }
    // [SignalMethod("pressed")]
    // private void on_pressed_change_controller()
    // {
    //     ChangeControllerHandler.Run(moveController);
    // }
}
