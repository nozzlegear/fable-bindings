# [Fable](https://github.com/fable-compiler) F# bindings for JS packages.

The packages in this repo are a mix of both pure `Fable.Import` bindings and compiled bindings. The distinction is that "pure" bindings contain no code except type information, where "compiled" bindings contain helper code. 

**Important:** compiled bindings must include the F# source code in a folder named `fable` in order to compile it to JS. Add this to your fsproj to include that source code:

```xml
<ItemGroup>
  <Content Include="*.fsproj" PackagePath="fable\" />
  <Content Include="src\" PackagePath="fable\src\" />
</ItemGroup>
```

Pure bindings should be published with the name `Fable.Import.PackageName`, while compiled bindings are just `Fable.PackageName`.
