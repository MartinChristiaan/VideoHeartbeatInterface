module Chart
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open System
open Layout
open Communication
// or you can use attributes:
type UpdatePolicy =
|Replace
|Add 

type Dataset = 
    {
        data : float array ;
        label : string;
        
    }

type ChartWrapper = 
    abstract CreateChart : float array -> string-> Dataset array -> HTMLElement-> obj
    abstract UpdateData  : obj -> string array -> Dataset array -> unit 

let float2string (flt:float) = 
    flt.ToString()

[<Import("*", "./chartWrapper.js")>]
let cUtil : ChartWrapper = jsNative


let determineUpdatePolicy policyStr =
    match policyStr with
    |_ when policyStr = "Add" -> Add
    |_ when policyStr = "Replace"->Replace
let splitInto (count : int) (arr :  array<'T>) =
    let len = arr.Length
    let splitsize = len/count
    Array.init count (fun index -> arr.[index * splitsize..(index+1)*splitsize-1])





let createFigure(instructionsfull : string array) = 

    let instructions=  instructionsfull|>Array.skip 2

    let xlen = instructions.[0]|>int
    let noDatasets = instructions.[1]|>int
    
    let updatePolicy = instructions.[2]|>determineUpdatePolicy
    let xname = instructions.[3]
    let ynames = instructions.[4..4+noDatasets-1]
    let data = instructions|>Array.skip (4+noDatasets)|> Array.map float
       
    let x = data|>Array.take xlen 
    let datasetData = data|>Array.skip xlen

    let datasetValuesSplit = datasetData|>splitInto noDatasets

    let datasets =  Array.map2(fun name data -> 
        {data = data; label = name;}) ynames datasetValuesSplit

    let canvascontainer = createElement "div" "figureContainer"
    let canvas = createElement "canvas" ""
    uiutil.AddToParent canvascontainer canvas
    //datasets <- states|>Seq.map(fun nv -> {label = nv.name; data = [|nv.value|]})|>Seq.toArray 
    let figure = cUtil.CreateChart x xname datasets canvas
    figure,canvascontainer,x,datasets,updatePolicy

let addValues existing newvalues =
    Array.append existing newvalues |> last 200

let addValueToDataset(ds:Dataset) (values : float array) =
    {ds with data = addValues ds.data values}
let replaceDataInDataset (ds:Dataset) (values : float array) =
    {ds with data = values}

let updateFigure(figure:obj, x:float array, datasets:Dataset array,updatePolicy:UpdatePolicy)(instructions : string array) =
    let noDatasets = datasets.Length
    let newValues = instructions|>Array.skip 2|>Array.map float|>splitInto (noDatasets+1)
    

    let receivedX = newValues|>Array.head
    let receivedData = newValues|>Array.tail

    let newx,newdatasets =
        match updatePolicy with
        |Add -> 
            addValues x receivedX,Array.map2 addValueToDataset datasets receivedData
        |Replace -> receivedX,Array.map2 replaceDataInDataset datasets receivedData

    cUtil.UpdateData figure (newx|>Array.map (float2string>> fun x -> x.[0..4])) newdatasets
    figure,newx,newdatasets,updatePolicy


