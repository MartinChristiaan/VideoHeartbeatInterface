module Communication
open Fable.Import
open Fable.Core
open Fable.Import.Browser

open Browser
open Fable.Core.JsInterop
open System.Timers
open Fable.PowerPack
open Fable.PowerPack.Fetch

type HTTPWrapper = 
    abstract updateField : string -> string -> string  -> unit



type SliderParam = 
  {
    name : string;
    min : float;
    max : float;
    value : float;    
  }

[<Import("*", "./httpWrapper")>]
let http : HTTPWrapper = jsNative

// let communicate() = 
//     http.getRequest("http://127.0.0.1:5000/timestep")
//     |>console.log

let last n xs = 
    let len = Array.length xs
    if len > n then
        Array.skip ((Array.length xs) - n) xs
    else
        xs
let range min max xs = 
    let len = Array.length xs
    let delta= max - min
    Array.skip ((Array.length xs) - min) xs|>Array.take delta
    

let BaseURL = "http://127.0.0.1:5000/"

let getInstructionsURL = "getInstructions"
let updateUIURL = "UIUpdate"
let figureUpdateURL = "figureUpdate"

let UpdateField fieldname value  = 
  http.updateField (BaseURL+updateUIURL) fieldname value  

 
let (<*>) fs xs = Array.map2 (fun f x -> f x) fs xs
let map3 f xs bs cs = Array.map2 f xs bs <*> cs
let map4 f xs bs cs ds = map3 f xs bs cs <*> ds
let map5 f xs bs cs ds es = map4 f xs bs cs ds <*>  es

// let getNameValuePairsFromJson (text:string) =
//     let itemstxt = text|>removeChar "["|> removeChar "]" |> removeChar "\""|> removeChar " "
//     let items = itemstxt.Split ','
//     let noItems = items.Length
//     let names = items|>Seq.take(noItems/2)
//     let values = items|>last(noItems/2)|> Seq.map float
//     Seq.map2(fun name valu -> {name=name;value=valu}) names values


    
    // let statepairs = states|>Seq.map (fun st -> st.Split)

let getRequest(target:string)(callback : string -> unit) =
    fetch (BaseURL+target) []
    |> Promise.bind (fun res -> res.text())
    |> Promise.map callback



    
let timedGetRequest(callback : string -> unit) =
      promise {
        do! Promise.sleep 60
        let! res = fetch (BaseURL+figureUpdateURL) []
        return res
      }
      |> Promise.bind (fun res -> res.text())
      |> Promise.map callback|>ignore






