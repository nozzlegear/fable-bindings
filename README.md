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

