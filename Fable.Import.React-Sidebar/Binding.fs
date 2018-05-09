module Fable.Import.ReactSidebar

open Fable.Core
open Fable.Core.JsInterop
module R = Fable.Helpers.React

type SidebarProps =
    /// Add a custom class to the root component
    | RootClassName of string
    /// Add a custom class to the sidebar
    | SidebarClassName of string
    /// Add a custom class to the content
    | ContentClassName of string
    /// Add a custom class to the overlay
    | OverlayClassName of string
    | Sidebar of React.ReactElement
    /// Callback called when the sidebar wants to change the open prop. Happens after sliding the sidebar and when the overlay is clicked when the sidebar is open.
    | OnSetOpen of (bool -> unit)
    /// If the sidebar should be always visible
    | Docked of bool
    /// If the sidebar should be open
    | Open of bool
    /// If transitions should be enabled
    | Transitions of bool
    /// If touch gestures should be enabled
    | Touch of bool
    /// Width in pixels you can start dragging from the edge when the sidebar is closed.
    | TouchHandleWidth of int
    /// Distance the sidebar has to be dragged before it will open/close after it is released.
    | DragToggleDistance of int
    /// Place the sidebar on the right
    | PullRight of bool
    /// Enable/Disable sidebar shadow
    | Shadow of bool
    /// Inline styles. These styles are merged with the defaults and applied to the respective elements.
    | Styles of React.CSSProperties

let private sidebar': React.ComponentClass<_> =
    import "default" "react-sidebar"

let sidebar (props: SidebarProps list) =
    let props = keyValueList CaseRules.LowerFirst props
    R.from sidebar' props
