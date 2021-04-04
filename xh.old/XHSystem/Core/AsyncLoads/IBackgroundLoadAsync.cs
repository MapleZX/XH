using Godot;
using System;
using System.Threading.Tasks;

namespace XHSystem
{
    public interface IBackgroundLoadAsync
    {
        bool isLoading { get; }
        int Progress { get; }
        int MaxProgress { get; }        
        Task LoadAsync(string path);
    }
}