module Fable.Import.ReactFlexifit 

open System 
open Fable.Core 
open Fable.Core.JsInterop 

type IProps = 
    | AspectRatio of float
    | Height of int 
    | Width of int 
    
let private flexifit': React.ComponentClass<_> = 
    import "default" "react-flexifit-ts"
    
module R = Fable.Helpers.React    
    
let flexifit (props: IProps list) (child: React.ReactElement) = 
    let propList = keyValueList CaseRules.LowerFirst props 
    // Flexifit will throw an error if given more than one child 
    R.from flexifit' propList [child]    
