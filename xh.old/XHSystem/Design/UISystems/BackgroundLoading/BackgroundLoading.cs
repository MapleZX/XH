using Godot;
using System;
using System.Threading.Tasks;
using XHSystem;

public class BackgroundLoading : BackgroundLoadAsync
{
    [Node("CanvasLayer/ProgressBar")] ProgressBar progressBar;
    [Node("CanvasLayer/ColorRect")] ColorRect colorRect;

    // public override void _Ready()
    // {
    //     base._Ready();
    // }

    [SignalMethod("load_begin")]
    private void on_load_begin(int stage_count)
    {
        progressBar.Show();
        colorRect.Show();

        progressBar.MaxValue = stage_count;
    }

    [SignalMethod("progress")]
    private void on_load(int stage)
    {
        progressBar.Value = stage;
    }
    
    [SignalMethod("load_done")]
    private void on_done()
    {
        progressBar.Hide();
        colorRect.Hide();
    }
}
