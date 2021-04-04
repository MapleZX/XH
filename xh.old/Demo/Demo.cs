using Godot;
using System;
using System.Threading.Tasks;
using XHSystem;

public class Demo : Node2D
{
    [Export] string path = "res://Demo/Demo1.tscn";
    [Node("CanvasLayer/Button")] Button button;
    BackgroundLoadAsync backgroundLoad;
    // [Service("background_load")] IBackgroundLoadAsync backgroundLoad;
    public override void _Ready()
    {
        backgroundLoad = this.Singleton<BackgroundLoadAsync>("BackgroundLoading");
        this.RegisterNode(); // 注册节点
        this.SignalConnect(); // 连接信号
        // var service = this.Context().GetXHCollection(); // 获取上下文
    }

    [SignalMethod("pressed", "button")]
    private void change_screen()
    {
        Hide();
        Task task = backgroundLoad.LoadAsync(path); 
        // Hide();
        // PauseMode = PauseModeEnum.Stop;
        // if (!backgroundLoad.isLoading)
        // {
        //     // Hide();
        //     // PauseMode = PauseModeEnum.Stop;
        //     Task task = backgroundLoad.LoadAsync(path, this); 
        // }   
    }

    [SignalMethod("load_begin", "backgroundLoad")]
    private void on_load_begin(int stage_count)
    {
        QueueFree();
    }

    // [SignalMethod("load_done", "backgroundLoad")]
    // private void on_load_done()
    // {
    //     // QueueFree();
    // }
}
