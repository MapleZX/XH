using Godot;
using System;

namespace XHSystem.old
{
	public static class PathDetail
	{
		// public const string User = "user://";
		// public const string Res = "res://";
	
		// public static string DirNewFile(this Node node, string path, PathStatus status)
		// {
		// 	Directory dir = new Directory();
		// 	var dirPath = node.GetPathPrefix(status) + path;
		// 	if (dir.DirExists(dirPath)) return dirPath;
		// 	dir.MakeDirRecursive(dirPath);
		// 	return dirPath;
		// }

		// public static string DirNewFile(this Node node, Directory dir, string path, PathStatus status)
		// {
		// 	dir = new Directory();
		// 	var dirPath = node.GetPathPrefix(status) + path;
		// 	if (dir.DirExists(dirPath)) return dirPath;
		// 	dir.MakeDirRecursive(dirPath);
		// 	return dirPath;
		// }

		// public static void RemoveFile(this Node node, string file, PathStatus status)
		// {
		// 	Directory dir = new Directory();
		// 	var dirPath = node.GetPathPrefix(status) + file;
		// 	if (dir.FileExists(dirPath)) dir.Remove(dirPath);
		// }

		// public static void RemoveFile(this Node node, Directory dir, string file, PathStatus status)
		// {
		// 	dir = new Directory();
		// 	var dirPath = node.GetPathPrefix(status) + file;
		// 	if (dir.FileExists(dirPath)) dir.Remove(dirPath);
		// }

		// public static string GetPathPrefix(this Node node, PathStatus status)
		// {
		// 	var path = "";
		// 	if (status == PathStatus.Custom)
		// 	{
		// 		path = "";
		// 	}
		// 	else if (status == PathStatus.User)
		// 	{
		// 		path = PathDetail.User;
		// 	}
		// 	else if (status == PathStatus.Res)
		// 	{
		// 		path = PathDetail.Res;
		// 	}
		// 	else
		// 	{
		// 		path = "";
		// 	}
		// 	return path;
		// }
	}
}
