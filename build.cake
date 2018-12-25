
var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// BUILD TASK
//////////////////////////////////////////////////////////////////////

Task("Clean").Does(() =>
{
	DeleteDirectory("./artifacts", recursive: true);
	DeleteFile("./skvd.zip");
});

Task("Build").Does(() =>
{
	
	var settings = new DotNetCoreBuildSettings
	{
		Framework = "netcoreapp2.0",
		Configuration = "Release",
		OutputDirectory = "./artifacts/bin"
	};
	
	DotNetCoreBuild(".", settings);
});

Task("PackZip").Does(() =>
{
	var files = GetFiles("./artifacts/bin/*");
	Zip("./artifacts/bin", "skvd.zip", files);
});
 
//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Travis")
    .IsDependentOn("Build");

Task("All")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("PackZip");
	
Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);