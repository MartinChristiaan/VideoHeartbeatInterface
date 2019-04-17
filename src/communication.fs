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
    abstract getRequest : string -> string
    abstract putRequest  : string -> string -> unit

type SliderParam = 
  {
    name : string;
    min : float;
    max : float;
    value : float;    
  }

[<Import("*", "./httpWrapper")>]
let http : HTTPWrapper = jsNative

let baseUrl = "http://127.0.0.1:5000/"
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
    



let removeChar (unwanted : string) (input:string)=
    input.Replace(unwanted,"")

type NameValuePair =
  {
    name:string;
    value : float;
  }

 type InputData =
  {
    name:string;
    value : float;
    min : float;
    max : float;
  }
let (<*>) fs xs = Array.map2 (fun f x -> f x) fs xs
let map3 f xs bs cs = Array.map2 f xs bs <*> cs
let map4 f xs bs cs ds = map3 f xs bs cs <*> ds

let everyNth n offset seq = 
  seq |> Array.mapi (fun i el -> el, i)              // Add index to element
      |> Array.filter (fun (el, i) -> i % n = offset) // Take every nth element
      |> Array.map fst   

type Category = 
  {
    name : string;
    noItems : int;
  }

let getInputDataFromJson (text:string) =
    console.log text
    let itemstxt = text|>removeChar "["|> removeChar "]" 
                  |> removeChar "\""|>removeChar","|>removeChar"'"
                  |> removeChar "("|> removeChar ") "|>removeChar"{"|>removeChar"}"|>removeChar":"
    let items_categories = itemstxt.Split '_'
    let categories = items_categories.[1].Split ' '
    
    let catagorynames =  categories|> everyNth 2 0
    let catagoryNOitems =  categories|> everyNth 2 1|> Array.map int // number of items per category
    
    
    let items = items_categories.[0].Split ' '
    let names = items|>everyNth 4 0
    let values = items|>everyNth 4 1|> Array.map float
    let mins = items|>everyNth 4 2|> Array.map float
    let maxs = items|>everyNth 4 3|> Array.map float    
    
    let idata = map4(fun name valu min max -> {name=name;value=valu;min=min; max =max}) names values mins maxs
    let categories = Array.map2(fun name noitems -> {name=name; noItems = noitems}) catagorynames catagoryNOitems
    idata,categories
let getNameValuePairsFromJson (text:string) =
    let itemstxt = text|>removeChar "["|> removeChar "]" |> removeChar "\""|> removeChar " "
    let items = itemstxt.Split ','
    let noItems = items.Length
    let names = items|>Seq.take(noItems/2)
    let values = items|>last(noItems/2)|> Seq.map float
    Seq.map2(fun name valu -> {name=name;value=valu}) names values

let getValuesFromJson (text:string) =
    let itemstxt = text|>removeChar "["|> removeChar "]" |> removeChar "\""|> removeChar " "
    itemstxt.Split ','|> Seq.map float
    
    // let statepairs = states|>Seq.map (fun st -> st.Split)

let getRequest req = 
  fetch (baseUrl+req) []
    |> Promise.bind (fun res -> res.text())
    |> ignore

let getStates(callback : NameValuePair seq -> unit) =
    let onResult = getNameValuePairsFromJson>>callback
    fetch "http://127.0.0.1:5000/states" []
    |> Promise.bind (fun res -> res.text())
    |> Promise.map onResult

let getInputs(callback : InputData array * Category array -> unit) =
    let onResult = getInputDataFromJson>>callback
    fetch "http://127.0.0.1:5000/inputData" []
    |> Promise.bind (fun res -> res.text())
    |> Promise.map onResult

let setInputData name catagoryname value min max = 
  let inputs = [value; min; max]|>Seq.map(fun x -> x.ToString())|>Seq.append [catagoryname; name]
               |>String.concat " "
  
  http.putRequest "http://127.0.0.1:5000/setInputData" inputs

let putList dest values = 
  let inputs = values|>Seq.map(fun x -> x.ToString())|>String.concat " "
  console.log (baseUrl+dest)  
  http.putRequest (baseUrl+dest) inputs
 

let timer = new Timer(100.0)
let getValues(frequency : float)(callback : float seq -> unit) =


    let onResult = getValuesFromJson>>callback
    let elapsed x =      
      
      fetch "http://127.0.0.1:5000/timestep" []
      |> Promise.bind (fun res -> res.text())
      |> Promise.map onResult|>ignore
    timer.Elapsed.Add elapsed






