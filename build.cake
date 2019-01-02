
var target = Argument("target", "All");

//////////////////////////////////////////////////////////////////////
// BUILD TASK
//////////////////////////////////////////////////////////////////////

Task("Clean").Does(() =>
{
	if(DirectoryExists("./artifacts"))
	{
		DeleteDirectory("./artifacts", new DeleteDirectorySettings {
			Recursive = true,
			Force = true
		});
	}
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
	Zip("./artifacts/bin", "./artifacts/skvd.zip", files);
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