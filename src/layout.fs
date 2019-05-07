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
let visualdiv = document.getElementById("visual")

let createElement eltype elclass =
    let el = document.createElement(eltype)
    el.className <- elclass
    el

type layoutWrapper = 
    abstract AddToParent : HTMLElement -> HTMLElement -> unit
    abstract SetSliderValue : Element -> float -> unit
    abstract SetSliderOnInput : Element -> (float -> unit) -> unit
    abstract CreateSlider  : float -> float -> float -> HTMLElement
    abstract CreateNumericInput  : float -> (float -> unit) -> HTMLElement
    abstract AdjustNumericInput  : string -> float -> (float -> unit) -> HTMLElement
    abstract SetType  :  string -> HTMLElement-> HTMLElement
// type SliderParam = 
//   {
//     name : string;
//     min : float;
//     max : float;
//     value : float;    
//   }

[<Import("*", "./uiWrapper")>]
let uiutil : layoutWrapper = jsNative

 
let addToControl element = uiutil.AddToParent controldiv element

let addChildren parent children =
    children|>Seq.iter(fun child -> uiutil.AddToParent parent child)
    

let createDefaultUIHeader label =
    let text = document.createElement("p")
    text.className <- "is-size-7"
    text.innerText <- label
    text

let createStandardUIContainer header element = 
    let defaultUIContainer = document.createElement("div")  
    defaultUIContainer.setAttribute("class","column has-text-centered")
    addChildren defaultUIContainer [header; element]
    defaultUIContainer

let createElementFromHTML html =
    let div = document.createElement("div")
    div.innerHTML <- html
    div.firstChild


// let param = {name = "TestParameter"; min = 0.0; max = 100.0; value = 60.0 }
// createSliderWidget param



