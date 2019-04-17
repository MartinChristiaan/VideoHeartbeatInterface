module Chart
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open System
open Layout
open Communication
// or you can use attributes:


type Dataset = 
    {
        data : float array ;
        label : string ;    
    }
type ChartWrapper = 
    abstract CreateChart : string -> Dataset array -> obj option
    abstract UpdateData  : obj -> string array -> Dataset array -> unit 

let float2string (flt:float) = 
    flt.ToString()
let mutable t = [|0.0;|]
let mutable datasets : Dataset array = [||]

[<Import("*", "./chartWrapper.js")>]
let cUtil : ChartWrapper = jsNative
let createFigure(states : NameValuePair seq) = 
    t <- [|0.0;|]
    datasets <- states|>Seq.map(fun nv -> {label = nv.name; data = [|nv.value|]})|>Seq.toArray 
    let figure = cUtil.CreateChart "myChart" datasets
    cUtil.UpdateData figure (t|>Array.map float2string) datasets
    figure
    

let updatePlot(values : float seq)( dt : float)(figure : obj) =
    let lastValue (arr:_[]) = 
        arr.[arr.Length - 1]
    let addone x =
        x+dt

    let last n xs = 
        let len = Array.length xs
        if len > n then
            Array.skip ((Array.length xs) - n) xs
        else
            xs

    let addValueToDataset(ds:Dataset) (value : float) =
        {ds with data = Array.append ds.data [|value|] |> last 400 }


    t <-  Array.append t [|t|>lastValue|>addone|] |> last 400
    datasets <- Array.map2 addValueToDataset datasets (values|>Seq.toArray)
    cUtil.UpdateData figure (t|>Array.map (float2string>> fun x -> x.[0..4])) datasets
    ()


