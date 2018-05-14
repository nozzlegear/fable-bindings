module Fable.Import.MobxReact

open System
open Fable.Core
open Fable.Core.JsInterop
module R = Fable.Helpers.React

type IMobxReact =
    abstract Observer: React.ComponentClass<obj>
    abstract Provider: React.ComponentClass<obj>

let private mobxReact: IMobxReact = import "*" "mobx-react"

let Provider = R.from<obj> mobxReact.Provider []

let inline ofFunction (f: unit -> React.ReactElement): React.ReactElement = unbox f

/// Note: Observer will not update if you do not use an observed value *within* the function. The component instead needs to access the value *inside* the function to observe it.
///
/// Example 1, the counter text will never change because `myButton` is static and run once at startup outside the observer:
/// ```
/// let myButton = R.button [] [sprintf "Counter value %i" mobxStore.counterValue |> R.str]
/// let observer = R.observer (fun _ -> myButton)
/// ```
///
/// Example 2, the counter text will change because `myButton` is computed within the observer function:
/// ```
/// let myButton () = R.button [] [sprintf "Counter value %i" mobxStore.counterValue |> R.str]
/// let observer = R.observer (fun _ -> myButton())
/// ```
let Observer (f: unit -> React.ReactElement) = 
    let wrappedFunc () = 
        try f()
        with e ->
            Browser.console.error("MobxReact.Observer caught an error and failed to render child function:", e)
            R.noscript [] [] 

    R.from mobxReact.Observer null [ofFunction wrappedFunc]
