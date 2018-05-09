module Fable.Import.ReactMaterialNavbar
open Fable.Core
open Fable.Core.JsInterop
module R = Fable.Helpers.React

type IProps =
    | Title of string
    | Color of string
    | Background of string
    | LeftAction of React.ReactElement
    | RightAction of React.ReactElement
    | Style of React.CSSProperties
    | Key of U2<string, float>

let private navbar': React.ComponentClass<_> =
    import "Navbar" "react-material-navbar"

let navbar (props: IProps list) =
    let propList = keyValueList CaseRules.LowerFirst props
    R.from navbar' propList []
