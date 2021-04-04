using Godot;
using System;
using System.Collections.Generic;
using XHSystem;

public class TouchScreenButtonControl : Control
{
    [Node("TextureProgress")] TextureProgress textureProgress;
    [Node("TextureRect")] TextureRect textureRect;
    [Node("TouchScreenButton")] TouchScreenButton touchScreenButton;
    [Node("Timer")] Timer timer;

    [Export] string action = "";
    [Export] float interval = 0.5f;
    public double cooldown = 0d;
    private double cooldownValue;
    private bool isPressed = false;

    public override void _Ready()
    {
        this.RegisterNode(); // 注册节点
        this.SignalConnect(); // 连接信号

        touchScreenButton.Action = action;
    }

    public void SetTexture(Texture texture)
    {
        textureRect.Texture = texture;
    }

    public void SetProgress(double value)
    {
        textureProgress.Value = value;
    }

    public void SetTouchButton(double cd, Texture texture)
    {
        cooldown = cd;
        SetTexture(texture);
    }

    public void SetTouchButton(double cd, Texture texture, double value)
    {
        SetTouchButton(cd, texture);
        SetProgress(value);
    }

    [SignalMethod("pressed", "touchScreenButton")]
    private void on_touch_pressed()
    {
        if (cooldown <= 0 || isPressed) return;
        isPressed = true;
        textureProgress.Value = textureProgress.MaxValue;
        cooldownValue = (textureProgress.MaxValue / cooldown) * interval;
        // cooldownValue = textureProgress.MaxValue / (cooldown / interval);     
        timer.Start(interval);
    }

    [SignalMethod("timeout", "timer")]
    private void on_button_cd()
    {
        textureProgress.Value -= cooldownValue;
        if (textureProgress.Value <= textureProgress.MinValue) 
        {
            isPressed = false;
            timer.Stop();
        }
    }
}
