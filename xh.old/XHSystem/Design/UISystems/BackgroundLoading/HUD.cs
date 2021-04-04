using Godot;
using System;
using System.Threading.Tasks;
using XHSystem;

public class HUD : BaseCanvasLayer
{
    protected override void GetBackgroundLoadAsync(out BackgroundLoadAsync back)
    {
        back = this.Singleton<BackgroundLoadAsync>("BackgroundLoading");
    }
}
