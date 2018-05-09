module Fable.Import.JwtSimple

open System
open Fable.Core
open Fable.Core.JsInterop

type Algorithm =
    | HS256
    | HS384
    | HS512
    | RS256
    with
    override x.ToString () =
        match x with
        | HS256 -> "HS256"
        | HS384 -> "HS384"
        | HS512 -> "HS512"
        | RS256 -> "RS256"

type IJSCookie =
    [<PassGenerics>]
    abstract encode: value: 'a -> secret: string -> algorithm: string option -> string
    abstract decode: token: string -> secret: string option -> skipVerification: bool -> algorithm: string option -> obj

let private imported = import<IJSCookie> "*" "jwt-simple"

/// Encodes the value to a Json Web Token, signing it with the given secret key and algorithm.
/// NOTE: It's highly recommended that you *don't* use this on the frontend, as you'll need to make your secret signing key available to the client. Instead, your server should handle creating Json Web Tokens.
[<PassGenerics>]
let encode value secret (algorithm: Algorithm) =
    let alg = Some <| algorithm.ToString()
    imported.encode value secret alg

/// Decodes the token string and skips verification. Allows for working with Json Web Tokens without making your secret signing key available to the client.
/// Decoding will default to using the algorithm specified in the token's payload.
/// NOTE: your server should **always** verify the token to ensure it hasn't been tampered with.
[<PassGenerics>]
let decodeNoVerify<'a> token =
    // The jwt-simple lib will parse the value from json automatically, but that loses F# type info. Restringify it and then parse with Fable to retain types.
    imported.decode token None true None
    |> toJson
    |> ofJson<'a>

/// Decodes the token string and verifies it with the given secret key. Allows for working with Json Web Tokens without making your secret signing key available to the client.
/// Decoding will default to using the algorithm specified in the token's payload.
/// NOTE: It's highly recommended that you *don't* use this on the frontend, as you'll need to make your secret signing key available to the client. Instead, your server should handle verifying Json Web Tokens.
[<PassGenerics>]
let decodeAndVerify<'a> token secret =
    // The jwt-simple lib will parse the value from json automatically, but that loses F# type info. Restringify it and then parse with Fable to retain types.
    imported.decode token (Some secret) false None
    |> toJson
    |> ofJson<'a>
