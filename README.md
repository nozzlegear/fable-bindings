# [Fable](https://github.com/fable-compiler) F# bindings for JS packages.

The packages in this repo are a mix of both pure `Fable.Import` bindings and compiled bindings. The distinction is that "pure" bindings contain no code except type information, where "compiled" bindings contain helper code. 

**Important:** compiled bindings must include the F# source code in a folder named `fable` in order to compile it to JS. Add this to your fsproj to include that source code:

```xml
<ItemGroup>
  <Content Include="*.fsproj" PackagePath="fable\" />
  <Content Include="src\" PackagePath="fable\src\" />
</ItemGroup>
```

If the package contains bindings (whether pure or impure) for an NPM or JS package, it should use the namespace `Fable.Import.PackageName`. If it does not contain NPM package bindings, just use `Fable.PackageName`.

### Publishing

1. `dotnet pack -c Release`
2. `dotnet nuget push --source nuget.org -k "$env:NUGET_KEY" project/bin/Release/netstandard2.0/package.nupkg`

If you receive this error during `dotnet pack`, do `rm -r` on the project's bin and obj folders.

`C:\Users\nozzl\source\fable-bindings\.paket\Paket.Restore.targets(190,5): error : The given path's format is not supported. [C:\Users\nozzl\source\fable-bindings\js-cookie\js-cookie.fsproj]`
