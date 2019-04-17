
module Layout
open Fable.Import
open Fable
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser

open Browser
open Fable.Core.JsInterop
open Communication
let controldiv = document.getElementById("control")


type layoutWrapper = 
    abstract AddToParent : HTMLElement -> HTMLElement -> unit
    abstract CreateSlider  : float -> float -> float -> (float -> unit) -> HTMLElement
    abstract CreateNumericInput  : float -> (float -> unit) -> HTMLElement
    abstract AdjustNumericInput  : string -> float -> (float -> unit) -> HTMLElement
    
// type SliderParam = 
//   {
//     name : string;
//     min : float;
//     max : float;
//     value : float;    
//   }

let inline (<!>) f xList = List.map f xList

let inline (<*>) gList xList = List.map2 (id) gList xList

let map4 f w x y z = f <!> w <*> x <*> y <*> z
let map5 f v w x y z = f <!> v <*> w <*> x <*> y <*> z
let map6 f u v w x y z = f <!> u <*> v <*> w <*> x <*> y <*> z

[<Import("*", "./uiWrapper")>]
let uiutil : layoutWrapper = jsNative

let createSliderWidget label min max defaultVal onValueChange =
    let sliderTextContainer = document.createElement("div")
    console.log "create"
    sliderTextContainer.setAttribute("class","column has-text-centered")

    let text = document.createElement("p")
    text.className <- "is-size-7"
    let mutable value = defaultVal

    let onSliderValueChange newvalue =
       value<-newvalue
       text.innerText <- label + " : "+ value.ToString()
       onValueChange value
    onSliderValueChange value
    
    let slider = uiutil.CreateSlider min max value onSliderValueChange

    text|>uiutil.AddToParent controldiv        
    slider|> uiutil.AddToParent sliderTextContainer 
    sliderTextContainer|> uiutil.AddToParent controldiv
    
open System.Collections.Generic
open System.Collections.Generic


let values = List([20.0;20.0;20.0;10.0;10.0;10.0;3.0;15.0;])
let mins = List.init values.Count (fun i -> 0.0)
let maxs_ = List.init (values.Count-2) (fun i -> 255.0)
let maxs = maxs_ @ [20.0;20.0] 

let sendNewValues() =
    putList "updateClassifier" values
let titles = ["Hue Avg";"Sat Avg"; "V Avg"; "Hue Var";"Sat Var"; "V Var"; "ElipseSize" ;"Blur"]
let updateFuncs = List.init values.Count (fun i -> ((fun x -> values.[i]<- x;sendNewValues() )))

let button = document.createElement("button")

for i in [0..7] do
    createSliderWidget titles.[i] mins.[i] maxs.[i] (values|>Seq.toList).[i] updateFuncs.[i]

















    





// let param = {name = "TestParameter"; min = 0.0; max = 100.0; value = 60.0 }
// createSliderWidget param



