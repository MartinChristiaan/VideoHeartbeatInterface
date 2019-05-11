module Communication
open Fable.Import
open Fable.Core
open Fable.Import.Browser
open Util
open Browser
open Fable.Core.JsInterop
open System.Timers
open Fable.PowerPack
open Fable.PowerPack.Fetch
open PythonTypes
open System
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


type DataCallback = ClassName*FieldName->string -> HTMLElement


let getTargets (datarequests : ((ClassName*FieldName)*DataCallback) list) postCreationCallback  =
  let classnameString = datarequests|>List.map (fun ((classname,_),_)-> classname)|>String.concat " "
  let fieldnameString = datarequests|>List.map (fun ((_,fieldname),_)-> fieldname)|>String.concat " "
  //let callbacks = datarequests|>List.map (fun (_,_,callback)-> callback)|>List.toArray
  let callback (answers:string) =    
    answers.Split ','
    |>Array.toList
    |>List.map2 (fun  ((classname,fieldname),callback) answer -> 
      (callback (classname,fieldname) answer)) datarequests
    |> postCreationCallback  
    

  http.getTargets (BaseURL+GetTargetsURL) classnameString fieldnameString callback 0




let rec getFigureTargets (dataAdresses : (ClassName*FieldName) list list) (callbacks : TimedDataCallback array) (prevDateTime:DateTime) (prevtime:Time) =
  let getClassnameString figuredataAdresses = figuredataAdresses|>List.map (fun (classname,_)-> classname)|>String.concat " "
  let getFieldnameString figuredataAdresses = figuredataAdresses|>List.map (fun (_,fieldname)-> fieldname)|>String.concat " "

  let classnameString = dataAdresses|>List.map getClassnameString|> String.concat "/"
  let fieldnameString = dataAdresses|>List.map getFieldnameString|> String.concat "/"
  
  //let callbacks = datarequests|>List.map (fun (_,_,callback)-> callback)|>List.toArray

  let callback (answers:string) =
    
    let curTime = DateTime.Now
    let dTime = (curTime-prevDateTime).Milliseconds|>float|>(fun x-> x/1000.0)
    let time = prevtime+dTime    
    answers.Split '/'
    |>Array.map (fun x -> x.Split ',') 
    |>Array.iter2 (fun callback answer -> callback answer time) callbacks
    getFigureTargets dataAdresses callbacks curTime time


  http.getTargets (BaseURL+GetTargetsURL) classnameString fieldnameString callback 60


let getRequest(target:string)(callback : string -> unit) =
    fetch (BaseURL+target) []
    |> Promise.bind (fun res -> res.text())
    |> Promise.map callback




