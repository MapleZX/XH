using Godot;
using System;
using GodotArray = Godot.Collections.Array;

namespace XHSystem
{
    /// <summary>
    /// 切换场景的时候隐藏掉 CanvasLayer.
    /// </summary>
    public abstract class BaseCanvasLayer : CanvasLayer
    {
        private GodotArray controls;
        protected BackgroundLoadAsync backgroundLoadAsync;

        public override void _Ready()
        {
            controls = GetChildren();
            GetBackgroundLoadAsync(out backgroundLoadAsync);
            backgroundLoadAsync.Connect("load_begin", this, nameof(hide_control));
        }

        protected abstract void GetBackgroundLoadAsync(out BackgroundLoadAsync back);

        public virtual void hide_control(int stage_count)
        {
            foreach (var node in controls)
            {
                var control = node as Control;
                if (control != null)
                {
                    control.Hide();
                    // control.Visible = false;
                }
            }
        }
    }
}
