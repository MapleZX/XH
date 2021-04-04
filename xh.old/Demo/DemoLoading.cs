using Godot;
using System;
using System.Threading.Tasks;

public class DemoLoading : Control
{
    ProgressBar progressBar;
    bool isClick = false;
    public override void _Ready()
    {
        progressBar = GetNode<ProgressBar>("ProgressBar");
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouse && @event.IsPressed() && !isClick)
        {
            isClick = true;
            Task task = LoadAsync("res://Demo/Demo.tscn");
            GD.Print(isClick);
        }
    }
    public async Task LoadAsync(string path)
    {
        
        using (var loader = ResourceLoader.LoadInteractive(path))
        {
            progressBar.MaxValue = (loader.GetStageCount());
            GD.Print("Loading...");
            Resource resource = null;
            while (resource == null)
            {
                OS.DelayMsec(300);
                progressBar.Value = loader.GetStage();
                var err = loader.Poll();
                if (err == Error.FileEof) 
                {
                    using (resource = loader.GetResource())
                    {
                        var screen = resource as PackedScene;
                        var root = GetNode("/root/");
                        root.AddChild(screen.Instance());
                        break;
                    }
                }
                await ToSignal(GetTree(), "idle_frame");
            }
            GD.Print("Done.");
            QueueFree();
        }
       
    }
}
