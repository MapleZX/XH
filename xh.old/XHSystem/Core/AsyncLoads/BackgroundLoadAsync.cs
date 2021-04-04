using Godot;
using System;
using System.Threading.Tasks;

namespace XHSystem
{
    /// <summary>
    /// 场景的异步加载
    /// </summary>
    public abstract class BackgroundLoadAsync : Node, IBackgroundLoadAsync
    {
        public bool isLoading { get; private set; }
        public int Progress { get; private set; }
        public int MaxProgress { get; private set; }

        [Signal] public delegate void progress(int stage);
        [Signal] public delegate void load_begin(int stage_count);
        [Signal] public delegate void load_done();

        public override void _Ready()
        {
            this.RegisterNode();
            this.SignalConnect();
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="path">加载场景的路径</param>
        /// <returns></returns>
        public virtual async Task LoadAsync(string path)
        {
            using (var loader = ResourceLoader.LoadInteractive(path))
            {
                // GD.Print("Loading...");

                MaxProgress = loader.GetStageCount();
                EmitSignal("load_begin", loader.GetStageCount());

                isLoading = true;
                Resource resource = null;
                
                while (resource == null)
                {
                    // OS.DelayMsec(300);

                    Progress = loader.GetStage();
                    EmitSignal("progress", loader.GetStage());

                    var err = loader.Poll();

                    if (err == Error.FileEof) 
                    {
                        EmitSignal("load_done");
                        isLoading = false;

                        using (resource = loader.GetResource())
                        {
                            // EmitSignal("load_done");
                            // isLoading = false;
                            var screen = resource as PackedScene;
                            var root = this.Root();
                            root.AddChild(screen.Instance());
                            break;
                        }
                    }
                    await ToSignal(GetTree(), "idle_frame");
                }
                // GD.Print("Done.");
                // EmitSignal("load_done");
            }
        }
    }
}