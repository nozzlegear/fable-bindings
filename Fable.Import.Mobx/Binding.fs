module rec Fable.Import.Mobx
open System
open Fable.Core
open Fable.Core.JsInterop
open Fable

type IChange<'T> =
    interface
    abstract ``type``: string
    abstract newValue: 'T with get, set
    end

type IComputed<'T> =
    abstract get: unit -> 'T
    abstract observe: block: (IChange<'T> -> unit) -> unit

type IObservable<'T> =
    inherit IComputed<'T>
    abstract set: 'T -> unit
    abstract intercept: (IChange<'T> -> IChange<'T> option) -> (unit -> unit)

type IMobx =
    abstract runInAction: block: (unit -> unit) -> unit
    abstract observable: 'T -> 'T
    abstract computed: computeFunction: (unit -> 'T) -> IComputed<'T>
    abstract autorun: block: (unit -> unit) -> unit
    // abstract intercept: observable: IObservable<'T> -> (IChange<'T> -> IChange<'T> option) -> (unit -> unit)

let private mobx: IMobx = import "*" "mobx"

/// Runs the given function within an "action" context. This should be used whenever you change an observable's value, as it batches up all changes and optimizes them for performance.
///
/// ```
/// let changeSomeValue toValue = runInAction (fun _ -> someValue.set toValue; someOtherValue.set toAnotherValue)
/// ```
let runInAction = mobx.runInAction

/// Takes a value and makes it observable so that when the value is changed all dependent observers will be notified.
/// This uses the observable.box function from Mobx, which is much more functional and doesn't require mutability.
[<Emit("require('mobx').observable.box")>]
let observable<'T> = jsNative |> unbox<'T -> IObservable<'T>>

/// Observes a value and runs the given function every time it changes. The intercept function can then modify the new value, return the new value as-is, or return `None` to prevent the value from changing at all.
/// The function itself returns a function that will dispose the observer when called.
///
/// ```
/// let myObservable = observable "hello"
/// let interceptNewValue change =
///     match change.newValue with
///     | "hello tom" ->
///         // Don't say hello to Tom, return None to prevent this change
///         None
///     | "hello bob" ->
///         // Capitalize bob's name
///         change.newValue <- "Hello Bob!"
///         Some change
///     | _ ->
///         // Let the change happen as-is
///         Some change
/// let disposeInterceptor = intercept interceptNewValue
/// // Dispose the interceptor at a later date, which stops it from running on all future changes
/// disposeInterceptor()
/// ```
let intercept<'T> (onChange: IChange<'T> -> IChange<'T> option) (observable: IObservable<'T>) =
    observable.intercept onChange

/// The same as intercept, but with its arguments reversed.
let interceptBack observable onChange = intercept onChange observable

/// Runs the given function every time one of the observables the function uses has changed, returning a value that can itself be observed.
let computed = mobx.computed

/// Runs the given function every time one of the observables the function uses has changed.
let autorun = mobx.autorun

let get<'T> (computed: IComputed<'T>) = computed.get()

let observe<'T> (computed: IComputed<'T>) = computed.observe

let set<'T> (observable: IObservable<'T>) (value: 'T) = observable.set value
