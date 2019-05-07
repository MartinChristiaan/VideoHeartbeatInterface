module Chart
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open System
open Layout
open Communication

type Dataset = 
    {
        data : float array ;
        label : string;        
    }

type ChartWrapper = 
    abstract CreateChart : float array -> float array array -> string array-> HTMLElement-> obj
    abstract AppendTimeSeries  : obj -> string array -> string -> unit 
    abstract UpdateData  : obj -> string array -> Dataset array -> unit 

let float2string (flt:float) = 
    flt.ToString()

[<Import("*", "./chartWrapper.js")>]
let cUtil : ChartWrapper = jsNative

let splitInto (count : int) (arr :  array<'T>) =
    let len = arr.Length
    let splitsize = len/count
    Array.init count (fun index -> arr.[index * splitsize..(index+1)*splitsize-1])


let updateTime = 60


let rec updateTimeFigure figure classnames fieldnames (prevDateTime:DateTime) (prevtime : float)  (data:string) =
    let values = data.Split ','|> splitInto 2 
    let curTime = DateTime.Now
    let dTime = (curTime-prevDateTime).Milliseconds|>float|>(fun x-> x/1000.0)
    let time = prevtime+dTime

    
    cUtil.AppendTimeSeries figure values.[1] (time.ToString().[0..4])
    http.getTargets (BaseURL+GetTargetsURL) classnames fieldnames (updateTimeFigure figure classnames fieldnames curTime time) updateTime


let rec updateReplacingFigure figure classnames fieldnames (data:string) =
    let values = data.Split ','|> splitInto 2 
    let curTime = DateTime.Now

    
    cUtil.AppendTimeSeries figure values.[1] (time.ToString().[0..4])
    http.getTargets (BaseURL+GetTargetsURL) classnames fieldnames (updateTimeFigure figure classnames fieldnames curTime time) updateTime


let createTimeFigure (ylabels : string array) classnames fieldnames = 

    let canvascontainer = createElement "div" "figureContainer"
    let canvas = createElement "canvas" "canvas"
    uiutil.AddToParent canvascontainer canvas
    
    //datasets <- states|>Seq.map(fun nv -> {label = nv.name; data = [|nv.value|]})|>Seq.toArray 
    let yvalues = Array.init ylabels.Length (fun x -> [||])
    let figure = cUtil.CreateChart [||] yvalues ylabels canvas
    // invoke update
    let startTime = DateTime.Now
    http.getTargets (BaseURL+GetTargetsURL) classnames fieldnames (updateTimeFigure figure classnames fieldnames startTime 0.0) updateTime
    canvascontainer
    

let createReplacingFigure (ylabels : string array) classnames fieldnames = 

    let canvascontainer = createElement "div" "figureContainer"
    let canvas = createElement "canvas" "canvas"
    uiutil.AddToParent canvascontainer canvas
    
    //datasets <- states|>Seq.map(fun nv -> {label = nv.name; data = [|nv.value|]})|>Seq.toArray 
    let yvalues = Array.init ylabels.Length (fun x -> [||])
    let figure = cUtil.CreateChart [||] yvalues ylabels canvas
    // invoke update
    let startTime = DateTime.Now
    http.getTargets (BaseURL+GetTargetsURL) classnames fieldnames (updateTimeFigure figure classnames fieldnames startTime 0.0) updateTime
    canvascontainer


let addValues existing newvalues =
    Array.append existing newvalues |> last 200

let addValueToDataset(ds:Dataset) (values : float array) =
    {ds with data = addValues ds.data values}
let replaceDataInDataset (ds:Dataset) (values : float array) =
    {ds with data = values}

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


