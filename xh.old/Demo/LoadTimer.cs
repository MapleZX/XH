using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using XHSystem;

public class LoadTimer : Node2D
{
    public override void _Ready()
    {
        // Directory dir = new Directory();
        // File file = new File();

        // testData[] ts = new testData[2];
        // ts[0] = new testData(1, "no");
        // ts[1] = new testData(2, "no");
        // testData[] ts1 = new testData[2];
        // ts1[0] = new testData(1, "no1");
        // ts1[1] = new testData(2, "no2");

        // var d1 = this.LoadSingleData("Demo/Test1/noPassword.save", new testData(1, "noPassword"), typeof(testData), file, dir, PathStatus.Res);
        // var d2 = this.LoadSingleData<testData>("Demo/Test1/noPassword1.save", new testData(2, "noPassword"), file, dir, PathStatus.Res);
        // var d3 = this.LoadMultipleData("Demo/Test/noPassword2.save", ts, typeof(testData), file, dir, PathStatus.Res);
        // var d4 = this.LoadMultipleData<testData>("Demo/Test/noPassword3.save", ts1, file, dir, PathStatus.Res);

        // var d5 = this.LoadSingleData("Demo/Test/Password.save", new testData(1, "noPassword"), typeof(testData), "123", file, dir, PathStatus.Res);
        // var d6 = this.LoadSingleData<testData>("Demo/Test/Password1.save", new testData(2, "noPassword"),  "123",file, dir, PathStatus.Res);
        // var d7 = this.LoadMultipleData("Demo/Test/Password2.save", ts, typeof(testData),  "123",file, dir, PathStatus.Res);
        // var d8 = this.LoadMultipleData<testData>("Demo/Test/Password3.save", ts1,  "123",file, dir, PathStatus.Res);

        // GD.Print(d1);
        // var s = OS.GetTicksUsec();
        // this.Context();
        // // node = new XHEventHandler<BulletEventArgs>();
        // // node1 = new XHEventHandler();
        // // node2 = new XHEventHandler<DamageEventArgs>();
        // GD.Print(OS.GetTicksUsec() - s);
        // GD.Print(node);
        // GD.Print(node1);
        // GD.Print(node2);
        // GetNode<FileDialog>("CanvasLayer/FileDialog").Show();
    }
    [Service("Bullet")] IXHEventHandler<BulletEventArgs> node;
    [Service("VirtualJoystick")] IXHEventHandler node1;
    [Service("DamageControl")] IXHEventHandler<DamageEventArgs> node2;
    public class testData 
    {
        public int id;
        public string name;

        public testData(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
