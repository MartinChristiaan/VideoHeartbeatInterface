module Chart
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open System
open Layout
open Communication
open PythonTypes
open Util
type Dataset = 
    {
        data : float array ;
        label : string;        
    }

type ChartWrapper = 
    abstract CreateChart : float array -> float array array -> string array -> string-> HTMLElement-> obj
    abstract AppendTimeSeries  : obj -> float array -> string -> unit 
    abstract UpdateData  : obj -> float array array -> unit 

let float2string (flt:float) = 
    flt.ToString()

[<Import("*", "./chartWrapper.js")>]
let cUtil : ChartWrapper = jsNative

let splitInto (count : int) (arr :  array<'T>) =
    let len = arr.Length
    let splitsize = len/count
    Array.init count (fun index -> arr.[index * splitsize..(index+1)*splitsize-1])




let updateTimeFigure figure  (data:string array) (time:Time) =
    let values = data|>Array.map float 
    cUtil.AppendTimeSeries figure values (time.ToString().[0..4])   
    

let updateReplacingFigure figure  (data:string array) (time:Time) =
    let values = data|>Array.map (fun x -> x.Split('@'))|> Array.map (Array.map float) 
    cUtil.UpdateData figure values 
    


let createTimeFigure (ylabels : string array) = 

    let canvascontainer = createElement "div" "figureContainer"
    let canvas = createElement "canvas" "canvas"
    uiutil.AddToParent canvascontainer canvas
    
    //datasets <- states|>Seq.map(fun nv -> {label = nv.name; data = [|nv.value|]})|>Seq.toArray 
    let yvalues = Array.init ylabels.Length (fun x -> [||])
    let figure = cUtil.CreateChart [||] yvalues ylabels "Time (s)" canvas 
    let updateMethod:TimedDataCallback = updateTimeFigure figure
    updateMethod,canvascontainer //,(updateTimeFigure figure dataAdresses startTime 0.0)
    

let createReplacingFigure (ylabels : string array) xaxisLabel = 

    let canvascontainer = createElement "div" "figureContainer"
    let canvas = createElement "canvas" "canvas"
    uiutil.AddToParent canvascontainer canvas
    
    let yvalues = Array.init ylabels.Length (fun x -> [||])
    let figure = cUtil.CreateChart [||] yvalues ylabels xaxisLabel canvas
    let updateMethod = updateReplacingFigure figure 
    updateMethod,canvascontainer


// let addValues existing newvalues =
//     Array.append existing newvalues |> last 200

// let addValueToDataset(ds:Dataset) (values : float array) =
//     {ds with data = addValues ds.data values}
// let replaceDataInDataset (ds:Dataset) (values : float array) =
//     {ds with data = values}

// let updateFigure(canvas, x:float array, datasets:Dataset array,updatePolicy:UpdatePolicy)(instructions : string array) =
//     let noDatasets = datasets.Length
//     let newValues = instructions|>Array.skip 2|>Array.map float|>splitInto (noDatasets+1)
    

//     let receivedX = newValues|>Array.head
//     let receivedData = newValues|>Array.tail

//     let newx,newdatasets =
//         match updatePolicy with
//         |Add -> 
//             addValues x receivedX,Array.map2 addValueToDataset datasets receivedData
//         |Replace -> receivedX,Array.map2 replaceDataInDataset datasets receivedData

//     cUtil.UpdateData figure (newx|>Array.map (float2string>> fun x -> x.[0..4])) newdatasets
//     figure,newx,newdatasets,updatePolicy


