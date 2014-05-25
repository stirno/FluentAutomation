pvc.Task("nuget-push", () => {
	pvc.Source(
		"FluentAutomation/FluentAutomation.Core.csproj",
		"FluentAutomation.SeleniumWebDriver/FluentAutomation.SeleniumWebDriver.csproj",
		"FluentAutomation.WatiN/FluentAutomation.WatiN.csproj"
		)
	   .Pipe(new PvcNuGetPack(
			createSymbolsPackage: true
	   ))
	   .Pipe(new PvcNuGetPush());
});