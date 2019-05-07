module Communication
open Fable.Import
open Fable.Core
open Fable.Import.Browser

open Browser
open Fable.Core.JsInterop
open System.Timers
open Fable.PowerPack
open Fable.PowerPack.Fetch
open PythonTypes
type URL = string

type Value = string
type ValueType = string
type MethodName = string


type HTTPWrapper = 
    abstract getTargets : URL -> ClassName -> FieldName -> (string -> unit) -> int-> unit
    abstract updateTarget : URL -> ClassName -> FieldName -> Value -> ValueType -> unit
    abstract invokeMethod : URL -> ClassName -> MethodName -> unit


[<Import("*", "./httpWrapper")>]
let http : HTTPWrapper = jsNative


let BaseURL  = "http://127.0.0.1:5000/"//"http://192.168.178.66:5000/"

let GetTargetsURL = "getTargets"
let updateTargetURL = "updateTarget"
let invokeMethodURL = "invokeMethod"




let getTargets (datarequests : (ClassName*FieldName) list) (callbacks:(string -> unit) array) =
  let classnameString = datarequests|>List.map (fun (classname,_)-> classname)|>String.concat " "
  let fieldnameString = datarequests|>List.map (fun (_,fieldname)-> fieldname)|>String.concat " "
  let callback (answers:string) =
    answers.Split ','|>Array.iter2 (fun callback answer -> (callback answer)) callbacks
    

  http.getTargets (BaseURL+GetTargetsURL) classnameString fieldnameString callback 0


let getRequest(target:string)(callback : string -> unit) =
    fetch (BaseURL+target) []
    |> Promise.bind (fun res -> res.text())
    |> Promise.map callback




