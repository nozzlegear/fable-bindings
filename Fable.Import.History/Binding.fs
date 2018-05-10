module Fable.Import.History

open Fable.Core.JsInterop

type IBrowserHistory =
    abstract length: int
    abstract location: Browser.Location
    abstract listen: (Browser.Location -> string -> unit) -> (unit -> unit)
    abstract push: path: string -> unit
    abstract replace: path: string -> unit
    abstract go: int -> unit
    abstract goBack: unit -> unit
    abstract goForward: unit -> unit
    // abstract canGo: int -> bool

let createBrowserHistory = import<unit -> IBrowserHistory> "createBrowserHistory" "history"
